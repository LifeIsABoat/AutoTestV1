using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Engine
{
    abstract class AbstractEngineCommunicator
    {
        public abstract object execute(EngineCommand command, int timeout = -1);
    }
}
