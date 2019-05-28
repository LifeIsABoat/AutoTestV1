using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractMoveToHomeScreenExecutorMFCTP : AbstractCommandExecutor
    {
        protected ScreenIdentify homeScreenIdentify;
        protected AbstractCommandExecutor keyClicker;
        protected AbstractCommandExecutor rawScreenLoader;

        public AbstractMoveToHomeScreenExecutorMFCTP(ScreenIdentify homeScreenIdentify)
        {
            this.homeScreenIdentify = homeScreenIdentify;
            this.rawScreenLoader = StaticCommandExecutorList.get(CommandList.list_r);
            this.keyClicker = StaticCommandExecutorList.get(CommandList.click_k);
        }

        public override void execute(object param)
        {
            try
            {
                cmdMutex.WaitOne();
                StaticLog4NetLogger.commandExecutorLogger.Info("move ~ start.");
                backToHomeScreen();
                StaticLog4NetLogger.commandExecutorLogger.Info("move ~ succeed.");
                cmdMutex.ReleaseMutex();
            }
            catch (FTBAutoTestException excp)
            {
                StaticLog4NetLogger.commandExecutorLogger.Warn("move ~ failed.\nReason:" + excp.Message);
                cmdMutex.ReleaseMutex();
                throw excp;
            }
        }

        protected abstract void waitForBackHome();

        private void backToHomeScreen()
        {
            ////1step, check current screen
            //if (true == checkHome())
            //    return;//change Home over

            //2step, try to push home Key
            keyClicker.execute(Machine.MFCTPKeyCode.HOME_KEY);
            if (true == checkHome())
                return;//change Home over

            //3step, try to wait for back home
            waitForBackHome();
            if (true == checkHome())
                return;//change Home over

            throw new FTBAutoTestException("Can't go to home screen.");
        }

        protected bool checkHome()
        {
            //Get current screen
            Screen currentScreen = new Screen();
            rawScreenLoader.execute(currentScreen);

            //Check Screen
            return homeScreenIdentify.Equals(currentScreen.getIdentify());
        }
    }
}
