using Tool.DAL;
using Tool.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class EwsToMachineOptionChecker : AbstractChecker
    {
        //check Example:
        //ret1 = OptioinOperatorByEWS.setOption(path, optionWord);
        //get Panel's optionWord From TcIndex 
        //compare Panel's optionWord with ret1's optionWord;
        int currentTcIndex, levelCount;
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            opinion = this.GetType().Name;
            //report log start
            StaticLog4NetLogger.reportLogger.Info(" OptionName- " + opinion + " Check Start.");
            //do for
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                TestCheckResult checkResult = TestCheckResult.NG;
                currentTcIndex = tcIterator.currentItem();
                //CurrentTcIndex
                levelCount = treeMemory.getLevelCount(currentTcIndex);
                if (0 == levelCount)
                    throw new FTBAutoTestException("getLevelCount failed by 0 levelCount.");

                StaticLog4NetLogger.reportLogger.Info("TC-" + tcIterator.currentItem() + " levelCount: " + levelCount.ToString());
                if (isBlackList(ref checkResult, tcIterator.currentItem(), levelCount) == false)
                {
                    bool result = false;
                    string ftbButtonWord = treeMemory.getLevelButtonWord(levelCount, -1, currentTcIndex);
                    //do get option selectstate from screen
                    object runtimeScreen = TestRuntimeAggregate.getLogScreen(currentTcIndex, levelCount);
                    try
                    {
                        result = checkOptionSetting(ftbButtonWord, runtimeScreen);
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " checkOptionSetting OK");
                        checkResult = (result == true) ? TestCheckResult.OK : TestCheckResult.NG;
                    }
                    catch (FTBAutoTestException ex)
                    {
                        StaticLog4NetLogger.reportLogger.Warn("TC-" + currentTcIndex + "check " + opinion + " failed by " + ex.Message);
                        StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                }
                //do common reportLog
                TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, checkResult, levelCount);
                StaticLog4NetLogger.reportLogger.Info(" opinionName-" + opinion +
                                            " TC-" + tcIterator.currentItem() +
                                            " level-" + levelCount +
                                            " result-" + checkResult.ToString());
            }//end for
            //report log end
            StaticLog4NetLogger.reportLogger.Info(" OptionName-" + opinion + " Check End.");
        }//end check

        private bool checkOptionSetting(string ftbButtonWord, object runtimeScreen)
        {
            bool checkResult = false;
            if (ftbButtonWord == null || runtimeScreen == null)
            {
                checkResult = false;
            }

            ControlButton currentButton = null;
            Screen currentScreen = null;
            if (runtimeScreen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                IIterator screenReadIterator;
                screenReadIterator = screenAggregate.createReadIterator();
                for (screenReadIterator.first(); !screenReadIterator.isDone(); screenReadIterator.next())
                {
                    int index = screenReadIterator.currentItem();
                    Screen tmpScreen = screenAggregate.readScreen(index);
                    currentButton = getTargetControlButton(tmpScreen, ftbButtonWord);
                    if (currentButton != null)
                    {
                        currentScreen = tmpScreen;
                        break;
                    }
                }
            }
            else if (runtimeScreen is Screen)
            {
                currentScreen = (Screen)runtimeScreen;
                currentButton = getTargetControlButton(currentScreen, ftbButtonWord);
            }

            if (currentButton != null)
            {
                if (null == currentButton.stringList || 0 == currentButton.stringList.Count)
                    return checkResult;

                if (getControlButtonStatus(currentButton) == ControlButtonStatus.Selected)
                {
                    if (ftbButtonWord == getStringElementStr(currentButton.stringList[0]))
                    {
                        checkResult = true;
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " select " + getStringElementStr(currentButton.stringList[0]).ToString());
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " ftbButtonWord " + ftbButtonWord.ToString());
                    }
                }
                else
                {
                	//todo
                    if (Regex.IsMatch(ftbButtonWord, "^(BRN)?Manual:", RegexOptions.IgnoreCase))
                    {
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + "Mabybe NowScreen is ManualInputScreen,you Mabybe need manual test.");
                        checkResult = false;
                    }
                    else if (Regex.IsMatch(ftbButtonWord, @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase))
                    {
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + "Mabybe NowScreen is Ip Adress Screen,you can't change the MachineIP.");
                        checkResult = false;
                    }
                    else if (Regex.IsMatch(ftbButtonWord, "^DispOnly:", RegexOptions.IgnoreCase))
                    {
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " It's Maybe a Info Screen.");
                        checkResult = false;
                    }
                }
            }
            else
            {
                checkResult = false;
            }

            showStatusCheckResult(ControlButtonStatus.Selected.ToString(), currentButton, currentScreen);
            showStringCheckResult(ftbButtonWord, currentButton, currentScreen);
            return checkResult;
        }
        private ControlButton getTargetControlButton(Screen screen, string buttonWord)
        {
            List<AbstractControl> tmpControlList = screen.getControlList(typeof(ControlButton));
            if (tmpControlList == null)
            {
                return null;
            }
            foreach (ControlButton button in tmpControlList)
            {
                if (null == button.stringList || 0 == button.stringList.Count)
                    continue;

                if (button.stringList[0].str == buttonWord)
                {
                    return button;
                }
            }
            return null;
        }

        private ControlButtonStatus getControlButtonStatus(ControlButton button)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
            {
                //check by log
                return button.statusShow;
            }
            //check by camera
            Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = TestRuntimeAggregate.getTransferButtonStatusResult(currentTcIndex, levelCount);
            if (null == buttonStatusResult || !buttonStatusResult.ContainsKey(button))
            {
                throw new FTBAutoTestException("Check button status error.");
            }
            else
            {
                return buttonStatusResult[button];
            }
        }
        private string getStringElementStr(ElementString eleStr)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
            {
                //check by log
                return eleStr.str;
            }
            //check by camera
            Dictionary<ElementString, string> ocrResult = TestRuntimeAggregate.getTransferOcrResult(currentTcIndex, levelCount);
            if (ocrResult != null && ocrResult.ContainsKey(eleStr))
            {
                return ocrResult[eleStr];
            }
            else
            {
                throw new FTBAutoTestException("String check Error");
            }
        }
        private void showStatusCheckResult(string standardStatus, ControlButton currentButton, Screen currentScreen)
        {
            //if ocr is not used, can't get image, so that it can't do show operation 
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //target button is not existed in current screen, so that it can't do show operation 
            if (currentButton == null || currentScreen == null)
                return;

            //structure standard wordsList
            List<string> standardStatusList = new List<string>();
            standardStatusList.Add(standardStatus);

            //structure current words rectList and wordsList
            List<Rectangle> currentRectList = new List<Rectangle>();
            List<string> currentStatusList = new List<string>();
            if (currentButton.stringList.Count == 0)
            {
                currentButton.stringList.Add(new ElementString("", null, currentButton.rect));
            }

            Rectangle currentRect = currentButton.rect;
            currentRectList.Add(currentRect);

            string currentStatus = getControlButtonStatus(currentButton).ToString();
            currentStatusList.Add(currentStatus);

            string imagePath = TestRuntimeAggregate.getTransferImagePath(currentScreen, currentTcIndex, levelCount);

            Engine.EngineOCR ocr = new Engine.EngineOCR();
            ocr.ShowButtonCheckResult(imagePath, standardStatusList, currentRectList, currentStatusList);
        }
        private void showStringCheckResult(string ftbButtonWord, ControlButton currentButton, Screen currentScreen)
        {
            //if ocr is not used, can't get image, so that it can't do show operation 
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //target button is not existed in current screen, so that it can't do show operation 
            if (currentButton == null || currentScreen == null)
                return;

            //structure standard wordsList
            List<string> standardStatusList = new List<string>();
            standardStatusList.Add(ftbButtonWord);

            //structure current words rectList and wordsList
            List<Rectangle> currentRectList = new List<Rectangle>();
            List<string> currentWordList = new List<string>();
            Rectangle currentRect = null;
            string currentWord = "NotFound";
            if (currentButton.stringList.Count == 0)
            {
                currentRect = currentButton.rect;
            }
            else
            {
                currentWord = getStringElementStr(currentButton.stringList[0]);
                currentRect = currentButton.stringList[0].rect;
            }
            currentWordList.Add(currentWord);
            currentRectList.Add(currentRect);

            string imagePath = TestRuntimeAggregate.getTransferImagePath(currentScreen, currentTcIndex, levelCount);

            Engine.EngineOCR ocr = new Engine.EngineOCR();
            ocr.ShowStringCheckResult(imagePath, standardStatusList, currentRectList, currentWordList);
        }
    }
}
