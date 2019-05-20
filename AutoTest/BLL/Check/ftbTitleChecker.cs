using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class ftbTitleChecker : AbstractChecker
    {
        private int currentTcIndex, currentLevelIndex;
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
                currentTcIndex = tcIterator.currentItem();

                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    currentLevelIndex = levelIterator.currentItem();
                    TestCheckResult checkResult = TestCheckResult.NA;
                    if (isBlackList(ref checkResult) == false)
                    {
                        if (currentLevelIndex > 0)
                        {
                            string targetTitle = treeMemory.getLevelButtonToScreenTitle(currentLevelIndex - 1);
                            if (targetTitle != "")
                            {
                                object screen = TestRuntimeAggregate.getLogScreen(currentTcIndex, currentLevelIndex);
                                checkResult = checkTitle(targetTitle, screen);
                            }
                        }
                    }
                    TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, checkResult, currentLevelIndex);
                    //report log
                    StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion +
                                                        "TC-" + tcIterator.currentItem() +
                                                        "level-" + currentLevelIndex +
                                                        "result-" + checkResult.ToString());
                }
            }
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check End");
        }
        private TestCheckResult checkTitle(string targetTitle, object screen)
        {
            TestCheckResult reslut = TestCheckResult.NG;
            Screen tempScreen;
            if (screen == null)
            {
                return TestCheckResult.NG;
            }
            if (screen is Screen)
            {
                tempScreen = (Screen)screen;
            }
            else
            {
                tempScreen = ((AbstractScreenAggregate)screen).getFirstScreen();
            }

            List<AbstractControl> title = tempScreen.getControlList(typeof(ControlTitle));
            if (title != null)
            {
                reslut = (((ControlTitle)title[0]).str.str == targetTitle) ? TestCheckResult.OK : TestCheckResult.NG;
            }
            return reslut;
        }
    }
}
