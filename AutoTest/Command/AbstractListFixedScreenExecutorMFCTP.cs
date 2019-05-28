using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractListFixedScreenExecutorMFCTP : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor rawScreenLoader;

        public override void execute(object param)
        {
            Screen screen = param as Screen;
            if (null == screen)
                throw new FTBAutoTestException("Load screen error by invalid param.");

            StaticLog4NetLogger.commandExecutorLogger.Info("list-f start.");
            try
            {
                rawScreenLoader.execute(screen);
                fixScreen(screen);
                StaticCurrentScreen.set(screen);
                StaticLog4NetLogger.commandExecutorLogger.Info("list-f succeed.");
                StaticLog4NetLogger.commandExecutorLogger.Debug("list-f succeed.\r\n" + screen.listControl() + "\r\n");
            }//todo
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("list-f failed.\r\nReason:" + excp.Message);
                throw excp;
            }
        }

        protected AbstractListFixedScreenExecutorMFCTP()
        {
            rawScreenLoader = StaticCommandExecutorList.get(CommandList.list_r);
        }
        protected abstract void fixScreen(Screen screen);
    }
}
