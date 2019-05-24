using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TempTcManagerFtbString : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TempTcManagerFtbString()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();

            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            //use for option level
            AbstractCmnTestHandler screenLoaderOpition = new OneLevelCmnTestScreenLoder();
            AbstractCmnTestHandler targetButtonLoaderOption = new OneLevelCmnTestTargetButtonLoder();
            screenLoaderOpition.setHandler(targetButtonLoaderOption);

            AbstractCmnTestHandler goTempRootHdler = new TempGoTempRoot();
            AbstractCmnTestHandler tempLevelTransferHandler = new TempLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoaderOpition);
            goTempRootHdler.setHandler(tempLevelTransferHandler);
            tempLevelTransferHandler.setHandler(optionSelectHandler);

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("FtbStringChecker"))
            {
                checkList.Add(new FtbStringChecker());
            }

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("ftbTitleChecker"))
            {
                checkList.Add(new ftbTitleChecker());
            }

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
    }
}
