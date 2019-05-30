using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Tool.Machine
{
    class ComponentIOSerial:AbstractComponentMachineIO
    {
        //if reanon,readon_flag=0
        private int readOnFlag = -1;
        private SerialPort serialPort;
        private StringBuilder buf = new StringBuilder(0,1024*1024);
        Mutex bufMutex = new Mutex();
        public ComponentIOSerial(SerialConfig serialConfig)
        {
            if (null == serialConfig)
            {
                throw new FTBAutoTestException("serialInfo cannot be empty");
            }
            serialPort = new SerialPort();
            serialPort.BaudRate = serialConfig.baudRate;
            serialPort.Parity = serialConfig.parity;
            serialPort.DataBits = serialConfig.dataBits;
            serialPort.StopBits = serialConfig.stopBits;
            serialPort.PortName = serialConfig.portName;
            serialPort.ReadBufferSize = serialPort.ReadBufferSize;
            serialPort.WriteBufferSize = serialPort.WriteBufferSize;
            serialPort.ReadTimeout = serialConfig.timeout;
            serialPort.WriteTimeout = serialConfig.timeout;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private bool CheckTeraTerm()
        {
            Process[] app = Process.GetProcessesByName("ttermpro");
            if (app.Length > 0 && app[0].MainWindowTitle.Contains("Tera Term VT"))
            {
                return true;
            }
            return false;
        }
        //establish connection
        public override void connect()
        {
            try
            {
                if (CheckTeraTerm() == true)
                {
                    Process[] p = Process.GetProcessesByName("ttermpro");
                    if (p.Length != 0)
                    {
                        foreach (Process item in p)
                        {
                            if (item.ProcessName == "ttermpro") { item.Kill(); }
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Close TeraTerm failed!.");
            }
            Thread.Sleep(2000);
            try
            {
                serialPort.Open();
            }
            catch
            {
                throw new FTBAutoTestException("Connect Serial port failed, please check Com number.");
            }
        }
        //disconnect connection
        public override void disconnect()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                throw new FTBAutoTestException("Disconnect Serial port failed,");
            }
        }
        public override void write(string cmd)
        {
            byte[] sendbuf = new byte[cmd.Length];
            //sendbuf = System.Text.Encoding.ASCII.GetBytes(cmd);
            int i= 0;
            while (i < cmd.Length)
            {
                sendbuf[i] = (byte)cmd[i];
                i++;
            }
            if (null == serialPort || !serialPort.IsOpen)
            {
                throw new FTBAutoTestException("SerialPort not initialized");
            }
            try
            {
                serialPort.Write(sendbuf, 0, sendbuf.Length);
            }
            catch
            {
                throw new FTBAutoTestException("Send command to MFC Failed!");
            }
        }
        //Read the data in the read cache
        public override string read()
        {
            string ret = "";
            bufMutex.WaitOne();
            if (buf.Length > 0)
            {
                ret = buf.ToString();
                buf.Remove(0, buf.Length);
            }
            bufMutex.ReleaseMutex();
            return ret;
        }
        //Determine the read cache whether to open 
        public override bool isReadON() 
        {
            if (readOnFlag == 0)
                return true;
            else
                return false;
        }
        //Determine the read cache whether to close 
        public override bool isReadOFF()
        {
            if (readOnFlag == -1)
                return true;
            else
                return false;
        }
        //Open the read cache and be able to receive data
        public override void readON()
        {
            if (readOnFlag == 0)
            {
                throw new Exception("Delegates had openned");
            }
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceiver);
            readOnFlag = 0;
        }
        //Close the read cache and can not read the new data
        public override void readOFF()
        {
            if (readOnFlag == -1)
            {
                throw new Exception("Delegates had closed");
            }
            readOnFlag = -1;
            serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceiver);
        }

        private void DataReceiver(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string indata = serialPort.ReadExisting();
                bufMutex.WaitOne();
                buf.Append(indata);
                bufMutex.ReleaseMutex();
            }
            catch
            {
                throw new FTBAutoTestException("The specified port is not open");
            }
        }
    }//class IoUseSerial
}
