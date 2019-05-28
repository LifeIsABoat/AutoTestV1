using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Engine
{
    class EngineCommand
    {
        public string name { get; set; }
        public object param { get; set; }

        public EngineCommand()
        {
            name = "";
            param = null;
        }

        public bool isValid()
        {
            if (null == name || "" == name)
                return false;
            if (null == param)
                return false;
            return true;
        }
    }
}
