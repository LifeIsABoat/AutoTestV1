using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    abstract class AbstractChecker
    {
        protected IFTBCommonAPI treeMemory;
        protected string opinion; 

        public abstract void check();
        public virtual bool isBlackList(ref TestCheckResult checkResult,int tcIndex = -1,int levelIndex = -1)
        {
            string path = treeMemory.getLevelDir(levelIndex, -1, tcIndex);
            string optionStr = treeMemory.getLevelButtonWord(levelIndex, -1, tcIndex);
            OpinionRunBlackListInfo black = TestRuntimeAggregate.getOpinionBlackList(opinion);
            if (black == null)
            {
                return false;
            }
            RunBlackList NABlackList = black.NABlackList;
            RunBlackList NTBlackList = black.NTBlackList;
            if (NTBlackList != null && NTBlackList.blackList != null && NTBlackList.blackList.Contains(path))
            {
                checkResult = TestCheckResult.NT;
                return true;
            }
            else if (NABlackList != null && NABlackList.blackList != null && NABlackList.blackList.Contains(path))
            {
                checkResult = TestCheckResult.NA;
                return true;
            }
            else if (NTBlackList != null && NTBlackList.regulations != null)
            {
                foreach (string pattren in NTBlackList.regulations)
                {
                    if (Regex.IsMatch(optionStr, pattren, RegexOptions.IgnoreCase))
                    {
                        checkResult = TestCheckResult.NT;
                        return true;
                    }
                }
            }
            else if (NABlackList != null && NABlackList.regulations != null)
            {
                foreach (string pattren in NABlackList.regulations)
                {
                    if (Regex.IsMatch(optionStr, pattren, RegexOptions.IgnoreCase))
                    {
                        checkResult = TestCheckResult.NA;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
