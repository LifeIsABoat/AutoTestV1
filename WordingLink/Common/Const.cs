using System.Collections.Generic;
using System.IO;

namespace WordingLink
{
    public static class CONST
    {
        //运行路径目录
        public static readonly string RUNCURRENTPATH = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        //Common Config
        public static readonly string EXCELSUFFIX = @"Excel|*.xls;*.xlsx;*.xlsm|ALL|*.*";

        #region 共通ファイル情報
        public static readonly string FILEPATH_CONFIGURE = Path.Combine(RUNCURRENTPATH, "Configure.ini");//@"./Configure.ini";
        public static readonly string FILEPATH_LOG = Path.Combine(RUNCURRENTPATH, "LOG.TXT");//@"./LOG.TXT";
        public static readonly string FILEPATH_MODELINFO = Path.Combine(RUNCURRENTPATH, "ModelInfo.xml");//@"./ModelInfo.xml";
        public static readonly string JSON_FILEPATH = @"./Config";
        public static readonly string FILENAME_HINAGATA = "hinagata.txt";
        public static readonly string FILENAME_STRIDLINK = "strid_link.inf";
        public static readonly string FILENAME_HMISTRID = "hmi_strid_def.h";
        public static readonly string FILENAME_WORDINGBAK = "文言申請システム(検索用).xlsm";
        public static readonly string FILENAME_WORDINGSAVE = "文言申請システム(リンク完了).xlsm";
        public static readonly string FILENAME_FINALSAVE = "最終文言エクセル(リンク完了).xlsm";
        #endregion

        #region 環境設定 ファイル情報
        public static readonly string SETCION_SAVEPATH = "SaveLoadPath";
        public static readonly string KEY_SAVEPATH_FINAL = "FinalFilePath";
        public static readonly string KEY_SAVEPATH_WORDING = "WordingFilePath";
        public static readonly string KEY_SAVEPATH_MSGNO = "MsgNoFilePath";
        public static readonly string KEY_SAVEPATH_OUTPUT = "OutPutPath";
        public static readonly string KEY_SAVEPATH_PROJECT = "ProjectPath";
        public static readonly string SETCION_FINALEXCEL = "FinalExcelData";
        public static readonly string KEY_FINALEXCEL_SHEETNAME = "WorkSheetName";
        public static readonly string KEY_FINALEXCEL_LANGSTART = "LangStart";
        public static readonly string KEY_FINALEXCEL_TITLELINE = "TitleLine";
        public static readonly string KEY_FINALEXCEL_LANGEND = "LangEnd";
        public static readonly string SETCION_WORDINGEXCEL = "WordingExcelData";
        public static readonly string KEY_WORDINGEXCEL_TITLELINE = "ResultStartLine";

        public static readonly string SETCION_OUTPUT = "OutPut";
        public static readonly string KEY_OUTPUT_KIND = "OutPutKind";
        public static readonly string KEY_KIND_DEFAULT = "Default";
        public static readonly string KEY_KIND_MERGE = "Merge";
        public static readonly string KEY_KIND_PERFORCE = "Perforce";
        public static readonly string KEY_OUTPUT_MERGEAPP_PATH = "MergeAppPath";
        public static readonly string KEY_OUTPUT_USER = "UserName";
        public static readonly string KEY_OUTPUT_PORT = "ServicePort";
        public static readonly string KEY_OUTPUT_CLIENT = "ClientName";
        public static readonly string KEY_OUTPUT_CHANGELIST = "ChangeList";
        public static readonly string KEY_CHANGELIST_DEFAULT = "Default";
        public static readonly string KEY_CHANGELIST_CREATE = "新規";
        public static readonly string KEY_CHANGELIST_UPDATE = "Update";

        public static readonly string SETCION_VERSION = "Version";
        public static readonly string KEY_VERSION_MODE = "Mode";
        public static readonly string KEY_MODE_SPEED = "TopSpeed";
        public static readonly string KEY_MODE_STABLE = "Stable";

        public static readonly string SETCION_SETTINGS = "Settings";
        public static readonly string KEY_SETTINGS_MODE = "Output";

        #endregion

        #region Excel 共通情報
        public static readonly string EXCEL_COMMON_HONYAKU = @"__(T_T)__HONYAKU";
        public static readonly string EXCEL_COMMON_MSGNO = "MsgNo";
        public static readonly string EXCEL_COMMON_LCD = "LCD";
        public static readonly string EXCEL_COMMON_NA = "N/A";
        public static readonly string EXCEL_COMMON_ENTER = "|00H,0DH,00H,0AH|";

