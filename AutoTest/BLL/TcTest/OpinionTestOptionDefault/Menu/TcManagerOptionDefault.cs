using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class TcManagerOptionDefault : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerOptionDefault()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            checkList.Add(new BLL.Check.OptionDefaultChecker());

            //use for one level transfer
            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            //use for option level
            AbstractCmnTestHandler screenLoaderOpition = new OneLevelCmnTestScreenLoder(true,true);
            AbstractCmnTestHandler targetButtonLoaderOption = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransferOption = new OneLevelCmnTestNextLevelTransfer();
            screenLoaderOpition.setHandler(targetButtonLoaderOption);
            targetButtonLoaderOption.setHandler(levelTransferOption);

            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoaderOpition);
            goHomeHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);

            AbstractCmnTestHandler limitRangeHandler = new TotalTCCmnTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(goHomeHandler,this.GetType().Name);
            AbstractCmnTestHandler tcOpinionChecker = new TotalTCCmnTestOpinionChecker(checkList);
            limitRangeHandler.setHandler(tcRunHandler);
            tcRunHandler.setHandler(tcOpinionChecker);

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
                return false;
            }

            return true;
        }
    }
}
