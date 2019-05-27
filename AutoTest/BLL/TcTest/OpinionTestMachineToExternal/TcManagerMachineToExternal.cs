using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class TcManagerMachineToExternal : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerMachineToExternal()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            //save screenLoder
            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            //UseForOptionSetting
            AbstractCmnTestHandler screenLoderOption = new OneLevelCmnTestScreenLoder(true, true);
            AbstractCmnTestHandler targetButtonLoderOption = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransferOption = new OneLevelCmnTestNextLevelTransfer();
            screenLoderOption.setHandler(targetButtonLoderOption);
            targetButtonLoderOption.setHandler(levelTransferOption);

            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoderOption);
            AbstractCmnTestHandler machineSetCheckHandler = new MachineSetCheck();

            goHomeHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);
            optionSelectHandler.setHandler(machineSetCheckHandler);

            AbstractCmnTestHandler previousHandler = machineSetCheckHandler;

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("MachineToRspOptionChecker"))
            {
                checkList.Add(new BLL.Check.MachineToRspOptionChecker());
                AbstractCmnTestHandler PanelToGetRspOptionHandler = new MachineToRsp();
                //after optionSelect then goHome
                previousHandler.setHandler(PanelToGetRspOptionHandler);
                previousHandler = PanelToGetRspOptionHandler;
            }

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("MachineToEwsOptionChecker"))
            {
                checkList.Add(new BLL.Check.MachineToEwsOptionChecker());
                AbstractCmnTestHandler PanelToGetEwsOptionHandler = new MachineToEws();
                //after optionSelect then goHome
                previousHandler.setHandler(PanelToGetEwsOptionHandler);
                previousHandler = PanelToGetEwsOptionHandler;
            }

            //do limitRange
            AbstractCmnTestHandler limitRangeHandler = new TotalTCCmnTestLimitRangeHandler(this);
            AbstractCmnTestHandler tcRunHandler = new TotalTCCmnTestRunHandler(goHomeHandler, this.GetType().Name);
            AbstractCmnTestHandler tcOpinionChecker = new TotalTCCmnTestOpinionChecker(checkList);
            limitRangeHandler.setHandler(tcRunHandler);
            tcRunHandler.setHandler(tcOpinionChecker);

            //assign limitRangeHandler to first
            first = limitRangeHandler;
        }//end public TcMangerOpintionSetting
        public override void run()
        {
            //start run
            first.execute();
        }
        public override bool isTcValid(int tcIndex)
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            if ((treeMemory.isRspValid(tcIndex) == true) || (treeMemory.isEwsValid(tcIndex) == true))
            {
                return true;
            }
            return false;
        }//end public isTcVaild
    }//end class TcMangerOpintionSetting
}
