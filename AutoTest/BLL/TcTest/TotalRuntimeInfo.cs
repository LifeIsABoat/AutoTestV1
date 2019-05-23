using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tool.DAL;
using System.Windows.Forms;

namespace Tool.BLL
{
    class TestPathInfo
    {
        public string machineLogPath;
        public string commandLogPath;
        public string ocrLogPath;
        public string screenImagePath;
    }
    class TestCameraScreenInfo
    {
        public Dictionary<ElementString, string> ocrResult = new Dictionary<ElementString, string>();
        public Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = new Dictionary<ControlButton, ControlButtonStatus>();
    }
    class TestLevelInfo
    {
        public Dictionary<Screen,string> imagePathDict = new Dictionary<Screen, string>();
        //RspOptionWords
        public string RspOptionWords = null;
        //EWSOptionWords
        public string EwsOptionWords = null;
        //input content
        public string inputContent = null;

        public List<string> currentWordsList = new List<string>();

        public Screen logScreen = null;
        public AbstractScreenAggregate screenAggregate = null;
        public ControlButton logButton = null;
        public TestCameraScreenInfo cameraScreen = null;
    }
    public enum TestCheckResult
    {
        NA,
        OK,
        NG,
        NT,
    }
    public enum TestOneTCStatus
    {
        Transfering,
        OptionChecking
    }
    class TestRunTimeInfo
    {
        //Dictionary<TcRunManagerName, TestPathInfo>
        public Dictionary<string, TestPathInfo> evidencePath = new Dictionary<string, TestPathInfo>();

        //Dictionary<TcRunManagerName,TransferInfo>
        public Dictionary<string, Dictionary<int, TestLevelInfo>> levelInfoList = new Dictionary<string, Dictionary<int, TestLevelInfo>>();

        //Dictionary<OpinionName,TransferInfo>
        public Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = new Dictionary<string, Dictionary<int, TestLevelInfo>>();
       
        //Dictionary<OpinionName,CheckResult>
        public Dictionary<string, Dictionary<int, TestCheckResult>> checkResult = new Dictionary<string, Dictionary<int, TestCheckResult>>();

    }
    static class TestRuntimeAggregate
    {
        private static int currentTcIndex,currentLevelIndex;
        private static int currentScreenIndex;
        private static int currentOpinionIndex;
        private static string currentTcManager;
        private static string[] argument;
        private static TestOneTCStatus oneTcStatus;
        private static string startTime;
        private static string tcRunMessage;
        private static string totalManagerRunMessage;
        private static IFTBCommonAPI treeMemory;
        private static IScreenCommonAPI screenMemory;

        private static Dictionary<string, List<string>> selectedOpinionInfo;
        private static List<OpinionRunBlackListInfo> opinionBlackList = null;
        private static Dictionary<string, List<string>> selectedTcRunManagerInfo;
    
        private static string currentTime;

        [JsonProperty]
        private static Dictionary<int, TestRunTimeInfo> runtimeTcScreenInfo;

        public static void import(string runtimeFilePath = null)
        {
            if (null == runtimeFilePath)
            {
                runtimeTcScreenInfo = new Dictionary<int, TestRunTimeInfo>();
                selectedOpinionInfo = new Dictionary<string, List<string>>();
                // = new Dictionary<string, opinionBlackListAndRules>();
                selectedTcRunManagerInfo = new Dictionary<string, List<string>>();
            }
            else
            {
                try { runtimeTcScreenInfo = JsonConvert.DeserializeObject<Dictionary<int, TestRunTimeInfo>>(getTextJson(runtimeFilePath)); }
                catch (System.Threading.ThreadAbortException ex) { throw ex; }
                catch (Exception) { throw new FTBAutoTestException("Import runtime aggregate error by invaild format."); }
            }
        }

        public static void createRuntimeInfo(int tcIndex)
        {
            if(runtimeTcScreenInfo.Keys.Contains(tcIndex) == false)
            {
                TestRunTimeInfo runtimeInfo = new TestRunTimeInfo();
                List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
                foreach(string opinion in opinionList)
                {
                    Dictionary<int, TestCheckResult> resultDict = new Dictionary<int, TestCheckResult>();
                    runtimeInfo.checkResult.Add(opinion, resultDict);
                }
                runtimeTcScreenInfo.Add(tcIndex, runtimeInfo);
            }
            else
            {
                //throw new FTBAutoTestException(string.Format("tcIndex:{0} RuntimeInfo has already been created.", tcIndex));
                return;
            }
        }
        public static void clearSelectedOpinion()
        {
            selectedOpinionInfo.Clear();
        }

