using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    //sheet modeldata info
    public struct ModelData
    {
        public string model_name { get; set; }
        public string model_mcc { get; set; }
    }
    //sheet countrydata info
    public struct CountryData
    {
        public string country_name { get; set; }
        public string country_mcc { get; set; }
    }
    //sheet continent countrydata info
    public struct ContinentCountryData
    {
        public string continent_name { get; set; }
        public List<string> country_name { get; set; }
    }
    //class discribe supported language
    public class SupportLanguageDate
    {
        public string country_name { get; set; }
        public List<string> support_language_name { get; set; }
    }
    public struct ModelCountry
    {
        public string model_name { get; set; }
        public string modelxcounty { get; set; }
    }
    /*
     *  Description:FTB Mcc info
     */
    public class FTBMccInfo
    {
        public List<ModelData> modeldatalist;
        public List<CountryData> countrydatalist;
        public List<ContinentCountryData> continentcountrydatalist;
        public List<ModelCountry> supportModelCountryDataList;
        public string _MODEL = "model", _COUNTRY = "country";
        public FTBMccInfo()
        {
            modeldatalist = new List<ModelData>();
            countrydatalist = new List<CountryData>();
            continentcountrydatalist = new List<ContinentCountryData>();
            supportModelCountryDataList = new List<ModelCountry>();
        }
    }

    public struct LangData
    {
        public string model_name { get; set; }
        public List<SupportLanguageDate> supportlanguageinfolist { get; set; }
    }

    /*
    *  Description:FTB Mcc info
    */
    public class FTBlanguageInfo
    {
        public List<LangData> langdatelist;
        public string _MODEL = "model", _COUNTRY = "country";
        public FTBlanguageInfo()
        {
            langdatelist = new List<LangData>();
        }
    }

}
