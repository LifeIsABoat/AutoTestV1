using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlOptionIconFixerByLog : AbstractControlFixerByLog
    {
        public ControlOptionIconFixerByLog(LogScreen logScreen,
                                       ControlFixerCondition condition = null)
            : base(logScreen, condition)
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

            if (isOptionIcon(imageElementList) == false)
            {
                return;
            }

            if (null != imageElementList)
            {
                fixOptionIconList(imageElementList, rawScreen);
            }
            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageOnly, true);
            if (null != imageElementList)
            {
                fixUnknowIcon(imageElementList, rawScreen);
            }
        }
        private bool isOptionIcon(List<AbstractElement> imageElementList)
        {
            if (imageElementList != null)
            {
                foreach (AbstractElement currentImgElement in imageElementList)
                {
                    int count = imageElementList.Count(ele => (ele.rect.y == currentImgElement.rect.y &&
                                                        ele.rect.w == currentImgElement.rect.w &&
                                                        ele.rect.h == currentImgElement.rect.h));
                    if (count == 5 || count == 11)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void fixOptionIconList(List<AbstractElement> imageElementList, Screen rawScreen)
        {
            List<AbstractElement> optionIconList = null;
            foreach (AbstractElement currentImgElement in imageElementList)
            {
                List<AbstractElement> tempOptionIconList = imageElementList.FindAll(ele => (ele.rect.y == currentImgElement.rect.y &&
                                                        ele.rect.w == currentImgElement.rect.w &&
                                                        ele.rect.h == currentImgElement.rect.h));

                if (tempOptionIconList.Count == 11 || tempOptionIconList.Count == 5)
                {
                    optionIconList = tempOptionIconList;
                    break;
                }
                else
                {
                    tempOptionIconList.Clear();
                }
            }
            if (optionIconList != null)
            {
                optionIconList.Sort(delegate (AbstractElement x, AbstractElement y)
                {
                    return x.rect.x.CompareTo(y.rect.x);
                });

                ElementString strEle = findNumElement(rawScreen.getElementShipKeyList(ElementShipType.StringOnly, true));
                for (int i = 0; i < optionIconList.Count; i++)
                {
                    //Icon
                    //create control button
                    ControlButton button = new ControlButton();
                    LogControl logBtn = logScreen.getItem(optionIconList[i].id);
                    button.statusShow = parseButtonStatus(logBtn);
                    button.setIdentifyId(logBtn.dataHolderID);
                    button.rect = optionIconList[i].rect;
                    button.imageList.Add((ElementImage)optionIconList[i]);
                    button.toScreen = null;
                    int num1 = i - optionIconList.Count / 2;
                    int num2 = num1 * 10;
                    button.helpInfoList.Add(num1 > 0 ? ("+" + num1.ToString()) : num1.ToString());
                    button.helpInfoList.Add(num2 > 0 ? ("+" + num2.ToString()) : num2.ToString());
                    if (strEle != null && button.helpInfoList.Contains(((ElementString)strEle).str))
                    {
                        button.stringList.Add(strEle);
                    }
                    button.hasFixed();
                    rawScreen.addControl(button);
                }
            }
        }

        private ElementString findNumElement(List<AbstractElement> strElementList)
        {
            if (strElementList == null)
            {
                return null;
            }
            List<AbstractElement> tempOptionIconList = null;
            foreach (AbstractElement Element in strElementList)
            {
                tempOptionIconList = strElementList.FindAll(ele => (ele.rect.y == Element.rect.y &&
                                                        ele.rect.w == Element.rect.w &&
                                                        ele.rect.h == Element.rect.h));
                if (tempOptionIconList.Count == 2)
                {
                    break;
                }
                else
                {
                    tempOptionIconList = null;
                }
            }
            if (tempOptionIconList != null)
            {
                int basex = tempOptionIconList[0].rect.x + tempOptionIconList[1].rect.x + tempOptionIconList[0].rect.w;
                foreach (AbstractElement Element in strElementList)
                {
                    int diff = 2 * Element.rect.x + Element.rect.w - basex;
                    if (diff >= -1 && diff <= 1)
                    {
                        return Element as ElementString;
                    }
                }
            }
            return null;
        }
        private void fixUnknowIcon(List<AbstractElement> imageElementList, Screen rawScreen)
        {
            //Correcte String Button
            foreach (ElementImage imageElement in imageElementList)
            {
                //Icon
                //create control button
                ControlButton button = new ControlButton();
                LogControl logBtn = logScreen.getItem(imageElement.id);
                button.statusShow = parseButtonStatus(logBtn);
                button.setIdentifyId(logBtn.dataHolderID);
                button.rect = imageElement.rect;
                button.imageList.Add(imageElement);
                button.toScreen = null;
                button.hasFixed();
                rawScreen.addControl(button);
            }
        }     
    }
}
