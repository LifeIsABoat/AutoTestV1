using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    //Fix Button
    class ControlButtonFixerByLog : AbstractControlFixerByLog
    {
        public ControlButtonFixerByLog(LogScreen logScreen,
                                       ControlFixerCondition condition = null)
            :base(logScreen,condition)
        {
        }
        
        protected override void doFix(Screen rawScreen)
        {
            //Screen Protected
            if (null == rawScreen)
                throw new FTBAutoTestException("Fix control error by input empty screen.");

            //Correcte Icon
            List<AbstractElement> imageElementList = null;
            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageOnly, true);
            if (null != imageElementList)
            {
                foreach (ElementImage imageElement in imageElementList)
                {
                    Screen toScreen = new Screen();
                    if (rawScreen.getIdentify().scrId == "SCRN_FAX"
                        || rawScreen.getIdentify().scrId == "SCRN_COPY_START")
                    {
                        /*do Nothing*/
                    }
                    else
                    {
                        if (imageElement.rect.x == imageElement.rect.y
                            && imageElement.rect.w == imageElement.rect.h
                            && imageElement.rect.getCenter().x == 29
                            && imageElement.rect.getCenter().y == 29)
                        {
                            /*do Nothing*/
                        }
                        else
                        {
                            //Icon Or Arrow
                            if (false == isIconByClick(imageElement, rawScreen, toScreen))
                                continue;
                        }
                    }

                    //Icon
                    //create control button
                    ControlButton button = new ControlButton();
                    LogControl logBtn = logScreen.getItem(imageElement.id);
                    button.statusShow = parseButtonStatus(logBtn);
                    button.setIdentifyId(logBtn.dataHolderID);
                    button.rect = imageElement.rect;
                    button.imageList.Add(imageElement);
                    button.toScreen = toScreen;
                    button.hasFixed();
                    rawScreen.addControl(button);
                }
            }

            //Correcte String Button
            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
            if (null != imageElementList)
            {
                foreach (ElementImage imageElement in imageElementList)
                {
                    //Button Or Tag
                    if (false == isStringButtonByFixTagFirst(imageElement))
                        continue;

                    //Button
                    List<AbstractElement> stringList = null;
                    stringList = rawScreen.getElementShipValueList(imageElement);
                    if (null == stringList)
                        continue;

                    //sort by position
                    stringList.Sort((str1, str2) =>
                    {
                        return str1.rect.y - str2.rect.y;
                    });

                    //create control button
                    ControlButton button = new ControlButton();

                    LogControl logBtn = logScreen.getItem(imageElement.id);
                    button.statusShow = parseButtonStatus(logBtn);
                    button.setIdentifyId(logBtn.dataHolderID);

                    button.rect = imageElement.rect;
                    button.imageList.Add(imageElement);
                    button.stringList = stringList.ConvertAll(x => (ElementString)x);
                    button.hasFixed();
                    rawScreen.addControl(button);
                }
            }
        }

        private bool isStringButtonByClick(ElementImage imageElement,
                                           Screen preScreen,
                                           Screen toScreen)
        {
            //init param
            Position pos = imageElement.rect.getCenter();
            //fix by check
            return isMoveToNewScreenByClick(pos, preScreen, toScreen);
        }

        private bool isStringButtonByFixTagFirst(ElementImage imageElement)
        {
            return true;
        }

        private bool isIconByClick(ElementImage imageElement,
                                   Screen preScreen,
                                   Screen toScreen)
        {
            //init param
            Position pos = imageElement.rect.getCenter();
            //fix by check
            bool isScreenChanged = isMoveToNewScreenByClick(pos, preScreen, toScreen);

            //to fix copy screen
            if(isScreenChanged == false)
            {
                List<AbstractControl> arrorList = preScreen.getControlList(typeof(AbstractControlArrow));
                if (arrorList != null && arrorList[0] is ControlArrowLeftRight)
                {
                    if (pos.x < Screen.screenSize.w / 2)
                    {
                        Position p = ((AbstractControlArrow)arrorList[0]).getLastArrow().rect.getCenter();
                        tpClicker.execute(p);
                    }
                    else if (pos.x > Screen.screenSize.w / 2)
                    {
                        Position p = ((AbstractControlArrow)arrorList[0]).getFirstArrow().rect.getCenter();
                        tpClicker.execute(p);
                    }
                }
            }

            return isScreenChanged;
        }
    }
}
