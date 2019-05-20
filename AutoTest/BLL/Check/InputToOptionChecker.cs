using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL.Check
{
    class InputToOptionChecker : AbstractChecker
    {
        //private IFTBCommonAPI treeMemory;
        private int currentTcIndex, currentLevelIndex;
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            IIterator levelIterator = treeMemory.createLevelIterator();

            opinion = this.GetType().Name;
            string inputContentStr = null;
            string preLevelButtonWord = null;
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                currentTcIndex = tcIterator.currentItem();
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    currentLevelIndex = levelIterator.currentItem();
                    int LevelCount = treeMemory.getLevelCount(tcIterator.currentItem());
                    if (currentLevelIndex < treeMemory.getLevelCount(currentTcIndex) - 1)
                    {
                        continue;
                    }
                    if (treeMemory.isLevelOption(currentLevelIndex) == false)
                    {
                        //get previouslevel button word
                        preLevelButtonWord = treeMemory.getLevelButtonWord(currentLevelIndex);
                    }
                    else
                    {
                        //get inputContent from runtime
                        inputContentStr = TestRuntimeAggregate.getInputContent(tcIterator.currentItem(), opinion, LevelCount);
                    }
                }
                //do try or catch
                try
                {
                    TestCheckResult preOptionCheckResult = TestCheckResult.NG;
                    if (isBlackList(ref preOptionCheckResult, currentTcIndex, currentLevelIndex) == false)
                    {
                        currentLevelIndex--;
                        //get pre option screen
                        object preOptionScreen = TestRuntimeAggregate.getOpinionScreen(currentTcIndex, opinion, currentLevelIndex);
                        if (null != preOptionScreen)
                        {
                            //check previous level screen option value with inputScreen's inputContent
                            bool result = checkPreLevelSetting(inputContentStr, preOptionScreen, preLevelButtonWord);
                            preOptionCheckResult = (result == true) ? TestCheckResult.OK : TestCheckResult.NG;
                        }
                    }
                    TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, preOptionCheckResult, currentLevelIndex);
                    StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " set result " + opinion + preOptionCheckResult.ToString());
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIterator.currentItem() + " check " + opinion + " failed by " + ex.Message);
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                }
            }//end for() loop
        }//end public void check()

        private bool checkPreLevelSetting(string strInputContent, object targetScreen, string preLevelBtnWord)
        {
            if (strInputContent == null || targetScreen == null || preLevelBtnWord == null)
            {
                return false;
            }
            ControlButton targetButton = null;
            //check previous level screen option value
            if (targetScreen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)targetScreen;
                IIterator screenReadIterator;
                screenReadIterator = screenAggregate.createReadIterator();
                screenReadIterator.first();
                while (!screenReadIterator.isDone())
                {
                    int index = screenReadIterator.currentItem();
                    Screen currentScreen = screenAggregate.readScreen(index);
                    targetButton = getTargetControlButton(currentScreen, preLevelBtnWord);
                    if (targetButton != null)
                    {
                        break;
                    }
                    screenReadIterator.next();
                }
            }
            else if (targetScreen is Screen)
            {
                Screen currentScreen = (Screen)targetScreen;
                targetButton = getTargetControlButton(currentScreen, preLevelBtnWord);
            }
            //return check result 
            if (targetButton != null && targetButton.stringList.Count > 1)
            {
                if (getStringElementStr(targetButton.stringList[1]) == strInputContent)
                {
                    return getStringElementStr(targetButton.stringList[1]) == strInputContent;
                }
                else
                {
                    string onlyNumberStr = RemoveNotNumber(getStringElementStr(targetButton.stringList[1]));
                    return onlyNumberStr == strInputContent;
                }
            }
            return false;
        }//end private bool checkPreLevelSetting

        private static string RemoveNotNumber(string key)
        {
            return Regex.Replace(key, @"[^\d]*", "");
        }

        private ControlButton getTargetControlButton(Screen screen,string btnWord)
        {
            List<AbstractControl> tmpControlList = screen.getControlList(typeof(ControlButton));
            if (tmpControlList == null)
            {
                return null;
            }
            foreach (ControlButton button in tmpControlList)
            {
                if (getStringElementStr((button).stringList[0]) == btnWord)
                {
                    return button;
                }
            }
            return null;
        }

        private string getStringElementStr(ElementString eleStr)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
            {
                return eleStr.str;//check by log
            }
            else
            {
                throw new FTBAutoTestException("String check Error");
            }
        }
    }//end class InputToOptionChecker
}
