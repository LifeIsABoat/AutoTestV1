using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tool.Command
{
    abstract class AbstractMoveToTragetScreenExecutorMFCTP : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor tpClicker;
        protected AbstractCommandExecutor btnClicker;
        protected AbstractCommandExecutor homeScreenChanger;
        protected AbstractCommandExecutor rawScreenLoader;
        protected AbstractCommandExecutor fixedScreenLoader;

        public AbstractMoveToTragetScreenExecutorMFCTP()
        {
            this.rawScreenLoader = StaticCommandExecutorList.get(CommandList.list_r);
            this.fixedScreenLoader = StaticCommandExecutorList.get(CommandList.list_f);
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
            this.btnClicker = StaticCommandExecutorList.get(CommandList.click_w);
            this.homeScreenChanger = StaticCommandExecutorList.get(CommandList.move_r);
        }

        public override void execute(object param)
        {
            string targetDir = param as string;
            if (null == targetDir)
                throw new FTBAutoTestException("Move to target screen error by invalid param.");
            
            List<string> btnWordsList = new List<string>();
            MatchCollection matchedStrings = Regex.Matches(targetDir, @"[^/]{1,}(/{2,})*[^/]*");
            foreach (Match match in matchedStrings)
            {
                btnWordsList.Add(match.ToString().Replace("//", "/"));
            }

            if (null == btnWordsList)
                throw new FTBAutoTestException("Move to target screen error by invalid param.");          
            StaticLog4NetLogger.commandExecutorLogger.Info("move " + targetDir + " start.");
            try
            {
                change(btnWordsList);
                StaticLog4NetLogger.commandExecutorLogger.Info("move " + targetDir + " succeed.");
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("move " + targetDir + " failed.\nReason:" + excp.Message);
                throw excp;
            }
        }

        protected abstract void change(List<string> btnWordsList);
    }
}
