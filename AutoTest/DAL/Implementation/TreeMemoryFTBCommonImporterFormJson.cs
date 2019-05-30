using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using System.Numerics;

namespace Tool.DAL
{
    /*
     *  Description: Import data manipulation interface of Implementation class
     */
    class TreeMemoryFTBCommonImportFormJson : IFTBCommonAggregateImport
    {
        //file path
        private string path;
        //Store a screenComponent type tree
        private AbstractScreenComponent screenComponentTree;
        public TreeMemoryFTBCommonImportFormJson(string path)
        {
            this.path = path;
        }
        
        void IFTBCommonAggregateImport.import(IFTBCommonAggregate dataAccessor)
        {
            if (path == null)
            {
                throw new FTBAutoTestException("Path cannot be empty");
            }
            string buf = getTextJson(path);
            if (buf == null)
            {
                throw new FTBAutoTestException("File content is empty");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic modelDy;
            try
            {
                modelDy = js.Deserialize<dynamic>(buf); //Deserialize become dynamic model
            }
            catch
            {
                throw new FTBAutoTestException("File content is not JSON format");
            }

            this.screenComponentTree = creatTree(modelDy["levelnode"]);
            addFTBMccStaticProperties(modelDy["ftb_mcc_attribute"]);
            addFTBContidionStaticProperties(modelDy["ftb_condition_attribute"]);

            ((TreeMemoryFTBCommonAggregate)dataAccessor).setRoot(screenComponentTree);




        }
        /* 
         *  Description: binary string to decimal BigInteger
         *  Param: string number( < 128 )
         *  Return: binary BigInteger
         *  Exception:
         *  Example: addFTBContidionStaticProperties(modelDy);
         */
        private BigInteger binaryTOdecimal(string buff)
        {
            int n = buff.Length;
            BigInteger sum = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                sum += (BigInteger)((buff[i] - '0') * (Math.Pow(2, n - 1 - i)));
            }
            return sum;
        }
        /* 
         *  Description: binary string to decimal BigInteger
         *  Param: BigInteger number
         *  Return: binary string( < 256 )
         *  Exception:
         *  Example: addFTBContidionStaticProperties(modelDy);
         */
        private string decimalTObinary(BigInteger buff)
        {
            int rem;
            BigInteger step = new BigInteger(2);
            int i = 255;
            int[] s = new int[256];
            string result = string.Empty;

            while (i != 0)
            {
                rem = (int)(buff % step);       
                buff = buff / step;
                s[i--] = rem;
            }
            for (i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrEmpty(result))
                    result += s[i];
                else
                    result = s[i].ToString();
            }
            return result;
        }

        /* 
         *  Description: Read the contents of the specified JSON file
         *  Param: path - JSON file path
         *  Return: JSON file content
         *  Exception: FTBAutoException
         *  Example: str = getTextJson(path);
         */
        private string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {

                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }

        /* 
         *  Description: Construct a tree
         *  Param: modelDy - The object of JSON dynamic anti serialization
         *  Return: Constructed tree
         *  Exception:
         *  Example: tree = creatTree(modelDy);
         */
        private AbstractScreenComponent creatTree(dynamic modelDy)
        {
            AbstractScreenComponent tree = null;
            LableInfo title = null;
            ButtonInfo butt = null;

            if (null == modelDy)
            {
                return null;
            }
            //Determine whether the field contains "ftbbutton"
            if (((IDictionary<string, object>)modelDy).ContainsKey("ftbbutton"))
            {
                dynamic ftbButton = modelDy["ftbbutton"];
                butt = new ButtonInfo(ftbButton["us_words"], int.Parse(ftbButton["condition_index"].ToString()),
                    ftbButton["function_name"], ftbButton["string_id"]);

                //Determine whether the field contains "ftbbuttontitle"
                if (((IDictionary<string, object>)modelDy).ContainsKey("ftbbuttontitle"))
                {
                    dynamic ftbButtonTitle = modelDy["ftbbuttontitle"];
                    title = new LableInfo(ftbButtonTitle["us_words"], ftbButtonTitle["function_name"],
                        ftbButtonTitle["string_id"]);
                }// if "ftbbuttontitle"

                tree = new ButtonComposite(butt, title);
                //Determine whether the field contains list "levelnodes"
                if (((IDictionary<string, object>)modelDy).ContainsKey("levelnodes"))
                {
                    foreach (dynamic levelNode in modelDy["levelnodes"])
                    {
                        tree.addScreenComponent(creatTree(levelNode));
                    }
                }
            }// if "ftbbutton"
            //Determine whether the field contains "ftboption"
            else if (((IDictionary<string, object>)modelDy).ContainsKey("ftboption"))
            {
                dynamic ftbOption = modelDy["ftboption"];
                butt = new OptionInfo(ftbOption["us_words"], int.Parse(ftbOption["condition_index"]),
                                         binaryTOdecimal(ftbOption["tc_mcc"]), ftbOption["comment"], ftbOption["factory_setting"],
                                         ftbOption["function_name"], ftbOption["string_id"], ftbOption["rsp"], ftbOption["ews"]);
                tree = new OptionLeaf(butt);
            }// else if "ftbOption"
            return tree;
        }

