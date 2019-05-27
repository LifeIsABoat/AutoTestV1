using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using Tool.Engine;
using Tool.DAL;
using System.Text.RegularExpressions;
using System.Threading;

namespace Tool.BLL
{
    class RspToMachineOptionSetttings : AbstractCmnTestHandler
    {
        List<string> path = new List<string>();//define path
        //OptioinOperatorByRSP MachineOptiontByRspSet = new OptioinOperatorByRSP();
        string RspSelfCheckResult = null;
        //string opinion;
        const string opinion = "RspToMachineOptionChecker";
        string rspScreenId = "SCRN_POPUP_REMOTE_SET";
        public RspToMachineOptionSetttings()
        {
            //"Set IP Type.Example:10.244.4.151"
            //MachineOptiontByRspSet.setMachineIP("MachineIP");
        }
        public override void execute()
        {
            OptioinOperatorByRSP MachineOptiontByRspSet = new OptioinOperatorByRSP();
            MachineOptiontByRspSet.setMachineIP("MachineIP");
            //Clear the Last Time's path
            path.Clear();
            //Get Now TcIndex
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            //do get levelcount
            int levelCount = treeMemory.getLevelCount(TestRuntimeAggregate.getCurrentTcIndex());
            string ftbButtonWord = treeMemory.getLevelButtonWord(levelCount, -1, currentTcIndex);
            //frist to getTcDir(currentTcIndex)
            //then Assign now currentTcIndex to GiveOptionRouteToRsp  
            string GiveOptionRouteToRsp = treeMemory.getTcDir(currentTcIndex);
            //according the @"[^/]{1,}(/{2,})*[^/]*" to Match GiveOptionRouteToRsp
            MatchCollection RspStrings = Regex.Matches(GiveOptionRouteToRsp, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in RspStrings)
            {
                //Replace RspStrings "//" to "/")
                //do path.Add
                path.Add(match.ToString().Replace("//", "/"));
            }

            if (ftbButtonWord != "")
            {
                //Remove the last Route
                path.RemoveAt(path.Count - 1);
            }
            //get Panel's Settings from NowTcIndex
            string PanelOptionWordsSettings = treeMemory.getOptionWords(TestRuntimeAggregate.getCurrentTcIndex());
            try
            {
                RspSelfCheckResult = MachineOptiontByRspSet.setOption(path, PanelOptionWordsSettings);
            }
            catch
            {
                RspSelfCheckResult = "[NG]Expection.Reason:要求チャネルは,Remote Setup無応答してから,00:00:59.9969991,後にタイムアウトしました";
            }

            if (RspSelfCheckResult == null)
                throw new FTBAutoTestException("failed by null RspSelfCheckResult.");
            if (PanelOptionWordsSettings == null || GiveOptionRouteToRsp == null || ftbButtonWord == null)
                throw new FTBAutoTestException("failed by null vlaue.");

            ////Save RspSelfCheckRet to memory
            TestRuntimeAggregate.setRspOptionWords(RspSelfCheckResult.ToString(), TestRuntimeAggregate.getCurrentTcIndex(), opinion, levelCount);
            while (true)//loop get NowScreen'Identify
            {
                Screen currentRawScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
                if (currentRawScreen.getIdentify().scrId != rspScreenId)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(500);
                    ////Sleep for milliseconds because NowScreen' title is "Connecting to PC".
                    ////Avoid a mistake:Assigned the homeScreen's FixResult to  "Connecting to PC" Screen
                    ////until the NowScreen is homeScreen "SCRN_STANDBY".
                }
            }
            if (RspSelfCheckResult == "[OK]")
            {
               base.execute();
            }
            else
            {
                //do nothing 
            }
            //output RspSelfCheckResult and MachineOptionSet
            StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + " MachineOptionSet:" + PanelOptionWordsSettings.ToString());
            StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + " RspSelfCheckResult:" + RspSelfCheckResult.ToString());
        }//end public
    }//end class
}
