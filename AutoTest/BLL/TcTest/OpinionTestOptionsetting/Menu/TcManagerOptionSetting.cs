using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TcManagerOptionSetting : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerOptionSetting()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();

            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoder);

            goHomeHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);

            AbstractCmnTestHandler previousHandler = optionSelectHandler;

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("OptionSettingChecker"))
            {
                checkList.Add(new BLL.Check.OptionSettingChecker());
                AbstractCmnTestHandler toOpintion = new ToOption();
                previousHandler.setHandler(toOpintion);
                previousHandler = toOpintion;
            }

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("OptionSettingFromHomeChecker"))
            {
                checkList.Add(new BLL.Check.OptionSettingFromHomeChecker());
                AbstractCmnTestHandler homeToOpintion = new HomeToOption();
                previousHandler.setHandler(homeToOpintion);
                previousHandler = homeToOpintion;
            }

            AbstractCmnTestHandler limitRangeHandler = new TotalTCCmnTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(goHomeHandler, this.GetType().Name);
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
