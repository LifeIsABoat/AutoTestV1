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
    class EwsDefault : AbstractCmnTestHandler
    {
        List<string> path = new List<string>();
        //OptioinOperatorByEWS PanelToEwsPath = new OptioinOperatorByEWS();
        private string EwsSelfCheckRet;//define
        string rspScreenId = "SCRN_POPUP_REMOTE_SET";
        const string opinion = "EwsDefaultCheckWithFTB";
        //Give Rsp the Machine IP
        public EwsDefault()
        {
            //"Set IP Type.Example: (10.244.4.151)"
            //PanelToEwsPath.setMachineIP("MachineIP");
        }

        public override void execute()
        {
            OptioinOperatorByEWS PanelToEwsPath = new OptioinOperatorByEWS();
            PanelToEwsPath.setMachineIP("MachineIP");
            StaticCommandExecutorList.get(CommandList.move_r).execute();

            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            //Get Now TcIndex
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            int levelTcIndex = treeMemory.getLevelCount(currentTcIndex);
            if (treeMemory.isEwsValid(currentTcIndex) == true)////if EWSFTB is "Y"
            {
                //Clear the Last Time's path
                path.Clear();
                string ftbButtonWord = treeMemory.getLevelButtonWord(levelTcIndex, -1, currentTcIndex);
                //frist to getTcDir(currentTcIndex)
                //then Assign now currentTcIndex to GiveOptionRouteToEws 
                string GiveOptionRouteToEws = treeMemory.getTcDir(currentTcIndex);
                //according the @"[^/]{1,}(/{2,})*[^/]*" to Match GiveOptionRouteToEws
                MatchCollection EwsStrings = Regex.Matches(GiveOptionRouteToEws, @"[^/]{1,}(/{2,})*[^/]*");
                foreach (Match match in EwsStrings)
                {
                    //Replace  EwsStrings "//" to "/")
                    //do path.Add
                    path.Add(match.ToString().Replace("//", "/"));
                }

                if (ftbButtonWord != "")
                {
                    //Remove the last Route
                    path.RemoveAt(path.Count - 1);
                }

                try
                {
                    EwsSelfCheckRet = PanelToEwsPath.getOption(path);
                }
                catch
                {
                    EwsSelfCheckRet = "[NG]Expection.Reason:要求チャネルは,Google Chrome無応答を待機してから,00:00:59.9969991,後にタイムアウトしました";
                }

                if (EwsSelfCheckRet == null)
                    throw new FTBAutoTestException("failed by null EwsSelfCheckRet.");
                if (GiveOptionRouteToEws == null || ftbButtonWord == null)
                    throw new FTBAutoTestException("failed by null vlaue.");

                TestRuntimeAggregate.setEwsOptionWords(EwsSelfCheckRet.ToString(), TestRuntimeAggregate.getCurrentTcIndex(), opinion, levelTcIndex);
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
                        Thread.Sleep(600);
                        ////Sleep for milliseconds because NowScreen' title is "Connecting to PC".
                        ////Avoid a mistake:Assigned the homeScreen's FixResult to  "Connecting to PC" Screen
                        ////until the NowScreen is homeScreen "SCRN_STANDBY".
                    }
                }
                //output EwsSelfCheckRet
                StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + "EwsSelfCheckRet:" + EwsSelfCheckRet.ToString());
            }

            base.execute();
        }//end public
    }//end class
}
