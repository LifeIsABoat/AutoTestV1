using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    enum ControlSoftkeyStatus
    {
        OK,             //softkeyScreen's OK button
        delete,         //delete button
        rightMove,      //
        leftMove,       //
        rightShift,     //
        leftShift       //
    }
    public enum ScreenType
    {
        EnglishOnly,
        EnglishWithNumber,
        NumberOnly
    }
    class ControlInput : AbstractControl
    {
        public List<ControlButton> numExistButtonList;
        public List<ControlButton> imageWithStringButtonList;
        public List<ControlButton> imageOnlyButtonList;
        public Dictionary<ControlSoftkeyStatus, ControlButton> specialBtnAggregate;
        public ScreenType screenType;

        public ControlInput()
            :base()
        {
            this.numExistButtonList = new List<ControlButton>();
            this.imageOnlyButtonList = new List<ControlButton>();
            this.imageWithStringButtonList = new List<ControlButton>();
            this.specialBtnAggregate = new Dictionary<ControlSoftkeyStatus, ControlButton>();
        }

        public override void hasFixed()
        {
            foreach (ControlButton numExistButton in numExistButtonList)
                numExistButton.hasFixed();
            foreach (ControlButton imageOnlyButton in imageOnlyButtonList)
                imageOnlyButton.hasFixed();
            foreach (ControlButton imageWithStringButton in imageWithStringButtonList)
                imageWithStringButton.hasFixed();
        }

        public override string ToString()
        {
            string str = "";

            str += "Input:";
            if (null == rect)
                str += "null,\r\n";
            else
                str += rect.ToString() + ",\r\n";
            str += "{";
            string tmpStr = "";
            foreach (ControlButton numExistButton in numExistButtonList)
                tmpStr += "\r\nNumExist" + numExistButton.ToString();
            foreach (ControlButton imageOnlyButton in imageOnlyButtonList)
                tmpStr += "\r\nImageOnly" + imageOnlyButton.ToString();
            foreach (ControlButton imageWithStringButton in imageWithStringButtonList)
                tmpStr += "\r\nImageWithString" + imageWithStringButton.ToString();
            //
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};";

            return str;
        }

        public override bool Equals(AbstractControl targetControl)
        {
            ControlInput button = targetControl as ControlInput;
            if (null == button)
                return false;

            if (numExistButtonList.Count != button.numExistButtonList.Count)
                return false;
            if (0 != numExistButtonList.Where((a, i) => a != numExistButtonList[i]).Count())
                return false;

            if (imageOnlyButtonList.Count != button.imageOnlyButtonList.Count)
                return false;
            if (0 != imageOnlyButtonList.Where((a, i) => a != imageOnlyButtonList[i]).Count())
                return false;

            if (imageWithStringButtonList.Count != button.imageWithStringButtonList.Count)
                return false;
            if (0 != imageWithStringButtonList.Where((a, i) => a != imageWithStringButtonList[i]).Count())
                return false;

            return base.Equals(targetControl);
        }
    }
}
