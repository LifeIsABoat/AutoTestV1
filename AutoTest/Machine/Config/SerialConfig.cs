using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Tool.Machine
{
    public class SerialConfig
    {
        public int baudRate;
        public Parity parity;
        public int dataBits;
        public StopBits stopBits;
        public string portName;
        public int timeout;
        public int readBufferSize;
        public int writeBufferSize;

        public SerialConfig(string portName,
                            int baudRate = 1200,
                            int dataBist = 8,
                            Parity parity = Parity.None,
                            StopBits stopBits = StopBits.One,
                            int readBufferSize = 16*1024,
                            int writeBufferSize = 16*1024,
                            int timeout = 500)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.dataBits = dataBist;
            this.parity = parity;
            this.timeout = timeout;
            this.stopBits = stopBits;
            this.readBufferSize = readBufferSize;
            this.writeBufferSize = writeBufferSize;
        }
    }
}
