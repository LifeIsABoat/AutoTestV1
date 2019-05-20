using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class ScreenCheck
    {
        public bool check(Screen ftbScreen, object currentScreen)
        {
            if (currentScreen is Screen)
            {
                //object to Screen
                Screen tempCurrentScreen = (Screen)currentScreen;
                ControlButton currentControlButton;
                ControlButton ftbControlButton;
                //equal scrId
                if (ftbScreen.getIdentify().scrId == tempCurrentScreen.getIdentify().scrId)
                {
                    return true;
                }
                else
                {
                    if (ftbScreen.getControlList().Count != tempCurrentScreen.getControlList().Count)
                    {
                        return false;
                    }
                    for (int index = 0; index < ftbScreen.getControlList().Count; index++)
                    {
                        //get ControlButton
                        currentControlButton = (ControlButton)tempCurrentScreen.getControlList()[index];
                        ftbControlButton = (ControlButton)ftbScreen.getControlList()[index];
                        //only equal index 0
                        if (currentControlButton.stringList[0] != ftbControlButton.stringList[0])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else if (currentScreen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate tempCurrentScreenAggregate = (AbstractScreenAggregate)currentScreen;
                if (ftbScreen.getIdentify().scrId == tempCurrentScreenAggregate.getFirstScreen().getIdentify().scrId)
                {
                    return true;
                }
                else
                {
 
                }
                return true;
            }
            return false;
        }
    }
}
