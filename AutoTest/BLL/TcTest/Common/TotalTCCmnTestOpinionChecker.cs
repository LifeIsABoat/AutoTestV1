using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class TotalTCCmnTestOpinionChecker : AbstractCmnTestHandler
    {
        List<AbstractChecker> checkList = null;

        public TotalTCCmnTestOpinionChecker(List<AbstractChecker> testOpinionCheckerList)
        {
            checkList = testOpinionCheckerList;
        }
        public override void execute()
        {
            foreach(AbstractChecker checker in checkList)
            {
                checker.check();
            }

            base.execute();
        }
    }
}
