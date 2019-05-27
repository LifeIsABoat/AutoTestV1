using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using Tool.Engine;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class MachineToEws : AbstractCmnTestHandler
    {
        List<string> path = new List<string>();
        //OptioinOperatorByEWS PanelToEwsPath = new OptioinOperatorByEWS();
        private string EwsSelfCheckRet;//define
        const string opinion = "MachineToEwsOptionChecker";
        //Give Rsp the Machine IP
        public MachineToEws()
        {
            //"Set IP Type.Example: (10.244.4.151)"
            //PanelToEwsPath.setMachineIP("MachineIP");
        }

        public override void execute()
        {
            OptioinOperatorByEWS PanelToEwsPath = new OptioinOperatorByEWS();
            PanelToEwsPath.setMachineIP("MachineIP");
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            //Get Now TcIndex
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            if (treeMemory.isEwsValid(currentTcIndex) == true)////if EWSFTB is "Y"
            {
                //do goHome before TcRun
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                //Clear the Last Time's path
                path.Clear();
                string ftbButtonWord = treeMemory.getLevelButtonWord(TestRuntimeAggregate.getCurrentLevelIndex(), -1, currentTcIndex);
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

                TestRuntimeAggregate.setEwsOptionWords(EwsSelfCheckRet.ToString(), TestRuntimeAggregate.getCurrentTcIndex(), opinion, TestRuntimeAggregate.getCurrentLevelIndex());
                //output EwsSelfCheckRet
                StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + "EwsSelfCheckRet:" + EwsSelfCheckRet.ToString());
            }

            base.execute();
        }//end public
    }//end class
}