        public static void addSelectedOpinion(string OpinionName, string Detail, string Range, string Type)
        {
            if (selectedOpinionInfo.Keys.Contains(OpinionName) == false)
            {
                List<string> oneOpinionInfo = new List<string>();
                oneOpinionInfo.Add(Detail);
                oneOpinionInfo.Add(Range);
                oneOpinionInfo.Add(Type);
                selectedOpinionInfo.Add(OpinionName, oneOpinionInfo);
            }
            else
            {
                throw new FTBAutoTestException("opinion has been already added.");
                //return;
            }
        }

        public static string getCurrentTime()
        {
            return currentTime;
        }
        public static void setCurrentTime(string time)
        {
            currentTime = time;
        }

        public static List<string> getSelectedOpinion()
        {
            return selectedOpinionInfo.Keys.ToList();
        }
        public static int getOpinionCount()
        {
            return selectedOpinionInfo.Count;
        }
        public static string getOpinionName(int opinionIndex)
        {
            List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
            if (opinionList.Count < opinionIndex)
            {
                //throw new FTBAutoTestException("opinionIndex is too large");
                return null;
            }
            return opinionList[opinionIndex];
        }
        public static string getOpinionType(int opinionIndex)
        {
            List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
            if (opinionList.Count < opinionIndex)
            {
                //throw new FTBAutoTestException("opinionIndex is too large");
                return null;
            }
            List<string> opinionMessage = selectedOpinionInfo[opinionList[opinionIndex]];

            return opinionMessage[opinionMessage.Count - 1];

        }
        public static List<string> getOpinionContent(int opinionIndex)
        {
            List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
            if (opinionList.Count < opinionIndex)
            {
                //throw new FTBAutoTestException("opinionIndex is too large");
                return null;
            }
            string opinionName = opinionList[opinionIndex];
            return selectedOpinionInfo[opinionName];
        }
        //get Opinion Index
        public static int getOpinionIndex(string opinionName)
        {
            List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
            for (int opIndex = 0; opIndex < opinionList.Count; opIndex++)
            {
                if(opinionName == opinionList[opIndex])
                    return opIndex;
                    
            }
            return -1;
        }
        //save TcRun start Time
        public static void setTcStartTime(string dateAndTime)
        {
            if (dateAndTime == null)
                throw new FTBAutoTestException("dateAndTime is null.");

            startTime = dateAndTime;
        }
        public static string getTcStartTime()
        {
            return startTime;
        }

        public static void addOpinionBlackList(List<OpinionRunBlackListInfo> blackList)
        {
            opinionBlackList = blackList;
        }
        public static OpinionRunBlackListInfo getOpinionBlackList(string opinionName)
        {
            if (opinionBlackList != null)
            {
                return opinionBlackList.Find(x => x.opinionName == opinionName);
            }
            else
            {
                return null;
            }
        }

        public static void clearSelectedTcRunManager()
        {
            selectedTcRunManagerInfo.Clear();
        }
        public static void setSelectedTcRunManager(string tcRunManagerName,string opinionName)
        {
            if (tcRunManagerName == null || opinionName == null)
                throw new FTBAutoTestException("Set tcRunManager failed by invalid param.");
            if (selectedTcRunManagerInfo.ContainsKey(tcRunManagerName))
            {
                if(selectedTcRunManagerInfo[tcRunManagerName].Contains(opinionName) == false)
                {
                    selectedTcRunManagerInfo[tcRunManagerName].Add(opinionName);
                }
                else
                {
                    //throw new FTBAutoTestException("opinionName has already been added");
                }
            }
            else
            {
                List<string> tcRunManagerOpinion = new List<string>();
                tcRunManagerOpinion.Add(opinionName);
                selectedTcRunManagerInfo.Add(tcRunManagerName, tcRunManagerOpinion);
            }
        }
        public static List<string> getSelectedTcRunManager()
        {
            return selectedTcRunManagerInfo.Keys.ToList();
        }
        public static  List<string> getTcRunManagerOpinion(string tcRunManagerName)
        {
            if (selectedTcRunManagerInfo.ContainsKey(tcRunManagerName))
                return selectedTcRunManagerInfo[tcRunManagerName];
            return null;
        }

