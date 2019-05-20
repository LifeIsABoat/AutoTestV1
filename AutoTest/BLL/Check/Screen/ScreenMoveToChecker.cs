using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class ScreenMoveToChecker : AbstractChecker
    {
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            IIterator levelIterator = treeMemory.createLevelIterator();

            opinion = this.GetType().Name;
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check Start");
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    if (treeMemory.isLevelOption(levelIterator.currentItem(), tcIterator.currentItem()) == false)
                    {
                        continue;
                    }
                    TestCheckResult checkResult = TestCheckResult.NG;
                    if (isBlackList(ref checkResult) == false)
                    {
                        object Screen = TestRuntimeAggregate.getLogScreen(tcIterator.currentItem(), levelIterator.currentItem());
                        List<string> helpInfo = treeMemory.getLevelButtonHelpInfo(levelIterator.currentItem(), tcIterator.currentItem());

                        if (helpInfo != null && helpInfo.Count > 0)
                        {
                            checkResult = checkScreenTitle(Screen, helpInfo[0]);
                        }
                    }
                    TestRuntimeAggregate.setCheckResult(tcIterator.currentItem(), opinion, checkResult, levelIterator.currentItem());
                    //report log
                    StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion +
                                                        "TC-" + tcIterator.currentItem() +
                                                        "level-" + levelIterator.currentItem() +
                                                        "result-" + checkResult.ToString());
                }
            }
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check End");
        }

        private TestCheckResult checkScreenTitle(object Screen, string targetTitle)
        {
            if(Screen == null || targetTitle == "")
            {
                return TestCheckResult.NG;
            }
            else
            {
                Screen tempScreen = null;
                if (Screen is AbstractScreenAggregate)
                {
                    AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)Screen;
                    tempScreen = screenAggregate.getFirstScreen();
                }
                else if (Screen is Screen)
                {
                    tempScreen = (Screen)Screen;
                }
                if (tempScreen != null)
                {
                    List<AbstractControl> controlList = tempScreen.getControlList(typeof(ControlTitle));
                    foreach(AbstractControl oneControl in controlList)
                    {
                        if(((ControlTitle)oneControl).str.str == targetTitle)
                        {
                            return TestCheckResult.OK;
                        }
                    }
                }
                return TestCheckResult.NG;
            }
        }
    }
}
