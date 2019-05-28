using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Engine
{
    interface IOptionOperateAPI
    {
        string setOption(List<string> path,string optionWords);
        string getOption(List<string> path);
        void setMachineIP(string ip);
    }
}