        public static void setCheckResult(int tcIndex, string opinion, TestCheckResult result, int levelIndex)
        {
            if(0 > tcIndex || null == opinion || 0 > levelIndex)
                throw new FTBAutoTestException("Set CheckResult failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, Dictionary<int, TestCheckResult>> checkResult = runtimeTcScreenInfo[tcIndex].checkResult;
            if (!checkResult.Keys.Contains(opinion))
            {
                checkResult.Add(opinion, new Dictionary<int, TestCheckResult>());
            }
            Dictionary<int, TestCheckResult> resultDict = checkResult[opinion];
            if (resultDict.Keys.Contains(levelIndex))
            {
                resultDict[levelIndex] = result;
            }
            else
            {
                resultDict.Add(levelIndex, result);
            }
            
            //else
            //{
            //    throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            //    //return;
            //}
        }
        public static Dictionary<int, TestCheckResult> getCheckResult(int tcIndex, int opinionIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestCheckResult>> checkResult = runtimeTcScreenInfo[tcIndex].checkResult;
                List<string> opinionList = selectedOpinionInfo.Keys.ToList<string>();
                if (opinionList.Count < opinionIndex)
                {
                    //throw new FTBAutoTestException("opinionIndex is too large");
                    return null;
                }
                string opinionName = opinionList[opinionIndex];
                if (checkResult.Keys.Contains(opinionName) == true)
                {
                    return checkResult[opinionName];//todo
                }
                else
                {
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
                return null;
            }            
        }

        public static int getLevelInfoListCount(int tcIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex)
                && runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
            {
                return runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager].Count;
            }
            else
            {
                // throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
                return -1;
            }
        }

        public static void setTreeMemory(IFTBCommonAPI treeMemory)
        {
            TestRuntimeAggregate.treeMemory = treeMemory;
        }
        public static IFTBCommonAPI getTreeMemory()
        {
            return treeMemory;
        }
        public static void setScreenMemory(IScreenCommonAPI screenMemory)
        {
            TestRuntimeAggregate.screenMemory = screenMemory;
        }
        public static IScreenCommonAPI getScreenMemory()
        {
            return screenMemory;
        }

        public static void setCurrentTcIndex(int tcIndex)
        {
            currentTcIndex = tcIndex;
        }
        public static int getCurrentTcIndex()
        {
            return currentTcIndex;
        }
        //set run over TcRunManager
        public static void setTotalManagerRun(string message)
        {
            totalManagerRunMessage = message;
            Application.DoEvents();
        }
        public static string getTotalManagerRun()
        {
            return totalManagerRunMessage;
        }
        //
        public static void setCurrentLevelIndex(int levelIndex)
        {
            currentLevelIndex = levelIndex;
        }
        public static int getCurrentLevelIndex()
        {
            return currentLevelIndex;
        }
        //set run over Tc
        public static void setTcMessage(string message)
        {
            tcRunMessage = message;
            Application.DoEvents();
        }
        public static string getTcMessage()
        {
            return tcRunMessage;
        }
        //
        public static void setCurrentTcManagerName(string tcRunManagerName)
        {
            if (null == tcRunManagerName)
                throw new FTBAutoTestException("Set Tc RunManagerName Error.");
            currentTcManager = tcRunManagerName;
        }
        public static string getcurrentTcManagerName()
        {
            return currentTcManager;
        }
        public static void setCurrentOpinionIndex(int opinionIndex)
        {
            currentOpinionIndex = opinionIndex;
        }
        public static int getCurrentOpinionIndex()
        {
            return currentOpinionIndex;
        }
        public static void setCurrentTCStatus(TestOneTCStatus status)
        {
            oneTcStatus = status;
        }
        public static TestOneTCStatus getCurrentTCStatus()
        {
            return oneTcStatus;
        }

