using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class FtbStringChecker: AbstractChecker
    {
        //private IFTBCommonAPI treeMemory;
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
                    try
                    {
                        TestCheckResult checkResult = TestCheckResult.NG;
                        if (isBlackList(ref checkResult) == false)
                        {
                            ControlButton ftbButton = loadFTBButtonControl(currentLevelIndex);

                            ControlButton currentButton = TestRuntimeAggregate.getLogButton(currentTcIndex, currentLevelIndex);
                            object runtimeScreen = TestRuntimeAggregate.getLogScreen(currentTcIndex, currentLevelIndex);
                            checkResult = checkButtonAndOpinion(ftbButton, currentButton, runtimeScreen);

                            showStringCheckResult(checkResult, ftbButton, currentButton, runtimeScreen);

                        }
                        TestRuntimeAggregate.setCheckResult(currentTcIndex, opinion, checkResult, currentLevelIndex);
                        StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " set level " + currentLevelIndex + " " + opinion + checkResult.ToString());
                    }
                    catch (FTBAutoTestException ex)
                    {
                        StaticLog4NetLogger.reportLogger.Warn("TC-" + currentTcIndex + " check " + opinion + " failed by " + ex.Message);
                        StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                }
            }
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check End");
        }
        private ControlButton loadFTBButtonControl(int levelIndex,
                                                     int btnIndex = -1, int tcIndex = -1)
        {
            ControlButton controlButton = new ControlButton();
            //get Button Own Info
            //  button Words String
            string buttonWordsStr = treeMemory.getLevelButtonWord(levelIndex,
                                                                  btnIndex, tcIndex);
            //Button Words Can't be Null in current ftb format
            if (null == buttonWordsStr)
                return null;
            ElementString buttonWordsElement = new ElementString(buttonWordsStr);
            controlButton.stringList.Add(buttonWordsElement);
            //  button status show
            controlButton.statusShow = ControlButtonStatus.Valid;

            //get toScreen Info
            Screen toScreen = null;
            //  toScreen Id
            string toScreenId = null;
            toScreenId = treeMemory.getLevelButtonToScreenId(levelIndex, btnIndex, tcIndex);
            if (null != toScreenId && "" != toScreenId)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                toScreen.setIdentifyScreenId(toScreenId);
            }

            //  toScreen Title
            string toScreenTitleStr = treeMemory.getLevelButtonToScreenTitle(levelIndex,
                                                                         btnIndex, tcIndex);
            if (null != toScreenTitleStr && "" != toScreenId)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                ElementString toScreenTitleElement = new ElementString(toScreenTitleStr);
                toScreen.addControl(new ControlTitle(toScreenTitleElement));
                toScreen.addElement(toScreenTitleElement);
            }

            //  toScreen elementStringList
            List<string> buttonWordsList = null;
            buttonWordsList = treeMemory.getLevelButtonWords(levelIndex + 1, tcIndex);
            if (null != buttonWordsList)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                foreach (string buttonWords in buttonWordsList)
                {
                    if ("" != buttonWords)
                    {
                        ElementString eleStr = new ElementString(buttonWords);
                        toScreen.addElement(eleStr);
                    }
                }
            }
            controlButton.toScreen = toScreen;
            return controlButton;
        }
        private TestCheckResult checkButtonAndOpinion(ControlButton ftbButton, ControlButton currentButton,object runtimeScreen)
        {
            string ftbButtonStr = null;
            string currentButtonStr = null;

            TestCheckResult result = TestCheckResult.NG;

            if (ftbButton.stringList.Count > 0)
            {
                ftbButtonStr = ftbButton.stringList[0].str;
            }

            if (null != currentButton)
            {
                if (currentButton.stringList.Count > 0)
                {
                    currentButtonStr = getStringElementStr(currentButton.stringList[0]);
                }

                if (StaticEnvironInfo.isIgnoreCase() == false)
                {
                    //it won't be null == null by ye san
                    if (ftbButtonStr == currentButtonStr)
                    {
                        result = TestCheckResult.OK;
                    }
                }
                else
                {
                    if (ftbButtonStr.ToLower() == currentButtonStr.ToLower())
                    {
                        result = TestCheckResult.OK;
                    }
                }
            }
            if (result != TestCheckResult.OK && ftbButtonStr != null)
            {
                if (ftbButtonStr == "")
                {
                    result = TestCheckResult.NA;
                }
                //todo to delete
                else if (ftbButtonStr == "[Basic Functions]" || ftbButtonStr == "[>:Quick Copy]")
                {
                    result = TestCheckResult.NA;
                }
                //else if (Regex.IsMatch(ftbButtonStr, "^Move to ", RegexOptions.IgnoreCase))
                //{
                //    //string pat = @"[^move to ""](.+)[^""]";
                //    //MatchCollection mt = Regex.Matches(ftbButtonStr, pat, RegexOptions.IgnoreCase);
                //    //if (mt.Count > 0)
                //    //{
                //    //    string moveToStr = mt[0].ToString();
                //    //}
                //    result = TestCheckResult.NA;
                //}
                //else if (Regex.IsMatch(ftbButtonStr, "^(BRN)?Manual:", RegexOptions.IgnoreCase))
                //{
                //    //string pat = @"[^(BRN)?Manual:](.+)";
                //    //string pat = @"(?<=(BRN)?Manua:).+";
                //    result = TestCheckResult.NA;
                //}
                else if (Regex.IsMatch(ftbButtonStr, "^DispOnly:", RegexOptions.IgnoreCase))
                {
                    string pat = @"(?<=DispOnly:).+";
                    Match mt = Regex.Match(ftbButtonStr, pat, RegexOptions.IgnoreCase);
                    result = checkLabel(mt.ToString(), runtimeScreen);
                }
                //else if (Regex.IsMatch(ftbButtonStr, @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase))
                //{
                //    //string pat = @"\[([^\]])*\]";
                //    result = TestCheckResult.NA;
                //}
                //else if (Regex.IsMatch(ftbButtonStr, @"^\[?Select:", RegexOptions.IgnoreCase))
                //{
                //    result = TestCheckResult.NA;
                //}
                //else if (Regex.IsMatch(ftbButtonStr, @"^\[?Direct:", RegexOptions.IgnoreCase))
                //{
                //    result = TestCheckResult.NA;
                //}
            }
            return result;
        }

        private TestCheckResult checkLabel(string dispInfo, object runtimeScreen)
        {
            string limit = Regex.Match(dispInfo, @"(?<=Limit:)\d*", RegexOptions.IgnoreCase).ToString();
            string charaset = Regex.Match(dispInfo, @"(?<=Charaset:)\w*", RegexOptions.IgnoreCase).ToString();
            int len = Convert.ToInt32(limit);

            if (runtimeScreen is Screen)
            {
                List<AbstractControl> labelList = ((Screen)runtimeScreen).getControlList(typeof(ControlLabel));
                foreach (AbstractControl label in labelList)
                {
                    string labelstr = getStringElementStr(((ControlLabel)label).str);
                    if (labelstr.Length <= len && charaset.ToUpper() == "NUM")
                    {
                        Int32 tmp = new Int32();
                        if (true == Int32.TryParse(labelstr, out tmp))
                            return TestCheckResult.OK;
                    }
                    else
                    {
                        return TestCheckResult.NA;
                    }
                }
            }
            return TestCheckResult.NG;
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

        private void showStringCheckResult(TestCheckResult checkResult, ControlButton ftbButton, ControlButton currentButton, object runtimeScreen)
        {
            //if ocr is not used, can't get image, so that it can't do show operation 
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //target button is not existed in current screen, so that it can't do show operation 
            if (runtimeScreen == null)
                return;
            Engine.EngineOCR ocr = new Engine.EngineOCR();
            string imagePath = "";
            if (checkResult == TestCheckResult.OK || checkResult == TestCheckResult.NG)
            {
                //structure standard wordsList
                List<string> ftbButtonWordList = new List<string>();
                string ftbButtonWord = "";

                if (ftbButton == null || ftbButton.stringList.Count == 0 || ftbButton.stringList[0].str == "")
                    throw new FTBAutoTestException("Show StringCheck Result failed by ftbWord null.");

                ftbButtonWord = ftbButton.stringList[0].str;
                ftbButtonWordList.Add(ftbButtonWord);

                //structure current words rectList and wordsList
                List<Rectangle> currentRectList = new List<Rectangle>();
                List<string> currentWordList = new List<string>();
                Rectangle currentRect = null;
                string currentWord = "NotFound";

                if (currentButton == null)
                {
                    currentRect = new Rectangle(0, 0, 1, 1);
                }
                else
                {
                    if (currentButton.stringList.Count == 0)
                    {
                        currentRect = currentButton.rect;
                    }
                    else
                    {
                        currentWord = getStringElementStr(currentButton.stringList[0]);
                        currentRect = currentButton.stringList[0].rect;
                    }
                }
                currentWordList.Add(currentWord);
                currentRectList.Add(currentRect);

                //get image path
                if (runtimeScreen is Screen)
                {
                    imagePath = TestRuntimeAggregate.getTransferImagePath((Screen)runtimeScreen, currentTcIndex, currentLevelIndex);
                }
                else if (runtimeScreen is AbstractScreenAggregate)
                {
                    AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                    IIterator screenReadIterator;
                    screenReadIterator = screenAggregate.createReadIterator();
                    ControlButton tmpButton = null;
                    for (screenReadIterator.first(); !screenReadIterator.isDone(); screenReadIterator.next())
                    {
                        int index = screenReadIterator.currentItem();
                        Screen currentScreen = screenAggregate.readScreen(index);

                        tmpButton = getTargetControlButton(currentScreen, ftbButtonWord);
                        if (tmpButton != null)
                        {
                            imagePath = TestRuntimeAggregate.getTransferImagePath(currentScreen, currentTcIndex, currentLevelIndex);
                            break;
                        }
                    }
                    if (imagePath == "")
                    {
                        imagePath = TestRuntimeAggregate.getTransferImagePath(((AbstractScreenAggregate)runtimeScreen).readScreen(0), currentTcIndex, currentLevelIndex);
                    }
                }

                ocr.ShowStringCheckResult(imagePath, ftbButtonWordList, currentRectList, currentWordList);
            }
        }
    }

}
