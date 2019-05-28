using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class MoveToScreenExecutorMFCTPByIdentifyList : AbstractMoveToScreenExecutorMFCTPByIdentifyList
    {
        protected override void change(List<ControlButtonIdentify> ButtonIdentify)
        {
            if (null == ButtonIdentify)
                throw new FTBAutoTestException("Can't change screen by null btnWordsList");

            foreach (ControlButtonIdentify ButtonWords in ButtonIdentify)
            {
                AbstractScreenAggregate screenAggregate = null;
                Screen currentScreen = new Screen();
                fixedScreenLoader.execute(currentScreen);
                if (currentScreen.isScrollable())
                {
                    screenAggregate = AbstractScreenAggregate.import(currentScreen);
                    while (!screenAggregate.isScreenContains(currentScreen))
                    {
                        screenAggregate.appendScreen(currentScreen);
                        screenAggregate.moveToNextScreen(currentScreen);
                        currentScreen = new Screen();
                        fixedScreenLoader.execute(currentScreen);
                    }
                }

                if (null == screenAggregate)
                {
                    tpClicker.execute(ButtonWords);
                    continue;
                }
                else
                {
                    IIterator screenShowIterator;
                    screenShowIterator = screenAggregate.createShowIterator();
                    screenShowIterator.first();
                    while (!screenShowIterator.isDone())
                    {
                        int index = screenShowIterator.currentItem();
                        try
                        {
                            tpClicker.execute(ButtonWords);
                            break;
                        }
                        catch (FTBAutoTestException) { }
                        screenShowIterator.next();
                    }
                    if (screenShowIterator.isDone())
                    {
                        string expMsg = string.Format("Can't find [{0}] button in current screen.", ButtonWords.btnWordsStr);
                        throw new FTBAutoTestException(expMsg);
                    }
                    else
                        continue;
                }
            }
        }
    }
}
