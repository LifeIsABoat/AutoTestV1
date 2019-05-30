using Tool.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     *  Description: FTB common management class interface
     */
    interface IFTBCommonAPI : IFTBCommonAggregate
    {
        /* 
       *  Description:get mcc model list
       *  Param:model_list - Save ModelList's key
       *  Return:List<string>
       *  Exception:
       *  Example:ftbmcc.getModelList(modelList);
       */
        List<string> getTotalModelList();

        /*
        *  Description:get mcc continent list
        *  Param:continent_list - Save ContinentList's key
        *  Return:List<string>
        *  Exception: 
        *  Example: ftbmcc.getContinentList(continentList); 
        */
         List<string> getTotalFilterContinentList(string model);
        void getTotalContinentList(List<string> continentList);
        /* 
        *  Description:get mcc continent list
        *  Param:continent - ContinentList's key
        *  Return:List<string>
        *  Exception:FTBAutoException
        *  Example: ftbmcc.getMccContinentCountryList(continent, continentCountryList);
        */
        List<string> getTotalFilterCountryList(string continent, string model);
        void getTotalCountryList(string continent,List<string> continentCountryList);

        /* 
         *  Description:set model mcc
         *  Param:model - Form selected model's word
         *  Param:country - Form selected country's word
         *  Return:
         *  Exception:FTBAutoException
         *  Example:ftbMcc.setModelAndCountry("MFC-J893N","US")
         */
        void setTotalModelAndCountry(string model, string country);
        string getSelectModel();
        string getSelectContinent();
        string getSelectCountry();
        /* 
         *  Description:get  model list
         *  Param:model_list - Save ModelList's key
         *  Return:List<string>
         *  Exception:
         *  Example:ftbMcc.getModelname(Modelname)
         */
       List<string> getTotalLanguageModelList();

        /* 
         *  Description:get mcc language list
         *  Param:language_list - Save languageList's key
         *  Param:model - model's key
         *  Param:country - country's key
         *  Return:List<string>
         *  Exception:
         *  Example:ftbMcc.addlanguageData(languagelist,"MFC", "USA")
         */
        List<string> getTotalLanguageList(string model, string country);
        string getTotalCondition(int conditionIndex);

        List<string> getTotalConditionList();
        List<string> getTotalPath();
        void setTotalSelected();
        void setTotalUnselected();

        void setTotalContidionType();

        void setTotalContidionIndex();

        void setTotalContidionUnselect();

        void setTotalHardwareDevice(int index);

        void setTotalOptionSetting(int index, int MatchTCIndex);

        void setTotalOptionSettingSelected();

        void setTotalNoConditionSelected();

        void setHardwareDeviceSelected(int conditionIndex);

        void setSortedTcSelected(List<int> tcIndexList);

        //
        void setModelAndCountrySelectInfoList(List<string> paramter);
        List<string> getModelAndCountrySelectInfoList();
        void setTcSelectInfoList(List<string> paramter);
        List<string> getTcSelectInfoList();
        void setStringBuff(string pam);
        string getStringBuff();
        //

        /*
         *  Description: setTcSelected
         *  Return: 
         *  Exception: 
         *  Example: setTcSelected(tcIndex);
         */
        void setTcSelected(int tcIndex);

        /*
         *  Description: setTcUnselected
         *  Return: 
         *  Exception: 
         *  Example: setTcUnselected(tcIndex);
         */
        void setTcUnselected(int tcIndex);

        //bool isTcValid(int tcIndex);

        /*
         *  Description: check whether tc is Selected
         *  Return: 
         *  Exception: 
         *  Example: setTcUnselected(tcIndex);
         */

        bool isTcSelected(int tcIndex);

        bool isTcConditionSelected(int tcIndex = -1);

        bool isLevelButtonVaild(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1);
        bool isLevelButtonVaild(string path,int conditionIndex);

        string getTcFactorySetting(int tcIndex = -1);
        string getTcComment(int tcIndex = -1);
        
        List<int> getTotalLevelConditionIndexList(int tcIndex = -1);
        bool isTcValid(int tcIndex = -1, int time = 0);
        int getTcIndexFromConditionTndex(int conditionIndex);

        /* 
         *  Description:Get TCIndex according to condition path
         *  Param:condition - condition path
         *  Return:int
         *  Exception:
         *  Example:getTcIndexFromConditopnString(conditionPath)
         */
        int getTcIndexFromConditionString(string condition);

        List<int> getOptionLevelBrotherButtonIndex(int tcIndex = -1);
        ConditionType getConditionType(int conditionIndex);
        string getTcDir(int tcIndex = -1);

        void setTcTestResult(bool result, int testOpinionIndex, int levelIndex = -1,int tcIndex = -1);
		/* 
         *  Description:set tc Conclusion
         *  Param:TcConclusion - Tc Conclusion
         *  Param:tcIndex - tc index
         *  Return:void
         *  Exception:
         *  Example:setTcConclusion(TcConclusion)
         */
        void setTcStatus(bool tcStatus,int tcIndex = -1);
        /* 
         *  Description:get tc Conclusion
         *  Param:tcIndex - tc index
         *  Return: TcTestResult
         *  Exception:
         *  Example:getTcTestResult()
         */
        bool getTcStatus(int tcIndex=-1);
        /* 
        *  Description:get tc Rsp
        *  Param:tcIndex - tc index
        *  Return: TcgetTcRsp
        *  Exception:
        *  Example:getTcRsp()
        */
        string getTcRsp(int tcIndex = -1);
        /* 
       *  Description:get tc Ews
       *  Param:tcIndex - tc index
       *  Return: getTcEws
       *  Exception:
       *  Example:getTcEws()
       */
        string getTcEws(int tcIndex = -1);
        /* 
        *  Description:Rsp is Valid
        *  Param:tcIndex - tc index
        *  Return: bool
        *  Exception:
        *  Example:isRspValid()
        */
        bool isRspValid(int tcIndex = -1);
        /* 
         *  Description:Ews id Valid
         *  Param:tcIndex - tc index
         *  Return: bool
         *  Exception:
         *  Example:isEwsValid()
         */
        bool isEwsValid(int tcIndex = -1);
       
        string getFactorySetting(int tcIndex = -1);
        string getOptionWords(int tcIndex = -1);
        /*
         *  Description: Get button condition
         *  Param: levelIndex - No. several level, Default for current
         *  Param: btnIndex - No. several index, Default for current
         *  Return: string the uswords return
         *  Exception: 
         *  Example: str = ftb.getBtnCondition(levelIndex, btnIndex); str = ftb.getBtnWordsStr();
         */
        int getLevelCondition(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1);
        List<int> getLevelCondition(string path);
        int getLevelCount(int tcIndex = -1);

        bool isLevelOption(int levelIndex, int tcIndex = -1);
        bool isLevelOption(string path, int conditionIndex = -1);
        /*
         *  Description: Gets uswords in the location of specified in list
         *  Param: btnWords - Designated uswords
         *  Param: levelIndex - The specified number of level ,Default for current level
         *  Return: int  in the location of list
         *  Exception: 
         *  Example: num = ftb.getBtnIndex(btnWords, levelIndex); num = ftb.getBtnIndex(btnWords);
         */
        int getLevelButtonIndex(string btnWords, int levelIndex = -1, int tcIndex = -1);
        
        /*
         *  Description: Gets specifies that "level" has several elements
         *  Param: levelIndex - The specified number of level ,Default for current level
         *  Return: int Number of element 
         *  Exception: 
         *  Example: num = ftb.getBtnCount(levelIndex); num = ftb.getBtnCount();
         */
        int getLevelButtonCount(int levelIndex = -1, int tcIndex = -1);

        List<string> getLevelButtonHelpInfo(int levelIndex = -1, int tcIndex = -1);
        /*
         *  Description: Get button path
         *  Param: levelIndex - No. several level, Default for current
         *  Param: btnIndex - No. several index, Default for current
         *  Return: string the path return
         *  Exception: 
         *  Example: str = ftb.getRunPath(levelIndex, btnIndex); str = ftb.getRunPath();
         */
        string getLevelDir(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1);

        /*
         *  Description: Get button uswords
         *  Param: levelIndex - No. several level, Default for current
         *  Param: btnIndex - No. several index, Default for current
         *  Return: string the uswords return
         *  Exception: 
         *  Example: str = ftb.getBtnWordsStr(levelIndex, btnIndex); str = ftb.getBtnWordsStr();
         */
        string getLevelButtonWord(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1, string continent = null);
        string getNowScreenLevelButtonWords(string path, int conditionIndex = -1);
        List<int> getPathTcIndexList(string path);
        List<string> getLevelSubButtonWords(string path,int conditionIndex = -1);
        List<int> getLevelSubButtonCondition(string path, int conditionIndex = -1);
        List<int> getLevelButtonFrontCondition(string path, int conditionIndex = -1);

        /*
         *  Description: Gets Specify the uswords collection of level
         *  Param: levelIndex - The specified number of level, Default for current level
         *  Return: List<string> uswords list 
         *  Exception: 
         *  Example: strlist = ftb.getBtnWordsStrList(levelIndex); strlist = ftb.getBtnWordsStrList();
         */
        List<string> getLevelButtonWords(int levelIndex = -1, int tcIndex = -1);

        /*
         *  Description: Get buttontitle uswords
         *  Param: levelIndex - No. several level, Default for current
         *  Param: btnIndex - No. several index, Default for current
         *  Return: string the uswords return
         *  Exception: 
         *  Example: str = ftb.getBtnToScrTitleStr(levelIndex, btnIndex); str = ftb.getBtnToScrTitleStr();
         */
        string getLevelButtonToScreenTitle(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1);
        string getLevelButtonToScreenTitle(string path, int conditionIndex=-1);
        /*
         *  Description: Get buttontitle functionName
         *  Param: levelIndex - No. several level, Default for current
         *  Param: btnIndex - No. several index, Default for current
         *  Return: string the functionName return
         *  Exception: 
         *  Example: str = ftb.getBtnToScrId(levelIndex, btnIndex); str = ftb.getBtnToScrId();
         */
        string getLevelButtonToScreenId(int levelIndex = -1, int btnIndex = -1, int tcIndex = -1);
        string getLevelButtonToScreenId(string path, int conditionIndex = -1);
     
    }
}
