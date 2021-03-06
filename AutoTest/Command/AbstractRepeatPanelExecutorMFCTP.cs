using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tool.Command
{
    abstract class AbstractRepeatPanelExecutorMFCTP : AbstractCommandExecutor
    {
        public override void execute(object param)
        {
            Position pos = param as Position;
            if (null == pos)
                throw new FTBAutoTestException("Click TP error by invalid param.");

            try
            {
                cmdMutex.WaitOne();
                StaticLog4NetLogger.commandExecutorLogger.Info("click-p \"" + pos.x + "," + pos.y + "\" start.");
                longClick(pos);
                StaticLog4NetLogger.commandExecutorLogger.Info("click-p \"" + pos.x + "," + pos.y + "\" succeed.");
                cmdMutex.ReleaseMutex();
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("click-p \"" + pos.x + "," + pos.y + "\" failed.\nReason:" + excp.Message);
                cmdMutex.ReleaseMutex();
                throw excp;
            }
        }

        public abstract void longClick(Position pos);
    }
}
