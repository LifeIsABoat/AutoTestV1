using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class OneLevelCmnTestTargetButtonLoder : AbstractCmnTestHandler
    {
        //从当前画面中拿出需要点击的button的链
        public OneLevelCmnTestTargetButtonLoder()
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
        }
        public override void execute()
        {
            //AbstractScreenAggregate screenAggregate = TestRuntimeAggregate.getScreenAggregate(TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            object trmpcurrentScreen = TestRuntimeAggregate.getLogScreen(TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            AbstractScreenAggregate screenAggregate = null;
            Screen currentScreen = null;
            if(trmpcurrentScreen is AbstractScreenAggregate)
            {
                screenAggregate = (AbstractScreenAggregate)trmpcurrentScreen;
            }
            else if(trmpcurrentScreen is Screen)
            {
                currentScreen = (Screen)trmpcurrentScreen;
            }
            ControlButton ftbButton = loadFTBButtonControl(TestRuntimeAggregate.getCurrentLevelIndex());
            ControlButton targetButton = null;
            if (screenAggregate != null)
            {
                IIterator screenReadIterator;
                screenReadIterator = screenAggregate.createReadIterator();
                screenReadIterator.first();
                while (!screenReadIterator.isDone())
                {
                    int index = screenReadIterator.currentItem();
                    Screen tempCurrentScreenAggregate = screenAggregate.readScreen(index);

                    targetButton = tempCurrentScreenAggregate.findButton(ftbButton.getIdentify());
                    if (targetButton != null)
                    {
                        break;
                    }
                    screenReadIterator.next();
                }
            }
            else
            {
                //根据FTB的需要运行的Tc的button去匹配当前画面的的所有button
                targetButton = currentScreen.findButton(ftbButton.getIdentify());
            }
            TestRuntimeAggregate.setLogButton(targetButton, TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());

            base.execute();
        }
    }
}