        //Access by tcIndex
        public static void setMachineLogPath(int tcIndex, string machineLogPath)
        {
            if (0 > tcIndex || null == machineLogPath)
                throw new FTBAutoTestException("Set MachineLogPath failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
            if (!evidencePath.Keys.Contains(currentTcManager))
            {
                evidencePath.Add(currentTcManager, new TestPathInfo());
            }
            evidencePath[currentTcManager].machineLogPath = machineLogPath;
        }
        public static string getMachineLogPath(int tcIndex, int opinionIndex)
        {
            if (0 > tcIndex || 0 > opinionIndex)
                throw new FTBAutoTestException("Get MachineLogPath failed by invalid param.");

            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, TestPathInfo> evidencePath =  runtimeTcScreenInfo[tcIndex].evidencePath;
                if (evidencePath.Count == 0)
                    return "";

                foreach (KeyValuePair<string,List<string>> kvp in selectedTcRunManagerInfo)
                {
                    if(kvp.Value.Contains(getOpinionName(opinionIndex))
                        &&evidencePath.ContainsKey(kvp.Key))
                    {
                        return evidencePath[kvp.Key].machineLogPath;
                    }
                }

                return "";
            }
            else
            {
                throw new FTBAutoTestException("Get MachineLogPath failed by invalid param.");
                //return null;
            }
        }
        public static void setCommandLogPath(int tcIndex, string commandLogPath)
        {
            if (0 > tcIndex  || null == commandLogPath)
                throw new FTBAutoTestException("Set CommandLogPath failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
            if (!evidencePath.Keys.Contains(currentTcManager))
            {
                evidencePath.Add(currentTcManager, new TestPathInfo());
            }
            evidencePath[currentTcManager].commandLogPath = commandLogPath;
        }
        public static string getCommandLogPath(int tcIndex, int opinionIndex)
        {
            if (0 > tcIndex || 0 > opinionIndex)
                throw new FTBAutoTestException("Get CommandLogPath failed by invalid param.");

            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
                if (evidencePath.Count == 0)
                    return "";

                foreach (KeyValuePair<string, List<string>> kvp in selectedTcRunManagerInfo)
                {
                    if (kvp.Value.Contains(getOpinionName(opinionIndex))
                        && evidencePath.ContainsKey(kvp.Key))
                    {
                        return evidencePath[kvp.Key].commandLogPath;
                    }
                }

                return "";
            }
            else
            {
                throw new FTBAutoTestException("Get CommandLogPath failed by invalid tcIndex.");
                //return null;
            }
        }
        public static void setOcrLogPath(int tcIndex, string ocrLogPath)
        {
            if (0 > tcIndex || null == ocrLogPath)
                throw new FTBAutoTestException("Set OcrLogPath failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
            if (!evidencePath.Keys.Contains(currentTcManager))
            {
                evidencePath.Add(currentTcManager, new TestPathInfo());
            }
            evidencePath[currentTcManager].ocrLogPath = ocrLogPath;
        }
        public static string getOcrLogPath(int tcIndex, int opinionIndex)
        {
            if (0 > tcIndex || 0 > opinionIndex)
                throw new FTBAutoTestException("Get CommandLogPath failed by invalid param.");

            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
                if (evidencePath.Count == 0)
                    return "";

                foreach (KeyValuePair<string, List<string>> kvp in selectedTcRunManagerInfo)
                {
                    if (kvp.Value.Contains(getOpinionName(opinionIndex))
                        && evidencePath.ContainsKey(kvp.Key))
                    {
                        return evidencePath[kvp.Key].ocrLogPath;
                    }
                }

                return "";
            }
            else
            {
                throw new FTBAutoTestException("Get ocrLogPath failed by invalid param.");
                //return null;
            }
        }
        public static void setScreenImagePath(int tcIndex, string screenImagePath)
        {
            if (0 > tcIndex || null == screenImagePath)
                throw new FTBAutoTestException("Set ScreenImagePath failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
            if (!evidencePath.Keys.Contains(currentTcManager))
            {
                evidencePath.Add(currentTcManager, new TestPathInfo());
            }
            evidencePath[currentTcManager].screenImagePath = screenImagePath;
        }
        public static string getScreenImagePath(int tcIndex, int opinionIndex)
        {
            if (0 > tcIndex || 0 > opinionIndex)
                throw new FTBAutoTestException("Get ScreenImagePath failed by invalid param.");
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, TestPathInfo> evidencePath = runtimeTcScreenInfo[tcIndex].evidencePath;
                if (evidencePath.Count == 0)
                    return "";

                foreach (KeyValuePair<string, List<string>> kvp in selectedTcRunManagerInfo)
                {
                    //string name = getOpinionName(opinionIndex);
                    if (kvp.Value.Contains(getOpinionName(opinionIndex))
                        && evidencePath.ContainsKey(kvp.Key))
                    {
                        return evidencePath[kvp.Key].screenImagePath;
                    }
                }

                return "";
            }
            else
            {
                throw new FTBAutoTestException("Get ScreenImagePath failed by invalid param.");
                //return null;
            }
        }

