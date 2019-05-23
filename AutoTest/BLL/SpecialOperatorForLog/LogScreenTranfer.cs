using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class LogScreenTranfer
    {
        public bool transferElement(LogScreen logScreen, Screen rawScreen)
        {
            if (null == logScreen)
                throw new FTBAutoTestException("Transfer error by empty logScreen.");
            if (null == rawScreen)
                throw new FTBAutoTestException("Transfer error by empty rawScreen.");

            //get element string List
            List <LogControl> strList = logScreen.getItemList(LogControl._STRTYPE);
            if (null != strList)
            {
                foreach (LogControl str in strList)
                {
                    if (!rawScreen.isInside(str.getRect()))
                        continue;
                    ElementString eleStr = new ElementString(str.text,
                                                             str.layerID,
                                                             str.getRect());
                    rawScreen.addElement(eleStr);
                }
            }

            //get element image list
            List<LogControl> btnList = logScreen.getItemList(LogControl._BTNTYPE);
            if (null != btnList)
            {
                foreach (LogControl btn in btnList)
                {
                    if (!rawScreen.isInside(btn.getRect()))
                        continue;
                    ElementImage eleImg = new ElementImage(btn.getRect(),
                                                           btn.layerID);
                    rawScreen.addElement(eleImg);
                }
            }

            //get Screen ID
            rawScreen.setIdentifyScreenId(logScreen.scrid);

            return true;
        }

        public bool transferElementShip(LogScreen logScreen, Screen screen)
        {
            if (null == logScreen)
                throw new FTBAutoTestException("Transfer error by empty logScreen.");

            //loop String Element List
            List<AbstractElement> elementList;
            elementList = screen.getElementList(typeof(ElementString));
            if (null != elementList)
            {
                foreach (ElementString eleStr in elementList)
                {
                    //get the LogString of ElementString
                    LogControl logStr = logScreen.getItem((ushort)eleStr.id);
                    if (null == logStr)
                    {
                        screen.addElementShip(eleStr, null);
                        continue;
                    }
                    //get the LogButtonList of LogString
                    List<LogControl> logBtnList;
                    logBtnList = logScreen.getItemList(LogControl._BTNTYPE,
                                                       logStr.dataHolderID);
                    if (null == logBtnList)
                    {
                        screen.addElementShip(eleStr, null);
                        continue;
                    }
                    //get the ElementString of LogButtonList
                    List<AbstractElement> eleImgList = new List<AbstractElement>();
                    foreach (LogControl logBtn in logBtnList)
                    {
                        ElementImage eleImg = (ElementImage)screen.getElement(logBtn.layerID);
                        if (null != eleImg)
                            eleImgList.Add(eleImg);
                    }
                    if (0 == eleImgList.Count)
                        screen.addElementShip(eleStr, null);
                    else
                        screen.addElementShip(eleStr, eleImgList);
                }
            }

            //loop Image Element List
            elementList = screen.getElementList(typeof(ElementImage));
            if (null != elementList)
            {
                foreach (ElementImage eleImg in elementList)
                {
                    //get the LogBtn of ElementImage
                    LogControl logBtn = logScreen.getItem((ushort)eleImg.id);
                    if (null == logBtn)
                    {
                        screen.addElementShip(eleImg, null);
                        continue;
                    }
                    //get the LogStringList of LogBtn
                    List<LogControl> logStrList;
                    logStrList = logScreen.getItemList(LogControl._STRTYPE,
                                                       logBtn.dataHolderID);
                    if (null == logStrList)
                    {
                        screen.addElementShip(eleImg, null);
                        continue;
                    }
                    //get the ElementImage of LogStringList
                    List<AbstractElement> eleStrList = new List<AbstractElement>();
                    foreach (LogControl logStr in logStrList)
                    {
                        ElementString eleStr = (ElementString)screen.getElement(logStr.layerID);
                        if (null != eleStr)
                            eleStrList.Add(eleStr);
                    }
                    if (0 == eleStrList.Count)
                        screen.addElementShip(eleImg, null);
                    else
                        screen.addElementShip(eleImg, eleStrList);
                }
            }

            //to fix copy screen 
            connectImgAndStr(screen);
            return true;
        }

        private void connectImgAndStr(Screen screen)
        {
            List<AbstractElement> imageElementList = screen.getElementShipKeyList(ElementShipType.ImageOnly);
            List<AbstractElement> stringElementList = screen.getElementShipKeyList(ElementShipType.StringOnly);

            if(imageElementList != null && stringElementList != null)
            {
                foreach(AbstractElement oneImageElement in imageElementList)
                {
                    Rectangle imgRect = oneImageElement.rect;
                    int imgCenter = imgRect.x + imgRect.x + imgRect.w;
                    foreach (AbstractElement oneStringElement in stringElementList)
                    {
                        Rectangle strRect = oneStringElement.rect;
                        int strCenter = strRect.x + strRect.x + strRect.w;
                        int yDiff = imgRect.y - (strRect.y + strRect.h);
                        if (imgCenter == strCenter && Math.Abs(yDiff) < 5)// to do 
                        {
                            screen.removeElementShip(oneImageElement);
                            screen.removeElementShip(oneStringElement);
                            List<AbstractElement> eleStrList = new List<AbstractElement>();
                            eleStrList.Add(oneStringElement);
                            screen.addElementShip(oneImageElement, eleStrList);
                            List<AbstractElement> eleImgList = new List<AbstractElement>();
                            eleImgList.Add(oneImageElement);
                            screen.addElementShip(oneStringElement, eleImgList);
                            break;
                        }
                    }
                }
            }
        }
    }

}
