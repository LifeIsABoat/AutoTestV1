using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace Tool.Machine
{
    class ComponentIOSocket : AbstractComponentMachineIO
    {
        private int readOnFlag = -1;
        private StringBuilder buf = new StringBuilder(0, 1024 * 1024);
        Mutex bufMutex = new Mutex();
        Socket socket;
        private string ipStr;
        private string port;
        public ComponentIOSocket(string iPAddress, string port)
        {
            this.ipStr = iPAddress;
            this.port = port;
        }
        public bool isConnected()
        {
            if (socket == null)
            {
                return false;
            }
            return socket.Connected;
        }
        //establish connection
        public override void connect()
        {
            //get ip address
            IPAddress ip;
            int portNum;
            try
            {
                if (CheckbvBoard() == false)
                {
                    System.Windows.Forms.MessageBox.Show("BvBoard is't Opened.Plase Open BvBoard.");
                    //throw new Exception("BvBoard is't Opened.Plase Open BvBoard First.");
                }
                if (CheckTeraTerm() == true)
                {
                    System.Windows.Forms.MessageBox.Show("BvBoard TeraTerm is Opened.Please close the BvBoard TeraTerm");
                    //throw new Exception("IOSocket Communicator Parse IP Address Error.");
                }
                if (!IPAddress.TryParse(ipStr, out ip))
                {
                    ip = Dns.GetHostAddresses(ipStr)[1];
                }
                if (!int.TryParse(port, out portNum))
                {
                    portNum = 1023;
                }
            }
            catch
            {
                throw new Exception("IOSocket Communicator Parse IP Address Error.");
            }

            //get ip endpoint
            IPEndPoint ipe = new IPEndPoint(ip, portNum);

            //connect socket
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.ReceiveTimeout = 200;
                socket.Connect(ipe);
            }
            catch (ArgumentNullException e)
            {
                System.Windows.Forms.MessageBox.Show($"connect filed IOSocket Communicator ArgumentNullException: {e.Message}");
            }
            catch (SocketException e)//
            {
                System.Windows.Forms.MessageBox.Show($"connect filed IOSocket Communicator SocketException: {e.Message}");
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"connect filed IOSocket Communicator Exception: {e.Message}");
            }
        }
        //disconnect connection
        public override void disconnect()
        {
            if (isConnected() == true)
            {
                socket.Close();
                Thread.Sleep(1000);
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private bool CheckTeraTerm()
        {
            IntPtr mainHandle = FindWindow(null, "localhost:1023 - Tera Term VT");
            return mainHandle != IntPtr.Zero;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        private bool CheckbvBoard()
        {
            Process[] app = Process.GetProcessesByName("Body");
            if (app.Length > 0 && app[0].MainWindowTitle == "bV_Body")
            {
                return true;
            }
            return false;
        }
        //Read the data in the read cache
        public override string read()
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024];
                int size = socket.Receive(buffer);
                String recv = Encoding.UTF8.GetString(buffer, 0, size);
                return recv;
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"IOSocket read failed: {ex.Message}");
            }
        }
        public override void write(string writeBuf)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(writeBuf);
                socket.Send(buffer);
                Console.WriteLine($"IO send{writeBuf}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"IOSocket write failed: {ex.Message}");
            }
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
        }
    }//class IoUseSerial
}
