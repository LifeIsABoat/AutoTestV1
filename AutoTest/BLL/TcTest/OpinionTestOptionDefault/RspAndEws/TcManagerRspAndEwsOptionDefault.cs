using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class TcManagerRspAndEwsOptionDefault : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerRspAndEwsOptionDefault()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            AbstractCmnTestHandler previousHandler = null;

            if ( TestRuntimeAggregate.getSelectedOpinion().Contains("RspDefaultCheckWithFTB") )
            {
                checkList.Add(new BLL.Check.RspDefaultCheckWithFTB());
                AbstractCmnTestHandler GetRspOptionHandler = new RspDefault();
                previousHandler = GetRspOptionHandler;
            }

            if ( TestRuntimeAggregate.getSelectedOpinion().Contains("EwsDefaultCheckWithFTB") )
            {
                checkList.Add(new BLL.Check.EwsDefaultCheckWithFTB());
                AbstractCmnTestHandler GetEwsOptionHandler = new EwsDefault();
                if (previousHandler != null)
                {
                    previousHandler.setHandler(GetEwsOptionHandler);
                }
                else
                {
                    previousHandler = GetEwsOptionHandler;
                }
            }

            //do limitRange
            AbstractCmnTestHandler limitRangeHandler = new TotalTCCmnTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(previousHandler, this.GetType().Name);
            AbstractCmnTestHandler tcOpinionChecker = new TotalTCCmnTestOpinionChecker(checkList);
            limitRangeHandler.setHandler(tcRunHandler);
            tcRunHandler.setHandler(tcOpinionChecker);

            //assign limitRangeHandler to first
            first = limitRangeHandler;
        }
        public override void run()
        {
            first.execute();
        }
        public override bool isTcValid(int tcIndex)
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string factoryStr = treeMemory.getFactorySetting(tcIndex);
            if (factoryStr == null || factoryStr == "" || factoryStr == "N")
            {
                //factoryStr isn't Y return false.Tc isn't run 
                return false;
            }
            if ((treeMemory.isRspValid(tcIndex) != true) || (treeMemory.isEwsValid(tcIndex) != true))
            {
                //if factoryStr is Y but rsp isn't y or ews isn't y
                return false;
            }

            return true;
        }
    }
}
