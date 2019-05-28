using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Command
{
    abstract class AbstractMoveToPreviousScreenExecutorMFCTP : AbstractCommandExecutor
    {
        public override void execute(object param)
        {
            try
            {
                cmdMutex.WaitOne();
                StaticLog4NetLogger.commandExecutorLogger.Info("move ../ start.");
                change();
                StaticLog4NetLogger.commandExecutorLogger.Info("move ../ succeed.");
                cmdMutex.ReleaseMutex();
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("move ../ failed.\nReason:" + excp.Message);
                cmdMutex.ReleaseMutex();
                throw excp;
            }
        }

        protected abstract void change();
    }
}
