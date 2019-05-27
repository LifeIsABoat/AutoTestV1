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
    class RspDefault : AbstractCmnTestHandler
    {
        List<string> path = new List<string>();
        //OptioinOperatorByRSP PanelToRspPath = new OptioinOperatorByRSP();
        private string RspSelfCheckRet;//define
        const string opinion = "RspDefaultCheckWithFTB";
        //Give Rsp the Machine IP
        public RspDefault()
        {
            //"Set IP Type.Example: (10.244.4.151)"
            //PanelToRspPath.setMachineIP("MachineIP");
        }

        public override void execute()
        {
            OptioinOperatorByRSP PanelToRspPath = new OptioinOperatorByRSP();
            PanelToRspPath.setMachineIP("MachineIP");
            StaticCommandExecutorList.get(CommandList.move_r).execute();

            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            //Get Now TcIndex
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            int levelTcIndex = treeMemory.getLevelCount(currentTcIndex);
            if (treeMemory.isRspValid(currentTcIndex) == true)////if RSPFTB is "Y"
            {
                //Clear the Last Time's path
                path.Clear();
                string ftbButtonWord = treeMemory.getLevelButtonWord(levelTcIndex, -1, currentTcIndex);
                //frist to getTcDir(currentTcIndex)
                //then Assign now currentTcIndex to GiveOptionRouteToRsp  
                string GiveOptionRouteToRsp = treeMemory.getTcDir(currentTcIndex);
                //according the @"[^/]{1,}(/{2,})*[^/]*" to Match GiveOptionRouteToRsp
                MatchCollection RspStrings = Regex.Matches(GiveOptionRouteToRsp, @"[^/]{1,}(/{2,})*[^/]*");
                foreach (Match match in RspStrings)
                {
                    //Replace  RspStrings "//" to "/")
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
                    RspSelfCheckRet = PanelToRspPath.getOption(path);
                }
                catch
                {
                    RspSelfCheckRet = "[NG]Expection.Reason:要求チャネルは,Remote Setup無応答してから,00:00:59.9969991,後にタイムアウトしました";
                }

                if (null == RspSelfCheckRet)
                    throw new FTBAutoTestException("failed by null RspSelfCheckRet.");
                if (GiveOptionRouteToRsp == null || ftbButtonWord == null)
                    throw new FTBAutoTestException("failed by null vlaue.");

                TestRuntimeAggregate.setRspOptionWords(RspSelfCheckRet.ToString(), TestRuntimeAggregate.getCurrentTcIndex(), opinion, levelTcIndex);
                //output RspSelfCheckResult
                StaticLog4NetLogger.reportLogger.Info("TC-" + TestRuntimeAggregate.getCurrentTcIndex() + "RspSelfCheckRet:" + RspSelfCheckRet.ToString());
            }

            base.execute();
        }//end public
    }//end class
}
