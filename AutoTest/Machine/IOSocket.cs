using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FTBAutoTestTool
{
    class IOSocket : AbstractIO
    {
        Socket socket;
        private string ipStr;
        private string port;

        public IOSocket(System.Xml.XmlNode ioInfo)
        {
            this.ipStr = ioInfo["IP"].InnerText;
            this.port = ioInfo["Port"].InnerText;
            connect();
        }

        public override bool isConnected()
        {
            if(socket == null)
            {
                return false;
            }
            return socket.Connected;
        }

        public override void connect()
        {
            //get ip address
            IPAddress ip;
            int portNum;
            try
            {
                if (CheckbvBoard() == false)
                {
                    System.Windows.Forms.MessageBox.Show("plase check BvBoard whether it opened or not");
                }
                if (CheckTeraTerm() == true)
                {
                    System.Windows.Forms.MessageBox.Show("Please close the BvBoard TeraTerm");
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
                //write("ｮ");
            }
            catch (ArgumentNullException e)
            {
                System.Windows.Forms.MessageBox.Show($"connect filed IOSocket Communicator ArgumentNullException: {e.Message}");
            }
            catch (SocketException e)
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
                write("exit");
                socket.Close();
            }
        }

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
            IntPtr mainHandle = FindWindow(null, "BH17 3.7TP パネル画面");
            //var tu = Convert.ToString(mainHandle.ToInt32(), 16);
            if (mainHandle != IntPtr.Zero)
            {
                var hwnd1 = FindWindowEx(mainHandle, IntPtr.Zero, "#32770", "");
                return mainHandle != IntPtr.Zero;
            }
            return false;
        }

        public override void dispose()
        {
            disconnect();
        }
    }
}
