/*
 * File Name: ScreenIdentify.cs
 * Creat Date: 2017-3-24
 * Description: Screen Identify Class
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
     * Description: Screen Identification calss for Identify Screen
     */
    class ScreenIdentify
    {
        public string scrId { get; set; }
        public string scrTitle { get; set;}
        public List<string> btnWordsVector { get; set; }

        /* 
         *  Description:default constructor
         *  Param:
         *  Return:
         *  Exception:
         *  Example:
         */
        public ScreenIdentify()
        {
            scrId = null;
            scrTitle = null;
            btnWordsVector = null;        
        }

        /* 
         *  Description:special constructor
         *  Param:scrId - the ScreenId of Screen Identify
         *        scrTitle - the Screen Title Info of Screen Identify,include Rectange and String
         *        btnWordsList - the btnWords List of target Screen
         *  Return:
         *  Exception:
         *  Example:ScreenIdentify(scrId,scrTitle)
         *          ScreenIdentify(scrId,scrTitle,btnWordsList)
         */
        public ScreenIdentify(string scrId, 
                              string scrTitle, 
                              List<string> btnWordsList = null)
        {
            this.scrId = scrId;
            this.scrTitle = scrTitle;
            this.btnWordsVector = btnWordsList;
        }

        /* 
         *  Description:the Equals Judge Method between two Screen Identifications
         *  Param:tmpScrIdentify - the Screen Identification to equals with
         *  Return:true - Current Screen Identification is equal to Param Identification
         *         false - Current Screen Identification is unequal to Param Identification
         *  Exception:
         *  Example:bool result = currentScrIdentify.Equals(tmpScrIdentify)
         */
        public bool Equals(ScreenIdentify tmpScrIdentify)
        {
            if (null == tmpScrIdentify)
                throw new FTBAutoTestException("Screen identify equals check error ,by null ScreenIdentify.");
            if (scrId != null && scrId == tmpScrIdentify.scrId)
            {
                return true;
            }
            else if (scrTitle != null && scrTitle == tmpScrIdentify.scrTitle)
            {
                return true;
            }
            else if(btnWordsVector!=null /*&& Compare(btnWordsList)*/)//todo
            {
                
            }

            return false;
        }

        public bool isNull()
        {
            if (null == scrId)
                return true;
            if (null == scrTitle)
                return true;
            if (null == btnWordsVector)
                return true;
            return false;
        }

        public bool isValid()
        {
            if (null != scrId && "" != scrId)
                return true;
            if (null != scrTitle && "" != scrTitle)
                return true;
            if (null != btnWordsVector && 0 != btnWordsVector.Count)
                return true;
            return false;
        }
    }
}