        #endregion

        #region 文言申請システム　ファイル情報
        public static readonly string EXCEL_WORDING_SHEET = "文言一覧";
        public static readonly int EXCEL_WORDING_STARTLINE = 15;
        public static readonly string EXCEL_WORDING_NO = "申請No";
        public static readonly string EXCEL_WORDING_SERIES = "シリーズ";
        public static readonly string EXCEL_WORDING_MODEL = "モデル";
        public static readonly string EXCEL_WORDING_FONT = "フォント種別";
        public static readonly string EXCEL_WORDING_FONT_1 = "ビットマップ";
        public static readonly string EXCEL_WORDING_FONT_2 = "プロポーショナル";
        public static readonly string EXCEL_WORDING_WORDCOUNT = "申請字数";
        public static readonly string EXCEL_WORDING_LINENUM = "申請行数";
        public static readonly string EXCEL_WORDING_APPLYHEIGHT = "申請高さ";
        public static readonly string EXCEL_WORDING_APPLYWIDTH= "申請幅";
        public static readonly string EXCEL_WORDING_FONTHEIGHT = "フォント高さ";
        public static readonly string EXCEL_WORDING_DESTINATION = "仕向け";
        public static readonly string EXCEL_WORDING_JPNONLY = "JPNのみ";
        public static readonly string EXCEL_WORDING_STRINGID = "String ID";
        public static readonly string EXCEL_WORDING_HMISTRINGID = "HMI String ID";
        public static readonly string EXCEL_WORDING_STRINGIDEND = "_DSP";
        public static readonly string EXCEL_WORDING_REQUESTTYPE = "依頼種別";
        public static readonly string EXCEL_WORDING_REQUESTCANCEL = "取消";
        public static readonly string EXCEL_WORDING_UK = "UK";
        public static readonly string EXCEL_WORDING_US = "USA";
        public static readonly string EXCEL_WORDING_JPN = "日本語\n※カナ入力に注意\n「モデル」欄参照";
        public static readonly string EXCEL_WORDING_JPE = "日本語(英語)\nJPN English";
        public static readonly string EXCEL_MACRO_LOGIN = "StartCertification";
        public static readonly string EXCEL_MACRO_SEARCH = "StartSearchItem";
        public static readonly string EXCEL_MACRO_CLEAR = "StartSearchItemClear";
        #endregion

        #region 最終文言エクセル ファイル情報
        public static readonly string EXCEL_FINAL_SHEET = "TransCheckSheet";
        public static readonly string EXCEL_FINAL_WORDCOUNT = "Number of requested characters";
        public static readonly string EXCEL_FINAL_LINENUM = "Number of requested lines";
        public static readonly int EXCEL_FINAL_STARTLINE = 11;
        public static readonly string EXCEL_FINAL_UK = "UK";
        public static readonly string EXCEL_FINAL_US = "USA";
        public static readonly string EXCEL_FINAL_JPN = "JPN";
        public static readonly string EXCEL_FINAL_JPE = "JPE";
        public static readonly string EXCEL_FINAL_STRINGID = "String ID";
        public static readonly string EXCEL_FINAL_CHNWORDCOUNT = "文字数(中国)";
        #endregion

        #region MsgNo使用状況 ファイル情報
        public static readonly string EXCEL_MSGNO_SHEET = "MsgNo使用状況";
        public static readonly int EXCEL_MSGNO_STARTLINE = 0;
        public static readonly string EXCEL_MSGNO_MSGNO = "No";
        #endregion

        #region コントロール 属性情報
        public static readonly string ITEM_VISIBLE = "Visible";
        public static readonly string ITEM_COLLAPSED = "Collapsed";
        public static readonly string ITEM_COLOR_WHITE = "WhiteSmoke";
        public static readonly string ITEM_COLOR_PARENT = "Transparent";
        public static readonly string ITEM_COLOR_RED = "Red";
        public static readonly string ITEM_COLOR_PINK = "Pink";
        public static readonly string ITEM_COLOR_BLACK = "Black";
        #endregion

