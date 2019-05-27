using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TempTcManagerOptionSetting : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TempTcManagerOptionSetting()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            checkList.Add(new BLL.Check.OptionSettingChecker());

            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            AbstractCmnTestHandler goTempRootHdler = new TempGoTempRoot();
            AbstractCmnTestHandler tempLevelTransferHandler = new TempLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoder);
            AbstractCmnTestHandler toOpintion = new ToOption();

            goTempRootHdler.setHandler(tempLevelTransferHandler);
            tempLevelTransferHandler.setHandler(optionSelectHandler);
            optionSelectHandler.setHandler(toOpintion);

            AbstractCmnTestHandler limitRangeHandler = new TempTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(goTempRootHdler, this.GetType().Name);
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
            DAL.IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string factoryStr = treeMemory.getFactorySetting(tcIndex);
            if (factoryStr == "Y" || factoryStr == "N")
            {
                return true;
            }
            return false;
        }
    }
}
