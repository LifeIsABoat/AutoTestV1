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
    class OneLevelCmnTestNextLevelTransfer : AbstractCmnTestHandler
    {
        //点击画面上的文言进行画面迁移的链
        public OneLevelCmnTestNextLevelTransfer()
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
        }
        public override void execute()
        {
            object trmpcurrentScreen = TestRuntimeAggregate.getLogScreen(TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            AbstractScreenAggregate screenAggregate = null;
            if (trmpcurrentScreen is AbstractScreenAggregate)
            {
                screenAggregate = (AbstractScreenAggregate)trmpcurrentScreen;
            }

            ControlButton ftbButton = loadFTBButtonControl(TestRuntimeAggregate.getCurrentLevelIndex());
            Screen currentRawScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
            if (currentRawScreen.getIdentify().scrId == "SCRN_SCAN_EMLSV_DESTINATION"
                && ftbButton.getIdentify().btnWordsStr == "Next")
            {
                clickButtonForScanToEmail(currentRawScreen, "Manual");
                StaticCommandExecutorList.get(CommandList.click_k).execute(Machine.MFCTPKeyCode.TEN1_KEY);
                StaticCommandExecutorList.get(CommandList.click_k).execute(Machine.MFCTPKeyCode.TEN1_KEY);
                Screen cawScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(cawScreen);
                clickButtonForScanToEmail(cawScreen, "OK");
                System.Threading.Thread.Sleep(1200);
                Screen scanToEmailScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_f).execute(scanToEmailScreen);
                StaticCurrentScreen.set(scanToEmailScreen);
            }
            string popResult = Regex.Replace(ftbButton.getIdentify().btnWordsStr, @"[^a-zA-Z0-9]+", "");
            if (String.Equals(popResult, "SelectProfilename", StringComparison.CurrentCultureIgnoreCase)
                || String.Equals(popResult, "SelectPC", StringComparison.CurrentCultureIgnoreCase))
            {
                Screen scanScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(scanScreen);
                List<AbstractElement> StringOnlyList = scanScreen.getElementShipKeyList(ElementShipType.StringOnly, true);
                AbstractElement btnWordsStr = StringOnlyList.Find(o => ((ElementString)o).str == "Select your Profile.");
                AbstractElement selectPCStr = StringOnlyList.Find(o => ((ElementString)o).str == "Select your PC.");
                int index = 0;
                foreach (AbstractElement one in StringOnlyList)
                {
                    if (((ElementString)one).str.Contains("No Profile found.")
                        || ((ElementString)one).str.Contains("No PC found."))
                    {
                        index = -1;
                        break;
                    }
                }
                if (index == -1)
                {
                    throw new FTBAutoTestException("テスト環境で事前準備しない!!!");
                }
                if ((btnWordsStr != null || selectPCStr != null) && index == 0)
                {
                    List<AbstractElement> stringWithImageList = scanScreen.getElementShipKeyList(ElementShipType.StringWithImage, true);
                    toGetListSort(stringWithImageList);
                    Position pos = stringWithImageList[stringWithImageList.Count - 1].rect.getCenter();
                    StaticCommandExecutorList.get(CommandList.click_p).execute(pos);
                }
                else if ((btnWordsStr != null || selectPCStr != null) && index == 0)
                {
                    return;
                }
                return;
            }

            if (null == screenAggregate)
            {
                StaticCommandExecutorList.get(CommandList.click_b).execute(ftbButton.getIdentify());
                setPopUpScreen();
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
                        StaticCommandExecutorList.get(CommandList.click_b).execute(ftbButton.getIdentify());
                        setPopUpScreen();
                        break;
                    }
                    catch (FTBAutoTestException)
                    {
                        //Click button Fail isn't exception in this situation
                    }
                    screenShowIterator.next();
                }
                if (screenShowIterator.isDone())
                {
                    string expMsg = string.Format("Can't find [{0}] button in current screen.", ftbButton.getIdentify().btnWordsStr);
                    throw new FTBAutoTestException(expMsg);
                }
            }
            base.execute();
        }
        private void toGetListSort(List<AbstractElement> imageElementList)
        {
            if (imageElementList.Count >= 2)
            {
                imageElementList.Sort(delegate (AbstractElement paramAnal, AbstractElement paramDigital)
                {
                    return paramAnal.rect.y.CompareTo(paramDigital.rect.y);
                });
            }
        }
        private void setPopUpScreen()
        {
            Screen mideScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(mideScreen);
            List<AbstractElement> imageElementList = mideScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
            if (imageElementList != null)
            {
                AbstractCommandExecutor tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
                if (imageElementList.Count >= 3) { }
                else if (imageElementList.Count == 2)
                {
                    imageElementList.Sort((img1, img2) =>
                    {
                        return img1.rect.x - img2.rect.x;
                    });
                    List<string> strList = new List<string>() { "OK", "No", "Cancel" };
                    List<AbstractElement> teraElementList = new List<AbstractElement>(imageElementList);
                    foreach (AbstractElement originImgElement in teraElementList)
                    {
                        List<AbstractElement> originStrElement = mideScreen.getElementShipValueList(originImgElement);
                        if (originStrElement == null)
                            continue;
                        if (originStrElement != null)
                        {
                            foreach (string str in strList)
                            {
                                if (String.Equals(str, ((ElementString)originStrElement[0]).str, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Position btnPos = imageElementList[1].rect.getCenter();
                                    tpClicker.execute(btnPos);
                                    System.Threading.Thread.Sleep(100);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (imageElementList.Count == 1)
                {
                    List<string> strList = new List<string>() { "OK", "No", "Cancel" };
                    List<AbstractElement> originStrElement = mideScreen.getElementShipValueList(imageElementList[0]);
                    foreach (string str in strList)
                    {
                        if (String.Equals(str, ((ElementString)originStrElement[0]).str, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Position btnPos = imageElementList[0].rect.getCenter();
                            tpClicker.execute(btnPos);
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    }
                }
            }
        }

        private void clickButtonForScanToEmail(Screen currentRawScreen, string buttonStr)
        {
            List<AbstractElement> StringWithImageElementList = currentRawScreen.getElementShipKeyList(ElementShipType.StringWithImage, true);
            AbstractElement btnWordsStr = StringWithImageElementList.Find(o => ((ElementString)o).str == buttonStr);
            if (btnWordsStr != null)
            {
                Position pos = currentRawScreen.getElementShipValueList(btnWordsStr)[0].rect.getCenter();
                StaticCommandExecutorList.get(CommandList.click_p).execute(pos);
            }
        }

    }
}
