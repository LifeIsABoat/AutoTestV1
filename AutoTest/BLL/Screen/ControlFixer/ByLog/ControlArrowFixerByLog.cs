using Tool.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlArrowFixerByLog : AbstractControlFixerByLog
    {
        private enum ArrowType
        {
            LeftRight,
            UpDown
        }

        public ControlArrowFixerByLog(LogScreen logScreen,
                                      ControlFixerCondition condition = null)
            : base(logScreen, condition)
        {
            
        }

        protected override void doFix(Screen rawScreen)
        {
            if (rawScreen.getIdentify().scrId == "SCRN_FAX"
                || rawScreen.getIdentify().scrId == "SCRN_COPY_START")
            {
                return;
            }
            //Screen Protected
            if (null == rawScreen)
                throw new FTBAutoTestException("Construct Fix control error by input empty screen.");

            //Correcte Arrow
            List<AbstractElement> imageElementList = null;
            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageOnly, true);
            if (null != imageElementList)
            {
                imageElementList = getLikelyArrowList(imageElementList);
                if (imageElementList == null || imageElementList.Count == 0)
                {
                    return;
                }

                List<ControlButton> arrowList = new List<ControlButton>();

                getArrowListByClick(imageElementList, arrowList, rawScreen);

                //Arrow Control Exsited
                if (0 != arrowList.Count)
                {
                    AbstractControl controlArrow;
                    switch (calibrateArrowType(arrowList))
                    {
                        case ArrowType.LeftRight:
                            controlArrow = new ControlArrowLeftRight(arrowList);
                            break;
                        case ArrowType.UpDown:
                            controlArrow = new ControlArrowUpDown(arrowList);
                            break;
                        default:
                            //Won't run to here
                            throw new FTBAutoTestException("Arrow Fixer condition meet unknow error.");
                    }
                    controlArrow.hasFixed();
                    rawScreen.addControl(controlArrow);
                }
            }
        }

        private List<AbstractElement> getLikelyArrowList(List<AbstractElement> imageElementList)
        {
            List<AbstractElement> tempImageElementList = new List<AbstractElement>();

            foreach(AbstractElement currentImgElement in imageElementList)
            {
                int count = 0;
                foreach (ElementImage tempImgElement in imageElementList)
                {
                    Rectangle diff_rect = currentImgElement.rect - tempImgElement.rect;
                    //left right
                    if ((0 != diff_rect.x && 0 == diff_rect.y)
                        && (0 == diff_rect.w)
                        && (0 == diff_rect.h))
                    {
                        //Arrow
                        count++;
                    }
                    //up down
                    else if ((0 == diff_rect.x && 0 != diff_rect.y)
                        && (0 == diff_rect.w)
                        && (0 == diff_rect.h))
                    {
                        return imageElementList;
                    }
                }
                if(count == 1)
                {
                    tempImageElementList.Add(currentImgElement);
                }
            }
            if (tempImageElementList.Count > 2)
            {
                tempImageElementList.Sort(delegate (AbstractElement x, AbstractElement y)
                {
                    return x.rect.x.CompareTo(y.rect.x);
                });
                List<AbstractElement> retImageElementList = new List<AbstractElement>();
                retImageElementList.Add(tempImageElementList[0]);
                retImageElementList.Add(tempImageElementList[tempImageElementList.Count - 1]);
                return retImageElementList;
            }

            return tempImageElementList;
            
        }

        //Fix Arrow Type
        private ArrowType calibrateArrowType(List<ControlButton> arrowList)
        {
            if (2 != arrowList.Count)
            {
                string expMsg = string.Format("Fix arrowType error by invalid arrowList.Count({0}).", arrowList.Count);
                throw new FTBAutoTestException(expMsg);
            }

            int diff_x, diff_y;
            diff_x = arrowList[0].rect.x - arrowList[1].rect.x;
            diff_y = arrowList[0].rect.y - arrowList[1].rect.y;

            //0 left/up 1 right/down
            //sort by position

            //sort by x position
            if (System.Math.Abs(diff_x) > System.Math.Abs(diff_y))
            {
                //x1  x0
                if (diff_x > 0)
                    arrowList.Reverse();
                return ArrowType.LeftRight;
            }
            //sort by y position
            else
            {
                //y1
                //y0
                if (diff_y > 0)
                    arrowList.Reverse();
                return ArrowType.UpDown;
            }
        }

        private ControlButton getArrowInfo(ElementImage imageElement)
        {
            //Arrow
            //create control button
            ControlButton arrowInfo = new ControlButton();
            LogControl logBtn = logScreen.getItem(imageElement.id);
            arrowInfo.statusShow = parseButtonStatus(logBtn);
            arrowInfo.setIdentifyId(logBtn.dataHolderID);
            arrowInfo.rect = imageElement.rect;
            arrowInfo.imageList.Add(imageElement);

            return arrowInfo;
        }

        private ControlButton getAnotherArrow(Screen screen, 
                                              ControlButton currentArrow)
        {
            List<AbstractElement> imageElementList = null;
            imageElementList = screen.getElementShipKeyList(ElementShipType.ImageOnly);
            if (null == imageElementList)
                throw new FTBAutoTestException("Arrow fixer meet unknow error. elementShipKeyList is null");
            foreach (ElementImage imageElement in imageElementList)
            {
                Rectangle diff_rect = currentArrow.rect - imageElement.rect;
                if ((0 == diff_rect.x ^ 0 == diff_rect.y)
                    && (0 == diff_rect.w)
                    && (0 == diff_rect.h))
                {
                    //Arrow
                    return getArrowInfo(imageElement);
                }
            }
            return null;
        }

        //is Arrow by fix Button(Icon) First
        private bool isArrowByFixButtonFirst(ElementImage imageElement)
        {
            return true;
        }

        private void getArrowListByFixButtonFirst(List<AbstractElement> imageElementList,
                                                  List<ControlButton> arrowList,
                                                  Screen rawScreen)
        {
            foreach (ElementImage imageElement in imageElementList)
            {
                //Strategy by fix Button first
                if (false == isArrowByFixButtonFirst(imageElement))
                    continue;
                //Arrow
                ControlButton arrowInfo = getArrowInfo(imageElement);
                ControlButton anotherArrowInfo = getAnotherArrow(rawScreen, arrowInfo);

                //the pair Arrow is exsited
                if (null != anotherArrowInfo)
                {
                    arrowList.Add(arrowInfo);
                    arrowList.Add(anotherArrowInfo);
                    if (2 != imageElementList.Count)
                        throw new FTBAutoTestException("Fix arrow error by unexpected imageElementList count.");
                    break;
                }
            }
        }

        //is Arrow by Click
        private bool isArrowByClick(ElementImage imageElement,
                                    Screen preScreen,
                                    Screen toScreen)
        {
            //init param
            Position pos = imageElement.rect.getCenter();
            //fix by check
            return !isMoveToNewScreenByClick(pos, preScreen, toScreen);
        }

        private void getArrowListByClick(List<AbstractElement> imageElementList,
                                         List<ControlButton> arrowList,
                                         Screen rawScreen)
        {
            foreach (ElementImage imageElement in imageElementList)
            {
                //Strategy by Click
                Screen toScreen = new Screen();
                if (false == isArrowByClick(imageElement, rawScreen, toScreen))
                    continue;

                ControlButton arrowInfo = getArrowInfo(imageElement);
                ControlButton tmpArrowInfo = getAnotherArrow(toScreen, arrowInfo);
                //the pair of Arrow isn't exsited
                if (null == tmpArrowInfo)
                    throw new FTBAutoTestException("Arrow fixer meet unknow error. AnotherArrow is null.");
                //the pair of Arrow can't find in previous screen 
                AbstractElement tmpImageElement;
                tmpImageElement = imageElementList.Find(ele => ele.id == tmpArrowInfo.imageList[0].id);
                if (null == tmpImageElement)
                    throw new FTBAutoTestException("Arrow fixer meet unknow error. can not find tmpArrowInfo id.");

                ControlButton anotherArrowInfo = getArrowInfo((ElementImage)tmpImageElement);

                //check for screen change
                if (!rawScreen.EqualsByStringList(toScreen))
                {
                    //back to previous list status
                    tpClicker.execute(anotherArrowInfo.rect.getCenter());
                    System.Threading.Thread.Sleep(500);
                    Screen bakScreen = new Screen();
                    rawScreenLoader.execute(bakScreen);
                    ModelInfo modelInfo = new ModelInfo();
                    modelInfo.loadModelInfo(StaticEnvironInfo.getModelInfoFullFileName());
                    if ((modelInfo.screenSize.w == 800 && modelInfo.screenSize.h == 480)
                        && StaticEnvironInfo.isMenuItemTested() == false)
                    {
                        //Temp(Fax,Copy,Scan)テスト時
                        //do nothing
                    }
                    else
                    {
                        if (!rawScreen.EqualsByStringList(bakScreen))
                        {
                            throw new FTBAutoTestException("Arrow fixer meet unknow error. StringList is not Equal.");
                        }
                    }
                }

                arrowList.Add(arrowInfo);
                arrowList.Add(anotherArrowInfo);
                if (2 != arrowList.Count)
                    throw new FTBAutoTestException("Fix arrow error by unexpected arrowList count.");
                break;
            }
        }
    }
}
