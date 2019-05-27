using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{
    class MachineSetCheck: AbstractCmnTestHandler
    {
        public override void execute()
        {
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();

            int LevelCount = TestRuntimeAggregate.getLevelInfoListCount(currentTcIndex);
           
            setOpinionInfo(currentTcIndex,LevelCount - 1 /*,false*/);
            moveToNextLevel(TestRuntimeAggregate.getLogButton(currentTcIndex, LevelCount - 1),
                            TestRuntimeAggregate.getLogScreen(currentTcIndex, LevelCount - 1));
            setOpinionInfo(currentTcIndex, LevelCount);

            base.execute();
        }

        private void moveToNextLevel(ControlButton targetButton, object screen)
        {
            if (targetButton == null || screen == null)
            {
                throw new FTBAutoTestException("moveToNextLevel failed");
            }

            Screen currentScreen = null;
            if (screen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggregate = (AbstractScreenAggregate)screen;
                IIterator screenShowIterator;
                screenShowIterator = screenAggregate.createShowIterator();
                screenShowIterator.first();
                while (!screenShowIterator.isDone())
                {
                    int index = screenShowIterator.currentItem();
                    currentScreen = screenAggregate.readScreen(index);
                    if (currentScreen.findButton(targetButton.getIdentify()) != null)
                    {
                        break;
                    }
                    //currentScreen = null;
                    screenShowIterator.next();
                }
            }
            else if (screen is Screen)
            {
                currentScreen = (Screen)screen;
                //if (currentScreen.findButton(targetButton.getIdentify()) == null)
                //{
                //    currentScreen = null;
                //}
            }

            StaticCurrentScreen.set(currentScreen);
            StaticCommandExecutorList.get(CommandList.click_b).execute(targetButton.getIdentify());
        }

        private void setOpinionInfo(int tcIndex, int levelIndex/*, bool isFixAll = true*/)
        {
            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);

            AbstractScreenAggregate screenAggregate = null;
            if (currentScreen.isScrollable()/* && isFixAll == true*/)
            {
                screenAggregate = AbstractScreenAggregate.import(currentScreen);
                //Screen addScreen = currentScreen;
                Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                while (!screenAggregate.isScreenContains(addScreen))
                {
                    screenAggregate.appendScreen(addScreen);
                    parseSingleScreen(addScreen, tcIndex, levelIndex);
                    screenAggregate.moveToNextScreen(addScreen);
                    addScreen = new Screen();
                    StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                }
            }
            else
            {
                parseSingleScreen(currentScreen, tcIndex, levelIndex);
            }
            if (screenAggregate == null)
            {
                TestRuntimeAggregate.setLogScreen(currentScreen, tcIndex, levelIndex);
            }
            else
            {
                TestRuntimeAggregate.setLogScreen(screenAggregate, tcIndex, levelIndex);
            }
        }

        private void parseSingleScreen(Screen screen, int tcIndex, int levelIndex)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //camera
            Engine.EngineCamera camera = new Engine.EngineCamera();

            //save image
            string imagePath = StaticEnvironInfo.getScreenImageFileName();
            camera.capture(imagePath);
            TestRuntimeAggregate.addTransferImagePathDict(screen, imagePath, tcIndex, levelIndex);
        }
    }
}
