using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Tool.DAL
{
   /*
    *  Description:Mcc info and method
    */
    public class Mcc
    {
        private static BigInteger modelMcc = 0;
        //save all the model,country,continent value and  position in FTB 
        private static Dictionary<string, BigInteger> mccModelData = new Dictionary<string, BigInteger>();
        private static Dictionary<string, BigInteger> mccCountryData = new Dictionary<string, BigInteger>(); 
        private static Dictionary<string, List<string>> mccContinentCountryDatas = new Dictionary<string, List<string>>();
        private static Dictionary<string, BigInteger> supportModelCountryData = new Dictionary<string, BigInteger>();
        public const string _MODEL = "model", _COUNTRY = "country";
        //current tc's mcc
        public BigInteger tcMcc { get; set; }

        public Mcc(BigInteger tcMcc)
        {
            this.tcMcc = tcMcc;
        }

        /* 
         *  Description:set model mcc
         *  Param:model - Form selected model's word
         *  Param:country - Form selected country's word
         *  Return:
         *  Exception:FTBAutoException
         *  Example:ftbMcc.setModelAndCountry("MFC-J893N","US")
         */
        public static void setModelAndCountry(string model, string country)
        {
            if (mccModelData.ContainsKey(model) && mccCountryData.ContainsKey(country))
            {
                modelMcc = mccModelData[model] | mccCountryData[country];
            }
            else
            {
                throw new FTBAutoTestException("MccModelData or MccCountryData not include this key");
            }
        }
        public static string getSelectedModel()
        {
            foreach (KeyValuePair<string, BigInteger> modelData in mccModelData)
            {
                if ((modelData.Value & modelMcc)!=0)
                {
                    return modelData.Key;
                }
            }
            return "";
        }
        public static string getSelectedCountry()
        {
            foreach (KeyValuePair<string, BigInteger> countryData in mccCountryData)
            {
                if ((countryData.Value & modelMcc) != 0)
                {
                    return countryData.Key;
                }
            }
            return "";
        }
        public static string getSelectedContinent(string country)
        {
            foreach (KeyValuePair<string, List<string>> continentCountryDatas in mccContinentCountryDatas)
            {
                for (int i = 0; i < continentCountryDatas.Value.Count; i++)
                {
                    if (continentCountryDatas.Value[i] == country)
                    {
                        return continentCountryDatas.Key;
                    }
                }
               
            }
            return "";
        }
       /* 
        *  Description:add data in mcc model list
        *  Param:model - model's word
        *  Param:model_value - model_value
        *  Return:
        *  Exception:
        *  Example:ftbMcc.addMccModelData("MFC-J893N","1000000000000000")
        */
        public static void addMccModelData(string model, BigInteger modelValue)
        {
            mccModelData.Add(model, modelValue);
        }
        /* 
     *  Description:add data in mcc SupportModelCountry list
     *  Param:model - model's word
     *  Param:modelXCountry - modelXCountry mcc
     *  Return:
     *  Exception:
     *  Example:ftbMcc.addMccModelData("MFC-J893N","1000000000000000")
     */
        public static void addSupportModelCountryData(string model, BigInteger modelXCountry)
        {
            supportModelCountryData.Add(model, modelXCountry);
        }
       /* 
        *  Description:add data in mcc country list
        *  Param:MccCountryData - save all country's word
        *  Param:country_value - country_value
        *  Param:country - country
        *  Return:
        *  Exception:
        *  Example:ftbMcc.addMccCountryData("USA","0000000000000001")
        */
        public static void addMccCountryData(string country, BigInteger countryValue)
        {
            mccCountryData.Add(country, countryValue);
        }

       /* 
        *  Description:add data in mcc continent list
        *  Param:MccContinentDatas - save country corresponding continent
        *  Param:continent - continent
        *  Param:country - country
        *  Return:
        *  Exception:
        *  Example:ftbMcc.addMccContinentList("US","USA")
        */
        public static void addMccContinentCountryList(string continent, string country)
        {
            if (mccContinentCountryDatas.ContainsKey(continent))
            {
                mccContinentCountryDatas[continent].Add(country);
            }
            else
            {
                List<string> country_list = new List<string>();
                country_list.Add(country);
                mccContinentCountryDatas.Add(continent, country_list);
            }
        }

        /* 
         *  Description:get mcc continent list
         *  Param:continent - ContinentList's key
         *  Return:List<string>
         *  Exception:FTBAutoException
         *  Example:
         */
    public static List<string> getMccContinentCountryList(string continent, string model)
        {
            List<string> continentCountryList = new List<string>();
            BigInteger modelCountryMcc = supportModelCountryData[model];
            int countryCount = modelCountryMcc.ToString().Length;
            BigInteger countryMcc = 0;
            Dictionary<string, List<string>>.KeyCollection MccContinentKeys = mccContinentCountryDatas.Keys;
            for (int index = 0; index < mccContinentCountryDatas[continent].Count; index++)
            {
                countryMcc = mccCountryData[mccContinentCountryDatas[continent][index]];
                countryMcc <<= mccModelData.Count;
                if ((modelCountryMcc & countryMcc) != 0)
                {
                    continentCountryList.Add(mccContinentCountryDatas[continent][index]);
                }
            }
            return continentCountryList;
        }
        public static void getCountryList(string continent,List<string> continentCountryList)
        {
            for (int index = 0; index < mccContinentCountryDatas[continent].Count; index++)
            {
                continentCountryList.Add(mccContinentCountryDatas[continent][index]);
            }
        }
       /* 
        *  Description:get mcc model list
        *  Param:model_list - Save ModelList's key
        *  Return:List<string>
        *  Exception:
        *  Example:
        */
    public static List<string> getModelList()
        {
            List<string> modelList = new List<string>();
            Dictionary<string, BigInteger>.KeyCollection MccModelKeys = mccModelData.Keys;
            List<string> continentList = new List<string>();
            foreach (string modelData in MccModelKeys)
            {
                continentList = getContinentList(modelData);
                if (continentList.Count != 0)
                {
                    modelList.Add(modelData);
                }
            }
            return modelList;
        }

       /* 
        *  Description:get Filter continent list
        *  Param:continent_list - Save ContinentList's key
        *  Return:List<string>
        *  Exception:
        *  Example:
        */
        public static List<string> getContinentList(string model)
        {
            List<string> continentList = new List<string>();
            if (supportModelCountryData.ContainsKey(model))
            {
                BigInteger modelCountryMcc = supportModelCountryData[model];

                BigInteger continentMcc = 0;
                Dictionary<string, List<string>>.KeyCollection MccContinentKeys = mccContinentCountryDatas.Keys;
                foreach (string continentData in MccContinentKeys)
                {
                    continentMcc = 0;
                    for (int index = 0; index < mccContinentCountryDatas[continentData].Count; index++)
                    {
                        continentMcc = continentMcc | mccCountryData[mccContinentCountryDatas[continentData][index]];
                    }
                    continentMcc <<= mccModelData.Count;
                    if ((modelCountryMcc & continentMcc) != 0)
                    {
                        continentList.Add(continentData);
                    }
                }
               return continentList;
            }
            else
            {
                throw new Exception("supportModelCountryData not ContainsKey model");
            }
        }
      /* 
      *  Description:get Filter continent list
      *  Param:continent_list - Save ContinentList's key
      *  Return:List<string>
      *  Exception:
      *  Example:
      */
        public static void getAllContinentList(List<string> continentList)
        {
            Dictionary<string, List<string>>.KeyCollection MccContinentKeys = mccContinentCountryDatas.Keys;
            foreach (string continentData in MccContinentKeys)
            {
                continentList.Add(continentData);
            }
        }
       /* 
        *  Description:return this tc can be test
        *  Param:
        *  Return:bool
        *  Exception:
        *  Example:
        */
        public bool isMccValid()
        {
            if ((modelMcc & tcMcc) == modelMcc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
