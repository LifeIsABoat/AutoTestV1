using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    public abstract class AbstractTcManager
    {
        //public List<IChecker> checkList = null;

        public abstract void run();

        public virtual bool isTcValid(int tcIndex)
        {
            return true;
        }
    }
}
