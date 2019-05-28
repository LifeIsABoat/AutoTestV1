using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractClickButtonIdExecutorMFCTP : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor tpClicker;

        protected AbstractClickButtonIdExecutorMFCTP()
        {
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
        }

        public override void execute(object param = null)
        {
            ControlButtonIdentify targetButtonIdentify = param as ControlButtonIdentify;
            if (null == targetButtonIdentify)
                throw new FTBAutoTestException("Move to target screen error by invalid param.");
            StaticLog4NetLogger.commandExecutorLogger.Info("click-w \"" + targetButtonIdentify.btnWordsStr + "\" start.");
            try
            {
                click(targetButtonIdentify);
                StaticLog4NetLogger.commandExecutorLogger.Info("click-w \"" + targetButtonIdentify.btnWordsStr + "\" succeed.");
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("click-w \"" + targetButtonIdentify.btnWordsStr + "\" failed.\nReason:" + excp.Message);
                throw excp;
            }
        }

        protected abstract void click(ControlButtonIdentify targetButtonIdentify);
    }
}
