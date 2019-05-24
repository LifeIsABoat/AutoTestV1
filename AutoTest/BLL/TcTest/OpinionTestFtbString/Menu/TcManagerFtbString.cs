using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TcManagerFtbString : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerFtbString()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();

            //use for one level transfer
            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder(true);
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            //use for option level
            AbstractCmnTestHandler screenLoaderOpition = new OneLevelCmnTestScreenLoder(true);
            AbstractCmnTestHandler targetButtonLoaderOption = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransferOption = new OneLevelCmnTestNextLevelTransfer();
            screenLoaderOpition.setHandler(targetButtonLoaderOption);
            targetButtonLoaderOption.setHandler(levelTransferOption);

            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoaderOpition);
            goHomeHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("FtbStringChecker"))
            {
                checkList.Add(new FtbStringChecker());
            }

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("ftbTitleChecker"))
            {
                checkList.Add(new ftbTitleChecker());
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
    }
}
