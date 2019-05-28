using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Tool.Engine
{
    class EngineCommunicatorByShareMemory : AbstractEngineCommunicator
    {
        private static int INVALID_HANDLE_VALUE = -1;
        private static uint PAGE_READWRITE = 0x04;
        private static uint FILE_MAP_ALL_ACCESS = 0x0002;

        private string shareMemoryFileName;
        private uint shareMemoryFileSize;
        private IntPtr shareMemoryFileHandle;
        private IntPtr shareMemoryPtr;

        private string writeSemaphoreName;
        private string readSemaphoreName;
        private Semaphore writeSemaphore;
        private Semaphore readSemaphore;

        public EngineCommunicatorByShareMemory(string shareMemoryFileName,
                                               string writeSemaphoreName,
                                               string readSemaphoreName,
                                               uint shareMemoryFileSize = 4096*1024)
            :base()
        {
            this.shareMemoryFileName = null;
            this.writeSemaphoreName = null;
            this.readSemaphoreName = null;
            if (null != shareMemoryFileName && "" != shareMemoryFileName)
                this.shareMemoryFileName = shareMemoryFileName;
            if (null != writeSemaphoreName && "" != writeSemaphoreName)
                this.writeSemaphoreName = writeSemaphoreName;
            if (null != readSemaphoreName && "" != readSemaphoreName)
                this.readSemaphoreName = readSemaphoreName;
            if (null == shareMemoryFileName || null == writeSemaphoreName || null == readSemaphoreName)
                throw new FTBAutoTestException("Init ComToEngineByShareMemory error by input invalid param.");
            this.shareMemoryFileSize = shareMemoryFileSize;

            createSemaphore();
            createShareMemory();
        }

        public override object execute(EngineCommand command, int timeout = -1)
        {
            //send command to execute
            if (!command.isValid())
                throw new FTBAutoTestException("Execute command error by set invalid command.");
            string commandString;
            try { commandString = Newtonsoft.Json.JsonConvert.SerializeObject(command); }
            catch { throw new FTBAutoTestException("Execute command error by set invalid command."); }
            byte[] commandBytes = Encoding.Default.GetBytes(commandString);
            cleanShareMemory(shareMemoryPtr, shareMemoryFileSize);
            copyToShareMemory(commandBytes, shareMemoryPtr);
            try { writeSemaphore.Release(); }
            catch (SemaphoreFullException) { throw new FTBAutoTestException("Execute command error by release error."); }

            //receive command execute result
            try
            {
                if (false == readSemaphore.WaitOne(timeout))
                    throw new FTBAutoTestException("Execute command error by timeout.");
            }
            catch(ThreadAbortException ex)
            //keep on receive, to keep semaphore status 
            {
                if (false == readSemaphore.WaitOne(timeout))
                    throw new FTBAutoTestException("Execute command error by timeout.");
                throw ex;
            }
            byte[] shareMemoryReceiveBuffer = new byte[shareMemoryFileSize];
            //clear buff
            for (int i = 0; i < shareMemoryFileSize; i++)
                shareMemoryReceiveBuffer[i] = 0x00;
            copyFromShareMemory(shareMemoryReceiveBuffer, shareMemoryPtr);
            string receiveString = Encoding.Default.GetString(shareMemoryReceiveBuffer).TrimEnd('\0');
            EngineCommand receiveCommand;
            try { receiveCommand = Newtonsoft.Json.JsonConvert.DeserializeObject<EngineCommand>(receiveString); }
            catch(ThreadAbortException ex){ throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }
            if (!receiveCommand.isValid())
                throw new FTBAutoTestException("Execute command error by get invalid command.");
            if (command.name!=receiveCommand.name)
                throw new FTBAutoTestException("Execute command error by get unexpected command.");
            return receiveCommand.param;
        }

        //ShareMemory Operation
        [DllImport("Kernel32.dll", EntryPoint = "CreateFileMapping")]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, //HANDLE hFile,
         UInt32 lpAttributes,//LPSECURITY_ATTRIBUTES lpAttributes,  //0
         UInt32 flProtect,//DWORD flProtect
         UInt32 dwMaximumSizeHigh,//DWORD dwMaximumSizeHigh,
         UInt32 dwMaximumSizeLow,//DWORD dwMaximumSizeLow,
         string lpName//LPCTSTR lpName
         );
        [DllImport("Kernel32.dll", EntryPoint = "OpenFileMapping")]
        private static extern IntPtr OpenFileMapping(
         UInt32 dwDesiredAccess,//DWORD dwDesiredAccess,
         int bInheritHandle,//BOOL bInheritHandle,
         string lpName//LPCTSTR lpName
         );
        [DllImport("Kernel32.dll", EntryPoint = "MapViewOfFile")]
        private static extern IntPtr MapViewOfFile(
         IntPtr hFileMappingObject,//HANDLE hFileMappingObject,
         UInt32 dwDesiredAccess,//DWORD dwDesiredAccess
         UInt32 dwFileOffsetHight,//DWORD dwFileOffsetHigh,
         UInt32 dwFileOffsetLow,//DWORD dwFileOffsetLow,
         UInt32 dwNumberOfBytesToMap//SIZE_T dwNumberOfBytesToMap
         );
        [DllImport("Kernel32.dll", EntryPoint = "UnmapViewOfFile")]
        private static extern int UnmapViewOfFile(IntPtr lpBaseAddress);
        [DllImport("Kernel32.dll", EntryPoint = "CloseHandle")]
        private static extern int CloseHandle(IntPtr hObject);
        private void createShareMemory()
        {
            try
            {
                IntPtr hFile = new IntPtr(INVALID_HANDLE_VALUE);
                shareMemoryFileHandle = CreateFileMapping(hFile, 0, PAGE_READWRITE, 0, shareMemoryFileSize, shareMemoryFileName);
                shareMemoryPtr = MapViewOfFile(shareMemoryFileHandle, FILE_MAP_ALL_ACCESS, 0, 0, shareMemoryFileSize);
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        private void closeShareMemory()
        {
            try
            {
                UnmapViewOfFile(shareMemoryPtr);
                CloseHandle(shareMemoryFileHandle);
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        private unsafe void copyFromShareMemory(byte[] dst, IntPtr src)
        {
            fixed (byte* pDst = dst)
            {
                byte* pdst = pDst;
                byte* psrc = (byte*)src;
                while ((*pdst++ = *psrc++) != '\0') ;
            }
        }
        private unsafe void copyToShareMemory(byte[] byteSrc, IntPtr dst)
        {
            fixed (byte* pSrc = byteSrc)
            {
                byte* pDst = (byte*)dst;
                byte* psrc = pSrc;
                for (int i = 0; i < byteSrc.Length; i++)
                {
                    *pDst = *psrc;
                    pDst++;
                    psrc++;
                }
            }
        }
        private unsafe void cleanShareMemory(IntPtr dst, uint sm_size)
        {
            byte* pDst = (byte*)dst;
            for (int i = 0; i < sm_size; i++)
            {
                *pDst = 0;
                pDst++;
            }
        }

        //Semaphore Operation
        private void createSemaphore()
        {
            writeSemaphore = new Semaphore(0, 1, writeSemaphoreName);
            readSemaphore = new Semaphore(0, 1, readSemaphoreName);
        }
        private void closeSemaphore()
        {
            writeSemaphore.Close();
            readSemaphore.Close();
        }
    }
}
