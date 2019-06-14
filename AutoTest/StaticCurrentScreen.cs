using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool
{
    static class StaticCurrentScreen
    {
        private static Screen currentScreen;

        static StaticCurrentScreen()
        {
            currentScreen = null;
        }

        public static void set(Screen currentScreen)
        {
            if (StaticCurrentScreen.currentScreen != null && currentScreen != null)
            {
                if(StaticCurrentScreen.currentScreen.EqualsByElementList(currentScreen) == true
                    && currentScreen.getControlList().Count == 0)
                {
                    return;
                }
            }
            StaticCurrentScreen.currentScreen = currentScreen;
        }

        public static Screen get()
        {
            if (null == currentScreen)
                throw new FTBAutoTestException("Get current screen error, current Screen is null.");
            return currentScreen;
        }
    }
}
