using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlTagFixerByLog : AbstractControlFixerByLog
    {
        private static List<string> tagWordsList;

        public static void setTagWords(List<string> tagWordsList)
        {
            if (null == tagWordsList || 0 == tagWordsList.Count)
                throw new FTBAutoTestException("Set tag words error by empty or null tagWordsList.");
            ControlTagFixerByLog.tagWordsList = tagWordsList;
        }

        public static List<string> getTagWords()
        {
            return tagWordsList;
        }

        public ControlTagFixerByLog(LogScreen logScreen,
                                    ControlFixerCondition condition = null)
            : base(logScreen, condition)
        {
            if (null == tagWordsList)
                throw new FTBAutoTestException("Create tagFixer error, tagWordsList is null.");
        }

        //is Tag by Click
        private bool isTagByClick(ElementImage imageElement, 
                                  Screen preScreen,
                                  Screen toScreen)
        {
            //init param
            Position pos = imageElement.rect.getCenter();
            //fix by check
            return !isMoveToNewScreenByClick(pos, preScreen, toScreen);
        }

        //is Tag by Words List
        private bool isTagByWordsList(ElementImage imageElement,Screen rawScreen)
        {
            List<AbstractElement> tagStringlist = null;
            tagStringlist = rawScreen.getElementShipValueList(imageElement);
            //Tag Must has single String in Current Machine
            if (null == tagStringlist || tagStringlist.Count != 1)
                return false;

            if (tagWordsList.Contains(((ElementString)tagStringlist[0]).str))
                return true;

            return false;
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
                throw new FTBAutoTestException("Fix control error by input empty screen.");

            //Correcte Tag
            List<AbstractElement> imageElementList = null;

            imageElementList = rawScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
            if (null != imageElementList)
            {
                List<ControlButton> tagInfoList = new List<ControlButton>();
                foreach (ElementImage imageElement in imageElementList)
                {
                    //Tag Or Button
                    Screen toScreen = null;
                    //if (true == isTagByClick(imageElement, rawScreen, toScreen))
                    if (false == isTagByWordsList(imageElement, rawScreen))
                        continue;

                    //Tag
                    List<AbstractElement> tagStringlist = null;
                    tagStringlist = rawScreen.getElementShipValueList(imageElement);
                    //Tag Must has single String in Current Machine
                    if (null == tagStringlist || tagStringlist.Count != 1)
                        continue;
                    //Get Tag Button Info
                    ControlButton tagInfo = new ControlButton();
                    tagInfo.imageList.Add(imageElement);
                    tagInfo.stringList = tagStringlist.ConvertAll(x => (ElementString)x);
                    LogControl logBtn = logScreen.getItem(imageElement.id);
                    tagInfo.statusShow = parseButtonStatus(logBtn);
                    tagInfo.setIdentifyId(logBtn.dataHolderID);
                    tagInfo.rect = imageElement.rect;
                    tagInfo.toScreen = toScreen;
                    tagInfoList.Add(tagInfo);
                }

                //Tag Control Exsited
                if (0 != tagInfoList.Count)
                {
                    ControlTag controlTag = new ControlTag(tagInfoList);
                    controlTag.hasFixed();
                    rawScreen.addControl(controlTag);
                }
            }
        }
    }
}
