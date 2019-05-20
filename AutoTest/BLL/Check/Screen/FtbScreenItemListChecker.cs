using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL.Check
{
    class FtbScreenItemListChecker : AbstractChecker
    {
        public string screenPath = "";
        IScreenCommonAPI screenMemory;
        IIterator ScreenIterator;
        TestCheckResult checkResult;
        int currentTcIndex, currentLevelIndex;
        public FtbScreenItemListChecker()
        {
            this.screenMemory = TestRuntimeAggregate.getScreenMemory();
            this.ScreenIterator = screenMemory.createScreenIterator();
            this.checkResult = TestCheckResult.NA;
        }

        public override void check()
        {
            opinion = this.GetType().Name;
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check Start");
            for (ScreenIterator.first(); !ScreenIterator.isDone(); ScreenIterator.next())
            {
                try
                {
                    currentTcIndex = ScreenIterator.currentItem();
                    List<string> screenPathList = screenMemory.getScreenPath(currentTcIndex);
                    List<string> ftbButtonWord = screenMemory.getScreenWords(currentTcIndex);

                    currentLevelIndex = screenPathList.Count;
                    //get Dictionary, include path and screen
                    List<string> pathList = new List<string>();
                    for (int index = 0; index < currentLevelIndex; index++)
                    {
                        string words = screenPathList[index].Replace("/", "//");
                        pathList.Add(words);
                    }
                    screenPath = string.Join("/", pathList);

                    object runtimeScreen = TestRuntimeAggregate.getLogScreen(currentTcIndex, currentLevelIndex);

                    if (isBlackList(ref checkResult, currentTcIndex, currentLevelIndex) == false)
                    {
                        //get check result
                        checkResult = checkScreen(ftbButtonWord, runtimeScreen);
                    }

                    //Put the results in the Dictionary
                    TestRuntimeAggregate.setCheckResult(currentTcIndex,
                                                        opinion,
                                                        checkResult,
                                                        currentLevelIndex
                                                        );
                    //report log
                    StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion +
                                                            "TC-" + currentTcIndex +
                                                            "level-" + currentLevelIndex +
                                                            "result-" + checkResult.ToString());
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("Screen-" + currentTcIndex + " check " + opinion + " failed by " + ex.Message);
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                }
            }
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check End");

        }

        private TestCheckResult checkScreen(List<string> ftbButtonWords, object runtimeScreen)
        {
            if (ftbButtonWords == null)
                throw new FTBAutoTestException("Check screen failed by invalid param.");
            if (runtimeScreen == null)
                return TestCheckResult.NG;

            List<string> currentButtonWords = new List<string>();
            Screen currentScreen = null;
            List<AbstractControl> currentButtonList = new List<BLL.AbstractControl>();
            if (runtimeScreen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                IIterator screenReadIterator;
                screenReadIterator = screenAggregate.createReadIterator();
                for (screenReadIterator.first(); !screenReadIterator.isDone(); screenReadIterator.next())
                {
                    currentButtonList.Clear();
                    int index = screenReadIterator.currentItem();
                    currentScreen = screenAggregate.readScreen(index);
                    currentButtonList = currentScreen.getControlList(typeof(ControlButton));
                    List<string> currentWords = getCurrentButtonWords(currentButtonList);
                    if (currentButtonList == null)
                    {
                        break;
                    }
                    currentButtonWords.AddRange(currentWords);
                     
                    //Screen String Check Result Show
                    showStringCheckResult(ftbButtonWords, currentButtonList, currentScreen, index);
                }
            }
            else if (runtimeScreen is Screen)
            {
                currentScreen = (Screen)runtimeScreen;
                currentButtonList = currentScreen.getControlList(typeof(ControlButton));
                currentButtonWords = getCurrentButtonWords(currentButtonList);

                //Screen String Check Result Show
                showStringCheckResult(ftbButtonWords, currentButtonList, currentScreen, 0);
            }

            if (null != currentButtonWords)
            {
                //Set currentWords to currentButtonWords
                TestRuntimeAggregate.setScreenCheckCurrentWords(currentTcIndex, currentLevelIndex, currentButtonWords);
            }

            return compareScreenItem(ftbButtonWords, currentButtonWords);
        }

        private List<string> getCurrentButtonWords(List<AbstractControl> buttonList)
        {
            if (buttonList == null)
                return null;

            buttonList = sort(buttonList);

            List<string> words = new List<string>();
            foreach (AbstractControl button in buttonList)
            {
                if (((ControlButton)button).stringList.Count > 0)
                {
                    string word = getStringElementStr(((ControlButton)button).stringList[0]);
                    words.Add(word);
                }
            }

            return words;
        }

        private string getStringElementStr(ElementString eleStr)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
            {
                //check by log
                return eleStr.str;
            }
            //check by camera
            Dictionary<ElementString, string> ocrResult = TestRuntimeAggregate.getTransferOcrResult(currentTcIndex, currentLevelIndex);
            if (ocrResult != null && ocrResult.ContainsKey(eleStr))
            {
                return ocrResult[eleStr];
            }
            else
            {
                throw new FTBAutoTestException("String check Error");
            }
        }

        private TestCheckResult compareScreenItem(List<string> ftbButtonWord, List<string> currentWord)
        {
            if (null == ftbButtonWord || null == currentWord)
            {
                //throw new FTBAutoTestException("Compare ScreenItem failed by invalid param.");
                return TestCheckResult.NG; 
            }
            int screenLines = screenMemory.getScreenLines();
            if (currentWord.Count > screenLines)
                if(0 != currentWord.Count % screenLines)
                    return TestCheckResult.NG;

            if (currentWord.Count != ftbButtonWord.Count)
            {
                return TestCheckResult.NG;
            }
            else
            {
                for (int i = 0; i < ftbButtonWord.Count; i++)
                {
                    if (currentWord[i] != ftbButtonWord[i])
                    {
                        return TestCheckResult.NG;
                    }
                }
            }
            return TestCheckResult.OK;
        }

        private List<AbstractControl> sort(List<AbstractControl> ButtonList)
        {
            for (int j = 0; j < ButtonList.Count - 1; j++)
            {
                for (int i = 0; i < ButtonList.Count - 1 - j; i++)
                {
                    if (ButtonList[i].rect.y > ButtonList[i + 1].rect.y)
                    {
                        AbstractControl button = ButtonList[i];
                        ButtonList[i] = ButtonList[i + 1];
                        ButtonList[i + 1] = button;
                    }
                    else if (ButtonList[i].rect.y == ButtonList[i + 1].rect.y)
                    {
                        if (ButtonList[i].rect.x > ButtonList[i + 1].rect.x)
                        {
                            AbstractControl button = ButtonList[i];
                            ButtonList[i] = ButtonList[i + 1];
                            ButtonList[i + 1] = button;
                        }
                    }
                }
            }
            return ButtonList;
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

        private void showStringCheckResult(List<string> ftbButtonWords, List<AbstractControl> currentButtonList, Screen currentScreen,int scrIndex)
        {
            //if ocr is not used, can't get image, so that it can't do show operation 
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //target button is not existed in current screen, so that it can't do show operation 
            if (ftbButtonWords == null || currentButtonList == null || currentScreen == null)
                return;
            if(scrIndex < 0)
                throw new FTBAutoTestException("Get screen falied.");

            //lines of screen
            int lines = screenMemory.getScreenLines();
            if (lines <= 0)
                throw new FTBAutoTestException("Get screen lines  falied.");

            List<string> ftbButtonWordList = new List<string>();
            //The current LEVEL has more than one screen
            if (ftbButtonWords.Count > lines)
            {
                if (ftbButtonWords.Count % lines != 0)
                    throw new FTBAutoTestException("ftbButtonWords get falied.");
                for (int ftbIndex = 0 + scrIndex * lines; ftbIndex < (scrIndex+1) * lines; ftbIndex++)
                {
                    if (ftbButtonWords.Count <= ftbIndex)
                    {
                        ftbButtonWordList.Add("NotFound");
                    }
                    else
                    {
                        ftbButtonWordList.Add(ftbButtonWords[ftbIndex]);
                    }
                }
            }
            //The current LEVEL has a screen
            else
            {
                if (ftbButtonWords.Count == 0)
                    throw new FTBAutoTestException("ftbButtonWords get falied.");

                //ftbButtonWordList = ftbButtonWords;//todo
                for (int ftbIndex = 0; ftbIndex < ftbButtonWords.Count; ftbIndex++)
                {
                    ftbButtonWordList.Add(ftbButtonWords[ftbIndex]);
                }

                //The actual screen is not one
                if (scrIndex > 0)
                {
                    ftbButtonWordList.Clear();
                    for (int ftbIndex = 0; ftbIndex < lines; ftbIndex++)
                    {
                        ftbButtonWordList.Add("NotFound");
                    }
                }
            }

            //prepair current button word list
            List<string> currentButtonWordList = getCurrentButtonWords(currentButtonList);

            //prepair current button rect list
            ControlButton currentButton = null;
            List<Rectangle> currentRectList = new List<Rectangle>();
            for (int btnIndex=0; btnIndex < currentButtonList.Count; btnIndex++)
            {
                currentButton = (ControlButton)currentButtonList[btnIndex];
                if (currentButton.stringList.Count > 0)
                {
                    currentRectList.Add(currentButton.stringList[0].rect);
                }
                else
                {
                    currentRectList.Add(currentButton.rect);
                }
            }

            //fix param when list not count unequals.
            if (ftbButtonWordList.Count > currentButtonWordList.Count)
            {
                for (int curIndex = currentButtonWordList.Count; curIndex < ftbButtonWordList.Count; curIndex++)
                {
                    currentButtonWordList.Add("NotFound");
                    currentRectList.Add(new Rectangle(0, 0, 1, 1));
                }
            }
            else if (ftbButtonWordList.Count < currentButtonWordList.Count)
            {
                for (int ftbIndex = ftbButtonWordList.Count; ftbIndex < currentButtonWordList.Count; ftbIndex++)
                {
                    ftbButtonWordList.Add("NotFound");
                }
            }

            //Get image path
            string imagePath = TestRuntimeAggregate.getTransferImagePath(currentScreen, currentTcIndex, currentLevelIndex);
            try
            {
                Engine.EngineOCR ocr = new Engine.EngineOCR();
                ocr.ShowStringCheckResult(imagePath, ftbButtonWordList, currentRectList, currentButtonWordList);
            }
            catch (FTBAutoTestException ex)
            {
                StaticLog4NetLogger.reportLogger.Warn("TC-" + currentTcIndex + " check " + opinion + " failed by " + ex.Message);
                StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
            }
        }

        public override bool isBlackList(ref TestCheckResult checkResult, int tcIndex = -1, int levelIndex = -1)
        {
            OpinionRunBlackListInfo black = TestRuntimeAggregate.getOpinionBlackList(base.opinion);
            if (black == null)
            {
                return false;
            }
            RunBlackList NABlackList = black.NABlackList;
            RunBlackList NTBlackList = black.NTBlackList;
            if (NTBlackList != null && NTBlackList.blackList != null && NTBlackList.blackList.Contains(screenPath))
            {
                checkResult = TestCheckResult.NT;
                return true;
            }
            else if (NTBlackList != null && NTBlackList.regulations != null)
            {
                List<string> ftbButtonWord = screenMemory.getScreenWords(tcIndex);
                for (int ftbIndex = 0; ftbIndex < ftbButtonWord.Count; ftbIndex++)
                {
                    foreach (string pattren in NTBlackList.regulations)
                    {
                        if (Regex.IsMatch(ftbButtonWord[ftbIndex], pattren, RegexOptions.IgnoreCase))
                        {
                            checkResult = TestCheckResult.NT;
                            return true;
                        }
                    }
                }
            }
            else if (NABlackList != null && NABlackList.blackList != null && NABlackList.blackList.Contains(screenPath))
            {
                checkResult = TestCheckResult.NA;
                return true;
            }
            else if (NABlackList != null && NABlackList.regulations != null)
            {
                List<string> ftbButtonWord = screenMemory.getScreenWords(tcIndex);
                if (ftbButtonWord.Count == 1)
                {
                    foreach (string pattren in NABlackList.regulations)
                    {
                        if (Regex.IsMatch(ftbButtonWord[0], pattren, RegexOptions.IgnoreCase))
                        {
                            checkResult = TestCheckResult.NA;
                            return true;
                        }
                    }
                }
            }          

            return false;
        }

    }
}
