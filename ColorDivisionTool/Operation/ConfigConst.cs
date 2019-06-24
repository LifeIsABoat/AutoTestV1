using System.IO;

namespace ColorDivisionTool.Operation
{
    public static class ConfigConst
    {
        //运行路径目录
        public static readonly string RUNCURRENTPATH = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        //Excel Sheet Name
        public static readonly string SHEETNAME_TRANSLATEFILE = "TransCheckSheet";
        public static readonly string SHEETNAME_ICONFONTFILE = "IconFont";

        //Constant content
        public static readonly string EMPTYLANGUAGE = "__(T_T)__HONYAKU";
        public static readonly string NALANGUAGE = "N/A";

        //Excel Title
        public static readonly string TITLE_MSGNO = "MsgNo";
        public static readonly string TITLE_APPLYNO = "申請No";
        public static readonly string TITLE_SINGLENUM = "Number of requested characters";
        public static readonly string TITLE_DOUBLENUM = "Number of requested characters (2byte)";
        public static readonly string TITLE_APPLYLINE = "Number of requested lines";
        public static readonly string TITLE_DESTINATION = "仕向け";
        public static readonly string TITLE_RSTWIDTH = "Requested width";
        public static readonly string TITLE_RSTHEIGHT = "Requested height";
        public static readonly string TITLE_FONTHEIGHT = "Font height";

        //Country Title
        public static readonly string COUNTRY_UKR = "UKR";
        public static readonly string COUNTRY_UK = "UK";
        public static readonly string COUNTRY_USA = "USA";
        public static readonly string COUNTRY_JPN = "JPN";
        public static readonly string COUNTRY_JPE = "JPE";

        //Files
        public static readonly string CONFIGUREFILEPATH = Path.Combine(RUNCURRENTPATH, "Configure.ini");//@".\Configure.ini";
        //public static readonly string CONFIGUREFILEPATH = @".\Configure.ini";
        public static readonly string ICONFONTFILE = Path.Combine(RUNCURRENTPATH, "IconFont.xlsx");//@".\IconFont.xlsx";

        //Configure File Section
        public static readonly string CHANGECOLORCOUNTRY = "ChangeColorCountry";
        public static readonly string JPANCOUNTRY = "JpanCountry";

        //Configure File key
        public static readonly string KEY_SJPN = "S_JPN";
        public static readonly string KEY_JJPN = "J_JPN";

        // Country start col & end col
        public static readonly int START_COL = 18;//为了节省运行时间，提升效率
        public static readonly int END_COL = 67;//为了节省运行时间，提升效率

        //Excel Background Color Index
        public static readonly double COLOR_RED = 255;     // 红色
        public static readonly double COLOR_GREEN = 5287936;   // 绿色
        public static readonly double COLOR_YELLOW = 65535;  //黄色
        public static readonly double COLOR_LESS = 16777215; //无色
        public static readonly double COLOR_ORANGE = 39423;// 桔黄色 39423
        public static readonly double COLOR_AQUAGREEN = 13421619;// 水绿色 13421619
        public static readonly double COLOR_GOLDEN = 52479;// 金色 52479
        // 淡紫色 16751052
        // 草绿色 52377
        // 粉红色 13408767
        // 淡蓝色 16764057
        // 咖啡色 13209
    }
}