        #region 替换文言字典
        public static Dictionary<string, string> g_WordingChangeData = new Dictionary<string, string>()
        {
            {"|18H|","|25H,A0H|"},
            {"|19H|","|25H,A1H|"},
            {"|7FH|","|21H,90H|"},
            {"|7EH|","|21H,D2H|"},
            {"|17H|","|25H,C4H|"},
            {"|16H|","|25H,BAH|"},
            {"|06H|","|25H,B2H|"},
            {"|07H|","|25H,BCH|"},
            {"|FDH,00H|","|E0H,00H|"},
            {"|FDH,01H|","|E0H,01H|"},
            {"|FDH,02H|","|E0H,02H|"},
            {"|FDH,03H|","|E0H,03H|"},
            {"|FDH,04H|","|E0H,04H|"},
            {"|FDH,05H|","|E0H,05H|"},
            {"|FDH,06H|","|E0H,06H|"},
            {"|FDH,07H|","|E0H,07H|"},
            {"|FDH,08H|","|E0H,08H|"},
            {"|FDH,09H|","|E0H,09H|"},
            {"|FDH,0AH|","|E0H,0AH|"},
            {"|FDH,0BH|","|E0H,0BH|"},
            {"|FDH,0CH|","|E0H,0CH|"},
            {"|FDH,0DH|","|E0H,0DH|"},
            {"|FDH,0EH|","|E0H,0EH|"},
            {"|FDH,0FH|","|E0H,0FH|"},
            {"|FDH,10H|","|E0H,10H|"},
            {"|FDH,11H|","|E0H,11H|"},
            {"|FDH,12H|","|E0H,12H|"},
            {"|FDH,13H|","|E0H,13H|"},
            {"|FDH,14H|","|E0H,14H|"},
            {"|FDH,15H|","|E0H,15H|"},
            {"|FDH,16H|","|E0H,16H|"},
            {"|FDH,17H|","|E0H,17H|"},
            {"|FDH,18H|","|E0H,18H|"},
            {"|FDH,19H|","|E0H,19H|"},
            {"|FDH,1AH|","|E0H,1AH|"},
            {"|FDH,1BH|","|E0H,1BH|"},
            {"|FDH,1CH|","|E0H,1CH|"},
            {"|FDH,1DH|","|E0H,1DH|"},
            {"|FDH,1EH|","|E0H,1EH|"},
            {"|FDH,1FH|","|E0H,1FH|"},
            {"|FDH,20H|","|E0H,20H|"},
            {"|FDH,21H|","|E0H,21H|"},
            {"|FDH,22H|","|E0H,22H|"},
            {"|FDH,23H|","|E0H,23H|"},
            {"|FDH,24H|","|E0H,24H|"},
            {"|FDH,25H|","|E0H,25H|"},
            {"|FDH,26H|","|E0H,26H|"},
            {"|FDH,27H|","|E0H,27H|"},
            {"|FDH,28H|","|E0H,28H|"},
            {"|FDH,29H|","|E0H,29H|"},
            {"|FDH,2AH|","|E0H,2AH|"},
            {"|FDH,2BH|","|E0H,2BH|"},
            {"|FDH,2CH|","|E0H,2CH|"},
            {"|FDH,2DH|","|E0H,2DH|"},
            {"|FDH,2EH|","|E0H,2EH|"},
            {"|FDH,2FH|","|E0H,2FH|"},
            {"|FDH,30H|","|E0H,30H|"},
            {"|FDH,31H|","|E0H,31H|"},
            {"--(T_T)--HONYAKU","__(T_T)__HONYAKU"},
            {"|08H|","ą"},
            {"|09H|","ć"},
            {"|0AH|","ę"},
            {"|0BH|","ł"},
            {"|0CH|","ń"},
            {"|0DH|","ś"},
            {"|0EH|","ź"},
            {"|0FH|","ż"},
            {"|14H|","€"},
            {"|15H|","¿"},
            {"|B3H|","Ą"},
            {"|B4H|","Ć"},
            {"|B5H|","Ę"},
            {"|B6H|","Ł"},
            {"|B7H|","Ń"},
            {"|B8H|","Ś"},
            {"|B9H|","Ż"},
            {"|BAH|","Ź"},
            {"\n","|00H,0DH,00H,0AH|"},
            {"　"," "},
            {" "," "},
            {"？","?"},
            {"→","|21H,D2H|"},
            {"。"," "},
            {"、"," "},
            {"<","|00H,3CH|"},
            {">","|00H,3EH|"},
            {"І","I"},
            {"і","i"},
            {"｢","「"},
            {"｣","」"},
            {" |00H,0DH,00H,0AH|","|00H,0DH,00H,0AH|"}
        };
        #endregion
    }
}
