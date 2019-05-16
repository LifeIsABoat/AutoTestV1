using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class OptionSettingChecker : AbstractChecker
    {
        int currentTcIndex, currentLevelIndex;
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            IIterator levelIterator = treeMemory.createLevelIterator();

            opinion = this.GetType().Name;
            string ftbButtonWord = null;
            string preFtbButtonWord = null;
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                //get standard words
                currentTcIndex = tcIterator.currentItem();
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    currentLevelIndex = levelIterator.currentItem();
                    if (currentLevelIndex < treeMemory.getLevelCount(currentTcIndex) - 1)
                    {
                        continue;
                    }
                    if (treeMemory.isLevelOption(currentLevelIndex) == false)
                    {
                        //previous level button words
                        preFtbButtonWord = treeMemory.getLevelButtonWord(currentLevelIndex);
                    }
                    else
                    {
                        //option value
                        ftbButtonWord = treeMemory.getLevelButtonWord(currentLevelIndex);
                    }
                }

                //do check operation
                try
                {
                    TestCheckResult preOptionCheckResult = TestCheckResult.NG;
                    TestCheckResult optionCheckResult = TestCheckResult.NG;
                    if (isBlackList(ref preOptionCheckResult, currentTcIndex, currentLevelIndex) == true)
                    {
                        //preOptionCheckResult = TestCheckResult.NA;
                        optionCheckResult = preOptionCheckResult;
                        TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, preOptionCheckResult, currentLevelIndex - 1);
                        TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, optionCheckResult, currentLevelIndex);
                    }
                    else
                    {
                        //check previous level screen option value
                        currentLevelIndex--;
                        object preRuntimeScreen = TestRuntimeAggregate.getOpinionScreen(currentTcIndex, opinion, currentLevelIndex);
                        if (null != preRuntimeScreen)
                        {
                            bool result = checkPreLevelSetting(ftbButtonWord, preRuntimeScreen, preFtbButtonWord);
                            preOptionCheckResult = (result == true) ? TestCheckResult.OK : TestCheckResult.NG;
                        }
                        TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, preOptionCheckResult, currentLevelIndex);

                        //check 
                        currentLevelIndex++;
                        object runtimeScreen = TestRuntimeAggregate.getOpinionScreen(currentTcIndex, opinion, currentLevelIndex);
                        if (null != runtimeScreen)
                        {
                            bool result = checkOptionSetting(ftbButtonWord, runtimeScreen);
                            optionCheckResult = (result == true) ? TestCheckResult.OK : TestCheckResult.NG;
                        }
                        TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, optionCheckResult, currentLevelIndex);
                    }
                    StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " set result " + opinion + preOptionCheckResult.ToString() + optionCheckResult.ToString());
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIterator.currentItem() + "check " + opinion + " failed by" + ex.Message);
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                }
            }
        }

        //check previous level screen option value
        private bool checkPreLevelSetting(string ftbButtonWord, object runtimeScreen, string preFtbButtonWord)
        {
            bool checkResult = false;
            //todo
            if (ftbButtonWord == null || runtimeScreen == null || preFtbButtonWord == null)
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
                screenReadIterator.first();
                while (!screenReadIterator.isDone())
                {
                    Screen tmpScreen = screenAggregate.readScreen(screenReadIterator.currentItem());
                    currentButton = getTargetControlButton(tmpScreen, preFtbButtonWord);
                    if (currentButton != null)
                    {
                        currentScreen = tmpScreen;
                        break;
                    }
                    screenReadIterator.next();
                }
            }
            else if (runtimeScreen is Screen)
            {
                currentScreen = (Screen)runtimeScreen;
                currentButton = getTargetControlButton(currentScreen, preFtbButtonWord);
            }

            if (currentButton != null && currentButton.stringList.Count > 1)
            {
                if (getStringElementStr(currentButton.stringList[1]) == ftbButtonWord)
                    checkResult = true;
            }
            else
            {
                checkResult = false;
            }

            showStringCheckResult(ftbButtonWord, currentButton, currentScreen);

            return checkResult;
        }

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
                if (getControlButtonStatus(currentButton) == ControlButtonStatus.Selected)
                    checkResult = true;
            }
            else
            {
                checkResult = false;
            }

            showStatusCheckResult(ControlButtonStatus.Selected.ToString(), currentButton, currentScreen);

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
            Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = TestRuntimeAggregate.getOpinionButtonStatusResult(currentTcIndex, GetType().Name, currentLevelIndex);
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
            Dictionary<ElementString, string> ocrResult = TestRuntimeAggregate.getOpinionOcrResult(currentTcIndex, GetType().Name, currentLevelIndex);
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

            string imagePath = TestRuntimeAggregate.getOpinionImagePath(currentTcIndex, opinion, currentLevelIndex, currentScreen);

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

            string currentWord = "NotFound";
            Rectangle currentRect = null;

            if (currentButton.stringList.Count < 2)
            {
                currentRect = currentButton.rect;
            }
            else
            {
                currentWord = getStringElementStr(currentButton.stringList[1]);
                currentRect = currentButton.stringList[1].rect;  
            }
            currentWordList.Add(currentWord);
            currentRectList.Add(currentRect);

            string imagePath = TestRuntimeAggregate.getOpinionImagePath(currentTcIndex, opinion, currentLevelIndex, currentScreen);

            Engine.EngineOCR ocr = new Engine.EngineOCR();
            ocr.ShowStringCheckResult(imagePath, standardStatusList, currentRectList, currentWordList);
        }
    }
}
