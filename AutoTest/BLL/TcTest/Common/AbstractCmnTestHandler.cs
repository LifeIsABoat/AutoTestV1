using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{
    class AbstractCmnTestHandler
    {
        protected AbstractCmnTestHandler nextHandler = null;
        protected DAL.IFTBCommonAPI treeMemory;
        protected DAL.IScreenCommonAPI screenMemory;
        public void setHandler(AbstractCmnTestHandler handler)
        {
            nextHandler = handler;
        }
        public virtual void execute()
        {
            if (nextHandler != null)
            {
                nextHandler.execute();
            }
        }

        protected virtual void clickButton(int TcIndex)
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();

            for (int levelIndex = 1; levelIndex < treeMemory.getLevelCount(TcIndex) + 1; levelIndex++)
            {
                Screen currentScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);

                AbstractScreenAggregate screenAggregate = null;
                if (currentScreen.isScrollable())
                {
                    screenAggregate = AbstractScreenAggregate.import(currentScreen);
                    Screen addScreen = currentScreen;
                    while (!screenAggregate.isScreenContains(addScreen))
                    {
                        screenAggregate.appendScreen(addScreen);
                        screenAggregate.moveToNextScreen(addScreen);
                        addScreen = new Screen();
                        StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                    }
                }

                ControlButton ftbButton = loadFTBButtonControl(levelIndex, -1, TcIndex);
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
                        int index = screenShowIterator.currentItem();
                        try
                        {
                            StaticCommandExecutorList.get(CommandList.click_b).execute(ftbButton.getIdentify());
                            break;
                        }
                        catch (FTBAutoTestException) { }
                        screenShowIterator.next();
                    }
                    if (screenShowIterator.isDone())
                    {
                        string expMsg = string.Format("Can't find [{0}] button in current screen.", ftbButton.getIdentify().btnWordsStr);
                        throw new FTBAutoTestException(expMsg);
                    }
                }
            }
        }

        protected virtual ControlButton loadFTBButtonControl(int levelIndex,
                                             int btnIndex = -1, int tcIndex = -1)
        {
            ControlButton controlButton = new ControlButton();
            //get Button Own Info
            //  button Words String
            string buttonWordsStr = treeMemory.getLevelButtonWord(levelIndex,
                                                                  btnIndex, tcIndex);
            //Button Words Can't be Null in current ftb format
            if (null == buttonWordsStr)
                return null;
            ElementString buttonWordsElement = new ElementString(buttonWordsStr);
            controlButton.stringList.Add(buttonWordsElement);
            //  button status show
            controlButton.statusShow = ControlButtonStatus.Valid;

            //get toScreen Info
            Screen toScreen = null;
            //  toScreen Id
            string toScreenId = null;
            toScreenId = treeMemory.getLevelButtonToScreenId(levelIndex, btnIndex, tcIndex);
            if (null != toScreenId && "" != toScreenId)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                toScreen.setIdentifyScreenId(toScreenId);
            }

            //  toScreen Title
            string toScreenTitleStr = treeMemory.getLevelButtonToScreenTitle(levelIndex,
                                                                         btnIndex, tcIndex);
            if (null != toScreenTitleStr && "" != toScreenId)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                ElementString toScreenTitleElement = new ElementString(toScreenTitleStr);
                toScreen.addControl(new ControlTitle(toScreenTitleElement));
                toScreen.addElement(toScreenTitleElement);
            }

            //  toScreen elementStringList
            List<string> buttonWordsList = null;
            buttonWordsList = treeMemory.getLevelButtonWords(levelIndex + 1, tcIndex);
            if (null != buttonWordsList)
            {
                if (null == toScreen)
                    toScreen = new Screen();
                foreach (string buttonWords in buttonWordsList)
                {
                    if ("" != buttonWords)
                    {
                        ElementString eleStr = new ElementString(buttonWords);
                        toScreen.addElement(eleStr);
                    }
                }
            }
            controlButton.toScreen = toScreen;

            //to press [Direct:
            List<string> helpInfoList = treeMemory.getLevelButtonHelpInfo(levelIndex, tcIndex);
            if (helpInfoList != null)
            {
                controlButton.helpInfoList = helpInfoList;
            }
            return controlButton;
        }
    }
}
