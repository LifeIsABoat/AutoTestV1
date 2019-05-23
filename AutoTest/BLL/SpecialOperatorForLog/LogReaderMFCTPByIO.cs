using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Tool.Machine;

namespace Tool.BLL
{
    class LogReaderMFCTPByIO
    {
        public const string GetScreenNameCmd = "|n";
        public const string GetScreenInfoCmd = "|s2";

        private static AbstractComponentMachineIO io = null;
        private static Stopwatch stopwatch = new Stopwatch();
        //private static string backupScreenName = "[TS:SC:SN(null,0)]\r\n";
        

        public static void setIO(AbstractComponentMachineIO io)
        {
            LogReaderMFCTPByIO.io = io;
        }
        //command
        public LogReaderMFCTPByIO()
        {
            if (null == io)
                throw new FTBAutoTestException("Create logreader error by null io.");
        }

        /*
         *  Description: get screeninfo or screenid
         *  Param: 
         *  Return: string - screeninfo + screenid
         *  Exception: 
         *  Example: 
         */
        public string read(uint timeOutMs = 1000)
        {
            bool logReadOpenFlag = io.isReadON();
            //Open Log Reader
            if (!logReadOpenFlag && io.isReadOFF())
                io.readON();

            //get screenid
            string scrIdBuf = "";
            stopwatch.Restart();
            io.write(GetScreenNameCmd);
            while (!LogParserMFCTP.isScrNameFinished(scrIdBuf))
            {
                //if wait over timeOutMs,break;
                if (stopwatch.ElapsedMilliseconds > timeOutMs)
                {
                    //scrIdBuf = backupScreenName;
                    break;
                }
                System.Threading.Thread.Sleep(5);
                scrIdBuf += io.read();
            }
            //backupScreenName = scrIdBuf;
            stopwatch.Stop();

            //get screeninfo
            string scrInfoBuf = "";
            stopwatch.Restart();
            io.write(GetScreenInfoCmd);
            while (!LogParserMFCTP.isScrInfoFinished(scrInfoBuf))
            {
                //if wait over timeOutMs,break;
                if (stopwatch.ElapsedMilliseconds > timeOutMs)
                {
                    break;
                }
                System.Threading.Thread.Sleep(5);
                scrInfoBuf += io.read();
            }
            stopwatch.Stop();

            if (!logReadOpenFlag && io.isReadON())
                io.readOFF();

            string retLog = scrIdBuf + scrInfoBuf;

            StaticLog4NetLogger.mfcLogger.Debug("ReadLog.\r\n"+retLog);
            StaticLog4NetLogger.mfcLogger.Info("ReadLog.");

            return retLog;
        }
    }
}
