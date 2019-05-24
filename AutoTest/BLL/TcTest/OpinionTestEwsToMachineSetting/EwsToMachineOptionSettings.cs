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
    class EwsToMachineOptionSetttings : AbstractCmnTestHandler
    {
        List<string> path = new List<string>();//define path
        //OptioinOperatorByEWS MachineOptiontByEwsSet = new OptioinOperatorByEWS();
        string EwsSelfCheckResult = null;
        //string opinion;
        const string opinion = "EwsToMachineOptionChecker";
        
        public EwsToMachineOptionSetttings()
        {
            //"Set IP Type.Example:10.244.4.151"
            //MachineOptiontByEwsSet.setMachineIP("MachineIP");
        }
        public override void execute()
        {
            OptioinOperatorByEWS MachineOptiontByEwsSet = new OptioinOperatorByEWS();
            MachineOptiontByEwsSet.setMachineIP("MachineIP");
            //Clear the Last Time's path
            path.Clear();
            //Get Now TcIndex
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            //do get levelcount
            int levelCount = treeMemory.getLevelCount(TestRuntimeAggregate.getCurrentTcIndex());
            string ftbButtonWord = treeMemory.getLevelButtonWord(levelCount, -1, currentTcIndex);
            //frist to getTcDir(currentTcIndex)
            //then Assign now currentTcIndex to GiveOptionRouteToEws  
            string GiveOptionRouteToEws = treeMemory.getTcDir(currentTcIndex);
            //according the @"[^/]{1,}(/{2,})*[^/]*" to Match GiveOptionRouteToEws
            MatchCollection EwsStrings = Regex.Matches(GiveOptionRouteToEws, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in EwsStrings)
            {
                //Replace EwsStrings "//" to "/")
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
                EwsSelfCheckResult = MachineOptiontByEwsSet.setOption(path, PanelOptionWordsSettings);
            }
            catch
            {
                EwsSelfCheckResult = "[NG]Expection.Reason:要求チャネルは,Google Chrome無応答を待機してから,00:00:59.9969991,後にタイムアウトしました";
            }

            if (EwsSelfCheckResult == null)
                throw new FTBAutoTestException("failed by null EwsSelfCheckResult.");
            if (PanelOptionWordsSettings == null || GiveOptionRouteToEws == null || ftbButtonWord == null)
                throw new FTBAutoTestException("failed by null vlaue.");

            ////Save RspSelfCheckRet to memory
            TestRuntimeAggregate.setEwsOptionWords(EwsSelfCheckResult.ToString(), TestRuntimeAggregate.getCurrentTcIndex(), opinion, levelCount);
            if (EwsSelfCheckResult == "[OK]")
            {
                base.execute();
            }
            else
            {
                //do nothing 
            }
            //output RspSelfCheckResult and MachineOptionSet
            StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + " MachineOptionSet:" + PanelOptionWordsSettings.ToString());
            StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + " EwsSelfCheckResult:" + EwsSelfCheckResult.ToString());
        }//end public
    }//end class
}
