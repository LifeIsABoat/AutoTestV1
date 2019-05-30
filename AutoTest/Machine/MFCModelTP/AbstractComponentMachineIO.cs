using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Machine
{
    abstract class AbstractComponentMachineIO
    {
        //establish connection
        public virtual void connect() { }

        //disconnect connection
        public virtual void disconnect() { }

        //Read the data in the read cache
        public abstract string read();

        //Send the data out
        public abstract void write(string writeBuf);

        //Open the read cache and be able to receive data
        public virtual void readON() { }

        //Close the read cache and can not read the new data
        public virtual void readOFF() { }

        //Determine the read cache whether to open 
        public abstract bool isReadON();

        //Determine the read cache whether to close 
        public abstract bool isReadOFF();
        
    }
}
