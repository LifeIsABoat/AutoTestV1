using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlInputFixer : AbstractControlFixerByLog
    {
        public ControlInputFixer(LogScreen logScreen)
            :base(logScreen)
        {
        }
        private bool isSoftKeyScreen(Screen rawScreen)
        {
            List<AbstractElement> elementStringList = rawScreen.getElementShipKeyList(ElementShipType.StringWithImage);

            if (elementStringList == null)
            {
                return false;
            }
            List<AbstractElement> stringElementList = null;
            //findAll the "str.Count() == 1" in the elementStringList
            stringElementList = elementStringList.FindAll(x => ((ElementString)x).str.Count() == 1);
            //if the "str.Count() == 1" 's count >=10 
            //It's means the screen is a SoftKeyScreen return true
            if (stringElementList.Count >= 10)
            {
                return true;
            }
            return false;
        }

        //Fix Input
        protected override void doFix(Screen rawScreen)
        {
            if (StaticEnvironInfo.isMenuItemTested() == false)
            {
                return;
            }
            if (false == isSoftKeyScreen(rawScreen))//if the screen isn't the SoftKeyScreen
                return;
            //new Control inputButton
            ControlInput inputButton = new ControlInput();
            //Screen Protected
            if (null == rawScreen)
                throw new FTBAutoTestException("Fix control error by rawScreen is null.");

            List<AbstractElement> imageOnlyElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageOnly);
            List<AbstractElement> imageWithStringElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageWithString);
            List<AbstractElement> elementList = new List<AbstractElement>();
            if (imageOnlyElementList == null || imageWithStringElementList == null)
                throw new FTBAutoTestException("Fix control error by empty List.");

            if (imageOnlyElementList != null)//first add imageOnlyElementList to elementList
            {
                elementList.AddRange(imageOnlyElementList);
            }
            if (imageWithStringElementList != null)//then add imageWithStringElementList to elementList
            {
                elementList.AddRange(imageWithStringElementList);
            }
            if (elementList.Count == 0)
                throw new FTBAutoTestException("Fix control error by empty elementList.");

            //fix OK Rank Button except "OK"||"Space"||number "0"
            getScreenLastRankList(elementList, rawScreen ,inputButton);

            //start fix input example:0-9||a-z||A-Z...
            List<AbstractElement> imageElementList = null;
            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
            getEnglishAndNumberList(imageElementList, rawScreen, inputButton);

            //fix remaining ImageOnlyButton,likely "delete btn"
            List<AbstractElement> onlyImageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageOnly, true);
            getnumExistButtonList(onlyImageElementList, rawScreen, inputButton);

            //add all inputButton to rawScreen
            rawScreen.addControl(inputButton);
        }//end doFix()
        
        private List<AbstractElement> getRankList(List<AbstractElement> elementList, Screen rawScreen)
        {
            List<string> strList = new List<string>(){"OK","Space","0"};
            List<AbstractElement> teraElementList = new List<AbstractElement>(elementList);
            foreach (AbstractElement originImgElement in teraElementList)
            {
                List<AbstractElement> originStrElement = rawScreen.getElementShipValueList(originImgElement);
                if (originStrElement == null)
                    continue;
                if (originStrElement != null && strList.Contains(((ElementString)originStrElement[0]).str))
                {
                    elementList.Remove(originImgElement);
                }
            }
            return elementList;
        }

        private AbstractElement getElementStr(Screen rawScreen)
        {
            List<AbstractElement> stringAndImageElementList = rawScreen.getElementShipKeyList(ElementShipType.StringWithImage);
            if (stringAndImageElementList == null)
                throw new FTBAutoTestException("Fix control error by empty elementStringList.");

            AbstractElement okButtonOnlyStr;
            okButtonOnlyStr = stringAndImageElementList.Find(o => ((ElementString)o).str == "OK");
            if (okButtonOnlyStr != null)
            {
                return rawScreen.getElementShipValueList(okButtonOnlyStr)[0];
            }

            AbstractElement stringSpaceBtnOnly;
            stringSpaceBtnOnly = stringAndImageElementList.Find(s => ((ElementString)s).str == "Space");
            if (stringSpaceBtnOnly != null)
            {
                return rawScreen.getElementShipValueList(stringSpaceBtnOnly)[0];
            }

            AbstractElement zeroBtnOnlyStr;
            zeroBtnOnlyStr = stringAndImageElementList.Find(z => ((ElementString)z).str == "0");
            if (zeroBtnOnlyStr != null)
            {
                return rawScreen.getElementShipValueList(zeroBtnOnlyStr)[0];
            }
            else
            {
                throw new FTBAutoTestException("Fix control error by empty stringAndImageElementList.");
            }
        }//end private getElementStr(Screen rawScreen)

        private void toGetListSort(List<ControlButton> imageElementList)
        {
            if (imageElementList.Count > 2)
            {
                imageElementList.Sort(delegate(ControlButton paramAnal, ControlButton paramDigital)
                {
                    //do List sort according to the rect.x
                    return paramAnal.rect.x.CompareTo(paramDigital.rect.x);
                });
            }
        }

        private void getScreenLastRankList(List<AbstractElement> elementList, Screen rawScreen, ControlInput inputButton)
        {
            if (elementList.Count > 0)//when elementList.Count > 0
            {
                Rectangle oneButtonRect = getElementStr(rawScreen).rect;
                List<AbstractElement> rankElementList = new List<AbstractElement>();
                rankElementList = getRankList(elementList, rawScreen);
                List<AbstractElement> buttonRectList = rankElementList.FindAll(R => ((ElementImage)R).rect.y == oneButtonRect.y && ((ElementImage)R).rect.h == oneButtonRect.h);
                List<ControlButton> imagetList = new List<ControlButton>();
                //fix OK Rank Button except "OK"||"Space"||number "0"
                foreach (ElementImage imageElement in buttonRectList)
                {
                    if (null == getElementStr(rawScreen))
                        continue;
                    Screen toScreen = new Screen();
                    ControlButton ButtonOnlyImage = new ControlButton();
                    //List<ControlButton> imageBtnElementList = new List<ControlButton>();
                    LogControl logOnlyImageBtn = logScreen.getItem(imageElement.id);
                    ButtonOnlyImage.statusShow = parseButtonStatus(logOnlyImageBtn);
                    ButtonOnlyImage.setIdentifyId(logOnlyImageBtn.dataHolderID);
                    ButtonOnlyImage.rect = imageElement.rect;
                    ButtonOnlyImage.imageList.Add(imageElement);
                    ButtonOnlyImage.toScreen = toScreen;
                    //ButtonOnlyImage.toScreen = toScreen;
                    //add fix result to imageOnlyButtonList
                    inputButton.imageOnlyButtonList.Add(ButtonOnlyImage);
                    ButtonOnlyImage.hasFixed();
                    continue;
                }
                toGetListSort(inputButton.imageOnlyButtonList);
            }//end if (elementList.Count > 0)
        }

        private void getEnglishAndNumberList(List<AbstractElement> imageElementList, Screen rawScreen, ControlInput inputButton)
        {
            if (null != imageElementList)
            {
                foreach (ElementImage imageElement in imageElementList)
                {
                    List<AbstractElement> stringList = null;
                    stringList = rawScreen.getElementShipValueList(imageElement);
                    if (null == stringList)
                        continue;
                    ControlButton imageWithStringbutton = new ControlButton();
                    LogControl logImageWithStringBtn = logScreen.getItem(imageElement.id);
                    imageWithStringbutton.statusShow = parseButtonStatus(logImageWithStringBtn);
                    imageWithStringbutton.setIdentifyId(logImageWithStringBtn.dataHolderID);
                    imageWithStringbutton.rect = imageElement.rect;
                    imageWithStringbutton.imageList.Add(imageElement);
                    imageWithStringbutton.stringList = stringList.ConvertAll(x => (ElementString)x);
                    //add the fix result to inputButton.imageWithStringButtonList
                    inputButton.imageWithStringButtonList.Add(imageWithStringbutton);
                    imageWithStringbutton.hasFixed();
                }//end foreach

                ControlButton okBtnStr;
                List<ControlButton> buttonList = new List<ControlButton>();
                buttonList.AddRange(inputButton.imageWithStringButtonList);
                okBtnStr = buttonList.Find(a => ((ControlButton)a).stringList[0].str == "OK");
                inputButton.specialBtnAggregate.Add(ControlSoftkeyStatus.OK, okBtnStr);

                //refuse screenType by str
                ControlButton btnSixStr;
                ControlButton btnQStr;
                btnSixStr = buttonList.Find(a => ((ControlButton)a).stringList[0].str == "6");
                btnQStr = buttonList.Find(a => ((ControlButton)a).stringList[0].str == "Q");
                if (btnSixStr != null && btnQStr == null)
                {
                    inputButton.screenType = ScreenType.NumberOnly;
                }
                else if (btnQStr != null && btnSixStr == null)
                {
                    inputButton.screenType = ScreenType.EnglishOnly;
                }
                else if (btnQStr != null && btnSixStr != null)
                {
                    inputButton.screenType = ScreenType.EnglishWithNumber;
                }
            }//end if and end Fix ImageWithString Button
        }

        private void getnumExistButtonList(List<AbstractElement> onlyImageElementList, Screen rawScreen, ControlInput inputButton)
        {
            if (onlyImageElementList != null)
            {
                //fix remaining ImageOnlyButton
                List<AbstractElement> onlyImgBtnList = null;
                for (int i = 0; i < onlyImageElementList.Count; i++)
                {
                    onlyImgBtnList = onlyImageElementList.FindAll(anyPara => anyPara.rect.y == onlyImageElementList[i].rect.y);
                    if (onlyImgBtnList.Count > 2)
                    {
                        break;
                    }
                }
                if (onlyImgBtnList.Count <= 2)
                {
                    return;
                }

                foreach (ElementImage oneImageElement in onlyImgBtnList)
                {
                    Screen toScreen = new Screen();
                    ControlButton ButtonOnlyNumExist = new ControlButton();
                    //Start Fix NumExistButton
                    LogControl logNumExistBtn = logScreen.getItem(oneImageElement.id);
                    ButtonOnlyNumExist.statusShow = parseButtonStatus(logNumExistBtn);
                    ButtonOnlyNumExist.setIdentifyId(logNumExistBtn.dataHolderID);
                    ButtonOnlyNumExist.rect = oneImageElement.rect;
                    ButtonOnlyNumExist.imageList.Add(oneImageElement);
                    ButtonOnlyNumExist.toScreen = toScreen;
                    //add fix result to inputButton.numExistButtonList
                    if (ButtonOnlyNumExist.rect != null)
                    {
                        inputButton.numExistButtonList.Add(ButtonOnlyNumExist);
                        ButtonOnlyNumExist.hasFixed();
                    }
                }
                toGetListSort(inputButton.numExistButtonList);//list sort
                inputButton.numExistButtonList.Reverse();
                inputButton.specialBtnAggregate.Add(ControlSoftkeyStatus.rightMove, inputButton.numExistButtonList[0]);
                inputButton.specialBtnAggregate.Add(ControlSoftkeyStatus.leftMove, inputButton.numExistButtonList[1]);
                inputButton.specialBtnAggregate.Add(ControlSoftkeyStatus.delete, inputButton.numExistButtonList[2]);

            }//end if (elementList.Count > 0)
        }
    }//end class ControlInputFixer()
}
