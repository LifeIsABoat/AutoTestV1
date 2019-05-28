using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractMoveToScreenExecutorMFCTPByIdentifyList : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor tpClicker;
        protected AbstractCommandExecutor fixedScreenLoader;

        public AbstractMoveToScreenExecutorMFCTPByIdentifyList()
        {
            this.fixedScreenLoader = StaticCommandExecutorList.get(CommandList.list_f);
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_b);  
        }

        public override void execute(object param)
        {
            List<ControlButtonIdentify> ButtonIdList = param as List<ControlButtonIdentify>;
            if (null == ButtonIdList)
                throw new FTBAutoTestException("Move to target screen error by invalid param.");

            StaticLog4NetLogger.commandExecutorLogger.Info("move " + ButtonIdList.ToString() + " start.");
            try
            {
                change(ButtonIdList);
                StaticLog4NetLogger.commandExecutorLogger.Info("move " + ButtonIdList.ToString() + " succeed.");
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("move " + ButtonIdList.ToString() + " failed.\nReason:" + excp.Message);
                throw excp;
            }
        }

        protected abstract void change(List<ControlButtonIdentify> ButtonIdentify);
    }
}
