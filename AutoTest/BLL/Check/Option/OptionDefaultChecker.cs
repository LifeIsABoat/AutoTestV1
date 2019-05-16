using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class OptionDefaultChecker : AbstractChecker
    {
        int currentTcIndex, currentLevelIndex;
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            IIterator levelIterator = treeMemory.createLevelIterator();

            opinion = this.GetType().Name;
            //for run Iterator
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                currentTcIndex = tcIterator.currentItem();
                if (TestRuntimeAggregate.getTreeMemory().isTcValid(currentTcIndex) == false)
                {
                    continue;
                }
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    currentLevelIndex = levelIterator.currentItem();
                    if (treeMemory.isLevelOption(levelIterator.currentItem(), tcIterator.currentItem()) == false)
                    {
                        continue;
                    }

                    //for check Iterator
                    List<int> indexList = treeMemory.getOptionLevelBrotherButtonIndex(currentTcIndex);
                    foreach (int tcIndex in indexList)
                    {
                        object runtimeScreen = null;
                        string ftbButtonWord = null;
                        ControlButton currentButton = null;
                        TestCheckResult checkResult = TestCheckResult.NG;
                        try
                        {
                            if (isBlackList(ref checkResult, tcIndex, currentLevelIndex) == false)
                            {
                                TestRuntimeAggregate.setMachineLogPath(tcIndex, StaticEnvironInfo.getMachineLogPath(currentTcIndex));
                                TestRuntimeAggregate.setCommandLogPath(tcIndex, StaticEnvironInfo.getCommandLogPath(currentTcIndex));
                                TestRuntimeAggregate.setOcrLogPath(tcIndex, StaticEnvironInfo.getOcrLogPath(currentTcIndex));
                                TestRuntimeAggregate.setScreenImagePath(tcIndex, StaticEnvironInfo.getScreenImagePath(currentTcIndex));

                                runtimeScreen = TestRuntimeAggregate.getLogScreen(currentTcIndex, currentLevelIndex);
                                ftbButtonWord = treeMemory.getLevelButtonWord(currentLevelIndex, -1, tcIndex);

                                if (runtimeScreen is AbstractScreenAggregate)
                                {
                                    AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                                    IIterator screenReadIterator = screenAggregate.createReadIterator();
                                    for (screenReadIterator.first(); !screenReadIterator.isDone(); screenReadIterator.next())
                                    {
                                        int index = screenReadIterator.currentItem();
                                        Screen currentScreen = screenAggregate.readScreen(index);
                                        currentButton = getTargetControlButton(currentScreen, ftbButtonWord);
                                        if (currentButton != null)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (runtimeScreen is Screen)
                                {
                                    Screen currentScreen = (Screen)runtimeScreen;
                                    currentButton = getTargetControlButton(currentScreen, ftbButtonWord);
                                }

                                if (currentButton != null)
                                {
                                    //Factory Setting Y Item
                                    if (currentTcIndex == tcIndex)
                                    {
                                        if (getControlButtonStatus(currentButton) == ControlButtonStatus.Selected)
                                            checkResult = TestCheckResult.OK;
                                        else
                                            checkResult = TestCheckResult.NG;

                                        showStatusCheckResult(checkResult, ControlButtonStatus.Selected.ToString(), currentButton, runtimeScreen, ftbButtonWord);
                                    }
                                    //Factory Setting N Item
                                    else
                                    {
                                        if (getControlButtonStatus(currentButton) == ControlButtonStatus.Invalid
                                            || getControlButtonStatus(currentButton) == ControlButtonStatus.Valid)
                                            checkResult = TestCheckResult.OK;
                                        else
                                            checkResult = TestCheckResult.NG;

                                        string standardStatus = ControlButtonStatus.Invalid.ToString() + "," + ControlButtonStatus.Valid.ToString();
                                        showStatusCheckResult(checkResult, standardStatus, currentButton, runtimeScreen, ftbButtonWord);
                                    }
                                }
                                else
                                {
                                    checkResult = TestCheckResult.NG;
                                }
                            }
                            TestRuntimeAggregate.setCheckResult(tcIndex, opinion, checkResult, currentLevelIndex);
                            StaticLog4NetLogger.reportLogger.Info("TC-" + tcIndex + " set result " + opinion + checkResult.ToString());

                        }
                        catch (FTBAutoTestException ex)
                        {
                            StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIndex + " check " + opinion + " failed by " + ex.Message);
                            StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                            System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                        }
                    }
                }
            }
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
            Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = TestRuntimeAggregate.getTransferButtonStatusResult(currentTcIndex, currentLevelIndex);
            if (null == buttonStatusResult || !buttonStatusResult.ContainsKey(button))
            {
                throw new FTBAutoTestException("Check button status error.");
            }
            else
            {
                return buttonStatusResult[button];
            }
        }

        private void showStatusCheckResult(TestCheckResult checkResult, string standardStatus, ControlButton currentButton, object runtimeScreen, string ftbButtonWord)
        {
            //target button is not existed in current screen, so that it can't do show operation 
            if (currentButton == null || false == StaticEnvironInfo.isOcrUsed())
                return;
            Engine.EngineOCR ocr = new Engine.EngineOCR();
            string imagePath = "";
            if (checkResult == TestCheckResult.OK || checkResult == TestCheckResult.NG)
            {
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

                if (runtimeScreen is Screen)
                {
                    imagePath = TestRuntimeAggregate.getTransferImagePath((Screen)runtimeScreen, currentTcIndex, currentLevelIndex);
                }
                else if (runtimeScreen is AbstractScreenAggregate)
                {
                    AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                    IIterator screenReadIterator;
                    screenReadIterator = screenAggregate.createReadIterator();
                    ControlButton button = null;
                    for (screenReadIterator.first(); !screenReadIterator.isDone(); screenReadIterator.next())
                    {
                        int index = screenReadIterator.currentItem();
                        Screen currentScreen = screenAggregate.readScreen(index);

                        button = getTargetControlButton(currentScreen, ftbButtonWord);
                        if (button != null)
                        {
                            imagePath = TestRuntimeAggregate.getTransferImagePath(currentScreen, currentTcIndex, currentLevelIndex);
                            break;
                        }
                    }
                }

                ocr.ShowButtonCheckResult(imagePath, standardStatusList, currentRectList, currentStatusList);
            }
        }

    }
}
