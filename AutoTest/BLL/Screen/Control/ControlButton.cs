/*
 * File Name: ButtonInfo.cs
 * Creat Date: 2017-3-24
 * Description: Button Info Class
 * Modify Content:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    enum ControlButtonStatus
    {
        Unknow,     //Button Status Unknow
        Valid,      //Button Status Valid
        Selected,   //Button Status Selected
        Invalid     //Button Status Invalid
    }

    /*
     * Description: Button Identification calss for Identify Button
     */
    class ControlButton : AbstractControl
    {
        private ControlButtonIdentify idenditify;
        public ControlButtonStatus statusShow;
        public List<ElementString> stringList;
        public List<ElementImage> imageList;
        public Screen toScreen;
        public List<string> helpInfoList;

        /* todo
         *  Description:special constructor
         *  Param:rect - the String Info of Current Button
         *        toScrIdentify - the Target Sscreen Idendification When Current Button Clicked
         *        btnIdentify - the ButtonId of Current Button from Log
         *        OptionStr - the Option string of Current Button from Camera
         *  Return:
         *  Exception:
         *  Example:ButtonInfo(rect,btnIdentify)
         *          ButtonIdentify(rect,btnIdentify,OptionStr)
         *          ButtonIdentify(btnWordsInfo,toScrIdentify,btnIdValue)
         */
        public ControlButton(ushort? id = null,
                             Rectangle rect = null)
            :base(rect)
        {
            this.statusShow = ControlButtonStatus.Unknow;
            this.idenditify = new ControlButtonIdentify();
            this.idenditify.id = id;
            this.stringList = new List<ElementString>();
            this.imageList = new List<ElementImage>();
            this.toScreen = null;
            this.helpInfoList = new List<string>();
        }

        public void fixIdentify()
        {
            if (0 != this.imageList.Count)
            {
                idenditify.hashValue = imageList[0].hashValue;
            }
            if (0 != this.stringList.Count)
            {
                idenditify.btnWordsStr = stringList[0].str;
            }
            if (null != this.toScreen)
            {
                idenditify.toScrIdentify = toScreen.getIdentify();
            }
            if(0 != this.helpInfoList.Count)
            {
                idenditify.helpInfoList = helpInfoList;
            }
        }
        public void setIdentifyId(ushort id)
        {
            this.idenditify.id = id;
        }

        public ControlButtonIdentify getIdentify()
        {
            if (idenditify.isNull())
                fixIdentify();
            return idenditify;            
        }

        public override void hasFixed()
        {
            foreach (ElementString elementString in stringList)
                elementString.hasFixed();
            foreach (ElementImage elementImage in imageList)
                elementImage.hasFixed();
        }

        public override string ToString()
        {
            string str = "";

            str += "Button:";
            if (null == this.idenditify.id)
                str += "null,";
            else
                str += string.Format("0x{0:x4},", this.idenditify.id);
            if (null == rect)
                str += "null,";
            else
                str += rect.ToString() + ",";
            str += statusShow.ToString() + "\r\n";
            str += "{";
            string tmpStr = "";
            foreach (ElementString elementString in stringList)
                tmpStr += "\r\n" + elementString.ToString();
            foreach (ElementImage elementImage in imageList)
                tmpStr += "\r\n" + elementImage.ToString();
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};";

            return str;
        }

        public override bool Equals(AbstractControl targetControl)
        {
            ControlButton button = targetControl as ControlButton;
            if (null == button)
                return false;

            if (null != idenditify && !idenditify.Equals(button.idenditify))
                return false;
            if (null == idenditify && null != button.idenditify)
                return false;

            if (statusShow != button.statusShow)
                return false;

            if (stringList.Count != button.stringList.Count)
                return false;
            if (0 != stringList.Where((a, i) => a != stringList[i]).Count())
                return false;

            if (imageList.Count != button.imageList.Count)
                return false;
            if (0 != imageList.Where((a, i) => a != imageList[i]).Count())
                return false;

            if (null != toScreen && !toScreen.EqualsById(button.toScreen))
                return false;
            if (null == toScreen && null != button.toScreen)
                return false;

            return base.Equals(targetControl);
        }
    }
}