        /* 
         *  Description: Add static attributes to the FTBMcc class
         *  Param: modelDy - The object of JSON dynamic anti serialization
         *  Return: 
         *  Exception:
         *  Example: addFTBMccStaticProperties(modelDy);
         */
        private void addFTBMccStaticProperties(dynamic modelDy)
        {
            //Determine whether the field contains "modeldatalist"
            if (((IDictionary<string, object>)modelDy).ContainsKey("modeldatalist"))
            {
                foreach (dynamic modeldata in modelDy["modeldatalist"])
                {
                    string name = modeldata["model_name"];
                    string svalue = modeldata["model_mcc"];
                    BigInteger value = binaryTOdecimal(svalue);
                    Mcc.addMccModelData(name, value);
                }
            }// if "modeldatalist"
            //Determine whether the field contains "countrydatalist"
            if (((IDictionary<string, object>)modelDy).ContainsKey("countrydatalist"))
            {
                foreach (dynamic countrydata in modelDy["countrydatalist"])
                {
                    string name = countrydata["country_name"];
                    string svalue = countrydata["country_mcc"];
                    BigInteger value = binaryTOdecimal(svalue);
                    Mcc.addMccCountryData(name, value);
                }
            }// if "countrydatalist"
            //Determine whether the field contains "continentcountrydatalist"
            if (((IDictionary<string, object>)modelDy).ContainsKey("continentcountrydatalist"))
            {
                foreach (dynamic continentCountry in modelDy["continentcountrydatalist"])
                {
                    string name = continentCountry["continent_name"];
                    foreach (string value in continentCountry["country_name"])
                    {
                        Mcc.addMccContinentCountryList(name, value);
                    }
                }
            }// if "continentcountrydatalist"
            if (((IDictionary<string, object>)modelDy).ContainsKey("supportModelCountryDataList"))
            {
                foreach (dynamic supportModelCountryData in modelDy["supportModelCountryDataList"])
                {
                    string name = supportModelCountryData["model_name"];
                    string value = supportModelCountryData["modelxcounty"];
                    Mcc.addSupportModelCountryData(name, binaryTOdecimal(value));
                }// if "supportModelCountryData"
            }
        }

        /* 
         *  Description: Add static attributes to the FTBContidion class
         *  Param: modelDy - The object of JSON dynamic anti serialization
         *  Return: 
         *  Exception:
         *  Example: addFTBContidionStaticProperties(modelDy);
         */
        private void addFTBContidionStaticProperties(dynamic modelDy)
        {
            //Determine whether the field contains "conditions_list"
            if (((IDictionary<string, object>)modelDy).ContainsKey("conditions_list"))
            {
                foreach (string condiTions in modelDy["conditions_list"])
                {
                    Contidion.addConditionList(condiTions);
                }
            }// if "conditions_list"
        }

        /* 
        *  Description: Add static attributes to the FTBMcc class
        *  Param: modelDy - The object of JSON dynamic anti serialization
        *  Return: 
        *  Exception:
        *  Example: addFTBMccStaticProperties(modelDy);
        */
        private void addFTBlanguageStaticProperties(dynamic modelDy)
        {       
            //Determine whether the field contains "countrydatalist"
            if (((IDictionary<string, object>)modelDy).ContainsKey("langdatelist"))
            {
                foreach (dynamic langdate in modelDy["langdatelist"])
                {
                    string modelname = langdate["model_name"];
                    dynamic supportlanguageinfo = langdate["supportlanguageinfolist"];
                    Dictionary<string, List<string>> supportlan = new Dictionary<string, List<string>>();
                    foreach (dynamic supportlanguage in supportlanguageinfo)
                    {
                        string countryname = supportlanguage["country_name"];
                        dynamic language = supportlanguage["support_language_name"];
                        List<string> languagelist = new List<string>();
                        foreach (string lan in language)
                        {
                            languagelist.Add(lan);
                        }
                        supportlan.Add(countryname, languagelist);
                    }
                    Language.addlanguageData(modelname, supportlan);
                }
            }// if "languagedatalist"          
        }
    }
}
