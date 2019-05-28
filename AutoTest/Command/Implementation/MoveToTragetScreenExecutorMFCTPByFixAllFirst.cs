using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class MoveToTragetScreenExecutorMFCTPByFixAllFirst : AbstractMoveToTragetScreenExecutorMFCTP
    {

        protected override void change(List<string> btnWordsList)
        {
            if (null == btnWordsList)
                throw new FTBAutoTestException("Can't change screen by null btnWordsList");

            for (int i = 0; i < btnWordsList.Count; i++)
            {
                if (0 == i)
                {
                    homeScreenChanger.execute();
                    continue;
                }

                AbstractScreenAggregate screenAggregate = null;
                Screen currentScreen = new Screen();
                fixedScreenLoader.execute(currentScreen);
                if (currentScreen.isScrollable())
                {
                    screenAggregate = AbstractScreenAggregate.import(currentScreen);
                    currentScreen = screenAggregate.toFirstScreen(currentScreen);
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
                    try
                    {
                        btnClicker.execute(btnWordsList[i]);
                    }
                    catch (FTBAutoTestException)
                    {
                        string expMsg = string.Format("Move to target screen error by can't find [{0}] button in current screen.", btnWordsList[i]);
                        throw new FTBAutoTestException(expMsg);
                    }
                }
                else
                {
                    IIterator screenShowIterator;
                    screenShowIterator = screenAggregate.createShowIterator();
                    screenShowIterator.first();
                    while (!screenShowIterator.isDone())
                    {
                        try
                        {
                            btnClicker.execute(btnWordsList[i]);
                            break;
                        }
                        catch (FTBAutoTestException) { }
                        screenShowIterator.next();
                    }
                    if (screenShowIterator.isDone())
                    {
                        string expMsg = string.Format("Move to target screen error by can't find [{0}] button in current screen.", btnWordsList[i]);
                        throw new FTBAutoTestException(expMsg);
                    }
                    else
                        continue;
                }
            }
        }
    }
}
