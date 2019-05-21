using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlFixerCondition
    {
        private List<ScreenIdentify> whiteList, blackList;

        public ControlFixerCondition()
        {
            whiteList = null;
            blackList = null;
        }

        public void addWhiteScreenIdentify(ScreenIdentify whiteScreenIdentify)
        {
            //only one kind can be excited
            blackList = null;

            //add whiteList
            if (null == whiteList)
                whiteList = new List<ScreenIdentify>();

            whiteList.Add(whiteScreenIdentify);
        }
        public void addBlackScreenIdentify(ScreenIdentify blackScreenIdentify)
        {
            //only one kind can be excited
            whiteList = null;

            //add black
            if (null == blackList)
                blackList = new List<ScreenIdentify>();

            blackList.Add(blackScreenIdentify);
        }

        public bool isValid(ScreenIdentify tmpScreenIdentify)
        {
            //When WhiteList & BlackList are null, anyScreen is Valid
            if (null == whiteList && null == blackList)
                return true;
            //When WhiteList isn't null.
            //if Target Screen is in the List, is Valid
            else if (null != whiteList)
            {
                foreach (ScreenIdentify screenIdentify in whiteList)
                {
                    if (screenIdentify.Equals(tmpScreenIdentify))
                        return true;
                }
                return false;
            }
            //When BlackList isn't null.
            //if Target Screen isn't in the List, is Valid
            else if (null != blackList)
            {
                foreach (ScreenIdentify screenIdentify in blackList)
                {
                    if (screenIdentify.Equals(tmpScreenIdentify))
                        return false;
                }
                return false;
            }
            //won't run here
            throw new FTBAutoTestException("Fixer condition meet unknow error.");
        }
    }
}
