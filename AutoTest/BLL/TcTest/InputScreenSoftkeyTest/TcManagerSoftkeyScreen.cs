using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class TcManagerSoftkeyScreen : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        public TcManagerSoftkeyScreen()
        {
            List<AbstractChecker> checkList = new List<AbstractChecker>();
            //Check
            checkList.Add(new BLL.Check.InputToOptionChecker());

            //use for one level transfer
            AbstractCmnTestHandler screenLoder = new OneLevelCmnTestScreenLoder(true);
            AbstractCmnTestHandler targetButtonLoder = new OneLevelCmnTestTargetButtonLoder();
            AbstractCmnTestHandler levelTransfer = new OneLevelCmnTestNextLevelTransfer();
            screenLoder.setHandler(targetButtonLoder);
            targetButtonLoder.setHandler(levelTransfer);

            //use for option level
            AbstractCmnTestHandler screenLoaderOpition = new OneLevelCmnTestScreenLoder(true,true);
            AbstractCmnTestHandler targetButtonLoaderOption = new OneLevelCmnTestTargetButtonLoder();
            screenLoaderOpition.setHandler(targetButtonLoaderOption);

            AbstractCmnTestHandler goHomeHandler = new OneTCCmnTestGoHome();
            AbstractCmnTestHandler levelTransferHandler = new OneTCCmnTestLevelTransfer(screenLoder);
            AbstractCmnTestHandler optionSelectHandler = new OneTCCmnTestOptionSelect(screenLoaderOpition);
            AbstractCmnTestHandler inputToOption = new InputToOption();
            goHomeHandler.setHandler(levelTransferHandler);
            levelTransferHandler.setHandler(optionSelectHandler);
            optionSelectHandler.setHandler(inputToOption);

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
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string ftbUsWords = treeMemory.getOptionWords(tcIndex);
            if (Regex.IsMatch(ftbUsWords, "^(BRN)?Manual:", RegexOptions.IgnoreCase))
            {
                return true;
            }
            else if (Regex.IsMatch(ftbUsWords, @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