        public static void clearTransferImagePathDict(int tcIndex = -1, int levelIndex = -1)
        {
            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    runtimeInfoList[levelIndex].imagePathDict.Clear();
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static void addTransferImagePathDict(Screen screen,string path, int tcIndex = -1, int levelIndex = -1)
        {
            if(null == screen || null == path)
                throw new FTBAutoTestException("Add Transfer ImagePath failed by invalid param");

            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, Dictionary<int, TestLevelInfo>> levelInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList;
            if (!levelInfoList.Keys.Contains(currentTcManager))
            {
                Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
            }
            Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
            if (runtimeInfoList.ContainsKey(levelIndex))
            {
                if (!runtimeInfoList[levelIndex].imagePathDict.ContainsValue(path))
                {
                    runtimeInfoList[levelIndex].imagePathDict.Add(screen, path);
                }
            }
            else
            {
                TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                oneRuntimeInfo.imagePathDict.Add(screen, path);
                runtimeInfoList.Add(levelIndex,oneRuntimeInfo);
            }
            //}
            //else
            //{
            //    throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            //}
        }
        public static string getTransferImagePath(Screen screen,int tcIndex = -1, int levelIndex = -1)
        {
            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    return null;
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    if (runtimeInfoList[levelIndex].imagePathDict.ContainsKey(screen))
                        return runtimeInfoList[levelIndex].imagePathDict[screen];
                    else
                        return null;
                }
                else
                {
                    //throw new FTBAutoTestException("levelIndex is out of range. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }

        public static void setLogScreen(object screen, int tcIndex, int levelIndex)
        {
            if (0 > tcIndex || 0 > levelIndex || null == screen)
                throw new FTBAutoTestException("Set LogScreen failed by invalid param.");

            if (!runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                runtimeTcScreenInfo.Add(tcIndex, new TestRunTimeInfo());
            }
            Dictionary<string, Dictionary<int, TestLevelInfo>> levelInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList;
            if (!levelInfoList.Keys.Contains(currentTcManager))
            {
                Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
            }
            Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
            if (runtimeInfoList.ContainsKey(levelIndex))
            {
                if (screen is Screen)
                {
                    runtimeInfoList[levelIndex].logScreen = (Screen)screen;
                }
                else if (screen is AbstractScreenAggregate)
                {
                    runtimeInfoList[levelIndex].screenAggregate = (AbstractScreenAggregate)screen;
                }
            }
            else
            {
                TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                if (screen is Screen)
                {
                    oneRuntimeInfo.logScreen = (Screen)screen;
                }
                else if (screen is AbstractScreenAggregate)
                {
                    oneRuntimeInfo.screenAggregate = (AbstractScreenAggregate)screen;
                }
                runtimeInfoList.Add(levelIndex, oneRuntimeInfo);
            }
        }
        public static object getLogScreen(int tcIndex, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    return null;
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    if(runtimeInfoList[levelIndex].logScreen != null)
                    {
                        return runtimeInfoList[levelIndex].logScreen;
                    }
                    else if(runtimeInfoList[levelIndex].screenAggregate != null)
                    {
                        return runtimeInfoList[levelIndex].screenAggregate;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //throw new FTBAutoTestException("levelIndex is out of range. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }
        public static void setLogButton(ControlButton button, int tcIndex, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    runtimeInfoList[levelIndex].logButton = button;
                }
                else
                {
                    TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                    oneRuntimeInfo.logButton = button;
                    runtimeInfoList.Add(levelIndex, oneRuntimeInfo);
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static ControlButton getLogButton(int tcIndex = -1, int levelIndex = -1)
        {
            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    return null;
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    return runtimeInfoList[levelIndex].logButton;
                }
                else
                {
                    //throw new FTBAutoTestException("levelIndex is out of range. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }

        public static void clearCameraImage(int tcIndex = -1, int levelIndex = -1)
        {
            if(-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    runtimeInfoList[levelIndex].cameraScreen = null;
                }
                else
                {
                    TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                    runtimeInfoList.Add(levelIndex ,oneRuntimeInfo);
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static void addTransferOcrResult(Dictionary<ElementString, string> ocrResult, int tcIndex = -1, int levelIndex=-1)
        {
            if (null == ocrResult || 0 == ocrResult.Count)
                return;
            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    if(null == runtimeInfoList[levelIndex].cameraScreen)
                        runtimeInfoList[levelIndex].cameraScreen = new TestCameraScreenInfo();
                    foreach (KeyValuePair<ElementString, string> kvp in ocrResult)
                    {
                        if (!runtimeInfoList[levelIndex].cameraScreen.ocrResult.ContainsKey(kvp.Key))
                        {
                            runtimeInfoList[levelIndex].cameraScreen.ocrResult.Add(kvp.Key, kvp.Value);
                        }
                    }
                }
                else
                {
                    TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                    oneRuntimeInfo.cameraScreen = new TestCameraScreenInfo();
                    oneRuntimeInfo.cameraScreen.ocrResult = ocrResult;
                    runtimeInfoList.Add(levelIndex ,oneRuntimeInfo);
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static Dictionary<ElementString, string> getTransferOcrResult(int tcIndex, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    return null;
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex) && null != runtimeInfoList[levelIndex].cameraScreen)
                {
                    return runtimeInfoList[levelIndex].cameraScreen.ocrResult;
                }
                else
                {
                    //throw new FTBAutoTestException("levelIndex is out of range. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }
        public static void addTransferButtonStatusResult(Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult, int tcIndex = -1, int levelIndex = -1)
        {
            if (null == buttonStatusResult || buttonStatusResult.Count == 0)
                return;
            if (-1 == tcIndex)
                tcIndex = currentTcIndex;
            if (-1 == levelIndex)
                levelIndex = currentLevelIndex;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> singleLevelInfoList = new Dictionary<int, TestLevelInfo>();
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, singleLevelInfoList);
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex))
                {
                    if (null == runtimeInfoList[levelIndex].cameraScreen)
                        runtimeInfoList[levelIndex].cameraScreen = new TestCameraScreenInfo();
                    foreach(KeyValuePair<ControlButton, ControlButtonStatus> kvp in buttonStatusResult)
                    {
                        if (!runtimeInfoList[levelIndex].cameraScreen.buttonStatusResult.ContainsKey(kvp.Key))
                        {
                            runtimeInfoList[levelIndex].cameraScreen.buttonStatusResult.Add(kvp.Key, kvp.Value);
                        }
                    }
                }
                else
                {
                    TestLevelInfo oneRuntimeInfo = new TestLevelInfo();
                    oneRuntimeInfo.cameraScreen = new TestCameraScreenInfo();
                    oneRuntimeInfo.cameraScreen.buttonStatusResult = buttonStatusResult;
                    runtimeInfoList.Add(levelIndex ,oneRuntimeInfo);
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static Dictionary<ControlButton, ControlButtonStatus> getTransferButtonStatusResult(int tcIndex, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    return null;
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (runtimeInfoList.ContainsKey(levelIndex) && null != runtimeInfoList[levelIndex].cameraScreen)
                {
                    return runtimeInfoList[levelIndex].cameraScreen.buttonStatusResult;
                }
                else
                {
                    //throw new FTBAutoTestException("levelIndex is out of range. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }

        public static object getRuntimeScreen(Screen currentRowScreen)
        {
            if (null == currentRowScreen)
                throw new FTBAutoTestException("Get runtime screen error.");
            foreach (TestRunTimeInfo runtimeInfo in runtimeTcScreenInfo.Values)
            {
                foreach (Dictionary<int, TestLevelInfo> singleLevelInfoList in runtimeInfo.levelInfoList.Values)
                {
                    if (null == singleLevelInfoList)
                        break;
                    foreach (TestLevelInfo levelInfo in singleLevelInfoList.Values)
                    {
                        if (null == levelInfo)
                            continue;
                        if (null != levelInfo.logScreen)
                        {
                            if (levelInfo.logScreen.getIdentify().Equals(currentRowScreen.getIdentify()))
                                return levelInfo.logScreen;
                        }
                        else if (null != levelInfo.screenAggregate)
                        {
                            if (levelInfo.screenAggregate.getFirstScreen().getIdentify().Equals(currentRowScreen.getIdentify()))
                                return levelInfo.screenAggregate;
                        }
                    }
                }
            }
            return null;
        }

        public static void setOpinionScreen(object screen, int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                TestLevelInfo oneLevelInfo = null;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }

                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                if (screen is Screen)
                {
                    oneLevelInfo.logScreen = (Screen)screen;
                }
                else if (screen is AbstractScreenAggregate)
                {
                    oneLevelInfo.screenAggregate = (AbstractScreenAggregate)screen;
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static object getOpinionScreen(int tcIndex, string opinion , int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        if (null == levelInfo.logScreen)
                        {
                            return levelInfo.screenAggregate;
                        }
                        else
                        {
                            return levelInfo.logScreen;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //throw new FTBAutoTestException("opinion is not in opinionInfoDict. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }
        public static void setOpinionImagePath(Screen screen, string imagePath, int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }

                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                if(oneLevelInfo.imagePathDict.Keys.Contains(screen))
                {
                    oneLevelInfo.imagePathDict[screen] = imagePath;
                }
                else
                {
                    oneLevelInfo.imagePathDict.Add(screen, imagePath);
                }
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static string getOpinionImagePath(int tcIndex, string opinion,  int levelIndex, Screen screen)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        if (levelInfo.imagePathDict.Keys.Contains(screen))
                        {
                            return levelInfo.imagePathDict[screen];
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //throw new FTBAutoTestException("opinion is not in opinionInfoDict. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }
        public static void addOpinionOcrResult(Dictionary<ElementString, string> ocrResult, int tcIndex , string opinion, int levelIndex)
        {
            if (null == ocrResult || 0 == ocrResult.Count)
                return;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }

                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                if (null == oneLevelInfo.cameraScreen)
                    oneLevelInfo.cameraScreen = new TestCameraScreenInfo();

                foreach(KeyValuePair<ElementString, string> kvp in ocrResult)
                    oneLevelInfo.cameraScreen.ocrResult.Add(kvp.Key,kvp.Value);
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static Dictionary<ElementString, string> getOpinionOcrResult(int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        if (null != levelInfo.cameraScreen)
                        {
                            return levelInfo.cameraScreen.ocrResult;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //throw new FTBAutoTestException("opinion is not in opinionInfoDict. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }
        public static void addOpinionButtonStatusResult(Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult, int tcIndex , string opinion, int levelIndex)
        {
            if (null == buttonStatusResult || 0 == buttonStatusResult.Count)
                return;
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }

                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                if (null == oneLevelInfo.cameraScreen)
                    oneLevelInfo.cameraScreen = new TestCameraScreenInfo();

                foreach(KeyValuePair<ControlButton, ControlButtonStatus> kvp in buttonStatusResult)
                    oneLevelInfo.cameraScreen.buttonStatusResult.Add(kvp.Key,kvp.Value);
            }
            else
            {
                throw new FTBAutoTestException("tcIndex is not in runtimeScreenInfo");
            }
        }
        public static Dictionary<ControlButton, ControlButtonStatus> getOpinionButtonStatusResult(int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        if (null != levelInfo.cameraScreen)
                        {
                            return levelInfo.cameraScreen.buttonStatusResult;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //throw new FTBAutoTestException("opinion is not in opinionInfoDict. ");
                    return null;
                }
            }
            else
            {
                //throw new FTBAutoTestException("tcIndex is out of range. ");
                return null;
            }
        }

        public static void setScreenCheckCurrentWords(int tcIndex, int levelIndex, List<string> currentWords)
        {
            if (0 > tcIndex || 0 > levelIndex)
                throw new FTBAutoTestException("Set screenCheck CurrentWords failed by invalid param.");

            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                if (!runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    runtimeTcScreenInfo[tcIndex].levelInfoList.Add(currentTcManager, new Dictionary<int, TestLevelInfo>());
                }
                Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                if (!runtimeInfoList.Keys.Contains(levelIndex))
                {
                    runtimeInfoList.Add(levelIndex, new TestLevelInfo());
                }
                runtimeInfoList[levelIndex].currentWordsList = currentWords;
            }
            else
            {
                throw new FTBAutoTestException("Set screenCheck CurrentWords failed.");
            }
        }
        public static List<string> getScreenCheckCurrentWords(int tcIndex, int levelIndex)
        {
            if (0 > tcIndex || 0 > levelIndex)
                throw new FTBAutoTestException("Get screenCheck CurrentWords failed by invalid param.");

            if (runtimeTcScreenInfo.Keys.Contains(tcIndex))
            {
                if (runtimeTcScreenInfo[tcIndex].levelInfoList.Keys.Contains(currentTcManager))
                {
                    Dictionary<int, TestLevelInfo> runtimeInfoList = runtimeTcScreenInfo[tcIndex].levelInfoList[currentTcManager];
                    if (runtimeInfoList.Keys.Contains(levelIndex))
                    {
                        return runtimeInfoList[levelIndex].currentWordsList;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
            {
                throw new FTBAutoTestException("Get screenCheck CurrentWords failed.");
            }
        }

        public static void setInputContent(string inputContent, int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                //TestLevelInfo oneLevelInfo = new TestLevelInfo();
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }
                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                //assign RspOptionWords to oneLevelInfo.RspOptionWords
                oneLevelInfo.inputContent = inputContent;
            }//end if
            else
            {
                throw new FTBAutoTestException("RspOptionWords is not in runtimeScreenInfo");
            }
        }//end setRspOptionWords

        public static string getInputContent(int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        //return oneLevelInfo.RspOptionWords
                        return levelInfo.inputContent;
                    }
                    else
                    {
                        return null;
                    }
                }//end Second if
                else
                {
                    return null;
                }
            }//end first if
            return null;
        }//end getRspOptionWords

        public static void setRspOptionWords(string RspOptionWords, int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                //TestLevelInfo oneLevelInfo = new TestLevelInfo();
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }
                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                //assign RspOptionWords to oneLevelInfo.RspOptionWords
                oneLevelInfo.RspOptionWords = RspOptionWords;
            }//end if
            else
            {
                throw new FTBAutoTestException("RspOptionWords is not in runtimeScreenInfo");
            }
        }//end setRspOptionWords
        public static string getRspOptionWords(int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        //return oneLevelInfo.RspOptionWords
                        return levelInfo.RspOptionWords;
                    }
                    else
                    {
                        return null;
                    }
                }//end Second if
                else
                {
                    return null;
                }
            }//end first if
            return null;
        }//end getRspOptionWords
        public static void setEwsOptionWords(string EwsOptionWords, int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                Dictionary<int, TestLevelInfo> levelInfoDict = null;
                //TestLevelInfo oneLevelInfo = new TestLevelInfo();
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    levelInfoDict = opinionInfoDict[opinion];
                }
                else
                {
                    levelInfoDict = new Dictionary<int, TestLevelInfo>();
                    opinionInfoDict.Add(opinion, levelInfoDict);
                }
                TestLevelInfo oneLevelInfo = null;
                if (levelInfoDict.Keys.Contains(levelIndex))
                {
                    oneLevelInfo = levelInfoDict[levelIndex];
                }
                else
                {
                    oneLevelInfo = new TestLevelInfo();
                    levelInfoDict.Add(levelIndex, oneLevelInfo);
                }

                //assign RspOptionWords to oneLevelInfo.RspOptionWords
                oneLevelInfo.EwsOptionWords = EwsOptionWords;
            }//end if
            else
            {
                throw new FTBAutoTestException("EwsOptionWords is not in runtimeScreenInfo");
            }
        }//end setEwsOptionWords
        public static string getEwsOptionWords(int tcIndex, string opinion, int levelIndex)
        {
            if (runtimeTcScreenInfo.Keys.Contains(tcIndex) == true)
            {
                Dictionary<string, Dictionary<int, TestLevelInfo>> opinionInfoDict = runtimeTcScreenInfo[tcIndex].opinionInfoDict;
                if (opinionInfoDict.Keys.Contains(opinion))
                {
                    Dictionary<int, TestLevelInfo> levelInfoDict = opinionInfoDict[opinion];
                    if (levelInfoDict.Keys.Contains(levelIndex))
                    {
                        TestLevelInfo levelInfo = levelInfoDict[levelIndex];
                        //return oneLevelInfo.RspOptionWords
                        return levelInfo.EwsOptionWords;
                    }
                    else
                    {
                        return null;
                    }
                }//end Second if
                else
                {
                    return null;
                }
            }//end first if
            return null;
        }//end getEwsOptionWords

        public static void setFormArguments(string[] args)
        {
            argument = args;
        }
        public static string[] getFormArguments()
        {
            return argument;
        }

        private static string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {

                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }
    }
}
