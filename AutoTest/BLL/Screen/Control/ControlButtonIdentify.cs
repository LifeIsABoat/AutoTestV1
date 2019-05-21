/*
 * File Name: ButtonIdentify.cs
 * Creat Date: 2017-3-24
 * Description: Button Identify Class
 * Modify Content:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    /*
     * Description: Button Identification calss for Identify Button
     */
    class ControlButtonIdentify
    {
        public ushort? id { get; set; }
        public ulong? hashValue { get; set; }
        public string btnWordsStr { get; set; }
        public ScreenIdentify toScrIdentify { get; set; }
        public List<string> helpInfoList;

        /* 
         *  Description:default constructor
         *  Param:
         *  Return:
         *  Exception:
         *  Example:
         */
        public ControlButtonIdentify()
        {
            id = null;
            hashValue = null;
            btnWordsStr = null;
            toScrIdentify = null;
        }

        /* todo
         *  Description:special constructor
         *  Param:btnWordsInfo - the String Info of Current Button
         *        toScrIdentify - the Target Screen Idendification When Current Button Clicked
         *        btnIdValue - the ButtonId of Current Button from Log
         *        hashValue - the HashValue of Current Button from Camera
         *  Return:
         *  Exception:
         *  Example:ButtonIdentify(btnWordsInfo)
         *          ButtonIdentify(btnWordsInfo,toScrIdentify)
         *          ButtonIdentify(btnWordsInfo,toScrIdentify,btnIdValue)
         *          ButtonIdentify(btnWordsInfo,toScrIdentify,null,hashValue)
         */
        public ControlButtonIdentify(string btnWordsStr,
                              ScreenIdentify toScrIdentify = null,
                              ushort? controlId = null,
                              ulong? hashValue = null)
        {
            this.id = controlId;
            this.btnWordsStr = btnWordsStr;
            this.toScrIdentify = toScrIdentify;
            this.hashValue = hashValue;
        }

        /* 
         *  Description:the Equals Judge Method between two Button Identifications
         *  Param:tmpBtnIdentify - the Button Identification to equals with
         *  Return:true - Current Button Identification is equal to Param Identification
         *         false - Current Button Identification is unequal to Param Identification
         *  Exception:
         *  Example:bool result = currentBtnIdentify.Equals(tmpBtnIdentify)
         */
        public bool Equals(ControlButtonIdentify tmpBtnIdentify)
        {
            if (null == tmpBtnIdentify)
                throw new FTBAutoTestException("Control button identify equals check error ,by null buttonIdentify.");
            if (hashValue != null && hashValue == tmpBtnIdentify.hashValue)
                return true;
            else if (btnWordsStr != null && btnWordsStr == tmpBtnIdentify.btnWordsStr)
                return true;
            else if (btnWordsStr != null && tmpBtnIdentify.helpInfoList != null && tmpBtnIdentify.helpInfoList.Contains(btnWordsStr))
                return true;
            else if (toScrIdentify != null && tmpBtnIdentify.toScrIdentify != null && toScrIdentify.Equals(tmpBtnIdentify.toScrIdentify))
                return true;
            //temp do
            else if (btnWordsStr != null && btnWordsStr.Replace(Environment.NewLine, " ") == tmpBtnIdentify.btnWordsStr)
                return true;
            //temp do
            else if (helpInfoList != null && helpInfoList.Contains(tmpBtnIdentify.btnWordsStr))
                return true;
            return false;
        }

        public bool isNull()
        {
            if (null == id)
                return true;
            if (null == hashValue)
                return true;
            if (null == btnWordsStr)
                return true;
            if (null == toScrIdentify)
                return true;
            return false;
        }

        public bool isValid()
        {
            if (null != id)
                return true;
            if (null != hashValue)
                return true;
            if (null != btnWordsStr && ""!= btnWordsStr)
                return true;
            if (null != toScrIdentify && toScrIdentify.isValid())
                return true;
            return false;
        }
    }
}
