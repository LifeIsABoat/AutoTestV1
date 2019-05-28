using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tool.Command
{
    abstract class AbstractClickMachineKeyExecutorMFCTP : AbstractCommandExecutor
    {
        public override void execute(object param)
        {
            string keyCode = param as string;
            if (null == keyCode)
                throw new FTBAutoTestException("Click key error by invalid param.");

            try
            {
                cmdMutex.WaitOne();
                StaticLog4NetLogger.commandExecutorLogger.Info("click-k \"" + keyCode + "\" start.");
                Thread.Sleep(300);
                click(keyCode);
                StaticLog4NetLogger.commandExecutorLogger.Info("click-k \"" + keyCode + "\" succeed.");
                cmdMutex.ReleaseMutex();
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("click-k \"" + keyCode + "\" failed.\nReason:" + excp.Message);
                cmdMutex.ReleaseMutex();
                throw excp;
            }
        }

        protected abstract void click(string keyCode);
    }
}
