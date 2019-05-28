using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tool.Command
{
    abstract class AbstractCommandExecutor
    {
        protected static Mutex cmdMutex = new Mutex();
        public abstract void execute(object param = null);
    }
}
