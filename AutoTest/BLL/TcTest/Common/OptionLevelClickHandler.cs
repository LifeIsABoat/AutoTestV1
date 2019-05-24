using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{
    class OptionLevelClickHandler : AbstractCmnTestHandler
    {
        public OptionLevelClickHandler()
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
        }
        public override void execute()
        {
            object logscreen = TestRuntimeAggregate.getLogScreen(TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            AbstractScreenAggregate screenAggregate = null;
            Screen currentScreen = null;
            if (logscreen is AbstractScreenAggregate)
            {
                screenAggregate = (AbstractScreenAggregate)logscreen;
            }
            else
            {
                currentScreen = (Screen)logscreen;
            }
            ControlButton ftbButton = loadFTBButtonControl(TestRuntimeAggregate.getCurrentLevelIndex());
            if (null == screenAggregate)
            {
                StaticCommandExecutorList.get(CommandList.click_b).execute(ftbButton.getIdentify());
            }
            else
            {
                IIterator screenShowIterator;
                screenShowIterator = screenAggregate.createShowIterator();
                screenShowIterator.first();
                while (!screenShowIterator.isDone())
                {
                    currentScreen = screenAggregate.readScreen(screenShowIterator.currentItem());
                    try
                    {
                        StaticCommandExecutorList.get(CommandList.click_b).execute(ftbButton.getIdentify());
                        break;
                    }
                    catch (FTBAutoTestException)
                    {
                        //Click button Fail isn't exception in this situation
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                    screenShowIterator.next();
                }
                if (screenShowIterator.isDone())
                {
                    string expMsg = string.Format("Can't find [{0}] button in current screen.", ftbButton.getIdentify().btnWordsStr);
                    throw new FTBAutoTestException(expMsg);
                }
            }

            if(currentScreen != null)
            {
                ControlButton targetButton = currentScreen.findButton(new ControlButtonIdentify("OK"));
                if (targetButton != null)
                {
                    StaticCommandExecutorList.get(CommandList.click_b).execute(targetButton.getIdentify());
                }
            }

            base.execute();
        }
    }
}
