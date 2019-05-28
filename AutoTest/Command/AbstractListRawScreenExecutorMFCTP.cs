using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractListRawScreenExecutorMFCTP : AbstractCommandExecutor
    {
        public override void execute(object param)
        {
            Screen screen = param as Screen;
            if (null == screen)
                throw new FTBAutoTestException("Load screen error by invalid param.");

            try
            {
                cmdMutex.WaitOne();
                StaticLog4NetLogger.commandExecutorLogger.Info("list-r start.");
                getScreen(screen);
                StaticCurrentScreen.set(screen);
                StaticLog4NetLogger.commandExecutorLogger.Info("list-r succeed.");
                StaticLog4NetLogger.commandExecutorLogger.Debug("list-r succeed.\r\n" + screen.listElement() + "\r\n" + screen.listElementShip() + "\r\n");
                cmdMutex.ReleaseMutex();
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("list-r failed.\r\nReason:" + excp.Message);
                cmdMutex.ReleaseMutex();
                throw excp;
            }
        }
        protected abstract void getScreen(Screen screen);
    }
}
