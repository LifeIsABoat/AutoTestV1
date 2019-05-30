using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    class Language
    {
        private static Dictionary<string, Dictionary<string, List<string>>> modelCountryLanguageData = new Dictionary<string, Dictionary<string, List<string>>>();
        /* 
        *  Description:add data in mcc LanguangDate list
        *  Param:modelname - save modelname corresponding LanguangDate
        *  Param:supportlan - supportlanuage dictionary
        *  Return:
        *  Exception:
        *  Example:ftbMcc.addlanguageData("MFC", supportlan)
        */
        public static void addlanguageData(string modelname, Dictionary<string, List<string>> countryLanguageData)
        {
            modelCountryLanguageData.Add(modelname, countryLanguageData);
        }

        /* 
        *  Description:get  model list
        *  Param:model_list - Save ModelList's key
        *  Return:List<string>
        *  Exception:
        *  Example:ftbMcc.getModelname(Modelname)
        */
        public static List<string> getModelname()
        {
            List<string> Modelname = new List<string>();
            Dictionary<string, Dictionary<string, List<string>>>.KeyCollection ModelKeys = modelCountryLanguageData.Keys;
            foreach (string modelData in ModelKeys)
            {
                Modelname.Add(modelData);
            }
            return Modelname;
        }

        /* 
        *  Description:get mcc language list
        *  Param:language_list - Save languageList's key
        *  Param:model - model's key
        *  Param:country - country's key
        *  Return:List<string>
        *  Exception:
        *  Example:ftbMcc.addlanguageData(languagelist,"MFC", "USA")
        */
        public static List<string> getlanguage(string model, string country)
        {
            List<string> languagelist = new List<string>();
            if (modelCountryLanguageData.ContainsKey(model))
            {
                dynamic Supportlanguage = modelCountryLanguageData[model];
                if (Supportlanguage.ContainsKey(country))
                {
                    foreach (string language in Supportlanguage[country])
                    {
                        languagelist.Add(language);
                    }
                    return languagelist;
                }
                else
                {
                    throw new FTBAutoTestException("LanguangDate not include ");
                }
            }
            else
            {
                throw new FTBAutoTestException("LanguangDate not include ");
            }
        }
    }
}
