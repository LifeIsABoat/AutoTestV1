using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Machine
{
    class ScreenSocket
    {
        Socket socket;
        private string ipStr;
        private string port;
        private int imgRcvSize;

        public ScreenSocket(string iPAddress, string port)
        {
            this.ipStr = iPAddress;
            this.port = port;
            string folderFullName = StaticEnvironInfo.getIntPutModelPath();
            if (folderFullName.Contains("37") || folderFullName.Contains("3.7"))
            {
                this.imgRcvSize = int.Parse("419958");
            }
            else if (folderFullName.Contains("27") || folderFullName.Contains("2.7"))
            {
                this.imgRcvSize = int.Parse("313038");
            }
            else if (folderFullName.Contains("57") || folderFullName.Contains("5.7"))
            {
                this.imgRcvSize = int.Parse("1536054");
            }
            connect();
        }
        ~ScreenSocket()
        {
            disconnect();
        }
        public bool isConnected()
        {
            if (socket == null)
            {
                return false;
            }
            return socket.Connected;
        }

        public void connect()
        {
            //get ip address
            IPAddress ip;
            int portNum;
            try
            {
                if (!IPAddress.TryParse(ipStr, out ip))
                {
                    ip = Dns.GetHostAddresses(ipStr)[1];
                }
                if (!int.TryParse(port, out portNum))
                {
                    portNum = 10010;
                }
            }
            catch
            {
                throw new Exception("ScreenSocket Communicator Parse IP Address Error.");
            }

            //get ip endpoint
            IPEndPoint ipe = new IPEndPoint(ip, portNum);

            //connect socket
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipe);
            }
            catch (ArgumentNullException e)
            {
                System.Windows.Forms.MessageBox.Show($"connect filed ScreenSocket Communicator ArgumentNullException: {e.Message}");
            }
            catch (SocketException e)
            {
                System.Windows.Forms.MessageBox.Show($"connect filed ScreenSocket Communicator SocketException: {e.Message}");
            }
        }

        //disconnect connection
        public void disconnect()
        {
            if (isConnected() == true)
            {
                socket.Close();
            }
        }

        public byte[] read()
        {
            try
            {
                byte[] buffer = new byte[imgRcvSize];
                int index = 0;
                while (index < imgRcvSize - 1)
                {
                    byte[] tmp = new byte[1024 * 1024];
                    int read_size = socket.Receive(tmp, 1024 * 1024, 0);
                    Buffer.BlockCopy(tmp, 0, buffer, index, read_size);
                    index += read_size;
                }
                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"IOSocket read failed: {ex.Message}");
            }
        }

        public void send(string writeBuf)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(writeBuf);
                socket.Send(buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"IOSocket write failed: {ex.Message}");
            }
        }
    }
}
