using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Tool.Machine;
using Tool.Command;
using Tool.DAL;
using Tool.Parser;
using Tool.Engine;
using Tool.UI;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tool.BLL
{
    class HeartBeatThread
    {
        private AutoResetEvent exitEvent;
        private Thread thread;
        private int waitTime;
        private bool isAlive;
        
        public HeartBeatThread(int waitTime)
        {
            exitEvent = new AutoResetEvent(false);
            this.waitTime = waitTime;
            thread = new Thread(process);
        }
        public void start()
        {
            isAlive = true;
            thread.Start();
        }
        public void stop()
        {
            exitEvent.Set();
            while (isAlive)
            {
                Thread.Sleep(100);
            }
        }
        public void abort()
        {
            exitEvent.Set();
            int cnt = 0;
            while (isAlive && cnt < 3)
            {
                cnt++;
                Thread.Sleep(100);
            }
            thread.Abort();
        }

        private void process()
        {
            int sleepCnt = 0;
            while (true)
            {
                //sleepCnt = 0
                if (0 == sleepCnt)
                {
                    try
                    {
                        StaticCommandExecutorList.get(CommandList.click_p).execute(new Position(0, 0));
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("HeartBeat" + ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                }
                // 0 < sleepCnt < waitTime/100
                else
                {
                    if (exitEvent.WaitOne(100))
                        break;
                }

                //sleepCnt control, loop into(0,sleepCnt)
                if (sleepCnt >= waitTime / 100)
                    sleepCnt = 0;
                else
                    sleepCnt++;
            }
            isAlive = false;
        }
    }
    class TotalManageRun
    {
        List<AbstractTcManager> tcMangerList;
        string portName;
        string[] testFormArguments;
        ModelInfo modelInfo = new ModelInfo();
        public bool isReportCreating = false;
        AbstractMachineMFCTP machineMFCTP;
        AbstractComponentMachineIO io;
        //private static AbstractCmnTestHandler checkHandler = null;

        //public static void setCheckHandler(AbstractCmnTestHandler handler)
        //{
        //    TotalManageRun.checkHandler = handler;
        //}

        public TotalManageRun(string port, string[] param)
        {
            this.portName = port;
            this.testFormArguments = param;
            tcMangerList = new List<AbstractTcManager>();
            TestRuntimeAggregate.setFormArguments(this.testFormArguments);
            //perpair tcRunManager
            List<string> tcRunManagerNameList = TestRuntimeAggregate.getSelectedTcRunManager();
            foreach (string tcRunManagerName in tcRunManagerNameList)
            {
                Type tcRunManagerType = Type.GetType("FTBAutoTestTool.BLL." + tcRunManagerName);
                if (null == tcRunManagerType)
                {
                    throw new FTBAutoTestException("Create TcRunManager failed by invalid name(" + tcRunManagerName + ").");
                }
                try
                {
                    tcMangerList.Add((AbstractTcManager)Activator.CreateInstance(tcRunManagerType));
                }
                catch (System.ServiceModel.EndpointNotFoundException ex) { throw ex; }
                catch (System.Reflection.TargetInvocationException ex) { throw ex; }
            }
        }

        public void run()
        {
            isReportCreating = false;
            int tcRunOverCount = 0;
            HeartBeatThread heartBeat = new HeartBeatThread(10000);
            try
            {
                //do tcRunInit/writeLog/OcrInit
                allParamInit();
                heartBeat.start();
                //execute tcRunManager
                //add TcStartTime To TestRuntimeAggregate
                TestRuntimeAggregate.setTcStartTime(DateTime.Now.ToString());
                foreach (AbstractTcManager oneTcManger in tcMangerList)
                {
                    TestRuntimeAggregate.setCurrentTcManagerName(oneTcManger.GetType().Name);
                    oneTcManger.run();
                    TestRuntimeAggregate.setTotalManagerRun(Math.Round(100.0 * (tcRunOverCount++ + 1) / tcMangerList.Count, 2) + "%");
                    if (StaticEnvironInfo.isOcrUsed())
                    {
                        Report.FlowGraph flowGraphCreater = new Report.FlowGraph();
                        flowGraphCreater.createFlowGraph();
                    }
                }
            }
            catch (ThreadAbortException)
            {
                heartBeat.abort();
                if (StaticEnvironInfo.isOcrUsed())
                {
                    Report.FlowGraph flowGraphCreater = new Report.FlowGraph();
                    flowGraphCreater.createFlowGraph();
                }
                ocrHalt();
                return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
            }
            isReportCreating = true;
            heartBeat.stop();
            TotalTCWriteReportHandler.writeReport();
            if (this.testFormArguments.Count() == 0)
            {
                ocrHalt();
            }
            isReportCreating = false;
        }

        private void allParamInit()
        {
            //tcRunInit Start
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            if (StaticEnvironInfo.isTPBvboardTested() == false)
            {
                //Initial Serial Config
                io = new ComponentIOSerial(new SerialConfig(portName));
            }
            else
            {
                io = new ComponentIOSocket("127.0.0.1", "1023");
            }

            //Initial Key Board
            AbstractComponentKeyBoardMFCTP keyBoard = new ComponentKeyBoardMFCTPUseIO(io);

            //Initial TouchPanel
            AbstractComponentTouchPanelMFCTP touchPanel = new ComponentTouchPanelMFCTPUseIO(io);

            //Initial Machine
            string machineConfigFilename = StaticEnvironInfo.getMachineConfigFileName();
            machineMFCTP = new MachineMFCTPUseIO(io, keyBoard, touchPanel, machineConfigFilename);

            //Test Start
            machineMFCTP.io.connect();
            machineMFCTP.io.write(MFCTPLogCode.StartAutoTest);

            //Create LogReader
            LogReaderMFCTPByIO.setIO(machineMFCTP.io);

            //Initial LogScreenChangeChecker
            LogScreenChangeChecker.setIO(io);

            TotalOpinionBlackList opinionBlackList = new TotalOpinionBlackList();
            opinionBlackList.loadOpinionBlackList(StaticEnvironInfo.getOpinionBlackListFullFileName());
            TestRuntimeAggregate.addOpinionBlackList(opinionBlackList.opinionBlackList);

            automaticWakeUp(io);
            Thread.Sleep(2000);

            modelInfo.loadModelInfo(StaticEnvironInfo.getModelInfoFullFileName());
            //Initial ScreenSize
            Screen.screenSize = modelInfo.screenSize;

            AbstractClickMachineTPExecutorMFCTP tpClick = new ClickMachineTPExecutorMFCTPByIO(machineMFCTP);
            AbstractCommandExecutor home = new ClickMachineKeyExecutorMFCTPByIO(machineMFCTP);
            if (modelInfo.screenSize.w == 432 && modelInfo.screenSize.h == 240)
            {
                Position scan = new Position(284, 148);
                tpClick.execute(scan);
                autoChangScanLayerID();
                home.execute(MFCTPKeyCode.HOME_KEY);
                Thread.Sleep(150);
                Position Copy = new Position(165, 148);
                tpClick.execute(Copy);
                Thread.Sleep(150);
                autoChangeNumerLayerID();
                Position left = new Position(414, 119);
                tpClick.execute(left);
                Thread.Sleep(150);
                autoChangCopyLayerID();
            }
            home.execute(MFCTPKeyCode.HOME_KEY);

            //Create Fixer
            ControlTagFixerByLog.setTagWords(modelInfo.tagList);
            ScreenDictionary.setTagWords(modelInfo.tagList);


            //Initial CommandExecutor
            foreach (BlackAndWhiteListInfo oneBlackList in modelInfo.blackList)
            {
                List<AbstractElement> elementList = oneBlackList.getElementList();
                if (elementList != null)
                {
                    ListRawScreenExecutorMFCTPByLog.addBlackList(oneBlackList.screenIdentify, elementList);
                }
            }
            foreach (BlackAndWhiteListInfo oneWhiteList in modelInfo.whiteList)
            {
                List<AbstractElement> elementList = oneWhiteList.getElementList();
                if (elementList != null)
                {
                    ListRawScreenExecutorMFCTPByLog.addWhiteList(oneWhiteList.screenIdentify, elementList);
                }
            }
            foreach (LogControlInfo oneVirtualList in modelInfo.virtualList)
            {
                List<LogControl> controlList = oneVirtualList.getControlList();
                if (controlList != null)
                {
                    ListRawScreenExecutorMFCTPByLog.addVirtualList(oneVirtualList.scrId, controlList);
                }
            }
            AbstractControlArrow.setHomeScrId(modelInfo.homeScreenIdentify.scrId);
            TempGoTempRoot.setHomeScrId(modelInfo.homeScreenIdentify.scrId);
            StaticCommandExecutorList.add(CommandList.click_k, new ClickMachineKeyExecutorMFCTPByIO(machineMFCTP));
            StaticCommandExecutorList.add(CommandList.click_p, new ClickMachineTPExecutorMFCTPByIO(machineMFCTP));
            StaticCommandExecutorList.add(CommandList.move_b, new MoveToPreviousScreenExecutorMFCTPByClickClearKey());
            StaticCommandExecutorList.add(CommandList.list_r, new ListRawScreenExecutorMFCTPByLog(new LogScreen()));
            StaticCommandExecutorList.add(CommandList.list_f, new ListFixedScreenExecutorMFCTPByLog(new LogScreen()));
            StaticCommandExecutorList.add(CommandList.click_b, new ClickButtonIdExecutorByFixAllFirst());
            StaticCommandExecutorList.add(CommandList.click_s, new ClickInputButtonExecutorByFixInputBtn());
            StaticCommandExecutorList.add(CommandList.click_w, new ClickButtonWordExecutorByFixAllFirst());
            StaticCommandExecutorList.add(CommandList.move_r, new MoveToHomeScreenExecutorBC4(modelInfo.homeScreenIdentify));
            StaticCommandExecutorList.add(CommandList.move_d_s, new MoveToTragetScreenExecutorMFCTPByFixAllFirst());
            StaticCommandExecutorList.add(CommandList.move_d_id, new MoveToScreenExecutorMFCTPByIdentifyList());

            IIterator tcIterator = treeMemory.createMccFilteredTcIterator();
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                TestRuntimeAggregate.createRuntimeInfo(tcIterator.currentItem());
            }
            //tcRunInit end

            //writeLog Start
            writeLog();

            if (this.testFormArguments.Count() == 0)
            {
                if (true == StaticEnvironInfo.isOcrUsed())
                {
                    //move to menu screen
                    moveToMenuScreen();
                }
            }

            //ocrInit Start
            ocrInit(machineMFCTP);
        }

        private void checkIPAddressCorrect()
        {
            machineMFCTP.io.write("\x14");
            Thread.Sleep(100);
            machineMFCTP.io.write("net_info\r\n");
            Thread.Sleep(1200);
            string net_info = machineMFCTP.io.read();
            for (int i = 0; i < 10; i++)
            {
                machineMFCTP.io.write("\b");
            }
            machineMFCTP.io.write("exit\r\n");
            if (net_info.Contains("IP Address"))
            {
                string select = splitSpecialNumStr(net_info);
                string ip = getIPAddress();
                if (!select.Contains(ip))
                {
                    IniFile aa = new IniFile(StaticEnvironInfo.getIntPutModelPath() + @"\" + "EWSAndRSPOptionOperator.ini");
                    Match m = Regex.Match(select, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
                    if (m.Success)
                    {
                        select = m.Value;
                    }
                    aa.IniWriteValue("AutoTestForRSP", "MachineIP", select);
                    aa.IniWriteValue("AutoTestForEWS", "MachineIP", select);
                }
            }
            Thread.Sleep(300);
        }
        private string splitSpecialNumStr(string modular)
        {
            string AddressName = null;
            string[] splitRet = modular.Split(new char[] { '\r', '\n' });
            List<string> newList = new List<string>(splitRet);
            newList.RemoveAll(it => it == "");
            foreach (string str in newList)
            {
                if (str.Contains("IP Address") && !str.Contains("IP Address ="))
                {
                    AddressName = str;
                    return AddressName;
                }
            }
            return AddressName;
        }
        private string getIPAddress()
        {
            string iniFileName = StaticEnvironInfo.getIntPutModelPath() + @"\" + "EWSAndRSPOptionOperator.ini";
            List<string> keylist = new List<string>();
            Dictionary<string, string> EWSIpInfo = new Dictionary<string, string>();
            Parser.INIParser.getKeyList("AutoTestForEWS", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                string value = Parser.INIParser.getvalue("AutoTestForEWS", iniFileName, keylist[i]);
                if (value != "")
                {
                    EWSIpInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();
            return EWSIpInfo["MachineIP"];
        }
        private void automaticWakeUp(AbstractComponentMachineIO io)
        {
            //Automatic wake up printer
            string scrIdBuf = "";
            bool logReadOpenFlag = io.isReadON();
            while (true)
            {
                machineMFCTP.io.write("\xae\r\n");
                Thread.Sleep(500);

                AbstractClickMachineKeyExecutorMFCTP keyClicker = new ClickMachineKeyExecutorMFCTPByIO(machineMFCTP);
                keyClicker.execute(MFCTPKeyCode.HOME_KEY);
                Thread.Sleep(500);

                //Open Log Reader
                if (!logReadOpenFlag && io.isReadOFF())
                    io.readON();
                machineMFCTP.io.write("|n\r\n");
                Thread.Sleep(500);
                scrIdBuf += machineMFCTP.io.read();

                if (LogParserMFCTP.isScrNameFinished(scrIdBuf))
                {
                    //Close Log Reader
                    if (!logReadOpenFlag && io.isReadON())
                        io.readOFF();
                    break;
                }
            }
        }
        private void autoChangScanLayerID()
        {
            AbstractCommandExecutor raw = new ListRawScreenExecutorMFCTPByLog(new LogScreen());
            Screen currentScreen = new Screen();
            raw.execute(currentScreen);
            List<AbstractElement> elementList = currentScreen.getElementList();
            foreach (AbstractElement oneElement in elementList)
            {
                for (int index = 0; index < modelInfo.virtualList[1].controlInfo.Count; index++)
                {
                    if (oneElement.rect.h == modelInfo.virtualList[1].controlInfo[index].h
                        && oneElement.rect.w == modelInfo.virtualList[1].controlInfo[index].w
                        && oneElement.rect.x == modelInfo.virtualList[1].controlInfo[index].x
                        && oneElement.rect.y == modelInfo.virtualList[1].controlInfo[index].y)
                    {
                        modelInfo.virtualList[1].controlInfo[index].layerID = (ushort)oneElement.id;
                    }
                }
            }
            Thread.Sleep(100);
        }
        private void autoChangeNumerLayerID()
        {
            AbstractCommandExecutor raw = new ListRawScreenExecutorMFCTPByLog(new LogScreen());
            Screen currentScreen = new Screen();
            raw.execute(currentScreen);
            List<AbstractElement> elementList = currentScreen.getElementList();
            foreach (AbstractElement oneElement in elementList)
            {
                for (int index = 0; index < modelInfo.blackList[2].elementImageList.Count; index++)
                {
                    if (oneElement.rect.h == modelInfo.blackList[2].elementImageList[index].rect.h
                        && oneElement.rect.w == modelInfo.blackList[2].elementImageList[index].rect.w
                        && oneElement.rect.x == modelInfo.blackList[2].elementImageList[index].rect.x
                        && oneElement.rect.y == modelInfo.blackList[2].elementImageList[index].rect.y)
                    {
                        modelInfo.blackList[2].elementImageList[index].id = (ushort)oneElement.id;
                    }
                }
            }
            Thread.Sleep(100);
        }
        private void autoChangCopyLayerID()
        {
            AbstractCommandExecutor raw = new ListRawScreenExecutorMFCTPByLog(new LogScreen());
            Screen currentScreen = new Screen();
            raw.execute(currentScreen);
            List<AbstractElement> elementList = currentScreen.getElementList();
            foreach (AbstractElement oneElement in elementList)
            {
                for (int index = 0; index < modelInfo.virtualList[0].controlInfo.Count; index++)
                {
                    if (oneElement.rect.h == modelInfo.virtualList[0].controlInfo[index].h
                        && oneElement.rect.w == modelInfo.virtualList[0].controlInfo[index].w
                        && oneElement.rect.x == modelInfo.virtualList[0].controlInfo[index].x
                        && oneElement.rect.y == modelInfo.virtualList[0].controlInfo[index].y)
                    {
                        modelInfo.virtualList[0].controlInfo[index].layerID = (ushort)oneElement.id;
                    }
                }
            }
            Thread.Sleep(100);
            foreach (AbstractElement oneElement in elementList)
            {
                for (int index = 0; index < modelInfo.blackList[3].elementImageList.Count; index++)
                {
                    if (oneElement.rect.h == modelInfo.blackList[3].elementImageList[index].rect.h
                        && oneElement.rect.w == modelInfo.blackList[3].elementImageList[index].rect.w
                        && oneElement.rect.x == modelInfo.blackList[3].elementImageList[index].rect.x
                        && oneElement.rect.y == modelInfo.blackList[3].elementImageList[index].rect.y)
                    {
                        modelInfo.blackList[3].elementImageList[index].id = (ushort)oneElement.id;
                    }
                }
            }
            Thread.Sleep(100);
        }
        private void moveToMenuScreen()
        {
            //update standard button image
            if (null == modelInfo.buttonStatusPath)
                throw new FTBAutoTestException("Parse standard button path error.");

            //prepair buttonWords list
            List<string> btnWordsList = new List<string>();
            MatchCollection matchedStrings = Regex.Matches(modelInfo.getCornerPath, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in matchedStrings)
            {
                btnWordsList.Add(match.ToString().Replace("//", "/"));
            }
            if (btnWordsList.Count < 1)
                throw new FTBAutoTestException("Parse standard button path error.");

            if (btnWordsList[0] == "")
                return;

            //prepair identify list
            List<ControlButtonIdentify> btnIdList = new List<ControlButtonIdentify>();
            string path = btnWordsList[0];
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            for (int i = 1; i < btnWordsList.Count; i++)
            {
                path += @"/" + btnWordsList[i].Replace(@"/", @"//");
                //get screen Identify
                string screenId = treeMemory.getLevelButtonToScreenId(path);
                //get screen title
                string screenTitle = treeMemory.getLevelButtonToScreenTitle(path);

                //set ScreenIdentify
                ScreenIdentify screenIdentify = new ScreenIdentify(screenId, screenTitle);

                //set buttonIdentify
                btnIdList.Add(new ControlButtonIdentify(btnWordsList[i], screenIdentify));
            }

            //run to targetScreen 
            try
            {
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                StaticCommandExecutorList.get(CommandList.move_d_id).execute(btnIdList);
            }
            catch (FTBAutoTestException)
            {
                throw new FTBAutoTestException("Run standard button path error.");
            }

        }
        private void ocrInit(AbstractMachineMFCTP machineMFCTP)
        {
            //ocrInit Start
            if (false == StaticEnvironInfo.isOcrUsed())
            {
                if ((TestRuntimeAggregate.getSelectedOpinion().Contains("RspDefaultCheckWithFTB")
                    || TestRuntimeAggregate.getSelectedOpinion().Contains("EwsDefaultCheckWithFTB")
                    || TestRuntimeAggregate.getSelectedOpinion().Contains("OptionDefaultChecker"))
                    && StaticEnvironInfo.isMenuItemTested() == true)
                {
                    reset(machineMFCTP);
                    //Sleep for secs until machine is quiet down
                    if (StaticEnvironInfo.isTPFirmTested() == true)
                    {
                        Thread.Sleep(17000);
                    }
                    else { Thread.Sleep(35000); }
                }
                return;
            }

            //Initial CameraEngine
            if (this.testFormArguments.Count() == 0)
            {
                System.Diagnostics.Process.Start("CameraEngine.exe");
            }
            Engine.EngineCommunicatorByShareMemory engineCommunicatorCamera = new Engine.EngineCommunicatorByShareMemory(StaticEnvironInfo.getCameraShareMemoryName(),
                                                                                                                         StaticEnvironInfo.getCameraWriteSemaphoreName(),
                                                                                                                         StaticEnvironInfo.getCameraReadSemaphoreName(),
                                                                                                                         StaticEnvironInfo.getCameraShareMemorySize());
            Engine.EngineCamera.setEngineCommunicator(engineCommunicatorCamera);
            Engine.EngineCamera.start(StaticEnvironInfo.getCameraConfigFileName());

            Engine.EngineCommunicatorByShareMemory engineCommunicatorOcr = new Engine.EngineCommunicatorByShareMemory(StaticEnvironInfo.getOcrShareMemoryName(),
                                                                                                                      StaticEnvironInfo.getOcrWriteSemaphoreName(),
                                                                                                                      StaticEnvironInfo.getOcrReadSemaphoreName(),
                                                                                                                      StaticEnvironInfo.getOcrShareMemorySize());
            //Initial OcrEngine
            if (this.testFormArguments.Count() == 0)
            {
                System.Diagnostics.Process.Start("OCREngine.exe");
            }
            Engine.EngineOCR.setEngineCommunicator(engineCommunicatorOcr);
            Engine.EngineOCR.start(StaticEnvironInfo.getOcrConfigFileName());
            Engine.EngineOCR.setScreenSize(Screen.screenSize);
            Engine.EngineOCR.setLanguage(@"USA");


            if (TestRuntimeAggregate.getSelectedOpinion().Contains("RspDefaultCheckWithFTB")
                || TestRuntimeAggregate.getSelectedOpinion().Contains("EwsDefaultCheckWithFTB")
                || TestRuntimeAggregate.getSelectedOpinion().Contains("OptionDefaultChecker"))
            {
                reset(machineMFCTP);
                //Sleep for secs until machine is quiet down
                Thread.Sleep(17000);
            }
            //Update standard button status info
            ocrUpdate();

            //get standard button status
            ControlButton selectedButton = new ControlButton();
            ElementImage selectedImage = modelInfo.buttonStatus[ControlButtonStatus.Selected];
            selectedButton.imageList.Add(selectedImage);
            selectedButton.statusShow = ControlButtonStatus.Selected;
            ControlButton validButton = new ControlButton();
            ElementImage validImage = modelInfo.buttonStatus[ControlButtonStatus.Valid];
            validButton.imageList.Add(validImage);
            validButton.statusShow = ControlButtonStatus.Valid;
            ControlButton invalidButton = new ControlButton();
            ElementImage invalidImage = modelInfo.buttonStatus[ControlButtonStatus.Invalid];
            invalidButton.imageList.Add(invalidImage);
            invalidButton.statusShow = ControlButtonStatus.Invalid;
            List<ControlButton> standardButtonList = new List<ControlButton>();
            standardButtonList.Add(selectedButton);
            standardButtonList.Add(invalidButton);
            standardButtonList.Add(validButton);

            Engine.EngineOCR.setStandardButtons(StaticEnvironInfo.getStandardButtonImage(), standardButtonList);
            //ocrInit end
        }
        private void ocrUpdate()
        {
            //update standard button image
            if (null == modelInfo.buttonStatusPath)
                throw new FTBAutoTestException("Parse standard button path error.");
            
            //prepair buttonWords list
            List<string> btnWordsList = new List<string>();
            MatchCollection matchedStrings = Regex.Matches(modelInfo.buttonStatusPath, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in matchedStrings)
            {
                btnWordsList.Add(match.ToString().Replace("//", "/"));
            }
            if (btnWordsList.Count < 1)
                throw new FTBAutoTestException("Parse standard button path error.");

            //prepair identify list
            List<ControlButtonIdentify> btnIdList = new List<ControlButtonIdentify>();
            string path = btnWordsList[0];
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            for (int i = 1; i < btnWordsList.Count; i++)
            {
                path += @"/" + btnWordsList[i].Replace(@"/", @"//");
                //get screen Identify
                string screenId = treeMemory.getLevelButtonToScreenId(path);
                //get screen title
                string screenTitle = treeMemory.getLevelButtonToScreenTitle(path);

                //set ScreenIdentify
                ScreenIdentify screenIdentify = new ScreenIdentify(screenId, screenTitle);

                //set buttonIdentify
                btnIdList.Add(new ControlButtonIdentify(btnWordsList[i], screenIdentify));
            }

            //run to targetScreen 
            try
            {
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                StaticCommandExecutorList.get(CommandList.move_d_id).execute(btnIdList);
            }
            catch(FTBAutoTestException)
            {
                throw new FTBAutoTestException("Run standard button path error.");
            }

            //updateScreen
            Dictionary<ControlButtonStatus, ElementImage> standardStatusDic = new Dictionary<ControlButtonStatus, ElementImage>();
            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);
            if (currentScreen.isScrollable())
            {
                AbstractScreenAggregate screenAggregate = null;
                screenAggregate = AbstractScreenAggregate.import(currentScreen);
                Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                while (!screenAggregate.isScreenContains(addScreen))
                {
                    screenAggregate.appendScreen(addScreen);

                    screenAggregate.moveToNextScreen(addScreen);
                    addScreen = new Screen();
                    StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                }

                IIterator showIterator = screenAggregate.createShowIterator();
                for(showIterator.first();!showIterator.isDone();showIterator.next())
                {
                    standardStatusDic.Clear();
                    List<AbstractControl> controlList = currentScreen.getControlList(typeof(ControlButton));
                    if (null == controlList)
                        throw new FTBAutoTestException("Get standard button error.");
                    foreach (ControlButton button in controlList)
                    {
                        if (ControlButtonStatus.Unknow == button.statusShow
                            || null == button.imageList
                            || button.imageList.Count != 1)
                            continue;

                        if (!standardStatusDic.ContainsKey(button.statusShow))
                        {
                            standardStatusDic.Add(button.statusShow, button.imageList[0]);
                        }
                    }

                    if (standardStatusDic.Count < 3)
                    {
                        controlList = currentScreen.getControlList(typeof(ControlArrowUpDown));
                        if (null == controlList || controlList.Count != 1)
                            break;

                        ControlArrowUpDown arrowControl = (ControlArrowUpDown)controlList[0];
                        ControlButton upButton = arrowControl.getFirstArrow();
                        ControlButton downButton = arrowControl.getLastArrow();

                        if (showIterator.currentItem() == 0)
                        {
                            upButton.statusShow = ControlButtonStatus.Invalid;
                        }
                        else if (showIterator.currentItem() == standardStatusDic.Count - 1)
                        {
                            downButton.statusShow = ControlButtonStatus.Invalid;
                        }

                        if (!standardStatusDic.ContainsKey(upButton.statusShow))
                        {
                            if (null != upButton.imageList && upButton.imageList.Count == 1)
                                standardStatusDic.Add(upButton.statusShow, upButton.imageList[0]);
                        }
                        if (!standardStatusDic.ContainsKey(downButton.statusShow))
                        {
                            if (null != downButton.imageList && downButton.imageList.Count == 1)
                                standardStatusDic.Add(downButton.statusShow, downButton.imageList[0]);
                        }
                    }
                    
                    if (standardStatusDic.Count == 3)
                        break;
                }
            }
            else
            {
                standardStatusDic.Clear();
                List<AbstractControl> controlList = currentScreen.getControlList(typeof(ControlButton));
                if (null == controlList)
                    throw new FTBAutoTestException("Get standard button error.");
                foreach (ControlButton button in controlList)
                {
                    if (ControlButtonStatus.Unknow == button.statusShow
                        || null == button.imageList
                        || button.imageList.Count != 1)
                        continue;

                    if (!standardStatusDic.ContainsKey(button.statusShow))
                    {
                        standardStatusDic.Add(button.statusShow, button.imageList[0]);
                    }
                }
            }

            if (standardStatusDic.Count != 3)
                throw new FTBAutoTestException("Get standard button error.");
            System.Threading.Thread.Sleep(3200);
            ////camera
            //Engine.EngineCamera camera = new Engine.EngineCamera();
            ////save image
            //string imagePath = StaticEnvironInfo.getStandardButtonImage();
            //camera.capture(imagePath);
            //camera
            ScreenSocket asss = new ScreenSocket("127.0.0.1", "10010");
            string imagePath = StaticEnvironInfo.getStandardButtonImage();
            asss.send("GetScreen");
            byte[] imgBuffer = asss.read();
            System.IO.Stream imgStream = new System.IO.MemoryStream(imgBuffer);
            Bitmap bm = new Bitmap(imgStream);
            Bitmap btnew22 = GetSmall(bm, 2);
            btnew22.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            System.Threading.Thread.Sleep(3200);
            modelInfo.buttonStatus = standardStatusDic;
            modelInfo.saveModelInfo();
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

        private void ocrHalt()
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            Engine.EngineCamera.stop();
            Engine.EngineOCR.stop();
        }

        private void writeLog()
        {
            string info = Environment.NewLine;
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();

            string selectModel = treeMemory.getSelectModel();
            string selectContinent = treeMemory.getSelectContinent();
            string selectCountry = treeMemory.getSelectCountry();

            info += ("ReportInfo:" + Environment.NewLine);
            info += ("総体設定情報:" + Environment.NewLine);
            info += ("selectModel:" + selectModel + Environment.NewLine);
            info += ("selectContinent:" + selectContinent + Environment.NewLine);
            info += ("selectCountry:" + selectCountry + Environment.NewLine);

            info += ("テスト観点一覧:" + Environment.NewLine);
            int optionCount = TestRuntimeAggregate.getOpinionCount();
            for (int index = 0; index < optionCount; index++)
            {
                List<string> testOpinion = TestRuntimeAggregate.getOpinionContent(index);
                string Opinionname = TestRuntimeAggregate.getOpinionName(index);
                info += ("番号:" + index + Environment.NewLine);
                info += ("観点名:" + Opinionname + Environment.NewLine);
                info += ("詳細:" + testOpinion[0] + Environment.NewLine);
                info += ("適用範囲:" + testOpinion[1] + Environment.NewLine);
            }
            info += ("ReportInfo End" + Environment.NewLine);
            StaticLog4NetLogger.reportLogger.Info(info);
        }

        private void reset(AbstractMachineMFCTP machineMFCTP)
        {
            //according reStartPath to reStart Machine
            if (null == modelInfo.reStartPath)
                throw new FTBAutoTestException("reStartPath is null.");

            //prepare buttonWordsList
            List<string> btnWordsList = new List<string>();
            MatchCollection matchedStrings = Regex.Matches(modelInfo.reStartPath, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in matchedStrings)
            {
                btnWordsList.Add(match.ToString().Replace("//", "/"));
            }
            if (btnWordsList.Count < 1)
                throw new FTBAutoTestException("Parse standard button path error.");

            //prepare identify list
            List<ControlButtonIdentify> btnIdList = new List<ControlButtonIdentify>();
            string path = btnWordsList[0];
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            for (int i = 1; i < btnWordsList.Count; i++)
            {
                path += @"/" + btnWordsList[i].Replace(@"/", @"//");
                //get screen Identify
                string screenId = treeMemory.getLevelButtonToScreenId(path);
                //get screen title
                string screenTitle = treeMemory.getLevelButtonToScreenTitle(path);
                //set ScreenIdentify
                ScreenIdentify screenIdentify = new ScreenIdentify(screenId, screenTitle);
                //set buttonIdentify
                btnIdList.Add(new ControlButtonIdentify(btnWordsList[i], screenIdentify));
            }

            try
            {
                //run to "Initial Setup/Reset/All Settings" to reStart machine
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                StaticCommandExecutorList.get(CommandList.move_d_id).execute(btnIdList);
            }
            catch (FTBAutoTestException)
            {
                throw new FTBAutoTestException("Run Initial Setup/Reset/All Settings failed.");
            }

            //get now screen
            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(currentScreen);

            string expMsg = string.Format("Can't find OK button in this screen.");

            ElementString buttonStringElement = new ElementString("OK");
            AbstractElement targetStringElement = currentScreen.findElement(buttonStringElement);
            if (null == targetStringElement)
                throw new FTBAutoTestException(expMsg);

            List<AbstractElement> targetImageElementList = currentScreen.getElementShipValueList(targetStringElement);
            if (null == targetImageElementList || 1 != targetImageElementList.Count)
                throw new FTBAutoTestException(expMsg);

            AbstractElement targetImageElement = targetImageElementList[0];

            if (null == targetImageElement.rect)
                throw new FTBAutoTestException(expMsg);
            //get OKBtn's rect
            Position targetPos = targetImageElement.rect.getCenter();
            AbstractRepeatPanelExecutorMFCTP tpClicker = new RepeatPanelExecutorByIO(machineMFCTP);
            //push and repeat "OK" Btn for more than "2s" to restart machine
            tpClicker.execute(targetPos);
            Thread.Sleep(8000);//sleep secs waite machine restart
            //after Machine reStart,Click ok or no
            startUp(machineMFCTP);
        }

        private void startUp(AbstractMachineMFCTP machineMFCTP)
        {
            //first to sleep 5s,until machineStartUpScreen isn't "Brother".
            Thread.Sleep(5000);
            //then send "\xae" Commmand to machine to do Tcrun or fix
            machineMFCTP.io.write(MFCTPLogCode.StartAutoTest);

            AbstractCommandExecutor tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
            //after Machine reStart,start Click "ok" or "No" Button
            //get first screen
            while (true)//loop get NowScreen'Identify
            {
                Thread.Sleep(750);
                Screen currentScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(currentScreen);
                if (currentScreen.getIdentify().scrId == "SCRN_STANDBY")
                {
                    break;
                }
                else
                {
                    List<AbstractElement> imageElementList = currentScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
                    if (imageElementList != null)
                    {
                        if (imageElementList.Count >= 3)
                        {
                            //do Nothing
                        }
                        else if (imageElementList.Count == 2)
                        {
                            //sort by x position
                            imageElementList.Sort((img1, img2) =>
                            {
                                return img1.rect.x - img2.rect.x;
                            });
                            Position btnPos = imageElementList[1].rect.getCenter();
                            tpClicker.execute(btnPos);
                            Thread.Sleep(500);
                        }
                        else if (imageElementList.Count == 1)
                        {
                            Position btnPos = imageElementList[0].rect.getCenter();
                            tpClicker.execute(btnPos);
                            Thread.Sleep(500);
                        }
                    }
                }//end if and else
            }//end while

            if (this.testFormArguments.Count() > 0)
            {
                //Sleep for secs until machine is quiet down
                Thread.Sleep(10000);
            }
            //to do
            //auto Change Language To English
            Thread.Sleep(30000);
        }//end private void startUp()

        private void autoChangeIpAddress()
        {
            machineMFCTP.io.write("\x14");
            Thread.Sleep(100);
            machineMFCTP.io.write("net_info\r\n");
            Thread.Sleep(1200);
            string net_info = machineMFCTP.io.read();
            for (int i = 0; i < 20; i++)
            {
                machineMFCTP.io.write("\b");
            }
            machineMFCTP.io.write("exit\r\n");
            Thread.Sleep(500);
            if (net_info.Contains("IP Address"))
            {
                string select = splitSpecialNumStr(net_info);
                string ip = getIPAddress();
                if (!select.Contains(ip))
                {
                    IniFile aa = new IniFile(StaticEnvironInfo.getIntPutModelPath() + @"\" + "EWSAndRSPOptionOperator.ini");
                    Match m = Regex.Match(select, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
                    if (m.Success)
                    {
                        select = m.Value;
                    }
                    aa.IniWriteValue("AutoTestForRSP", "MachineIP", select);
                    aa.IniWriteValue("AutoTestForEWS", "MachineIP", select);
                }
            }
            Thread.Sleep(300);
        }
        private void autoChangeLanguageToEnglish()
        {
            //before changLanguageToEnglish,go Home first.
            StaticCommandExecutorList.get(CommandList.move_r).execute();
            AbstractCommandExecutor tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
            string fileName = null;
            string folderFullName = StaticEnvironInfo.getIntPutModelPath() + @"\CountriesPosList";
            List<string> list = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            foreach (FileInfo file in TheFolder.GetFiles("*.txt"))
            {
                list.Add(file.FullName);
            }
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string country = treeMemory.getSelectCountry();
            string model = treeMemory.getSelectModel();

            string filePath = StaticEnvironInfo.getIntPutModelPath() + @"\EnglishIsDefaultLanguage.txt";
            List<string> oneList = readTxtFileToList(filePath);
            for (int j = 0; j < oneList.Count; j++)
            {
                if (country == oneList[j])
                {
                    //if Selected these Countries do nothing,and return.
                    return;
                }
            }

            foreach (string str in list)
            {
                if (str.Contains(treeMemory.getSelectModel().Trim(new Char[] { ' ' }))
                    && str.Contains(treeMemory.getSelectCountry()))
                {
                    fileName = str;
                }
            }

            if (fileName == null || (!fileName.Contains(model) && !fileName.Contains(country)))
            {
                startPrinterMonitor(model, country);
            }

            List<string> singleList = readTxtFileToList(fileName);
            if (singleList != null)
            {
                for (int i = 0; i < singleList.Count; i++)
                {
                    if (singleList[i] != "")
                    {
                        int idxStart = singleList[i].LastIndexOf("{");
                        string value = singleList[i].Substring(idxStart, singleList[i].Length - idxStart);
                        List<string> splitStrList = splitStr(value);
                        string xPosition = Regex.Replace(splitStrList[0], @"[^0-9]+", "");
                        string yPosition = Regex.Replace(splitStrList[1], @"[^0-9]+", "");
                        Position btnPos = new Position(Convert.ToInt32(xPosition), Convert.ToInt32(yPosition));
                        tpClicker.execute(btnPos);
                        Thread.Sleep(500);
                    }
                    else { break; }
                }
            }
            else
            {
                throw new FTBAutoTestException("get autoChangeLanguageToEnglish config falied.");
            }
            //after changLanguageToEnglish,go Home before TcRun.
            StaticCommandExecutorList.get(CommandList.move_r).execute();
        }

        private void startPrinterMonitor(string selectMode, string selectCountry)
        {
            MessageBoxButtons mesgButton = MessageBoxButtons.YesNo;
            string expMsg = string.Format("Can't Find [{0}] ButtonPos In Input CountriesPosList Files!!", selectMode + " " + selectCountry);
            DialogResult dr = MessageBox.Show(expMsg + "\nClick (Y) to start PrinterMonitor.exe and Start RecordClickPosButton,then save!!\nClick (N) to ignore this Message,But it Will make ToolRunError!!",
                expMsg, mesgButton, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (DialogResult.Yes == dr)
            {
                System.Diagnostics.Process.Start("PrinterMonitor.exe");
            }
            else
            {
                string throwMsg = string.Format("Can't Find [{0}] ButtonPos In Input CountriesPosList Files!!", selectMode + " " + selectCountry);
                throw new FTBAutoTestException(throwMsg);
            }
        }

        public List<string> readTxtFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs);
            //use StreamReader to read Text.
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            //read to end.
            string tmp = sr.ReadLine();
            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();
            }
            //after read close it.
            sr.Close();
            fs.Close();
            return list;
        }

        private List<string> splitStr(string str)
        {
            string[] splitRet = str.Split(new char[] { ',', '{', '}' });
            List<string> splitList = new List<string>();
            for (int i = 0; i < splitRet.Count(); i++)
            {
                if (splitRet[i] != "")
                {
                    splitList.Add(splitRet[i]);
                }
            }
            return splitList;
        }

        private void autoChangeCountries()
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string selectModel = treeMemory.getSelectModel();
            string selectCountry = treeMemory.getSelectCountry();
            string code = null;
            GetCountryCode get = new GetCountryCode();
            try
            {
                code = get.getCode(selectCountry);
            }
            catch (FTBAutoTestException)
            {
                string expMsg = string.Format("Can't Find [{0}] 74Code In NowModel's 74Code Table!!", selectCountry);
                throw new FTBAutoTestException(expMsg + "\nPlease check now Country's 74Code!!");
            }
            catch (Exception ex)
            {
                MessageBoxButtons mesgButton = MessageBoxButtons.YesNo;
                string expMsg = string.Format("Can't Find [{0}] 74Code In NowModel's 74Code Table!!", selectCountry);
                DialogResult dr = MessageBox.Show(expMsg + "\nPlease check now Country's 74Code!!\nClick Yes to Close this Thread",
                    expMsg, mesgButton, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (DialogResult.Yes == dr)
                {
                    Process.GetCurrentProcess().Kill();
                }
                MessageBox.Show(ex.StackTrace);
            }

            machineMFCTP.io.readON();
            machineMFCTP.io.write("\xae");
            machineMFCTP.io.write("any\r\n");
            string oneStr = machineMFCTP.io.read();
            simulateTeraTem(code);
            Thread.Sleep(2000);
            startUp(machineMFCTP);
        }

        private void simulateTeraTem(string code)
        {
            machineMFCTP.io.write("\x14");
            Thread.Sleep(1000);
            string ctrlAndT = machineMFCTP.io.read();
            machineMFCTP.io.readOFF();
            simulateMainten74(code);
            //if (ctrlAndT.Contains("ANALYSIS mode start"))
            //{
            //    simulateMainten74(code);
            //}
            //else if (ctrlAndT.Contains("COMMAND ERROR\r\nbrana"))
            //{
            //    simulateMainten74(code);
            //}
        }

        private void simulateMainten74(string code)
        {
            Thread.Sleep(1000);
            machineMFCTP.io.readON();
            machineMFCTP.io.write("mainten74\r\n");
            Thread.Sleep(1000);
            string maintenceStr = machineMFCTP.io.read();
            machineMFCTP.io.readOFF();
            if (maintenceStr.Contains("Mainten74 Code(Now:"))
            {
                Thread.Sleep(1000);
                machineMFCTP.io.readON();
                machineMFCTP.io.write(code + "\r\n");
                Thread.Sleep(1000);
                string codeStr = machineMFCTP.io.read();
                machineMFCTP.io.readOFF();
                if (codeStr.Contains("Input Data") && codeStr.Contains("MODEL_CODE:")
                    && codeStr.Contains("COUNTRY_CODE:"))
                {
                    Thread.Sleep(1000);
                    machineMFCTP.io.readON();
                    machineMFCTP.io.write("reset\r\n");
                    Thread.Sleep(1000);
                    string resetStr = machineMFCTP.io.read();
                    machineMFCTP.io.readOFF();
                }
            }
        }
    }
}
