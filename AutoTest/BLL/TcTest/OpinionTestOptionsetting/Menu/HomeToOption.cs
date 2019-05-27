using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Machine;
using Tool.Command;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class HomeToOption : AbstractCmnTestHandler
    {
        const string opinionStr = "OptionSettingFromHomeChecker";

        public override void execute()
        {
            //set current Opinion Index
            TestRuntimeAggregate.setCurrentTCStatus(TestOneTCStatus.OptionChecking);
            TestRuntimeAggregate.setCurrentOpinionIndex(-1);
            for(int i = 0;i< TestRuntimeAggregate.getOpinionCount();i++)
            {
                if (opinionStr == TestRuntimeAggregate.getOpinionName(i))
                {
                    TestRuntimeAggregate.setCurrentOpinionIndex(i);
                    break;
                }
            }
            if (-1 == TestRuntimeAggregate.getCurrentOpinionIndex())
                throw new FTBAutoTestException("Set Current Opinion Error.");

            //move back to home screen
            StaticCommandExecutorList.get(CommandList.move_r).execute();

            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            
            int LevelCount = TestRuntimeAggregate.getLevelInfoListCount(currentTcIndex);
            if (LevelCount < 1)
            {
                throw new FTBAutoTestException("LevelCount is incorrect");
            } 

            //move to preOption Screen
            for (int levelIndex = 1; levelIndex < LevelCount - 1; levelIndex++)
            {
                moveToNextLevel(TestRuntimeAggregate.getLogButton(currentTcIndex, levelIndex),
                                TestRuntimeAggregate.getLogScreen(currentTcIndex, levelIndex));
                System.Threading.Thread.Sleep(500);
            }

            setOpinionInfo(currentTcIndex, LevelCount - 1);
            moveToNextLevel(TestRuntimeAggregate.getLogButton(currentTcIndex, LevelCount - 1),
                            TestRuntimeAggregate.getOpinionScreen(currentTcIndex, opinionStr, LevelCount - 1));
            setOpinionInfo(currentTcIndex, LevelCount);

            if (TestRuntimeAggregate.getSelectedOpinion().Contains("OptionSettingChecker")
                && (TestRuntimeAggregate.getSelectedOpinion().Contains("OptionSettingFromHomeChecker")))
            {
                checkDifferentScreen(currentTcIndex);
            }

            base.execute();
        }
        private void checkDifferentScreen(int currentTcIndex)
        {
            List<string> path = new List<string>();
            DAL.IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            int levelCount = treeMemory.getLevelCount(TestRuntimeAggregate.getCurrentTcIndex());
            string ftbButtonWord = treeMemory.getLevelButtonWord(levelCount, -1, TestRuntimeAggregate.getCurrentTcIndex());
            string GiveOptionRouteToRsp = treeMemory.getTcDir(TestRuntimeAggregate.getCurrentTcIndex());
            MatchCollection RspStrings = Regex.Matches(GiveOptionRouteToRsp, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in RspStrings)
                path.Add(match.ToString().Replace("//", "/"));
            if (ftbButtonWord != "")
            {
                path.RemoveAt(path.Count - 1);
            }
            List<int> selectedTcIndexs = DAL.TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes;
            int nowTcIndex = selectedTcIndexs.IndexOf(currentTcIndex);
            if ((nowTcIndex + 1) < selectedTcIndexs.Count)
            {
                int afterNowTcIndex = selectedTcIndexs[nowTcIndex + 1];
                List<string> afterPathList = new List<string>();
                afterPathList = GetPath(afterNowTcIndex);
                if (!(path.All(afterPathList.Contains) && path.Count == afterPathList.Count))
                {
                    setDefaultValue();
                }
            }
            else
            {
                setDefaultValue();
            }
        }
        private void setDefaultValue()
        {
            if (TestRuntimeAggregate.getTreeMemory().isLevelOption(TestRuntimeAggregate.getCurrentLevelIndex(), TestRuntimeAggregate.getCurrentTcIndex()) == true)
            {
                System.Threading.Thread.Sleep(300);
                Screen currentScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(currentScreen);
                AbstractScreenAggregate screenAggregate = null;
                if (currentScreen.isScrollable())
                {
                    screenAggregate = AbstractScreenAggregate.import(currentScreen);
                    Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                    while (!screenAggregate.isScreenContains(addScreen))
                    {
                        screenAggregate.appendScreen(addScreen);
                        screenAggregate.moveToNextScreen(addScreen);
                        addScreen = new Screen();
                        StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                    }
                }
                List<string> stringList = currentScreen.getIdentify().btnWordsVector;
                List<int> indexList = TestRuntimeAggregate.getTreeMemory().getOptionLevelBrotherButtonIndex(TestRuntimeAggregate.getCurrentTcIndex());
                string factoryStr = null; int defaultTcIndex = 0;
                foreach (int tcIndex in indexList)
                {
                    factoryStr = TestRuntimeAggregate.getTreeMemory().getFactorySetting(tcIndex);
                    if (factoryStr == "Y")
                    {
                        defaultTcIndex = tcIndex;
                        break;
                    }
                }
                string OptionDefaultValue = TestRuntimeAggregate.getTreeMemory().getOptionWords(defaultTcIndex);
                Screen ssscreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_f).execute(ssscreen);
                ElementString buttonStringElement = new ElementString(OptionDefaultValue);
                AbstractElement targetStringElement = ssscreen.findElement(buttonStringElement);
                if (targetStringElement == null)
                {
                    int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
                    int LevelCount = TestRuntimeAggregate.getLevelInfoListCount(currentTcIndex);
                    object screen = TestRuntimeAggregate.getOpinionScreen(currentTcIndex, opinionStr, LevelCount);
                    AbstractScreenAggregate screenDregate = (AbstractScreenAggregate)screen;
                    IIterator showIterator = screenDregate.createShowIterator();
                    for (showIterator.first(); !showIterator.isDone(); showIterator.next())
                    {
                        Screen screenLists = new Screen();
                        StaticCommandExecutorList.get(CommandList.list_f).execute(screenLists);
                        if (screenLists.findElement(buttonStringElement) != null)
                        {
                            List<AbstractElement> targetImageElementList = screenLists.getElementShipValueList(screenLists.findElement(buttonStringElement));
                            AbstractElement targetImageElement = targetImageElementList[0];
                            Position targetPos = targetImageElement.rect.getCenter();
                            StaticCommandExecutorList.get(CommandList.click_p).execute(targetPos);
                            break;
                        }
                    }
                }
                else
                {
                    List<AbstractElement> targetImageElementList = ssscreen.getElementShipValueList(targetStringElement);
                    AbstractElement targetImageElement = targetImageElementList[0];
                    Position targetPos = targetImageElement.rect.getCenter();
                    StaticCommandExecutorList.get(CommandList.click_p).execute(targetPos);
                }

                setMidePopUpScreen();
            }
        }
        private void moveToNextLevel(ControlButton targetButton, object screen)
        {
            if (targetButton == null || screen == null)
            {
                throw new FTBAutoTestException("moveToNextLevel failed");
            }

            Screen currentScreen = null;
            if (screen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)screen;
                IIterator screenShowIterator;
                screenShowIterator = screenAggregate.createShowIterator();
                screenShowIterator.first();
                while (!screenShowIterator.isDone())
                {
                    int index = screenShowIterator.currentItem();
                    currentScreen = screenAggregate.readScreen(index);
                    if (currentScreen.findButton(targetButton.getIdentify()) != null)
                    {
                        break;
                    }
                    screenShowIterator.next();
                }
            }
            else if (screen is Screen)
            {
                currentScreen = (Screen)screen;
            }

            StaticCurrentScreen.set(currentScreen);
            StaticCommandExecutorList.get(CommandList.click_b).execute(targetButton.getIdentify());
            setMidePopUpScreen();
        }
        private void setMidePopUpScreen()
        {
            Screen mideScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(mideScreen);
            List<AbstractElement> imageElementList = mideScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
            if (imageElementList != null)
            {
                AbstractCommandExecutor tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
                if (imageElementList.Count >= 3) { }
                else if (imageElementList.Count == 2)
                {
                    List<string> strList = new List<string>() { "OK", "No", "Cancel" };
                    List<AbstractElement> teraElementList = new List<AbstractElement>(imageElementList);
                    foreach (AbstractElement originImgElement in teraElementList)
                    {
                        List<AbstractElement> originStrElement = mideScreen.getElementShipValueList(originImgElement);
                        if (originStrElement == null)
                            continue;
                        if (originStrElement != null)
                        {
                            foreach (string str in strList)
                            {
                                if (String.Equals(str, ((ElementString)originStrElement[0]).str, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Position btnPos = imageElementList[1].rect.getCenter();
                                    tpClicker.execute(btnPos);
                                    System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (imageElementList.Count == 1)
                {
                    List<string> strList = new List<string>() { "OK", "No", "Cancel" };
                    List<AbstractElement> originStrElement = mideScreen.getElementShipValueList(imageElementList[0]);
                    foreach (string str in strList)
                    {
                        if (String.Equals(str, ((ElementString)originStrElement[0]).str, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Position btnPos = imageElementList[0].rect.getCenter();
                            tpClicker.execute(btnPos);
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    }
                }
                else if (imageElementList.Count == 0)
                {
                    System.Threading.Thread.Sleep(550);
                }
            }
        }
        private void setOpinionInfo(int tcIndex,int levelIndex)
        {
            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);

            AbstractScreenAggregate screenAggregate = null;
            if (currentScreen.isScrollable())
            {
                screenAggregate = AbstractScreenAggregate.import(currentScreen);
                Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                while (!screenAggregate.isScreenContains(addScreen))
                { 
                    screenAggregate.appendScreen(addScreen);
                    parseSingleScreen(addScreen, tcIndex, levelIndex);
                    screenAggregate.moveToNextScreen(addScreen);
                    addScreen = new Screen();
                    StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                }
            }
            else
            {
                parseSingleScreen(currentScreen, tcIndex, levelIndex);
            }
            if (screenAggregate == null)
            {
                TestRuntimeAggregate.setOpinionScreen(currentScreen, tcIndex, opinionStr,levelIndex);
            }
            else
            {
                TestRuntimeAggregate.setOpinionScreen(screenAggregate, tcIndex, opinionStr, levelIndex);
            }
        }
        private List<string> GetPath(int afterNowTcIndex)
        {
            List<string> path = new List<string>();
            DAL.IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            int levelCount = treeMemory.getLevelCount(afterNowTcIndex);
            string GiveOptionRouteToRsp = treeMemory.getTcDir(afterNowTcIndex);
            MatchCollection RspStrings = Regex.Matches(GiveOptionRouteToRsp, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in RspStrings)
            {
                path.Add(match.ToString().Replace("//", "/"));
            }
            if (treeMemory.getLevelButtonWord(levelCount, -1, afterNowTcIndex) != "")
            {
                path.RemoveAt(path.Count - 1);
            }
            return path;
        }
        private Bitmap GetSmall(Bitmap bm, double times)
        {
            int nowWidth = (int)(bm.Width * times);
            int nowHeight = (int)(bm.Height * times);
            Bitmap newbm = new Bitmap(nowWidth, nowHeight);//新建一个放大后大小的图片

            if (times >= 1 && times <= 1.1)
            {
                newbm = bm;
            }
            else
            {
                Graphics g = Graphics.FromImage(newbm);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(bm, new System.Drawing.Rectangle(0, 0, nowWidth, nowHeight),
                    new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
            return newbm;
        }
        private void parseSingleScreen(Screen screen, int tcIndex, int levelIndex)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //camera
            Engine.EngineCamera camera = new Engine.EngineCamera();
            //ocr
            Engine.EngineOCR ocr = new Engine.EngineOCR();

            //save image
            ScreenSocket asss = new ScreenSocket("127.0.0.1", "10010");
            string imagePath = StaticEnvironInfo.getScreenImageFileName();
            asss.send("GetScreen");
            byte[] imgBuffer = asss.read();
            System.IO.Stream imgStream = new System.IO.MemoryStream(imgBuffer);
            Bitmap bm = new Bitmap(imgStream);
            Bitmap btnew22 = GetSmall(bm, 2);
            btnew22.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            System.Threading.Thread.Sleep(3200);
            TestRuntimeAggregate.setOpinionImagePath(screen, imagePath, tcIndex, opinionStr, levelIndex);
           
            //ocr Result storage path
            ocr.setWorkSpacePath(StaticEnvironInfo.getOcrLogPath());
            //ocr analays
            List<ElementString> elementStringList = screen.getElementList(typeof(ElementString)).ConvertAll(e => (ElementString)e);
            List<string> stringList = ocr.analyzeWords(imagePath, elementStringList);
            Dictionary<ElementString, string> ocrResult = new Dictionary<ElementString, string>();
            for (int i = 0; i < elementStringList.Count; i++)
            {
                ocrResult.Add(elementStringList[i], stringList[i]);
            }
            TestRuntimeAggregate.addOpinionOcrResult(ocrResult,tcIndex,opinionStr,levelIndex);
            //image button status analays
            List<AbstractControl> abstractButton = screen.getControlList(typeof(ControlButton));
            if (null != abstractButton)
            {
                List<ControlButton> buttonList = abstractButton.ConvertAll(e => (ControlButton)e);
                List<ControlButtonStatus> buttonStatus = ocr.analaysButtonStatus(imagePath, buttonList);
                Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = new Dictionary<ControlButton, ControlButtonStatus>();
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonStatusResult.Add(buttonList[i], buttonStatus[i]);
                }
                TestRuntimeAggregate.addOpinionButtonStatusResult(buttonStatusResult, tcIndex, opinionStr, levelIndex);
            }
        }
    }
}
