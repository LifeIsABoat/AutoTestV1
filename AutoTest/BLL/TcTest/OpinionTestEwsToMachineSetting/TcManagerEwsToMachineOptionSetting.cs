using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using Tool.BLL;

namespace Tool.BLL
{
    class TcManagerEwsToMachineOptionSetting : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerEwsToMachineOptionSetting()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            //Check RspToPanelOptionChecker
            checkList.Add(new Check.EwsToMachineOptionChecker());

            //save screen information and do (levelTransfer)
            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);
            //NewChain
            //save screen information but don't do levelTransfer
            AbstractCmnTestHandler optionScreenLoder = new OneLevelCmnTestScreenLoder(true, true);
            AbstractCmnTestHandler targetOptionButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            optionScreenLoder.setHandler(targetOptionButtonLoder);

            //Chain 
            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(optionScreenLoder);
      
            //do OptionSetMachineFromRspHandler
            AbstractCmnTestHandler EwsToMachineOptionHandler = new EwsToMachineOptionSetttings();

            goHomeHandler.setHandler(EwsToMachineOptionHandler);
            EwsToMachineOptionHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);

            //do limitRangeHandler
            AbstractCmnTestHandler limitRangeHandler = new TotalTCCmnTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(goHomeHandler, this.GetType().Name);
            AbstractCmnTestHandler tcOpinionChecker = new TotalTCCmnTestOpinionChecker(checkList);
            limitRangeHandler.setHandler(tcRunHandler);
            tcRunHandler.setHandler(tcOpinionChecker);
            //do first Handler
            first = limitRangeHandler;
        }//end TcManagerRspToMachineOptionSetting
        public override void run()
        {
            //start run
            first.execute();
        }
        public override bool isTcValid(int tcIndex)
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            if (treeMemory.isEwsValid(tcIndex) == true)
            {
                return true;
            }
            return false;
        }//end public isTcVaild
    }//end class TcMangerOpintionSetting
}
