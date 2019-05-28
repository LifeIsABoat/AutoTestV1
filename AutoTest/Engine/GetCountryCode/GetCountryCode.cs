using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.RSPService;
using System.ServiceModel;
using Tool.Parser;
using Tool.DAL;
using Tool.BLL;

namespace Tool.Engine
{
    class GetCountryCode
    {
        private Dictionary<string, string> modelSelectInfo;
        private Dictionary<string, string> countryInfo;

        public GetCountryCode()
        {
            modelSelectInfo = new Dictionary<string, string>();
            countryInfo = new Dictionary<string, string>();
            string iniFileName = StaticEnvironInfo.getIntPutModelPath() + @"\" + StaticEnvironInfo.getCountriesConfigName();
            getModelCode(iniFileName);
        }

        private void getModelCode(string iniFileName)
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string selectModel = treeMemory.getSelectModel();
            string selectCountry = treeMemory.getSelectCountry();
            List<string> keylist = new List<string>();
            INIParser.getKeyList(selectModel, iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                string value = INIParser.getvalue(selectModel, iniFileName, keylist[i]);
                if (value != "")
                {
                    modelSelectInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();

        }
        public string getCode(string str)
        {
            return modelSelectInfo[str];
        }
    }
}
