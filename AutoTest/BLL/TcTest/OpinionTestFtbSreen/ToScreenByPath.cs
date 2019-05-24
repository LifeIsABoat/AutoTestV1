using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tool.DAL;
using Tool.Command;
using System.Web.Script.Serialization;

namespace Tool.BLL
{ 
    class ToScreenByPath : AbstractCmnTestHandler
    {
        const string opinionStr = "FtbScreenItemListChecker";
        public object runtimeScreen = new object();
        List<string> pathList;
        public ToScreenByPath()
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.screenMemory = TestRuntimeAggregate.getScreenMemory();
            this.pathList = new List<string>();
        }
        public override void execute()
        {
            TestRuntimeAggregate.setCurrentTCStatus(TestOneTCStatus.Transfering);
            TestRuntimeAggregate.setCurrentOpinionIndex(-1);
            for (int i = 0; i < TestRuntimeAggregate.getOpinionCount(); i++)
            {
                if (opinionStr == TestRuntimeAggregate.getOpinionName(i))
                {
                    TestRuntimeAggregate.setCurrentOpinionIndex(i);
                    break;
                }
            }
            if (-1 == TestRuntimeAggregate.getCurrentOpinionIndex())
                throw new FTBAutoTestException("Set Current Opinion Error.");

            //get json text of condition
            int currentIndex = TestRuntimeAggregate.getCurrentTcIndex();
            pathList = screenMemory.getScreenPath(currentIndex);

            string path = "";
            //move to Screen by Identify (path)
            runtimeScreen = null;
            //todo
            for (int levelIndex = 1; levelIndex <= pathList.Count; levelIndex++)
            {
                TestRuntimeAggregate.setCurrentLevelIndex(levelIndex);

                path = path + "/" + pathList[levelIndex-1].Replace("/", "//");
                path = path.TrimStart('/');

                if (1 != levelIndex)
                {
                    //get screen Identify
                    string screenId = treeMemory.getLevelButtonToScreenId(path);
                    //get screen title
                    string screenTitle = treeMemory.getLevelButtonToScreenTitle(path);

                    //set Identify List
                    ScreenIdentify screenIdentify = new ScreenIdentify(screenId, screenTitle);

                    //click button according to ScreenIdentify
                    if (runtimeScreen is Screen)
                    {
                        StaticCommandExecutorList.get(CommandList.click_b).execute(new ControlButtonIdentify(pathList[levelIndex-1], screenIdentify));
                    }
                    else if (runtimeScreen is AbstractScreenAggregate)
                    {
                        IIterator screenShowIterator;
                        AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)runtimeScreen;
                        screenShowIterator = screenAggregate.createShowIterator();
                        screenShowIterator.first();
                        while (!screenShowIterator.isDone())
                        {
                            try
                            {
                                StaticCommandExecutorList.get(CommandList.click_b).execute(new ControlButtonIdentify(pathList[levelIndex-1], screenIdentify));
                                break;
                            }
                            catch (FTBAutoTestException)
                            {
                                //Click button Fail isn't exception in this situation
                            }
                            screenShowIterator.next();
                        }
                        if (screenShowIterator.isDone())
                        {
                            throw new FTBAutoTestException("Click failed by invalid runtimeScreen.");
                        }
                    }
                }
                runtimeScreen = saveScreenToDictionary(currentIndex, levelIndex);
            }

            base.execute();
        }

        private object saveScreenToDictionary(int currentIndex, int level)
        {
            //get current raw screen
            Screen currentRawScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);

            //get runtime screen
            object runtimeScreen = TestRuntimeAggregate.getRuntimeScreen(currentRawScreen);
            if (null != runtimeScreen)
            {
                if (runtimeScreen is Screen)
                {
                    //check runtime screen
                    if (!currentRawScreen.EqualsByElementList((Screen)runtimeScreen))
                    {
                        runtimeScreen = null;
                    }
                    else
                    {
                        //set to curent screen
                        StaticCurrentScreen.set((Screen)runtimeScreen);
                        parseSingleScreen((Screen)runtimeScreen);
                    }
                }
                else if (runtimeScreen is AbstractScreenAggregate)
                {
                    //check runtime screen aggregate
                    IIterator readIterator = ((AbstractScreenAggregate)runtimeScreen).createReadIterator();
                    for (readIterator.first(); !readIterator.isDone(); readIterator.next())
                    {
                        //check current screen page
                        Screen screen = ((AbstractScreenAggregate)runtimeScreen).readScreen(readIterator.currentItem());
                        if (!screen.EqualsByElementList(currentRawScreen))
                        {
                            //back to top of list
                            if (TestRuntimeAggregate.getCurrentLevelIndex() > 1)
                            {
                                ((AbstractScreenAggregate)runtimeScreen).toFirstScreen(screen);
                            }
                            //set to null for fix again
                            runtimeScreen = null;
                            System.IO.Directory.Delete(StaticEnvironInfo.getScreenImagePath(), true);
                            TestRuntimeAggregate.clearTransferImagePathDict();
                            TestRuntimeAggregate.clearCameraImage();
                            break;
                        }
                        //set to current screen
                        StaticCurrentScreen.set(screen);
                        parseSingleScreen(screen);
                        //move to list screen next page
                        if (readIterator.currentItem() + 1 < ((AbstractScreenAggregate)runtimeScreen).getCount())
                        {
                            ((AbstractScreenAggregate)runtimeScreen).showScreen(readIterator.currentItem() + 1);
                            //update current raw screen
                            currentRawScreen = new Screen();
                            StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
                        }
                    }
                }
            }

            if (null == runtimeScreen)
            {
                //save screen
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
                        parseSingleScreen(addScreen);
                        screenAggregate.moveToNextScreen(addScreen);
                        addScreen = new Screen();
                        StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                    }
                }
                else
                {
                    parseSingleScreen(currentScreen);
                }
                if (null == screenAggregate)
                {
                    TestRuntimeAggregate.setLogScreen(currentScreen, currentIndex, level);
                    return currentScreen;
                }
                else
                {
                    TestRuntimeAggregate.setLogScreen(screenAggregate, currentIndex, level);
                    return screenAggregate;
                }
            }
            else
            {  
                TestRuntimeAggregate.setLogScreen(runtimeScreen, currentIndex, level);
                return runtimeScreen;
            }
        }

        private void parseSingleScreen(Screen screen)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //camera
            Engine.EngineCamera camera = new Engine.EngineCamera();
            //ocr
            Engine.EngineOCR ocr = new Engine.EngineOCR();

            //save image
            string imagePath = StaticEnvironInfo.getScreenImageFileName();
            camera.capture(imagePath);
            TestRuntimeAggregate.addTransferImagePathDict(screen, imagePath);

            int levelIndex = TestRuntimeAggregate.getCurrentLevelIndex();
            if (levelIndex == pathList.Count)
            {
                //ocr Result storage path
                string logPath = StaticEnvironInfo.getOcrLogPath();
                ocr.setWorkSpacePath(logPath);
                //ocr analays
                List<ElementString> elementStringList = screen.getElementList(typeof(ElementString)).ConvertAll(e => (ElementString)e);
                List<string> stringList = ocr.analyzeWords(imagePath, elementStringList);
                Dictionary<ElementString, string> ocrResult = new Dictionary<ElementString, string>();
                for (int i = 0; i < elementStringList.Count; i++)
                {
                    ocrResult.Add(elementStringList[i], stringList[i]);
                }
                TestRuntimeAggregate.addTransferOcrResult(ocrResult);
            }
        }
    }
}
