using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;
using System.Text.RegularExpressions;

namespace Tool.Command
{
    class ListRawScreenExecutorMFCTPByLog : AbstractListRawScreenExecutorMFCTP
    {
        private static Dictionary<ScreenIdentify, List<AbstractElement>> blackList = new Dictionary<ScreenIdentify, List<AbstractElement>>();
        private static Dictionary<ScreenIdentify, List<AbstractElement>> whiteList = new Dictionary<ScreenIdentify, List<AbstractElement>>();
        private static Dictionary<string, List<LogControl>> virtualList = new Dictionary<string, List<LogControl>>();
        private static int maxFailedCnt = 0;
        private LogScreen logScreen;
        private LogReaderMFCTPByIO logReader;
        private LogParserMFCTP logParser;
        private LogScreenTranfer logTransfer;

        public static void addBlackList(ScreenIdentify screenIdentify, List<AbstractElement> elementList)
        {
            if (null == screenIdentify)
                throw new FTBAutoTestException("Add blacklist error by null screenIdentify.");
            if (null == elementList)
                throw new FTBAutoTestException("Add blacklist error by null elementList.");
            blackList.Add(screenIdentify, elementList);
        }
        public static void addWhiteList(ScreenIdentify screenIdentify, List<AbstractElement> elementList)
        {
            if (null == screenIdentify)
                throw new FTBAutoTestException("Add blacklist error by null screenIdentify.");
            //if (null == elementList)
            //    throw new FTBAutoTestException("Add blacklist error by null elementList.");
            whiteList.Add(screenIdentify, elementList);
        }
        public static void addVirtualList(string scrId, List<LogControl> controlList)
        {
            if (null == scrId)
                throw new FTBAutoTestException("Add VirtualList error by null scrId.");
            if (null == controlList)
                throw new FTBAutoTestException("Add VirtualList error by null controlList.");
            virtualList.Add(scrId, controlList);
        }
        public static Dictionary<ScreenIdentify, List<AbstractElement>> getBlackList()
        {
            return blackList;
        }
        public static void runBlackList(Screen screen)
        {
            foreach (KeyValuePair<ScreenIdentify, List<AbstractElement>> blackItem in blackList)
            {
                if (screen.getIdentify().Equals(blackItem.Key))
                {
                    foreach (AbstractElement blackElement in blackItem.Value)
                    {
                        AbstractElement tmpElement = screen.findElement(blackElement);
                        if (null == tmpElement)
                            continue;
                        List<AbstractElement> valueList = screen.getElementShipValueList(tmpElement);
                        if (null != valueList)
                        {
                            foreach (AbstractElement value in valueList)
                            {
                                screen.removeElementShip(value);
                                screen.removeElement(value);
                            }
                        }
                        screen.removeElementShip(tmpElement);
                        screen.removeElement(tmpElement);
                    }
                }
            }
        }
        public static void runWhiteList(Screen screen)
        {
            foreach (KeyValuePair<ScreenIdentify, List<AbstractElement>> whiteItem in whiteList)
            {
                if (screen.getIdentify().Equals(whiteItem.Key))
                {
                    List<AbstractElement> elementList = new List<AbstractElement>(screen.getElementList());
                    foreach (AbstractElement element in elementList)
                    {
                        if (!whiteItem.Value.Contains(element))
                        {
                            screen.removeElement(element);
                            screen.removeElementShip(element);
                        }
                    }
                }
            }
        }
        public static void runVirtualList(LogScreen screen)
        {
            foreach (KeyValuePair<string, List<LogControl>> virtualItem in virtualList)
            {
                if(screen.scrid == virtualItem.Key)
                {
                    foreach (LogControl virtualElement in virtualItem.Value)
                    {
                        LogControl tmpElement = screen.getItem(virtualElement.layerID);
                        if (null == tmpElement)
                            screen.ctls.Add(virtualElement);
                    }
                }
            }
        }

        public ListRawScreenExecutorMFCTPByLog(LogScreen logScreen)
        {
            //Initial
            this.logScreen = logScreen;

            //Initial operator
            this.logReader = new LogReaderMFCTPByIO();
            this.logParser = new LogParserMFCTP();
            this.logTransfer = new LogScreenTranfer();
        }
        protected override void getScreen(Screen screen)
        {
            //get logScreen
            for (int i = 8; i > 0; i--)
            {
                try
                {
                    logParser.parse(logReader.read(), logScreen);
                    maxFailedCnt = 0;
                    break;
                }
                catch (FTBAutoTestException ex)
                {
                    if (i > 1 && maxFailedCnt < 20)
                    {
                        maxFailedCnt++;
                        continue;
                    }
                    else
                        throw ex;
                }
            }

            //for some fix difficultly screen
            runVirtualList(logScreen);

            //transfer LogScreen to Screen
            logTransfer.transferElement(logScreen, screen);

            //prepair for Screen Element Ship
            logTransfer.transferElementShip(logScreen, screen);

            //remove element in screen black list
            runBlackList(screen);

            //remove element that is not in the screen whitelist
            runWhiteList(screen);

            //remove time in home&&menu screen
            runHomeAndMenuTimeBlackList(screen);
        }

        public static void runHomeAndMenuTimeBlackList(Screen screen)
        {
            string muneTopScreenId = "SCRN_MENU";
            string homeScreenId = "SCRN_STANDBY";
            //string pthnYearMonthDay = @"(\d{1,2}).(\d{1,2}).(\d{4})";
            string popHourAndMin = @"(\d{1,2}):(\d{1,2})";

            if (screen.getIdentify().scrId == homeScreenId || screen.getIdentify().scrId == muneTopScreenId)
            {
                //get NowScreen's elementStringList
                List<ElementString> elementStringList = screen.getElementList(typeof(ElementString)).ConvertAll(e => (ElementString)e);
                if (elementStringList == null)
                    throw new FTBAutoTestException("Fix control error by empty elementStringList.");

                ////if NowScreen have str:"Please Wait",remove it and It's img.
                AbstractElement pleaseWaitStr = elementStringList.Find(o => ((ElementString)o).str == "Please Wait");
                if(pleaseWaitStr != null)
                {
                    screen.removeElementShip(pleaseWaitStr);
                    screen.removeElement(pleaseWaitStr);
                }

                ////don't write "i<=".Because if you write this, it will throw Exception.
                for (int i = 0; i < elementStringList.Count(); i++)
                {
                    ////if NowScreen have timeStr,remove it and It's img.
                    if (Regex.IsMatch((((ElementString)elementStringList[i]).str), popHourAndMin, RegexOptions.IgnoreCase))
                    {
                        List<AbstractElement> valueList = screen.getElementShipValueList(elementStringList[i]);
                        if (null != valueList)
                        {
                            foreach (AbstractElement value in valueList)
                            {
                                screen.removeElementShip(value);
                                screen.removeElement(value);
                            }
                        }
                        screen.removeElementShip(elementStringList[i]);
                        screen.removeElement(elementStringList[i]);
                    }
                }
            }
        }//end runHomeTimeBlackList()

    }//end class
}
