using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Command
{
    abstract class AbstractClickButtonWordExecutorMFCTP : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor tpClicker;

        protected AbstractClickButtonWordExecutorMFCTP()
        {
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
        }

        public override void execute(object param = null)
        {
            string targetButtonWords = param as string;
            if (null == targetButtonWords)
                throw new FTBAutoTestException("Move to target screen error by invalid param.");
            StaticLog4NetLogger.commandExecutorLogger.Info("click-w \"" + targetButtonWords + "\" start.");
            try
            {
                click(targetButtonWords);
                StaticLog4NetLogger.commandExecutorLogger.Info("click-w \"" + targetButtonWords + "\" succeed.");
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("click-w \"" + targetButtonWords + "\" failed.\nReason:"+ excp.Message);
                throw excp;
            }
        }

        protected abstract void click(string targetButtonWords);
    }
}
