using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FTBExcel2Script
{
    class TCScriptCreat
    {
        /* 階層情報 */
        protected struct LevelInfo
        {
            /* 所在画面ID */
            public string screenID;
            /* Level名 */
            public string LevelName;
            /* Condition番号(0の場合はCondition無) */
            public int ConditionIndex;
        }

        /* 画面情報 */
        protected struct ScreenInfo
        {
            /* 画面ID */
            public string screenID;
            /* design名 */
            public string designName;
        }

        /* 置換Condition情報 */
        protected struct Condition_Replace
        {
            /* 画面ID */
            public string OldCondition;
            /* design名 */
            public string NewCondition;
        }

        /* 依据Condition種類执行不同操作 */
        protected struct ConditionReplaceInfo
        {
            /* 元Condition*/
            public string oldCondition;
            /* Condition種類 */
            public string conditionType;
            /* 置換内容 */
            public string conditionReplaceValue;
        }

        /* 手順情報 */
        protected struct StepInfo
        {
            /* design名 */
            public string designName;
            /* キーワード */
            public string DomainKW;
            /* ボタン名/リスト名 */
            public string us_word;
        }

        /* 期待結果情報 */
        //protected struct ExpectInfo
        //{
        //    /* design名 */
        //    public string designName;
        //    /* キーワード */
        //    public string DomainKW;
        //    /* ボタン名 */
        //    public string expectResult;
        //}

        private List<string> AllConditionList = null;

        public static List<string> AllLanguageList = null;

        private static List<ScreenInfo> allScreenInfo = null;
        private static List<Condition_Replace> allConditionReplaceInfo = null;

        private static List<ConditionReplaceInfo> totalConditionReplaceInfo = null;
        /* 全階層情報 */
        private string configurePath = Directory.GetCurrentDirectory() + "\\Configure.xlsx";
        protected List<LevelInfo> allLevelInfo = null;
        protected List<string> oneOpeConditionInfo = null;
        protected List<StepInfo> oneOpeProcedureInfo = null;
        //protected List<ExpectInfo> oneOpeResultInfo = null;
        protected StepInfo oneStepInfo;
        //protected ExpectInfo oneExpectInfo;

        protected const string DESIGN_NAME_TAIKI = "";

        protected static int Sel_Continent_Index;

        //protected List<string> countryList = null;
        protected static int Sel_Country_Index;

        protected static FTBMccInfo ftbMccAttri = null;

        /* 「TC1_全画面確認」の選択状態 */
        private static bool[] TC_AllScrnConfirm_SelFlag;
        enum TC_View_AllScrn
        {
            /* 「TC観点の選択：基本画面遷移」 */
            ScrnBasicChk = 0,
            /* 「TC観点の選択：画面タイトル」の選択状態 */
            ScrnTitle,
            /* 「TC観点の選択：リスト順序」の選択状態 */
            ListContent,
            /* 「TC観点の選択：項目有効性」の選択状態 */
            ItemEnable,
            /* 「TC観点の選択：右下角のデフォルト値」の選択状態 */
            SubDefaultValue,
            /* 「TC観点の選択：オプション画面のデフォルト値 */
            OptionDefaultValue,
            /* 「TC観点の選択：SoftKey入力種類」の選択状態 */
            ScrnSoftKey,
            /* 「TC観点の選択：情報表示文字種類」の選択状態 */
            ScrnMachinInfo,
            /* 最大值 */
            MAX
        };
        private static string TC_FuncNameAllScrnConfirm;


        /* 「TC2_オプション設定」の選択状態 */
        private static bool[] TC_OptionSet_SelFlag;
        enum TC_View_Option
        {
            /* 「TC観点の選択：オプション値変更」の選択状態 */
            ValueChg = 0,
            /* 「TC観点の選択：下标值確認」の選択状態 */
            SubValue,
            /* 最大值 */
            MAX
        };
        private static string TC_FuncNameAllOptionSet;

        private static string CurTCModelName;
        private string CurTCModuleName;
        /* テスト优先级(数字大者优先级高) */
        private int CurTCPriority;

        /* TC生成时，用例标题用 */
        private string ListValueName = "";

        private static string DeviceInitPass = "initpass";

        public static string FileSaveRootPath;
        private string SheetName;

        public static string CurTCProductID;

        /* TP7.0以外的机型存在Settings Button */
        private static bool settingsButtonFlg;
        /* TempSetがtrueの場合、オプション設定のTC手順に待機画面へ戻る必要ないです */
        private static bool TempSetFlg;

        /* trueの場合、機能可用のCheckが必要です */
        private bool FunEnableChkFlag = false;
        public string needReplacePath = string.Empty;
        public string replaceConditionContent = string.Empty;
        public string replacePathContent = string.Empty;


        private DataTable dtScriptInfoNoCondition;              /* TC生成：条件がない、手修正必要ない */
        private DataTable dtScriptInfoNoCondition_NeedRevise;   /* TC生成：条件がない、手修正必要 */
        private DataTable dtScriptInfoWithCondition;            /* TC生成：条件が有る、手修正必要 */

        private int TCCountNoCondition = 0, TCCountWithCondition = 0;

        /* 需要将关键字「菜单_XX」变更为「功能_XX」的Value值 */
        private static string[] strArrLevelInfo2FunList = {
            "Copies",
            "ID",
            ">",
            "Layout",
            "Options",
            "Address Book",
            "Call History",
            "Print Settings",
            "DefaultSettings"
        };

        /* 生成的TC需要进行手修正的Value值 */
        private static string[] strArrManualModify = {
            "Move to",
            "[",
            "]",
            "Input Valid Char"
        };

        private static string[] specified = {
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Network",
            "メニュー_選択    E-mail/IFAX",
            "メニュー_選択    Setup Server",
            "メニュー_選択    POP3/IMAP4",
            "メニュー_選択    Protocol",
            "オプション_選択    IMAP4",
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Network",
            "メニュー_選択    E-mail/IFAX",
            "メニュー_選択    Setup Server",
            "メニュー_選択    POP3/IMAP4",
            "メニュー_選択    Select Folder",
            "メニュー_選択    Specified",
            "入力_クリーン",
            "入力_入力    aaaa",
            "入力_コミット",
            "メニュー_右下値チェック    Select Folder    Specified",
            "メニュー_選択    Select Folder",
            "オプション_選択値チェック    Specified",
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Network",
            "メニュー_選択    E-mail/IFAX",
            "メニュー_選択    Setup Server",
            "メニュー_選択    POP3/IMAP4",
            "メニュー_右下値チェック    Select Folder    Specified",
            "メニュー_選択    Select Folder",
            "オプション_選択値チェック    Specified"
        };


        private static string[] carbonMenuCopies = {
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Printer",
            "メニュー_選択    Carbon Menu",
            "メニュー_選択    Carbon Copy",
            "オプション_選択    On",
            "メニュー_選択    Copies",
            "タイトル_チェック    Copies",
            "入力_クリーン",
            "入力_入力    1",
            "入力_コミット",
            "メニュー_右下値チェック    Copies    1",
            "メニュー_選択    Copies",
            "入力_值チェック    1",
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Printer",
            "メニュー_選択    Carbon Menu",
            "メニュー_右下値チェック    Copies    1",
            "メニュー_選択    Copies",
            "入力_值チェック    1"
        };
        private static string[] faxStorageBackupPrintOff = {
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Fax",
            "メニュー_選択    Setup Receive",
            "メニュー_選択    Memory Receive",
            "オプション_有効    Fax Storage",
            "オプション_選択    Fax Storage",
            "オプション_リストチェック    Backup Print: On    Backup Print: Off",
            "オプション_選択    Backup Print: Off",
            "メニュー_右下値チェック    Memory Receive    Fax Storage",
            "メニュー_選択    Memory Receive",
            "オプション_選択値チェック    Fax Storage",
            "オプション_選択    Fax Storage",
            "オプション_選択値チェック    Backup Print: Off",
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Fax",
            "メニュー_選択    Setup Receive",
            "メニュー_右下値チェック    Memory Receive    Fax Storage",
            "メニュー_選択    Memory Receive",
            "オプション_選択値チェック    Fax Storage",
            "オプション_選択    Fax Storage",
            "オプション_選択値チェック    Backup Print: Off"
        };



        private static string[] dialPrefix = {
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Initial Setup",
            "メニュー_選択    Dial Prefix",
            "オプション_選択    On",
            "メニュー_選択    Dial Prefix",
            "入力_クリーン",
            "入力_入力    8",
            "入力_コミット",
            "メニュー_選択    Dial Prefix",
            "オプション_選択    On",
            "メニュー_右下値チェック    Dial Prefix    8",
            "メニュー_選択    Dial Prefix",
            "入力_值チェック    8",
            "共通_バックホーム",
            "機能_選択    All Settings",
            "メニュー_選択    Initial Setup",
            "メニュー_選択    Dial Prefix",
            "オプション_選択    On",
            "メニュー_右下値チェック    Dial Prefix    8",
            "メニュー_選択    Dial Prefix",
            "入力_值チェック    8"
        };

        private static string[] pcFaxReceiveBackupPrint = {
            "1. 共通_バックホーム",
            "2. 機能_選択    All Settings",
            "3. メニュー_選択    Fax",
            "4. メニュー_選択    Setup Receive",
            "5. メニュー_選択    PC Fax Receive",
            "6. オプション_有効    On",
            "6. オプション_選択    On",
            "6. 機能_選択    OK",
            "7. 機能_選択    OK",
            "8. タイトル_チェック    PC Fax Receive",
            "8. オプション_リストチェック    Backup Print: On    Backup Print: Off",
            "8. オプション_有効    Backup Print: On",
            "8. オプション_選択    Backup Print: On",
            "9. メニュー_右下値チェック    PC Fax Receive    On",
            "10. メニュー_選択    PC Fax Receive",
            "11. オプション_選択値チェック    On",
            "12. 共通_バックホーム",
            "13. 機能_選択    All Settings",
            "14. メニュー_選択    Fax",
            "15. メニュー_選択    Setup Receive",
            "16. メニュー_右下値チェック    PC Fax Receive    On",
            "16. メニュー_選択    PC Fax Receive",
            "16. オプション_選択値チェック    On",
            "16. オプション_選択    On",
            "17. 機能_選択    OK",
            "17. 機能_選択    OK",
            "17. オプション_選択値チェック    Backup Print: On"
        };

        private static string[] faxMemoryReceiveFaxForward = {
            "1. 共通_バックホーム",
            "2. 機能_選択    All Settings",
            "3. メニュー_選択    Fax",
            "4. メニュー_選択    Setup Receive",
            "5. メニュー_選択    Memory Receive",
            "6. オプション_有効    Fax Forward",
            "6. オプション_選択    Fax Forward",
            "7. タイトル_チェック    Fax Forward",
            "8. オプション_リストチェック    Manual    Address Book",
            "8. メニュー_選択    Manual",
            "8. 入力_入力    12",
            "8. 入力_コミット",
            "9. タイトル_チェック    Fax Forward",
            "9. オプション_リストチェック    Backup Print: On    Backup Print: Off",
            "9. オプション_選択    Backup Print: Off",
            "9. メニュー_右下値チェック    Memory Receive    Fax Forward",
            "10. メニュー_選択    Memory Receive",
            "11. オプション_選択値チェック    Fax Forward",
            "12. 共通_バックホーム",
            "13. 機能_選択    All Settings",
            "14. メニュー_選択    Fax",
            "15. メニュー_選択    Setup Receive",
            "16. メニュー_右下値チェック    Memory Receive    Fax Forward",
            "16. メニュー_選択    Memory Receive",
            "17. オプション_選択値チェック    Fax Forward"
        };

        private static string[] dateYearMonthDay = {
            "1. 共通_バックホーム",
            "2. 機能_選択    All Settings",
            "3. メニュー_選択    Initial Setup",
            "4. メニュー_選択    Date & Time",
            "5. メニュー_選択    Date",
            "5. 入力_タイプチェック    Manual:<Limit:4,Charaset:NUM>",
            "5. 入力_入力    22",
            "5. 入力_コミット",
            "6. 入力_タイプチェック    Manual:<Limit:2,Charaset:NUM>",
            "6. 入力_入力    10",
            "6. 入力_コミット",
            "7. 入力_タイプチェック    Manual:<Limit:2,Charaset:NUM>",
            "7. 入力_入力    10",
            "7. 入力_コミット",
            "8. メニュー_右下値チェック    Date    10.10.2022"
        };

        private static string[] strArrHeadInfo = {
            "用例编号",
            "所属产品",
            "所属模块",
            "相关需求",
            "用例标题",
            "前置条件",
            "步骤",
            "预期",
            "实际情况",
            "关键词",
            "优先级",
            "用例类型",
            "适用阶段",
            "用例状态",
            "B",
            "R",
            "S",
            "结果",
            "由谁创建",
            "创建日期",
            "最后修改者",
            "修改日期",
            "用例版本",
            "相关用例",
        };

        /* 绘文言的值,右下值不确认 */
        private static string[] squareOptionWords = {
            "-2", "-1", "0", "+1", "+2",
            "-20", "-10", "0", "+10", "+20",
            "-90°", "-45°", "0", "+45°", "+90°",
            "-50", "-25", "0", "+25", "+50",
            "-5", "-4", "-3", "-2", "-1", "0", "+1", "+2", "+3", "+4", "+5",
            "-50", "-40", "-30", "-20", "-10","0", "+10", "+20", "+30", "+40", "+50"
        };

        private static string[] strArrCaseInfo = {
            "0",                //"用例编号：用例在用例库被分配的ID(属于自动分配),可全部设定为0"
            "N",                //"所属产品"
            "N",                //"所属模块"
            "Y",                //"相关需求，可设定为空"
            "N",                //"用例标题"
            "N",                //"前置条件"
            "N",                //"步骤"
            "N",                //"预期"
            "Y",                //"实际情况"
            "Y",                //"关键词，和“适用阶段”的内容相同"
            "N",                //"优先级，可设定（1、2、3、4）"
            "功能测试",          //"用例类型"
            "N",                //"适用阶段，语言选择（USA、CAN、CHN等）"
            "正常",              //"用例状态，可选择（正常、被阻塞、研究中"
            "0",                //"Bug数目"
            "0",                //"用例执行的结果数（初始为0）"
            "0",                //"用例的步骤数"
            "Y",                //"结果：当前case的测试结果,可空"
            "Y",                //"由谁创建：创建人,可空"
            "Y",                //"创建日期,可空"
            "Y",                //"最后修改者，可空"
            "Y",                //"修改日期，可空"
            "Y",                //"用例版本，可空"
            "Y",                //"相关用例，可空"
        };


        /* 画面の種別 */
        enum ScrnType
        {
            /* null */
            None = 0,
            /* リスト画面 */
            List,
            /* SoftKey画面 */
            SoftKey,
            /* MachinInfo画面(Disp Only) */
            MachinInfo,
            /* 未知画面 */
            Unknow,
            /* 最大值 */
            MAX
        };

        public TCScriptCreat()
        {

        }

        public bool settingsButtonExistSet
        {
            set { settingsButtonFlg = value; }
        }

        public static string SelModelName
        {
            set { CurTCModelName = value; }
        }

        public static int SelContinentIndex
        {
            set { Sel_Continent_Index = value; }
        }

        public static int SelCountryIndex
        {
            set { Sel_Country_Index = value; }
        }

        public static string FuncNameAllScrnConfirm
        {
            set { TC_FuncNameAllScrnConfirm = value; }
        }

        public static string FuncNameAllOptionSet
        {
            set { TC_FuncNameAllOptionSet = value; }
        }

        public FTBMccInfo CurFTBMccInfo
        {
            get { return ftbMccAttri; }
        }

        public bool TempSet
        {
            set { TempSetFlg = value; }
        }

        public string CurSheetName
        {
            set { SheetName = value; }
        }

        public int TCCount_NoCondition
        {
            get { return TCCountNoCondition; }
            set { TCCountNoCondition = value; }
        }

        public int TCCount_WithCondition
        {
            get { return TCCountWithCondition; }
            set { TCCountWithCondition = value; }
        }

        public static void TC_TestView_Content_SelFlag(bool[] TC_AllScrnConf_SelSts, bool[] TC_OptionSet_SelSts)
        {
            TC_AllScrnConfirm_SelFlag = new bool[(int)TC_View_AllScrn.MAX];
            for (int i = 0; i < TC_AllScrnConfirm_SelFlag.Length; i++)
            {
                TC_AllScrnConfirm_SelFlag[i] = TC_AllScrnConf_SelSts[i];
            }

            TC_OptionSet_SelFlag = new bool[(int)TC_View_Option.MAX];
            for (int i = 0; i < TC_OptionSet_SelFlag.Length; i++)
            {
                TC_OptionSet_SelFlag[i] = TC_OptionSet_SelSts[i];
            }
        }

        /* modelRelate情報読む */
        public static void read_modelRelateInfo(string fileFullPath)
        {
            ScreenInfo tempScreenInfo;
            allScreenInfo = new List<ScreenInfo>();
            DataTable modelRelateDT = CSVFileHelper.CSV2DataTable(fileFullPath);

            foreach (DataRow modelInfo in modelRelateDT.Rows)
            {
                if (true == modelInfo["LSRFB_70TP"].ToString().Equals("-"))
                {
                    continue;
                }
                tempScreenInfo.screenID = modelInfo["抽象画面ID"].ToString();
                tempScreenInfo.designName = modelInfo["画面レイアウトID名"].ToString();

                allScreenInfo.Add(tempScreenInfo);
            }

            return;
        }

        /* Condition置換情報読む */
        public static void read_FTB_Condition_Replace(string fileFullPath)
        {
            Condition_Replace ConditionReplaceInfo;
            allConditionReplaceInfo = new List<Condition_Replace>();

            if (false == File.Exists(fileFullPath))
            {
                return;
            }

            DataTable modelRelateDT = CSVFileHelper.Excel2DataTable(fileFullPath);

            foreach (DataRow modelInfo in modelRelateDT.Rows)
            {
                ConditionReplaceInfo.OldCondition = modelInfo["元Condition"].ToString();
                ConditionReplaceInfo.NewCondition = modelInfo["置換Condition"].ToString();

                allConditionReplaceInfo.Add(ConditionReplaceInfo);
            }

            return;
        }

        public static void readConditionReplaceContent(string fileFullPath)
        {
            ConditionReplaceInfo conditionReplaceInfo = new ConditionReplaceInfo();
            totalConditionReplaceInfo = new List<ConditionReplaceInfo>();
            if (false == File.Exists(fileFullPath))
            {
                return;
            }
            DataTable tcConditionsDT = CSVFileHelper.excelToDataTableBasedSheetIndex(fileFullPath, 2);

            foreach (DataRow tcConditions in tcConditionsDT.Rows)
            {
                conditionReplaceInfo.oldCondition = tcConditions[0].ToString(); //第1列的值
                conditionReplaceInfo.conditionType = tcConditions[1].ToString(); //第2列的值
                conditionReplaceInfo.conditionReplaceValue = tcConditions[2].ToString(); //第3列的值
                totalConditionReplaceInfo.Add(conditionReplaceInfo);
            }
            return;
        }

        /* 
         *  Description:write Script to csv
         *  Param: ftbMccAttri,levelNodel-levelNode param
         *  Param: index-sheet name index
         *  Return:
         *  Exception:
         *  Example:write2Script(ftbMccAttri,levelNodel,strSaveFileName)
         */
        public void write2Script(FTBMccInfo ftbMccAttri, LevelNode levelNodel, FTBConditionInfo ftbCondition)
        {
            AllConditionList = ftbCondition.conditions_list;

            allLevelInfo = new List<LevelInfo>();

            //创建一个空表
            dtScriptInfoNoCondition = new DataTable();
            foreach (string strHead in strArrHeadInfo)
            {
                dtScriptInfoNoCondition.Columns.Add(AddDoubleQuotes(strHead), typeof(String));
            }

            //创建一个空表
            dtScriptInfoNoCondition_NeedRevise = new DataTable();
            foreach (string strHead in strArrHeadInfo)
            {
                dtScriptInfoNoCondition_NeedRevise.Columns.Add(AddDoubleQuotes(strHead), typeof(String));
            }

            //创建一个空表
            dtScriptInfoWithCondition = new DataTable();
            foreach (string strHead in strArrHeadInfo)
            {
                dtScriptInfoWithCondition.Columns.Add(AddDoubleQuotes(strHead), typeof(String));
            }

            FunEnableChkFlag = true;

            Read_LevelNode_DetailInfo(levelNodel);

            TCCountNoCondition += dtScriptInfoNoCondition.Rows.Count;
            TCCountNoCondition += dtScriptInfoNoCondition_NeedRevise.Rows.Count;
            TCCountWithCondition += dtScriptInfoWithCondition.Rows.Count;

            SaveScriptFile(dtScriptInfoNoCondition, "No_Condition", true);
            SaveScriptFile(dtScriptInfoNoCondition_NeedRevise, "No_Condition_Manual_Modify", true);
            SaveScriptFile(dtScriptInfoWithCondition, "With_Condition", true);
        }

        /* LevelNodeから、詳細な階層情報読む */
        private void Read_LevelNode_DetailInfo(LevelNode levelNodel)
        {
            ScrnType tempScrnType = ScrnType.None;
            LevelNode tempLevelNode;
            LevelInfo tempLevelInfo;
            OptionNode tempOptionNode;
            if (null == levelNodel)
            {
                return;
            }
            string str_us_words = levelNodel.ftbbutton.us_words;
            string str_cur_scrn_id;
            int cur_IndexOfCondition;
            if (false == string.IsNullOrEmpty(str_us_words))
            {
                str_cur_scrn_id = levelNodel.ftbbuttontitle.cur_scrn_id;
                cur_IndexOfCondition = levelNodel.ftbbutton.condition_index;

                oneStepInfo.us_word = str_us_words;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(str_cur_scrn_id);

                tempLevelInfo.LevelName = str_us_words;
                tempLevelInfo.screenID = str_cur_scrn_id;
                tempLevelInfo.ConditionIndex = cur_IndexOfCondition;
                if (allLevelInfo.Count >= levelNodel.ftbbutton.cur_Level)
                {
                    allLevelInfo.RemoveRange(levelNodel.ftbbutton.cur_Level - 1, allLevelInfo.Count - levelNodel.ftbbutton.cur_Level + 1);
                    allLevelInfo.Add(tempLevelInfo);
                }
                else if (allLevelInfo.Count < levelNodel.ftbbutton.cur_Level)
                {
                    allLevelInfo.Add(tempLevelInfo);
                }

                /* 次階层画面の種別を取得する */
                tempScrnType = CheckNextScrnType(levelNodel);
                if (ScrnType.None != tempScrnType)
                {
                    if (ScrnType.SoftKey == tempScrnType)
                    {
                        /* SoftKey画面の確認 */
                        if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnBasicChk])
                        {
                            /* TC生成:SoftKey画面の確認 */
                            TCCreat_ScrnSoftKeyInfo(levelNodel);
                        }
                    }
                    else if (ScrnType.MachinInfo == tempScrnType)
                    {
                        /* MachinInfo画面(DispOnly)の確認 */
                        if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnBasicChk])
                        {
                            /* TC生成:MachinInfo画面(DispOnly)の確認 */
                            TCCreat_ScrnMachinInfo(levelNodel);
                        }
                    }
                    else
                    {
                        if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnBasicChk])
                        {
                            /* TC生成:需要输出画面的list情报、画面Title，检查Menu有效性 */
                            TCCreat_ScrnTitleAndAllListInfo(levelNodel);
                        }
                    }
                }
            }

            if (0 < levelNodel.levelnodes.Count)
            {
                for (int i = 0; i < levelNodel.levelnodes.Count; i++)
                {
                    /* リスト情報 */
                    if (levelNodel.levelnodes[i] is LevelNode)
                    {
                        tempLevelNode = levelNodel.levelnodes[i] as LevelNode;
                        Read_LevelNode_DetailInfo(tempLevelNode);
                    }
                    /* 値設定情報 */
                    else if (levelNodel.levelnodes[i] is OptionNode)
                    {
                        tempOptionNode = levelNodel.levelnodes[i] as OptionNode;

                        /* 取得当前阶层画面的画面种别 */
                        tempScrnType = CheckOptionScrnType(tempOptionNode.ftboption.us_words);
                        if (ScrnType.None != tempScrnType)
                        {
                            if (ScrnType.MachinInfo == tempScrnType)
                            {
                                /* MachinInfo画面のオプション設定確認が必要ない */
                            }
                            else if (ScrnType.SoftKey == tempScrnType)
                            {
                                /* SoftKey画面の值設定確認 */
                                if (true == TC_OptionSet_SelFlag[(int)TC_View_Option.ValueChg])
                                {
                                    /* TC生成:需要输出SoftKey画面值设定 */
                                    TCCreat_ScrnSoftKeyInfoChangeInput(tempOptionNode);
                                }
                            }
                            else
                            {
                                if (true == TC_OptionSet_SelFlag[(int)TC_View_Option.ValueChg])
                                {
                                    if (true == str_us_words.Equals("Local Language"))
                                    {
                                        continue;
                                    }
                                    ListValueName = tempOptionNode.ftboption.us_words;
                                    /* TC生成:List画面值设定 */
                                    TCCreat_ScrnListInfoChangeValue(tempOptionNode);
                                    ListValueName = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        /* Do nothing */
                    }
                }
            }

            return;
        }

        /* TC生成：画面标题和画面list,Menu有效性检查 */
        private void TCCreat_ScrnTitleAndAllListInfo(LevelNode levelNodel)
        {
            int level_index = 0;
            int listindex = 0;
            int tempIndex = 0;

            bool OptionExistFlg = false;
            string strDefaultValue = "";

            string str_temp_us_words = "";
            string strTempList = "";
            LevelNode tempLevelNode;
            OptionNode tempOptionNode;
            List<string> strListInfo = new List<string>();

            oneOpeConditionInfo = new List<string>();
            oneOpeProcedureInfo = new List<StepInfo>();
            //oneOpeResultInfo = new List<ExpectInfo>();

            if (null == levelNodel)
            {
                return;
            }

            /* 次阶层情报不存在 */
            if (0 == levelNodel.levelnodes.Count)
            {
                return;
            }

            /* 次阶层情报为空 */
            if ((1 == levelNodel.levelnodes.Count))
            {
                if (true == (levelNodel.levelnodes[0] is OptionNode))
                {
                    if (true == string.IsNullOrEmpty((levelNodel.levelnodes[0] as OptionNode).ftboption.us_words))
                    {
                        return;
                    }
                }
            }


            str_temp_us_words = levelNodel.ftbbutton.us_words;

            if (false == string.IsNullOrEmpty(str_temp_us_words))
            {
                /* 追加共通步骤 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                if (settingsButtonFlg == true && TempSetFlg == false)
                {
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                    oneStepInfo.us_word = "Settings";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                if (true == FunEnableChkFlag)
                {
                    FunEnableChkFlag = false;
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                    oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    oneStepInfo.us_word = allLevelInfo[0].LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }


                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    /* 有効なCondition情報を追加する */
                    AddCommonConditionInfo(tempLevelInfo.LevelName, tempLevelInfo.ConditionIndex);

                    if (0 == level_index)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    }
                    else
                    {
                        /* 各画面確認:测试Skip Blank Page Sensitivity时前置条件的追加 */
                        if ("Skip Blank Page Sensitivity".Equals(tempLevelInfo.LevelName))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Skip Blank Page";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Skip Blank Page
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "On"; // 选项_选择    On
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                        if ("Color/Gray TIFF Compression".Equals(tempLevelInfo.LevelName))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Resolution";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Resolution
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "300 dpi"; // 选项_选择    300 dpi
                            oneOpeProcedureInfo.Add(oneStepInfo);
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "File Type";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    File Type
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "TIFF Single-Page"; // 选项_选择    TIFF Single-Page
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                        if ("B&W TIFF Compression".Equals(tempLevelInfo.LevelName))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Scan Type";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Scan Type
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "Black and White"; // 选项_选择    Black and White
                            oneOpeProcedureInfo.Add(oneStepInfo);
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Resolution";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Resolution
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "300 dpi"; // 选项_选择    300 dpi
                            oneOpeProcedureInfo.Add(oneStepInfo);
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "File Type";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    File Type
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "TIFF Single-Page"; // 选项_选择    TIFF Single-Page
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                        /* 2-sided Copy Page Layout前置条件的追加 */
                        if ("2-sided Copy Page Layout".Equals(tempLevelInfo.LevelName))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "2-sided Copy";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    2-sided Copy
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "2-sided⇒2-sided"; // 选项_选择    On
                            oneOpeProcedureInfo.Add(oneStepInfo);
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Page Layout";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Page Layout
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "2in1(Portrait)"; // 选项_选择    2in1(Portrait)
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                        if (level_index == (allLevelInfo.Count - 1))
                        {
                            /* 項目有効性 */
                            if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ItemEnable]))
                            {
                                /* 項目有効性Check */
                                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Enable;
                                oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                                oneStepInfo.us_word = tempLevelInfo.LevelName;
                                oneOpeProcedureInfo.Add(oneStepInfo);
                            }

                            /* 右下角のデフォルト値 */
                            if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.SubDefaultValue]))
                            {
                                if (0 < levelNodel.levelnodes.Count)
                                {
                                    listindex = 0;
                                    /* デフォルト値取得 */
                                    for (listindex = 0; listindex < levelNodel.levelnodes.Count; listindex++)
                                    {
                                        if (levelNodel.levelnodes[listindex] is OptionNode)
                                        {
                                            OptionExistFlg = true;
                                            tempOptionNode = levelNodel.levelnodes[listindex] as OptionNode;
                                            if (true == tempOptionNode.ftboption.factory_setting.Equals("Y"))
                                            {
                                                strDefaultValue = tempOptionNode.ftboption.us_words;
                                                break;
                                            }
                                        }
                                    }
                                    bool squareOptionExit = false;
                                    List<string> squareOptionlist = new List<string>(squareOptionWords);
                                    /* 绘文言的值,右下值不确认 */
                                    if (squareOptionlist.Find(s => s == strDefaultValue) != null)
                                    {
                                        squareOptionExit = true;
                                    }
                                    if ((true == OptionExistFlg) && (false == string.IsNullOrEmpty(strDefaultValue)) && squareOptionExit == false)
                                    {
                                        /* 右下角のデフォルト値Check */
                                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                                        oneStepInfo.us_word = tempLevelInfo.LevelName + "    " + strDefaultValue;
                                        oneOpeProcedureInfo.Add(oneStepInfo);
                                    }
                                }
                            }
                        }

                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        /* design名設定 */
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                    }

                    oneStepInfo.us_word = tempLevelInfo.LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    AddCommonSetp_InputPSW();

                    level_index++;
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnTitle]))
                {
                    /* 需要输出画面的Title */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Title.Title;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                    oneStepInfo.us_word = levelNodel.ftbbutton.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ListContent]))
                {
                    OptionExistFlg = false;
                    /* 需要输出画面的list情报 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Option.List;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                    oneStepInfo.us_word = "";

                    if (0 < levelNodel.levelnodes.Count)
                    {
                        /* リスト情報 */
                        str_temp_us_words = "";

                        listindex = 0;
                        /* リスト情報作成 */
                        for (listindex = 0; listindex < levelNodel.levelnodes.Count; listindex++)
                        {
                            if (levelNodel.levelnodes[listindex] is LevelNode)
                            {
                                tempLevelNode = levelNodel.levelnodes[listindex] as LevelNode;
                                tempIndex = tempLevelNode.ftbbutton.condition_index;
                                strTempList = tempLevelNode.ftbbutton.us_words;
                            }
                            /* 値設定情報 */
                            else if (levelNodel.levelnodes[listindex] is OptionNode)
                            {
                                OptionExistFlg = true;
                                tempOptionNode = levelNodel.levelnodes[listindex] as OptionNode;
                                tempIndex = tempOptionNode.ftboption.condition_index;
                                strTempList = tempOptionNode.ftboption.us_words;

                                if (true == tempOptionNode.ftboption.factory_setting.Equals("Y"))
                                {
                                    strDefaultValue = strTempList;
                                }
                            }

                            /* 有効なCondition情報を追加する */
                            AddCommonConditionInfo(strTempList, tempIndex);

                            if ((false == string.IsNullOrEmpty(strTempList)) && (false == strListInfo.Contains(strTempList)))
                            {
                                strListInfo.Add(strTempList);
                            }
                        }

                        if (false == OptionExistFlg)
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.List;
                        }

                        if (0 < strListInfo.Count)
                        {
                            /* リスト情報合成 */
                            if (true == levelNodel.ftbbutton.us_words.Equals("Local Language"))
                            {
                                foreach (string strList in AllLanguageList)
                                {
                                    str_temp_us_words = str_temp_us_words + strList + "    ";
                                }
                            }
                            else
                            {
                                foreach (string strList in strListInfo)
                                {
                                    str_temp_us_words = str_temp_us_words + strList + "    ";
                                }
                            }
                            oneStepInfo.us_word = str_temp_us_words.Remove(str_temp_us_words.Length - 4, 4);
                        }
                    }
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.OptionDefaultValue])
                {
                    if ((true == OptionExistFlg) && (false == string.IsNullOrEmpty(strDefaultValue)))
                    {
                        /* 需要输出オプション画面のデフォルト値 */
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.SelectValue;
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                        oneStepInfo.us_word = strDefaultValue;
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                }

                CurTCModuleName = TC_FuncNameAllScrnConfirm;
                CurTCPriority = 102;
                MakeTC_With_OpeProcedureInfo();
            }

            return;
        }

        /* TC生成：画面标题和SoftKey画面情報 */
        private void TCCreat_ScrnSoftKeyInfo(LevelNode levelNodel)
        {
            int level_index = 0;
            string strDefaultValue = "";
            string str_temp_us_words = "";
            OptionNode tempOption;
            oneOpeProcedureInfo = new List<StepInfo>();
            oneOpeConditionInfo = new List<string>();
            //oneOpeResultInfo = new List<ExpectInfo>();

            if (null == levelNodel)
            {
                return;
            }

            /* 次阶层情报不存在 */
            if (0 == levelNodel.levelnodes.Count)
            {
                return;
            }

            if (true == (levelNodel.levelnodes[0] is OptionNode))
            {
                tempOption = levelNodel.levelnodes[0] as OptionNode;
            }
            else
            {
                /* 次阶层不是Option画面 */
                return;
            }

            str_temp_us_words = levelNodel.ftbbutton.us_words;

            if (false == string.IsNullOrEmpty(str_temp_us_words))
            {
                /* デフォルト値取得 */
                strDefaultValue = tempOption.ftboption.factory_setting;


                /* 追加共通步骤 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                if (settingsButtonFlg == true && TempSetFlg == false)
                {
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                    oneStepInfo.us_word = "Settings";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }
                if (true == FunEnableChkFlag)
                {
                    FunEnableChkFlag = false;
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                    oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    oneStepInfo.us_word = allLevelInfo[0].LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    /* 有効なCondition情報を追加する */
                    AddCommonConditionInfo(tempLevelInfo.LevelName, tempLevelInfo.ConditionIndex);

                    if (0 == level_index)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    }
                    else
                    {
                        if (level_index == (allLevelInfo.Count - 1))
                        {
                            /* 項目有効性 */
                            if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ItemEnable]))
                            {
                                /* 項目有効性Check */
                                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Enable;
                                oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                                oneStepInfo.us_word = tempLevelInfo.LevelName;
                                oneOpeProcedureInfo.Add(oneStepInfo);
                            }

                            /* 右下角のデフォルト値 */
                            if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.SubDefaultValue]))
                            {
                                if (false == string.IsNullOrEmpty(strDefaultValue))
                                {
                                    /* 右下角のデフォルト値Check */
                                    oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                                    oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                                    oneStepInfo.us_word = tempLevelInfo.LevelName + "    " + strDefaultValue;
                                    oneOpeProcedureInfo.Add(oneStepInfo);
                                }
                            }
                        }

                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        /* design名設定 */
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                    }

                    oneStepInfo.us_word = tempLevelInfo.LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    AddCommonSetp_InputPSW();

                    level_index++;
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnTitle]))
                {
                    /* 需要输出画面的Title */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Title.Title;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                    oneStepInfo.us_word = levelNodel.ftbbutton.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnSoftKey]))
                {
                    /* 需要Check画面的可用的SoftKey */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.Type;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                    oneStepInfo.us_word = tempOption.ftboption.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    /* 需要Check画面的输入_值 */
                    if (false == string.IsNullOrEmpty(strDefaultValue))
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.InputValue;
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempOption.ftboption.cur_scrn_id);
                        oneStepInfo.us_word = strDefaultValue;
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                }

                CurTCModuleName = TC_FuncNameAllScrnConfirm;
                CurTCPriority = 102;
                /* 根据oneOpeProcedureInfo情报生成TC */
                MakeTC_With_OpeProcedureInfo();
            }

            return;
        }

        /* TC生成：画面标题和MachinInfo画面情報 */
        private void TCCreat_ScrnMachinInfo(LevelNode levelNodel)
        {
            int level_index = 0;
            string str_temp_us_words = "";
            OptionNode tempOption;
            oneOpeProcedureInfo = new List<StepInfo>();
            oneOpeConditionInfo = new List<string>();
            //oneOpeResultInfo = new List<ExpectInfo>();

            if (null == levelNodel)
            {
                return;
            }

            /* 次阶层情报不存在 */
            if (0 == levelNodel.levelnodes.Count)
            {
                return;
            }

            if (true == (levelNodel.levelnodes[0] is OptionNode))
            {
                tempOption = levelNodel.levelnodes[0] as OptionNode;
            }
            else
            {
                /* 次阶层不是Option画面 */
                return;
            }

            str_temp_us_words = levelNodel.ftbbutton.us_words;

            if (false == string.IsNullOrEmpty(str_temp_us_words))
            {
                /* 追加共通步骤 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);
                /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                if (settingsButtonFlg == true && TempSetFlg == false)
                {
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                    oneStepInfo.us_word = "Settings";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }
                if (true == FunEnableChkFlag)
                {
                    FunEnableChkFlag = false;
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                    oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    oneStepInfo.us_word = allLevelInfo[0].LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    /* 有効なCondition情報を追加する */
                    AddCommonConditionInfo(tempLevelInfo.LevelName, tempLevelInfo.ConditionIndex);

                    if (0 == level_index)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    }
                    else
                    {
                        if (level_index == (allLevelInfo.Count - 1))
                        {
                            /* 項目有効性 */
                            if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ItemEnable]))
                            {
                                /* 項目有効性Check */
                                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Enable;
                                oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                                oneStepInfo.us_word = tempLevelInfo.LevelName;
                                oneOpeProcedureInfo.Add(oneStepInfo);
                            }
                        }

                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        /* design名設定 */
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                    }

                    oneStepInfo.us_word = tempLevelInfo.LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    AddCommonSetp_InputPSW();

                    level_index++;
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnTitle]))
                {
                    /* 需要输出画面的Title */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Title.Title;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(levelNodel.ftbbutton.next_scrn_id);
                    oneStepInfo.us_word = levelNodel.ftbbutton.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                if ((true == TC_AllScrnConfirm_SelFlag[(int)TC_View_AllScrn.ScrnMachinInfo]))
                {
                    /* 需要输出画面的Display Info */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_MachineInfo.Type;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(tempOption.ftboption.cur_scrn_id);
                    oneStepInfo.us_word = tempOption.ftboption.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                CurTCModuleName = TC_FuncNameAllScrnConfirm;
                CurTCPriority = 96;
                /* 根据oneOpeProcedureInfo情报生成TC */
                MakeTC_With_OpeProcedureInfo();
            }

            return;
        }

        /* TC生成：List画面值设定 */
        private void TCCreat_ScrnListInfoChangeValue(OptionNode optionNode)
        {
            int level_index = 0;
            oneOpeProcedureInfo = new List<StepInfo>();
            oneOpeConditionInfo = new List<string>();

            if (null == optionNode)
            {
                return;
            }

            string str_temp_us_words = "";
            str_temp_us_words = optionNode.ftboption.us_words;

            if (false == string.IsNullOrEmpty(str_temp_us_words))
            {
                /* 追加共通步骤 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                if (settingsButtonFlg == true && TempSetFlg == false)
                {
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                    oneStepInfo.us_word = "Settings";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }
                if (true == FunEnableChkFlag)
                {
                    FunEnableChkFlag = false;
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                    oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    oneStepInfo.us_word = allLevelInfo[0].LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                level_index = 0;
                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    /* Skip Blank Page Sensitivity 前置条件的追加 */
                    if ("Skip Blank Page Sensitivity".Equals(tempLevelInfo.LevelName))
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "Skip Blank Page";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Skip Blank Page
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "On"; // 选项_选择    On
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                    if ("File Type".Equals(tempLevelInfo.LevelName))
                    {
                        /* High Compression PDF Single-Page/TIFF Multi-Page 前置条件的追加 */
                        if ("High Compression PDF Single-Page".Equals(optionNode.ftboption.us_words)
                            || "High Compression PDF Multi-Page".Equals(optionNode.ftboption.us_words))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Scan Type";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Scan Type
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "Color"; // 选项_选择    Color
                            oneOpeProcedureInfo.Add(oneStepInfo);
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Resolution";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Resolution
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "300 dpi"; // 选项_选择    300 dpi
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                        else if ("TIFF Single-Page".Equals(optionNode.ftboption.us_words)
                            || "TIFF Multi-Page".Equals(optionNode.ftboption.us_words))
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            oneStepInfo.us_word = "Scan Type";
                            oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Scan Type
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                            oneStepInfo.us_word = "Black and White"; // 选项_选择    Black and White
                            oneOpeProcedureInfo.Add(oneStepInfo);
                        }
                    }
                    if ("Color/Gray TIFF Compression".Equals(tempLevelInfo.LevelName))
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "Resolution";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Resolution
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "300 dpi"; // 选项_选择    300 dpi
                        oneOpeProcedureInfo.Add(oneStepInfo);
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "File Type";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    File Type
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "TIFF Single-Page"; // 选项_选择    TIFF Single-Page
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                    if ("B&W TIFF Compression".Equals(tempLevelInfo.LevelName))
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "Scan Type";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Scan Type
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "Black and White"; // 选项_选择    Black and White
                        oneOpeProcedureInfo.Add(oneStepInfo);
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "Resolution";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Resolution
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "300 dpi"; // 选项_选择    300 dpi
                        oneOpeProcedureInfo.Add(oneStepInfo);
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "File Type";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    File Type
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "TIFF Single-Page"; // 选项_选择    TIFF Single-Page
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                    /* 2-sided Copy Page Layout前置条件的追加 */
                    if ("2-sided Copy Page Layout".Equals(tempLevelInfo.LevelName))
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "2-sided Copy";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    2-sided Copy
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "2-sided⇒2-sided"; // 选项_选择    On
                        oneOpeProcedureInfo.Add(oneStepInfo);
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        oneStepInfo.us_word = "Page Layout";
                        oneOpeProcedureInfo.Add(oneStepInfo); // 菜单_选择    Page Layout
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                        oneStepInfo.us_word = "2in1(Portrait)"; // 选项_选择    2in1(Portrait)
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }

                    /* 有効なCondition情報を追加する */
                    AddCommonConditionInfo(tempLevelInfo.LevelName, tempLevelInfo.ConditionIndex);

                    if (0 == level_index)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    }
                    else
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        /* design名設定 */
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                    }

                    oneStepInfo.us_word = tempLevelInfo.LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    AddCommonSetp_InputPSW();

                    level_index++;
                }

                /* 有効なCondition情報を追加する */
                AddCommonConditionInfo(str_temp_us_words, optionNode.ftboption.condition_index);

                /* 选项可用Check */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Enable;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = optionNode.ftboption.us_words;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 选项存在Check */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Option.Select;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = optionNode.ftboption.us_words;
                oneOpeProcedureInfo.Add(oneStepInfo);

                bool squareOptionExit = false;
                List<string> squareOptionlist = new List<string>(squareOptionWords);
                /* 绘文言的值,右下值不确认 */
                if (squareOptionlist.Find(s => s == optionNode.ftboption.us_words) != null)
                {
                    squareOptionExit = true;
                }
                /***********************************************************************************/
                /* Option值变更后，返回上一画面，确认List的下标值，然后再进入Option画面，确认选中值 */
                /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
                if (true == TC_OptionSet_SelFlag[(int)TC_View_Option.SubValue] && squareOptionExit == false)
                {
                    /* 确认List画面的右下角值 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(allLevelInfo[allLevelInfo.Count - 1].screenID);
                    /*
                     *补丁:Option値設定後前画面右下Option値的替换
                     * 右下Option値缩写相关的值经调查只有Scan和Fax中存在,所以Menu部分的List画面的右下角值无须修改
                     */
                    string replaceStr = string.Empty;
                    System.Data.DataTable sheet1ManualModifyDT = new System.Data.DataTable();
                    sheet1ManualModifyDT = CSVFileHelper.excelToDataTableBasedSheetIndex(configurePath, 0);//读取第1个sheet
                    foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
                    {
                        string operate = needReplacePathInfo["Option値設定後前画面右下Option値(Json Format)"].ToString();
                        if (string.IsNullOrEmpty(operate)) { continue; }
                        if ("mapping".Equals(operate.ToLower()))
                        {
                            string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                            needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                            List<string> strArrayList = new List<string>(strArray);
                            int strIndex = strArrayList.FindIndex(s => s == operate);
                            string jsonText = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                            if (string.IsNullOrEmpty(jsonText)) { continue; }
                            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(jsonText);
                            if (jsonObj != null && jsonObj[optionNode.ftboption.us_words] != null)
                            {
                                if (optionNode.ftbbuttontitle.us_words != null)
                                {
                                    if ("2-sided Fax".Equals(optionNode.ftbbuttontitle.us_words))
                                    {
                                        replaceStr = jsonObj[optionNode.ftboption.us_words].ToString();
                                        break;
                                    }
                                    else if ("2-sided Scan".Equals(optionNode.ftbbuttontitle.us_words))
                                    {
                                        replaceStr = string.Empty;
                                        break;
                                    }
                                    else
                                    {
                                        replaceStr = jsonObj[optionNode.ftboption.us_words].ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(replaceStr))
                    {
                        oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName + "    " + optionNode.ftboption.us_words;
                    }
                    else
                    {
                        oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName + "    " + replaceStr;
                    }
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                /* design名設定 */
                oneStepInfo.designName = Get_DesignName_From_ScrnID(allLevelInfo[allLevelInfo.Count - 1].screenID);
                oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 确认List画面的选中值 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Option.SelectValue;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = optionNode.ftboption.us_words;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
                /* Option值变更后，返回上一画面，确认List的下标值，然后再进入Option画面，确认选中值 */
                /************************************************************************************/


                /* 只有MenuSetting中的项目才需要返回待机再确认 */
                if (TempSetFlg == false)
                {
                    /**********************************/
                    /* 返回待机重新确认值设定是否成功 */
                    /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/

                    /* 追加共通步骤 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                    oneStepInfo.designName = "";
                    oneStepInfo.us_word = "";
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                    if (settingsButtonFlg == true && TempSetFlg == false)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.us_word = "Settings";
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                    for (level_index = 0; level_index < allLevelInfo.Count; level_index++)
                    {
                        if (0 == level_index)
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                            oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                        }
                        else
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            /* design名設定 */
                            oneStepInfo.designName = Get_DesignName_From_ScrnID(allLevelInfo[level_index].screenID);
                        }
                        oneStepInfo.us_word = allLevelInfo[level_index].LevelName;
                        oneOpeProcedureInfo.Add(oneStepInfo);


                        AddCommonSetp_InputPSW();

                        if (level_index == (allLevelInfo.Count - 2))
                        {
                            if (true == TC_OptionSet_SelFlag[(int)TC_View_Option.SubValue] && squareOptionExit == false)
                            {
                                /* 确认List画面的右下角值 */
                                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                                oneStepInfo.designName = Get_DesignName_From_ScrnID(allLevelInfo[allLevelInfo.Count - 1].screenID);
                                oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName + "    " + optionNode.ftboption.us_words;
                                oneOpeProcedureInfo.Add(oneStepInfo);
                            }
                        }
                    }

                    /* 确认List画面的选中值 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Option.SelectValue;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                    oneStepInfo.us_word = optionNode.ftboption.us_words;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
                    /* 返回待机重新确认值设定是否成功 */
                    /*********************************/
                }

                CurTCModuleName = TC_FuncNameAllOptionSet;
                CurTCPriority = 101;
                /* 根据oneOpeProcedureInfo情报生成TC */
                MakeTC_With_OpeProcedureInfo();
            }

            return;
        }

        /* TC生成：SoftKey画面值设定 */
        private void TCCreat_ScrnSoftKeyInfoChangeInput(OptionNode optionNode)
        {
            int level_index = 0;
            string str_temp_us_words = "";
            string inputTcPath = string.Empty; //第1列的值(InputTCPath)
            string inputTcTypeContent = string.Empty; //第2列的值(InputType)
            string inputValueContent = string.Empty; //第3列的值(InputValue)
            string rightBottomValue = string.Empty; //第4列的值(右下Option値)

            oneOpeProcedureInfo = new List<StepInfo>();
            oneOpeConditionInfo = new List<string>();
            //oneOpeResultInfo = new List<ExpectInfo>();

            str_temp_us_words = optionNode.ftboption.us_words;

            if (false == string.IsNullOrEmpty(str_temp_us_words))
            {
                /* 追加共通步骤 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);
                /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                if (settingsButtonFlg == true && TempSetFlg == false)
                {
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                    oneStepInfo.us_word = "Settings";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }
                if (true == FunEnableChkFlag)
                {
                    FunEnableChkFlag = false;
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                    oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    oneStepInfo.us_word = allLevelInfo[0].LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                level_index = 0;
                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    /* 有効なCondition情報を追加する */
                    AddCommonConditionInfo(tempLevelInfo.LevelName, tempLevelInfo.ConditionIndex);
                    if (0 == level_index)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                    }
                    else
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                        /* design名設定 */
                        oneStepInfo.designName = Get_DesignName_From_ScrnID(tempLevelInfo.screenID);
                    }

                    oneStepInfo.us_word = tempLevelInfo.LevelName;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    AddCommonSetp_InputPSW();

                    level_index++;
                }

                /* 有効なCondition情報を追加する */
                AddCommonConditionInfo(str_temp_us_words, optionNode.ftboption.condition_index);

                /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
                /* 补丁:配置Excel中第2个Sheet中如果存在和optionNode.ftboption.us_words(Manual:/[0-50]/1)相同的类型
                 * 取出替换的文本(content).将入力值中的"{Input Valid Char}"替换成inputValueContent
                 * 将右下角値確認中的"{Input Valid Char}"替换成rightBottomValue
                 */
                System.Data.DataTable sheet2ManualModifyDT = new System.Data.DataTable();
                sheet2ManualModifyDT = CSVFileHelper.excelToDataTableBasedSheetIndex(configurePath, 1); //读取第2个sheet
                foreach (System.Data.DataRow needReplacePathInfo in sheet2ManualModifyDT.Rows)
                {
                    inputTcPath = needReplacePathInfo[0].ToString(); //第1列的值(InputTCPath)
                    inputTcTypeContent = needReplacePathInfo[1].ToString(); //第2列的值(InputType)
                    inputValueContent = needReplacePathInfo[2].ToString(); //第3列的值(InputValue)
                    rightBottomValue = needReplacePathInfo[3].ToString(); //第4列的值(右下Option値)
                    if (inputTcTypeContent.Equals(str_temp_us_words))
                    {
                        break;
                    }
                }
                /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/

                /**************************************************************************************************/
                /* SoftKey画面的入力值值变更后，返回上一画面，确认List的下标值，然后再进入SoftKey画面，确认入力值 */
                /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/

                /* SoftKey画面清空Input */
                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.Clear;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* SoftKey画面输入值 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.Input;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = inputValueContent;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* SoftKey画面点击OK */
                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.OK;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);

                if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_Option.SubValue])
                {
                    /* 右下角値確認 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                    oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName + "    " + rightBottomValue;
                    oneOpeProcedureInfo.Add(oneStepInfo);
                }

                /* 再次进入SoftKey画面确认 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /* 确认SoftKey画面的入力值 */
                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.InputValue;
                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                oneStepInfo.us_word = inputValueContent;
                oneOpeProcedureInfo.Add(oneStepInfo);

                /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
                /* SoftKey画面的入力值值变更后，返回上一画面，确认List的下标值，然后再进入SoftKey画面，确认入力值 */
                /*************************************************************************************************/

                if (TempSetFlg == false)
                {
                    /**********************************/
                    /* 返回待机重新确认值设定是否成功 */
                    /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/

                    /* 追加共通步骤 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_Common.Back_Home;
                    oneStepInfo.designName = "";
                    oneStepInfo.us_word = "";
                    oneOpeProcedureInfo.Add(oneStepInfo);
                    /* 当settings Button存在时且是Menu Sheet的时 添加:功能_选择    Settings*/
                    if (settingsButtonFlg == true && TempSetFlg == false)
                    {
                        oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                        oneStepInfo.us_word = "Settings";
                        oneOpeProcedureInfo.Add(oneStepInfo);
                    }
                    level_index = 0;
                    for (level_index = 0; level_index < allLevelInfo.Count; level_index++)
                    {
                        if (0 == level_index)
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                            oneStepInfo.designName = DESIGN_NAME_TAIKI;/* 待機画面のdesign名 */
                        }
                        else
                        {
                            oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.Select;
                            /* design名設定 */
                            oneStepInfo.designName = Get_DesignName_From_ScrnID(allLevelInfo[level_index].screenID);
                        }

                        oneStepInfo.us_word = allLevelInfo[level_index].LevelName;
                        oneOpeProcedureInfo.Add(oneStepInfo);

                        AddCommonSetp_InputPSW();

                        if (level_index == (allLevelInfo.Count - 2))
                        {
                            if (true == TC_AllScrnConfirm_SelFlag[(int)TC_View_Option.SubValue])
                            {
                                /* 右下角値確認 */
                                oneStepInfo.DomainKW = DomainKeyWord.KW_Menu.SubValue;
                                oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                                oneStepInfo.us_word = allLevelInfo[allLevelInfo.Count - 1].LevelName + "    " + rightBottomValue;
                                oneOpeProcedureInfo.Add(oneStepInfo);
                            }
                        }
                    }

                    /* 确认SoftKey画面的入力值 */
                    oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.InputValue;
                    oneStepInfo.designName = Get_DesignName_From_ScrnID(optionNode.ftboption.cur_scrn_id);
                    oneStepInfo.us_word = inputValueContent;
                    oneOpeProcedureInfo.Add(oneStepInfo);

                    /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
                    /* 返回待机重新确认值设定是否成功 */
                    /*********************************/
                }

                CurTCModuleName = TC_FuncNameAllOptionSet;
                CurTCPriority = 101;
                /* 根据oneOpeProcedureInfo情报生成TC */
                MakeTC_With_OpeProcedureInfo();
            }

            return;
        }

        /* 根据oneOpeProcedureInfo情报生成TC */
        private void MakeTC_With_OpeProcedureInfo()
        {
            int arrindex = 0;
            /* TC的操作步骤的起始番号为1 */
            int stepindex = 1;
            /* TC的用例标题 */
            string str_temp_TC_title = "";
            /* TC的前提条件 */
            string str_temp_TC_conditions = "";
            /* TC的操作步骤 */
            string str_temp_TC_OpeProcedure = "";
            /* TC的预期 结果 */
            string str_temp_TC_ExpectResult = "";

            bool ConditionExist = false;    /* 条件有/无 */
            bool ManualModify = false;      /* 手修正必要/不必要 */

            StepInfo tempStepInfo;

            /* 关键词 */
            string str_temp_KeyWord = "";

            /* TC Condition Check */
            if (0 < oneOpeConditionInfo.Count)
            {
                ConditionExist = true;
                foreach (string strtemp in oneOpeConditionInfo)
                {
                    if (false == string.IsNullOrEmpty(strtemp))
                    {
                        if (true == strtemp.StartsWith("["))
                        {
                            str_temp_TC_conditions = str_temp_TC_conditions + strtemp + "\n";
                        }
                        else
                        {
                            str_temp_TC_conditions = str_temp_TC_conditions + strtemp + "、";
                        }
                    }
                }
                if (0 < str_temp_TC_conditions.Length)
                {
                    str_temp_TC_conditions = str_temp_TC_conditions.Remove(str_temp_TC_conditions.Length - 1);
                    str_temp_TC_conditions = str_temp_TC_conditions.Replace("\"", "\"\""); // 对双引号转义,以便生成csv格式不会错乱
                }
            }
            else
            {
                /* 手修正必要/不必要Check */
                foreach (StepInfo tempStep in oneOpeProcedureInfo)
                {
                    foreach (string strTemp in strArrManualModify)
                    {
                        if (string.IsNullOrEmpty(tempStep.us_word))
                        {
                            continue;
                        }
                        if (true == tempStep.us_word.Contains(strTemp))
                        {
                            if (false == tempStep.DomainKW.Equals(DomainKeyWord.KW_SoftInput.Type) && false == tempStep.DomainKW.Equals(DomainKeyWord.KW_MachineInfo.Type))
                            {
                                ManualModify = true;
                                break;
                            }
                        }
                    }
                }
            }

            /* 在脚本文件(.csv)中追加一行(一个TC) */
            DataRow dataRowTestCase;
            if (false == ConditionExist)
            {
                if (false == ManualModify)
                {
                    dataRowTestCase = dtScriptInfoNoCondition.NewRow();
                }
                else
                {
                    dataRowTestCase = dtScriptInfoNoCondition_NeedRevise.NewRow();
                }
            }
            else
            {
                dataRowTestCase = dtScriptInfoWithCondition.NewRow();
            }

            /* TC情報初期化 */
            foreach (string strHead in strArrHeadInfo)
            {
                dataRowTestCase[AddDoubleQuotes(strHead)] = strArrCaseInfo[arrindex] == "Y" ? AddDoubleQuotes("") : AddDoubleQuotes(strArrCaseInfo[arrindex]);
                arrindex++;
            }

            dataRowTestCase[AddDoubleQuotes("所属产品")] = AddDoubleQuotes(CurTCModelName + "(#" + CurTCProductID + ")");
            dataRowTestCase[AddDoubleQuotes("所属模块")] = AddDoubleQuotes(CurTCModuleName);
            /* 调整Temp的测试优先级,优先测试Temp的测试case */
            if (CurTCModuleName.Contains("各画面確認") && TempSetFlg == true)
            {
                CurTCPriority = 104;
            }
            else if (CurTCModuleName.Contains("オプション設定") && TempSetFlg == true)
            {
                CurTCPriority = 103;
            }

            /* "用例标题"の設定 */
            if (0 < allLevelInfo.Count)
            {
                foreach (LevelInfo tempLevelInfo in allLevelInfo)
                {
                    str_temp_TC_title = str_temp_TC_title + tempLevelInfo.LevelName + "→";
                }
                if (false == string.IsNullOrEmpty(ListValueName))
                {
                    str_temp_TC_title = str_temp_TC_title + ListValueName + "→";
                }
                str_temp_TC_title = str_temp_TC_title.Remove(str_temp_TC_title.Length - 1, 1);
            }
            str_temp_TC_title = str_temp_TC_title.Replace("\"", "\"\"");
            str_temp_TC_title = str_temp_TC_title.Replace("“", "'");
            str_temp_TC_title = str_temp_TC_title.Replace("”", "'");
            if (str_temp_TC_title.Contains("Move to")) //删除Move To相关的Tc
            {
                return;
            }
            if (str_temp_TC_title.Contains("Scan→to E-mail Server")
                || str_temp_TC_title.Contains("Scan→to USB")
                || str_temp_TC_title.Contains("Scan→to USB→Options"))
            {
                //删除Scan→to E-mail Server/Scan→to USB/Scan→to USB→Options相关的前置条件
                str_temp_TC_conditions = string.Empty;
            }

            dataRowTestCase[AddDoubleQuotes("用例标题")] = AddDoubleQuotes(str_temp_TC_title);

            /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
            /* 补丁:Scan系的前置条件的替换.配置Excel中的第一个Sheet中如果存在和用例标题路径相同的路径.
             * 取出替换的文本(content).将str_temp_TC_conditions替换成文本(content) 
             */
            System.Data.DataTable sheet1ManualModifyDT = new System.Data.DataTable();
            sheet1ManualModifyDT = CSVFileHelper.excelToDataTableBasedSheetIndex(configurePath, 0);//读取第1个sheet
            foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
            {
                needReplacePath = needReplacePathInfo[0].ToString(); //第1列的值
                if (string.IsNullOrEmpty(needReplacePath)) { continue; }
                string operate = needReplacePathInfo["前提条件置換"].ToString(); //前提条件置換列的值
                if (string.IsNullOrEmpty(operate)) { continue; }
                if (str_temp_TC_title.Contains(needReplacePath) && (false == string.IsNullOrEmpty(operate)))
                {
                    if ("replace".Equals(operate.ToLower()))
                    {
                        string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                        needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                        List<string> strArrayList = new List<string>(strArray);
                        int strIndex = strArrayList.FindIndex(s => s == operate);
                        string content = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                        str_temp_TC_conditions = content;
                        break;
                    }
                }
            }

            /**/
            string[] levelToFunctionStrArray = null;
            foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
            {
                needReplacePath = needReplacePathInfo[0].ToString(); //第1列的值
                if (string.IsNullOrEmpty(needReplacePath)) { continue; }
                string operate = needReplacePathInfo["タイプ"].ToString(); //タイプ列的值
                if (string.IsNullOrEmpty(operate)) { continue; }
                if ("replace".Equals(operate.ToLower()))
                {
                    string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                    needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                    List<string> strArrayList = new List<string>(strArray);
                    int strIndex = strArrayList.FindIndex(s => s == operate);
                    string content = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                    if (content.Equals("機能") || content.ToLower().Equals("function"))
                    {
                        levelToFunctionStrArray = needReplacePath.Split(new string[] { "\n" }, StringSplitOptions.None);
                    }
                    break;
                }
            }
            /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
            if (str_temp_TC_conditions.Contains("==")
                && !str_temp_TC_conditions.StartsWith("[")
                && !str_temp_TC_conditions.ToLower().Contains("#if")
                && !str_temp_TC_conditions.ToLower().Contains("#【laser】if"))
            {
                str_temp_TC_conditions = str_temp_TC_conditions.Replace("\n", "、");
                string[] arrayString = str_temp_TC_conditions.Split(new string[] { "、" }, StringSplitOptions.None);
                foreach (string str in arrayString)
                {
                    if (str.Contains("Tray5") || str.Contains("Tray#5") || str.Contains("Tray==5"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray4") || str.Contains("Tray#4") || str.Contains("Tray==4"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray3") || str.Contains("Tray#3") || str.Contains("Tray==3"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray2") || str.Contains("Tray#2") || str.Contains("Tray==2"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                }
                str_temp_TC_conditions = str_temp_TC_conditions.Replace("、", "&&");
            }
            else
            {
                /* 前置条件删除:
                 * [Next]:#if Address is input、 this item will appear.
                 * Skip Blank Page Sensitivity
                 * High Compression PDF Single-Page
                 * High Compression PDF Multi-Page
                 * TIFF Single-Page
                 * TIFF Multi-Page
                 * Distinctive
                 */
                if (str_temp_TC_conditions.Contains("[Next]:#if Address is input、 this item will appear."))
                {
                    str_temp_TC_conditions = str_temp_TC_conditions.Replace("[Next]:#if Address is input、 this item will appear.", "");
                }
                if (str_temp_TC_conditions.Contains("Skip Blank Page Sensitivity")
                    || str_temp_TC_conditions.Contains("High Compression PDF Single-Page")
                    || str_temp_TC_conditions.Contains("High Compression PDF Multi-Page")
                    || str_temp_TC_conditions.Contains("TIFF Single-Page")
                    || str_temp_TC_conditions.Contains("TIFF Multi-Page")
                    || str_temp_TC_conditions.Contains("Distinctive"))
                {
                    str_temp_TC_conditions = string.Empty;
                }
                str_temp_TC_conditions = str_temp_TC_conditions.Replace("\n", "、");
                string[] arrayString = str_temp_TC_conditions.Split(new string[] { "、" }, StringSplitOptions.None);
                foreach (string str in arrayString)
                {
                    if (str.Contains("Tray5") || str.Contains("Tray#5") || str.Contains("Tray==5"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray4") || str.Contains("Tray#4") || str.Contains("Tray==4"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray3") || str.Contains("Tray#3") || str.Contains("Tray==3"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                    if (str.Contains("Tray2") || str.Contains("Tray#2") || str.Contains("Tray==2"))
                    {
                        str_temp_TC_conditions = str; break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Type→Tray #1")
                || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Size→Tray #1")
                || str_temp_TC_title.Contains("Tray 1"))
            {
                str_temp_TC_conditions = "Tray>=1";
            }
            if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Type→Tray #2")
                || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Size→Tray #2")
                || str_temp_TC_title.Contains("Tray 2"))
            {
                str_temp_TC_conditions = "Tray>=2";
            }
            if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Type→Tray #3")
                || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Size→Tray #3")
                || str_temp_TC_title.Contains("Tray 3"))
            {
                str_temp_TC_conditions = "Tray>=3";
            }
            if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Type→Tray #4")
                || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Paper Size→Tray #4")
                || str_temp_TC_title.Contains("Tray 4"))
            {
                str_temp_TC_conditions = "Tray==4";
            }
            dataRowTestCase[AddDoubleQuotes("前置条件")] = AddDoubleQuotes(str_temp_TC_conditions);


            /* "步骤"の設定 */
            stepindex = 1;/* TC的操作步骤的起始番号为1 */
            for (int iloop = 0; iloop < oneOpeProcedureInfo.Count; iloop++)
            {
                tempStepInfo = oneOpeProcedureInfo[iloop];

                /* 只有MenuSetting sheet以外的项目才需要关键字置換 */
                if (TempSetFlg == true)
                {
                    /* 补丁:将功能Address Book中的Address Book替换成Address\r\nBook */
                    string addressBook = "Address Book"; string replaceAddressBook = @"Address\r\nBook";
                    if (true == tempStepInfo.DomainKW.Equals(DomainKeyWord.KW_Menu.Enable))
                    {
                        foreach (string strtemp in levelToFunctionStrArray)
                        {
                            if (tempStepInfo.us_word.Equals(strtemp))
                            {
                                /* 关键字置換(菜单可用→功能可用) */
                                tempStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Enable;
                                if (tempStepInfo.us_word.Equals(addressBook))
                                {
                                    tempStepInfo.us_word = replaceAddressBook;
                                }
                                break;
                            }
                        }
                    }

                    if (true == tempStepInfo.DomainKW.Equals(DomainKeyWord.KW_Menu.Select))
                    {
                        foreach (string strtemp in levelToFunctionStrArray)
                        {
                            if (tempStepInfo.us_word.Equals(strtemp))
                            {
                                /* 关键字置換(菜单选择→功能选择) */
                                tempStepInfo.DomainKW = DomainKeyWord.KW_Funtion.Select;
                                if (tempStepInfo.us_word.Equals(addressBook))
                                {
                                    tempStepInfo.us_word = replaceAddressBook;
                                }
                                break;
                            }
                        }
                    }
                }
                if (1 == stepindex)
                {
                    if ((true == string.IsNullOrEmpty(tempStepInfo.designName)) && (true == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = stepindex.ToString() + ". " + tempStepInfo.DomainKW;
                    }
                    else if ((true == string.IsNullOrEmpty(tempStepInfo.designName)) && (false == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = stepindex.ToString() + ". " + tempStepInfo.DomainKW + "    " + tempStepInfo.us_word;
                    }
                    else if ((false == string.IsNullOrEmpty(tempStepInfo.designName)) && (true == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = stepindex.ToString() + ". " + tempStepInfo.DomainKW + "_(" + tempStepInfo.designName + ")";
                    }
                    else
                    {
                        str_temp_TC_OpeProcedure = stepindex.ToString() + ". " + tempStepInfo.DomainKW + "_(" + tempStepInfo.designName + ")    " + tempStepInfo.us_word;
                    }
                }
                else
                {
                    if ((true == string.IsNullOrEmpty(tempStepInfo.designName)) && (true == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure + "\n" + stepindex.ToString() + ". " + tempStepInfo.DomainKW;
                    }
                    else if ((true == string.IsNullOrEmpty(tempStepInfo.designName)) && (false == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure + "\n" + stepindex.ToString() + ". " + tempStepInfo.DomainKW + "    " + tempStepInfo.us_word;
                    }
                    else if ((false == string.IsNullOrEmpty(tempStepInfo.designName)) && (true == string.IsNullOrEmpty(tempStepInfo.us_word)))
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure + "\n" + stepindex.ToString() + ". " + tempStepInfo.DomainKW + "_(" + tempStepInfo.designName + ")";
                    }
                    else
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure + "\n" + stepindex.ToString() + ". " + tempStepInfo.DomainKW + "_(" + tempStepInfo.designName + ")    " + tempStepInfo.us_word;
                    }
                }
                stepindex++;
            }
            str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("\"", "\"\"");
            str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("“", "'");
            str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("”", "'");
            //Options→File Name
            /*
             *补丁:更改タイトル
             */
            foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
            {
                needReplacePath = needReplacePathInfo[0].ToString(); //第1列的值
                if (string.IsNullOrEmpty(needReplacePath)) { continue; }
                string operate = needReplacePathInfo["タイトル置換"].ToString(); //"タイトル置換"列的值
                if (string.IsNullOrEmpty(operate)) { continue; }
                string[] stepArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> stepArrayList = new List<string>(stepArray);
                if (str_temp_TC_title.Equals(needReplacePath) && (false == string.IsNullOrEmpty(operate)) && "replace".Equals(operate.ToLower()))
                {
                    string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                    needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                    List<string> strArrayList = new List<string>(strArray);
                    int strIndex = strArrayList.FindIndex(s => s == operate);
                    string content = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                    string titleIs = string.Empty;
                    foreach (string oneStr in stepArrayList)
                    {
                        if (oneStr.Contains("标题_为") || oneStr.Contains("タイトル_チェック") || oneStr.Contains("Title_Is"))
                        {
                            titleIs = oneStr; break;
                        }
                    }
                    if (!string.IsNullOrEmpty(titleIs))
                    {
                        int index;
                        while ((index = stepArrayList.FindIndex(o => o == titleIs)) >= 0)
                        {
                            stepArrayList[index] = "8. タイトル_チェック    " + content;
                            break;
                        }
                        str_temp_TC_OpeProcedure = string.Join("\n", stepArrayList.ToArray());
                    }
                    break;
                }
            }

            /* 修改Date测试Case中的Year Month Day的用例标题和步骤 */
            if ("All Settings→Initial Setup→Date & Time→Date".Equals(str_temp_TC_title))
            {
                if (settingsButtonFlg == false)
                {
                    str_temp_TC_OpeProcedure = "1. 共通_バックホーム\n2. 機能_選択    All Settings\n3. メニュー_選択    Initial Setup\n4. メニュー_選択    Date & Time\n5. メニュー_有効    Date\n6. メニュー_選択    Date\n7. タイトル_チェック    Date\n8. 入力_タイプチェック    Manual:<Limit:4,Charaset:NUM>";
                }
                else
                {
                    str_temp_TC_OpeProcedure = "1. 共通_バックホーム\n2. 機能_選択    Settings\n2. 機能_選択    All Settings\n3. メニュー_選択    Initial Setup\n4. メニュー_選択    Date & Time\n5. メニュー_有効    Date\n6. メニュー_選択    Date\n7. タイトル_チェック    Date\n8. 入力_タイプチェック    Manual:<Limit:4,Charaset:NUM>";
                }
            }
            else if (str_temp_TC_title.Contains("All Settings→Initial Setup→Date & Time→Date→Year:")
                || "All Settings→Initial Setup→Date & Time→Date→Month:".Equals(str_temp_TC_title)
                || "All Settings→Initial Setup→Date & Time→Date→Day:".Equals(str_temp_TC_title))
            {
                if (settingsButtonFlg == false)
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", dateYearMonthDay);
                }
                else
                {
                    string[] array = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    ArrayList arrayList = new ArrayList(array);
                    arrayList.Insert(1, "1. 機能_選択    Settings");
                    array = (string[])arrayList.ToArray(typeof(string));
                    str_temp_TC_OpeProcedure = string.Join("\n", array);
                }
            }

            /* 删除Move To和[Direct:相关的Tc */
            if (str_temp_TC_OpeProcedure.Contains("[Direct:") || str_temp_TC_OpeProcedure.Contains("Move to")
                || str_temp_TC_title.Contains("2sided(1⇒2)") || str_temp_TC_title.Contains("2sided(2⇒2)")
                || str_temp_TC_title.Contains("2in1(ID)") || str_temp_TC_title.Contains("2in1") || str_temp_TC_title.Contains("Paper Save")
                || str_temp_TC_title.Contains("LCD Settings→Dim Timer→Off")
                || str_temp_TC_title.Contains("Admin Settings→Home Screen Settings→Icons")
                || str_temp_TC_title.Contains("Admin Settings→Restriction Management→Setting Lock Details→")
                || str_temp_TC_title.Contains("Admin Settings→Home Screen Settings→Tabs→Main Home Screen"))
            {
                return;
            }
            /* 删除Shortcut Settings和Fax→Received Faxes(Fax→Received Faxes)相关的Tc */
            if (str_temp_TC_title.Contains("Shortcut Settings") || str_temp_TC_title.Contains("Fax→Received Faxes")
                || str_temp_TC_title.Contains("WS Scan") || "All Settings→Print Reports".Equals(str_temp_TC_title))
            {
                return;
            }
            if ("Scan".Equals(str_temp_TC_title))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                //去除to My E-mail/to My Folder/WS Scan
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    WS Scan", string.Empty)
                    .Replace("    to My E-mail", string.Empty)
                    .Replace("    to My Folder", string.Empty);
            }
            if (str_temp_TC_title.Contains("All Settings→Initial Setup→Dial Prefix"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                if ("All Settings→Initial Setup→Dial Prefix".Equals(str_temp_TC_title))
                {
                    str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure
                        .Replace(DomainKeyWord.KW_Option.List + "    On    Off    Dial Prefix", DomainKeyWord.KW_Option.List + "    On    Off");
                }
            }

            /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
            string[] stripArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);

            /* "Scan→to Network", "Scan→to FTP/SFTP", "Scan→to SharePoint", "Scan→to HTTP(S)"移除左边Case中[Select:Profile name] */
            List<string> scanToList = new List<string>() { "Scan→to Network", "Scan→to FTP/SFTP", "Scan→to SharePoint", "Scan→to HTTP(S)" };
            if (scanToList.Find(o => o == str_temp_TC_title) != null)
            {
                ArrayList arrayList = new ArrayList(stripArray);
                if (stripArray[stripArray.Length - 1].Contains("[Select:Profile name]"))
                {
                    arrayList.RemoveAt(arrayList.Count - 1);
                    stripArray = (string[])arrayList.ToArray(typeof(string));
                    str_temp_TC_OpeProcedure = string.Join("\n", stripArray);
                }
            }

            /* 移除:菜单_选择    [Select:Profile name] */
            List<string> scanToProfileNameList = new List<string>() {
                "Scan→to Network→[Select:Profile name]",
                "Scan→to FTP/SFTP→[Select:Profile name]",
                "Scan→to SharePoint→[Select:Profile name]",
                "Scan→to HTTP(S)→[Select:Profile name]"
            };
            foreach (string profileNameStr in scanToProfileNameList)
            {
                ArrayList arrayList = new ArrayList(stripArray);
                if (str_temp_TC_title.Contains(profileNameStr) && stripArray[3].Contains("[Select:Profile name]"))
                {
                    arrayList.RemoveAt(3); //移除:4. 菜单_选择    [Select:Profile name]
                    stripArray = (string[])arrayList.ToArray(typeof(string));
                    str_temp_TC_OpeProcedure = string.Join("\n", stripArray);
                    break;
                }
            }
            if (scanToProfileNameList.Find(o => o == str_temp_TC_title) != null)
            {
                str_temp_TC_OpeProcedure = string.Join("\n", stripArray[0], stripArray[1], stripArray[2]);
            }

            /* 移除:菜单_选择    [Select:PC] */
            List<string> scanToPCSelectList = new List<string>() {
                "Scan→to PC→to File→[Select:PC]",
                "Scan→to PC→to OCR→[Select:PC]",
                "Scan→to PC→to Image→[Select:PC]",
                "Scan→to PC→to E-mail→[Select:PC]",
                "Scan→to PC→to File→<USB>",
                "Scan→to PC→to OCR→<USB>",
                "Scan→to PC→to Image→<USB>",
                "Scan→to PC→to E-mail→<USB>",
            };
            if ("Scan→to PC".Equals(str_temp_TC_title))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if ("Scan→to PC→to File".Equals(str_temp_TC_title) || "Scan→to PC→to OCR".Equals(str_temp_TC_title)
                || "Scan→to PC→to Image".Equals(str_temp_TC_title) || "Scan→to PC→to E-mail".Equals(str_temp_TC_title)
                || "Fax→Call History→Outgoing Call".Equals(str_temp_TC_title) || "All Settings→Network→Wired LAN→Wired Status".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("Status")
                || str_temp_TC_title.Contains("Scan→to E-mail Server→Address Book"))
            {
                string[] wiredStatus = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                wiredStatus[wiredStatus.Length - 1] = string.Empty;
                str_temp_TC_OpeProcedure = string.Join("\n", wiredStatus);
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            foreach (string profileNameStr in scanToPCSelectList)
            {
                ArrayList arrayList = new ArrayList(stripArray);
                if (str_temp_TC_title.Contains(profileNameStr) && stripArray[4].Contains("[Select:PC]"))
                {
                    arrayList.RemoveAt(4); //移除:5. 菜单_选择    [Select:PC]
                    stripArray = (string[])arrayList.ToArray(typeof(string));
                    str_temp_TC_OpeProcedure = string.Join("\n", stripArray);
                    break;
                }
            }
            if (scanToPCSelectList.Find(o => o == str_temp_TC_title) != null)
            {
                str_temp_TC_OpeProcedure = string.Join("\n", stripArray[0], stripArray[1], stripArray[2], stripArray[3]);
            }
            /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/

            if ("Scan→to E-mail Server→Next".Equals(str_temp_TC_title) || str_temp_TC_title.Contains("Local Language")
                || str_temp_TC_title.Contains("All Settings→Service")
                || str_temp_TC_title.Contains("All Settings→Network→Wired LAN→Wired Status→")
                || str_temp_TC_title.Contains("Status→G/O Active") || str_temp_TC_title.Contains("Status→Client Active")
                || str_temp_TC_title.Contains("Status→Not Connected") || str_temp_TC_title.Contains("Status→Off")
                || str_temp_TC_title.Contains("Signal→Signal") || str_temp_TC_title.Contains("→Not Connected")
                || str_temp_TC_title.Contains("All Settings→Network→WLAN→WLAN Status")
                || str_temp_TC_title.Contains("Fax→Call History→Outgoing Call→")
                || str_temp_TC_title.Contains("→PC Fax Receive→[Select:PC]")
                || str_temp_TC_title.Contains("Copy→>")//TBD
                || str_temp_TC_title.Equals("Fax")//TBD
                || (str_temp_TC_title.Contains("Admin Settings") && str_temp_TC_title.Contains("Password"))
                || str_temp_TC_title.Contains("BRNDispOnly:") || str_temp_TC_OpeProcedure.Contains("BRNDispOnly:")
                || str_temp_TC_title.Contains("Fax→Address Book→Edit→")////TBD
                || str_temp_TC_title.Contains("Scan→to E-mail Server→Address Book→")
                || str_temp_TC_title.Contains("Fax→Options→Broadcasting→Add Number→")//TBD
                || str_temp_TC_title.Contains("File Name→BRNDispOnly:")//TBD 
                || str_temp_TC_title.Contains("PC Fax Receive→<USB>")//TBD
                || str_temp_TC_title.Contains("Memory Receive→Forward to Cloud")//TBD
                || (str_temp_TC_title.ToUpper().Contains("TCP/IP→Boot Method→IP Boot Tries".ToUpper()) && str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_SoftInput.Input))
                || (str_temp_TC_title.ToUpper().Contains("Setup Receive→PC Fax Receive".ToUpper()) && str_temp_TC_OpeProcedure.Contains("<USB>    [Select:PC]")))
            {
                return;
            }

            if ("Copy→Options→Enlarge/Reduce→Reduce".Equals(str_temp_TC_title)
                || "Copy→Options→Enlarge/Reduce→Enlarge".Equals(str_temp_TC_title))
            {
                if (str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_Menu.SubValue + "    Enlarge/Reduce    Enlarge")
                || str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_Menu.SubValue + "    Enlarge/Reduce    Reduce"))
                    return;
            }
            /* Scan→to E-mail Server→Next→Options中间步骤的追加 */
            if (str_temp_TC_title.Contains("Scan→to E-mail Server→Next→Options"))
            {
                string[] scanToEmailArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                scanToEmailArray[3] = "4. 機能_選択    Manual\n4. 入力_入力    a\n4. 入力_コミット\n4. 機能_選択    Next";
                str_temp_TC_OpeProcedure = string.Join("\n", scanToEmailArray);
            }
            if (str_temp_TC_title.Contains("Scan→to E-mail Server→Destinations"))
            {
                string[] scanToEmailServerDestinations = {
                    "共通_バックホーム",
                    "機能_選択    Scan",
                    "メニュー_選択    to E-mail Server",
                    "機能_選択    Manual",
                    "入力_入力    a",
                    "入力_コミット",
                    "機能_有効    Destinations",
                    "機能_選択    Destinations",
                    "タイトル_チェック    Destinations"
                };
                str_temp_TC_OpeProcedure = string.Join("\n", scanToEmailServerDestinations);
            }
            if (str_temp_TC_title.Contains("Scan→to E-mail Server→Manual"))
            {
                string[] scanToEmailServerManual = {
                    "共通_バックホーム",
                    "機能_選択    Scan",
                    "メニュー_選択    to E-mail Server",
                    "機能_選択    Manual",
                    "タイトル_チェック    E-mail Address",
                    "入力_クリーン",
                    "入力_入力    bro",
                    "入力_コミット",
                    "機能_選択    Destinations"
                };
                str_temp_TC_OpeProcedure = string.Join("\n", scanToEmailServerManual);
            }

            /*
             * 补丁:画面遷移キーワード追加
             */
            foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
            {
                needReplacePath = needReplacePathInfo[0].ToString(); //第1列的值
                if (string.IsNullOrEmpty(needReplacePath)) { continue; }
                string operate = needReplacePathInfo["画面遷移キーワード追加"].ToString(); //画面遷移キーワード追加列的值
                if (string.IsNullOrEmpty(operate)) { continue; }
                if (str_temp_TC_title.Contains(needReplacePath) && (false == string.IsNullOrEmpty(operate)) && "replace".Equals(operate.ToLower()))
                {
                    string[] replaceArray = needReplacePath.Split('→');
                    Array.Reverse(replaceArray);//反转排序字符串数组
                    ArrayList arrayList = new ArrayList(stripArray);
                    int index = 0;
                    foreach (object obj in arrayList)
                    {
                        if (obj.ToString().Contains(DomainKeyWord.KW_Option.Select + "    " + replaceArray[0])
                            || obj.ToString().Contains("メニュー_選択" + "    " + replaceArray[0]))
                        {
                            string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                            needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                            List<string> strArrayList = new List<string>(strArray);
                            int strIndex = strArrayList.FindIndex(s => s == operate);
                            string content = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                            index = Array.IndexOf(stripArray, obj.ToString());
                            arrayList.Insert(index + 1, content);
                            break;
                        }
                    }
                    stripArray = (string[])arrayList.ToArray(typeof(string));
                    str_temp_TC_OpeProcedure = string.Join("\n", stripArray);
                    break;
                }
            }

            /* 补丁:TC_Condition系替换.如果str_temp_TC_conditions和TC_Condition Sheet中的元Condition相同.
             * 内部:将置换内容中的关键字的组合添加到当前步骤的前面
             */
            foreach (ConditionReplaceInfo tempReplaceInfo in totalConditionReplaceInfo)
            {
                if (string.IsNullOrEmpty(tempReplaceInfo.conditionType)) { continue; }
                string popResult = Regex.Replace(str_temp_TC_conditions, @"[^a-zA-Z0-9]+", "");
                string oldConditionResult = Regex.Replace(tempReplaceInfo.oldCondition, @"[^a-zA-Z0-9]+", "");
                if (popResult.Contains(oldConditionResult))
                {
                    if (("内部").Equals(tempReplaceInfo.conditionType) || ("内部").Equals(tempReplaceInfo.conditionType))
                    {
                        str_temp_TC_OpeProcedure = tempReplaceInfo.conditionReplaceValue + "\n" + str_temp_TC_OpeProcedure;
                        string oldCondi = tempReplaceInfo.oldCondition.Replace(",", "、").Replace("\"", "'");
                        str_temp_TC_conditions = str_temp_TC_conditions.Replace(oldCondi, string.Empty);
                        if (str_temp_TC_conditions.Contains("#If") || str_temp_TC_conditions.Contains("#if")
                            || str_temp_TC_conditions.Contains("#【laser】If") || str_temp_TC_conditions.Contains("#【laser】if"))
                        {
                            dataRowTestCase[AddDoubleQuotes("前置条件")] = AddDoubleQuotes(str_temp_TC_conditions);
                        }
                        else
                        {
                            dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                        }
                        break;
                    }
                }
            }

            /* Fax→Options→Coverpage Setup前置条件的追加 */
            if (str_temp_TC_title.Contains("Fax→Options→Coverpage Setup"))
            {
                string faxCoverpageSetup = "1. 共通_バックホーム\n2. 機能_選択    All Settings\n3. メニュー_選択    Initial Setup\n4. メニュー_選択    Station ID\n5. メニュー_選択    Fax\n6. 入力_クリーン\n7. 入力_入力    1234\n8. 入力_コミット";
                str_temp_TC_OpeProcedure = faxCoverpageSetup + "\n" + str_temp_TC_OpeProcedure;
                if (str_temp_TC_title.Contains("Fax→Options→Coverpage Setup→Coverpage Message")
                    || str_temp_TC_title.Contains("Fax→Options→Coverpage Setup→Total Pages"))
                {
                    string[] faxCoverpageArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    string faxCoverpage = string.Empty;
                    List<string> faxCoverpageList = new List<string>(faxCoverpageArray);
                    foreach (string oneStr in faxCoverpageList)
                    {
                        if (oneStr.Contains(DomainKeyWord.KW_Menu.Select + "    Coverpage Setup"))
                        {
                            faxCoverpage = oneStr; break;
                        }
                    }
                    if (!string.IsNullOrEmpty(faxCoverpage))
                    {
                        int index;
                        while ((index = faxCoverpageList.FindIndex(o => o == faxCoverpage)) >= 0)
                        {
                            faxCoverpageList.Insert(index + 1, DomainKeyWord.KW_Menu.Select + "    Coverpage Setup");
                            faxCoverpageList.Insert(index + 2, DomainKeyWord.KW_Option.Select + "    On");
                            break;
                        }
                        str_temp_TC_OpeProcedure = string.Join("\n", faxCoverpageList.ToArray());
                    }
                }
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            /* Copy→Options→Enlarge/Reduceの修正 */
            if (str_temp_TC_title.Contains("Copy→Options→Enlarge/Reduce→Enlarge→"))
            {
                string[] copyStepArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                string enlargeReduce = string.Empty;
                List<string> copyStepArrayList = new List<string>(copyStepArray);
                foreach (string oneStr in copyStepArrayList)
                {
                    if (oneStr.Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        enlargeReduce = oneStr; break;
                    }
                }
                if (!string.IsNullOrEmpty(enlargeReduce))
                {
                    int index;
                    while ((index = copyStepArrayList.FindIndex(o => o == enlargeReduce)) >= 0)
                    {
                        copyStepArrayList[index] = enlargeReduce.Replace("Enlarge", "Enlarge/Reduce");
                        copyStepArrayList.Insert(index + 1, DomainKeyWord.KW_Menu.Select + "    Enlarge/Reduce");
                        break;
                    }
                    str_temp_TC_OpeProcedure = string.Join("\n", copyStepArrayList.ToArray());
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                }
            }
            /* Copy→Options→Enlarge/Reduceの修正 */
            if (str_temp_TC_title.Contains("Copy→Options→Enlarge/Reduce→Reduce→"))
            {
                string[] copyStepArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                string enlargeReduce = string.Empty;
                List<string> copyStepArrayList = new List<string>(copyStepArray);
                foreach (string oneStr in copyStepArrayList)
                {
                    if (oneStr.Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        enlargeReduce = oneStr; break;
                    }
                }
                if (!string.IsNullOrEmpty(enlargeReduce))
                {
                    int index;
                    while ((index = copyStepArrayList.FindIndex(o => o == enlargeReduce)) >= 0)
                    {
                        copyStepArrayList[index] = enlargeReduce.Replace("Reduce", "Enlarge/Reduce");
                        copyStepArrayList.Insert(index + 1, DomainKeyWord.KW_Menu.Select + "    Enlarge/Reduce");
                        break;
                    }
                    str_temp_TC_OpeProcedure = string.Join("\n", copyStepArrayList.ToArray()).Replace("0.5", "50%");
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                }
            }
            if ("Copy→Options".Equals(str_temp_TC_title))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    2in1/1in1", string.Empty);
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if ("All Settings→General Setup→LCD Settings→Dim Timer".Equals(str_temp_TC_title))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    Off", string.Empty);
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            if ("Copy→Options→Enlarge/Reduce→Reduce".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("Copy→Options→Enlarge/Reduce")
                || str_temp_TC_title.Contains("Copy→Options→Page Layout")
                || str_temp_TC_title.Contains("Copy→Options→2-sided Copy Page Layout")
                || str_temp_TC_title.Contains("Copy→Options→2-sided Copy")
                || str_temp_TC_title.Contains("Copy→Options→Quality")
                || str_temp_TC_title.Contains("Copy→Options→Density")
                || str_temp_TC_title.Contains("Copy→Options→2in1/1in1"))
            {
                if ("Copy→Options→Enlarge/Reduce→Custom(25-400%)".Equals(str_temp_TC_title))
                {
                    string[] copyStepArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    string enlargeReduce = string.Empty;
                    List<string> copyStepArrayList = new List<string>(copyStepArray);
                    foreach (string oneStr in copyStepArrayList)
                    {
                        if (oneStr.Contains(DomainKeyWord.KW_Menu.Select + "    Custom(25-400%)")
                            || oneStr.Contains(DomainKeyWord.KW_Option.Select + "    Custom(25-400%)"))
                        {
                            enlargeReduce = oneStr; break;
                        }
                    }
                    if (!string.IsNullOrEmpty(enlargeReduce))
                    {
                        int index;
                        while ((index = copyStepArrayList.FindIndex(o => o == enlargeReduce)) >= 0)
                        {
                            copyStepArrayList.RemoveRange(index + 1, (copyStepArrayList.Count - index - 1));
                            copyStepArrayList.Add(DomainKeyWord.KW_SoftInput.Clear);
                            copyStepArrayList.Add(DomainKeyWord.KW_SoftInput.Input + "    120");
                            copyStepArrayList.Add(DomainKeyWord.KW_SoftInput.OK);
                            copyStepArrayList.Add(DomainKeyWord.KW_Menu.SubValue + "    Enlarge/Reduce    120%");
                            copyStepArrayList.Add(DomainKeyWord.KW_Menu.Select + "    Enlarge/Reduce");
                            copyStepArrayList.Add(DomainKeyWord.KW_Option.SelectValue + "    Custom(25-400%)");
                            copyStepArrayList.Add(DomainKeyWord.KW_Menu.Select + "    Custom(25-400%)");
                            copyStepArrayList.Add(DomainKeyWord.KW_SoftInput.InputValue + "    120");
                            break;
                        }
                        str_temp_TC_OpeProcedure = string.Join("\n", copyStepArrayList.ToArray());
                    }
                    str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("Enlarge/Reduce    Custom(25-400%)", "Enlarge/Reduce    100%");
                }
                if ((str_temp_TC_title.Contains("Copy→Options→Quality") && str_temp_TC_OpeProcedure.Contains("Lighter"))
                    || (str_temp_TC_title.Contains("Copy→Options→Density") && str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_Option.List + "    -2    -1    0    +1    +2")
                    && str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_Option.SelectValue + "    -1"))
                    || str_temp_TC_title.Contains("Copy→Options→2in1/1in1"))
                {
                    string[] qualityArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> qualityList = new List<string>(qualityArray);
                    qualityList.Insert(2, DomainKeyWord.KW_Funtion.Select + "    >");
                    qualityList.Insert(3, DomainKeyWord.KW_Option.Select + "    2in1(ID)");
                    str_temp_TC_OpeProcedure = string.Join("\n", qualityList.ToArray());
                }
                if (str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_Option.List + "    Off    1-sided⇒2-sided    2-sided⇒2-sided    2-sided⇒1-sided"))
                {
                    str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    Layout", "");
                }
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("0.5", "50%");
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if ("Copy→Options→Enlarge/Reduce→Custom(25-400%)".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("Copy→Options→Enlarge/Reduce"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Custom(25-400%)    100"))
                    {
                        arrayList.Remove(arrayList[i]);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("120", "100");
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("Copy→Options→2-sided Copy→Layout"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (settingsButtonFlg == false && arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Layout    "))
                    {
                        arrayList.Remove(arrayList[i]);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                    }
                    if (settingsButtonFlg == false && !arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Enlarge/Reduce")
                        && (arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Layout")
                        || arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Enlarge")
                        || arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Reduce")))
                    {
                        arrayList.Remove(arrayList[i]);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("Copy→Options→Enlarge/Reduce"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (settingsButtonFlg == false && !arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Enlarge/Reduce")
                        && (arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Enlarge")
                        || arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Reduce")))
                    {
                        arrayList.Remove(arrayList[i]);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }

            if (str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Setup Server→SMTP→Server→")
                || str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Setup Server→SMTP→Server→"))
            {
                string[] server = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> serverList = new List<string>(server);
                for (int i = 0; i < serverList.Count; i++)
                {
                    if (serverList[i].Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        string[] arrayStr = Regex.Split(serverList[i], @"\s{4,}");
                        arrayStr[1] = "Server";
                        serverList[i] = string.Join("    ", arrayStr);
                        serverList.Insert(i + 1, "メニュー_選択    Server");
                    }
                }
                str_temp_TC_OpeProcedure = string.Join("\n", serverList.ToArray());
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure
                    .Replace("タイトル_チェック    Name", "タイトル_チェック    Server");
            }
            if (str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Mail Address")
                || str_temp_TC_title.Contains("All Settings→Network→Wired LAN→TCP/IP→IP Address")
                || str_temp_TC_title.Contains("All Settings→Network→Wired LAN→TCP/IP→Subnet Mask"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        arrayList.Remove(arrayList[i]);
                    }
                }
                str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
            }
            if (str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Setup Server→SMTP→Auth. for SMTP→SMTP-AUTH"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Option.Select + "    SMTP-AUTH"))
                    {
                        arrayList.Insert(i + 1, "入力_入力    a\n入力_コミット\n入力_入力    a\n入力_コミット\n入力_入力    a\n入力_コミット");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Fax→Setup Receive→Remote Codes→Tel Answer") && CurTCModuleName.Contains("オプション設定"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                string inputValue = "130";
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_SoftInput.Input))
                    {
                        arrayList[i] = DomainKeyWord.KW_SoftInput.Input + "    " + inputValue;
                    }
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        arrayList[i] = DomainKeyWord.KW_Menu.SubValue + "    Tel Answer    " + inputValue;
                    }
                    if (arrayList[i].Contains(DomainKeyWord.KW_SoftInput.InputValue))
                    {
                        arrayList[i] = DomainKeyWord.KW_SoftInput.InputValue + "    " + inputValue;
                    }
                }
                str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
            }

            if (str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Setup Server→POP3/IMAP4→Select Folder→Specified")
                && CurTCModuleName.Contains("オプション設定"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                List<string> specifiedList = new List<string>(specified);
                if (settingsButtonFlg)
                {
                    int index;
                    while ((index = specifiedList.FindIndex(o => o == "機能_選択    All Settings")) >= 0)
                    {
                        specifiedList[index] = "機能_選択    Settings\n" + "機能_選択    All Settings";
                    }
                }
                str_temp_TC_OpeProcedure = string.Join("\n", specifiedList.ToArray());
            }


            if (str_temp_TC_title.ToUpper().Contains("TCP/IP→Boot Method→IP Boot Tries".ToUpper()))
            {
                string[] ipBootTries = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> ipBootTriesList = new List<string>(ipBootTries);
                for (int i = 0; i < ipBootTriesList.Count; i++)
                {
                    if (ipBootTriesList[i].Contains(DomainKeyWord.KW_Menu.Enable + "    IP Boot Tries"))
                    {
                        ipBootTriesList[i] = "オプション_選択    Auto";
                        ipBootTriesList.RemoveRange(i + 1, 2);
                        str_temp_TC_OpeProcedure = string.Join("\n", ipBootTriesList.ToArray());
                        break;
                    }
                }
            }
            if ("Fax→Options→Delayed Fax→Delayed Fax".Equals(str_temp_TC_title)
                || "Fax→Options→Coverpage Setup→Coverpage Setup".Equals(str_temp_TC_title)
                || "Fax→Options→Coverpage Setup→Coverpage Message".Equals(str_temp_TC_title))
            {
                string[] titleNotTestCase = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> titleNotTestCaseList = new List<string>(titleNotTestCase);
                for (int i = 0; i < titleNotTestCaseList.Count; i++)
                {
                    if (settingsButtonFlg == false &&
                        (titleNotTestCaseList[i].Contains(DomainKeyWord.KW_Title.Title + "    Delayed Fax")
                        || titleNotTestCaseList[i].Contains(DomainKeyWord.KW_Title.Title + "    Coverpage Setup")
                        || titleNotTestCaseList[i].Contains(DomainKeyWord.KW_Title.Title + "    Coverpage Message")))
                    {
                        titleNotTestCaseList.Remove(titleNotTestCaseList[i]);
                        str_temp_TC_OpeProcedure = string.Join("\n", titleNotTestCaseList.ToArray());
                        break;
                    }
                }
            }

            /* 给Copy→Options的Tray Use的MP>T1和T1>MP添加Condition*/
            if (str_temp_TC_title.Contains("Copy→Options→Tray Use")
                && (str_temp_TC_OpeProcedure.Contains("MP>T1") || str_temp_TC_OpeProcedure.Contains("T1>MP"))
                && (!str_temp_TC_OpeProcedure.Contains("MP>T1>T2") && !str_temp_TC_OpeProcedure.Contains("T2>T1>MP"))
                && (!str_temp_TC_OpeProcedure.Contains("MP>T1>T2>T3") && !str_temp_TC_OpeProcedure.Contains("T3>T2>T1>MP"))
                && (!str_temp_TC_OpeProcedure.Contains("MP>T1>T2>T3>T4") && !str_temp_TC_OpeProcedure.Contains("T4>T3>T2>T1>MP"))
                && (!str_temp_TC_OpeProcedure.Contains("MP>T1>T2>T3>T4>T5") && !str_temp_TC_OpeProcedure.Contains("T5>T4>T3>T2>T1>MP")))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==1";
            }

            if ((str_temp_TC_title.Contains("Options→Factory Reset") && !str_temp_TC_title.Contains("Options→Factory Reset→"))
                || (str_temp_TC_title.Contains("Options→Set New Default") && !str_temp_TC_title.Contains("Options→Set New Default→")))
            {
                string[] factoryResetArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                ArrayList arrayList = new ArrayList(factoryResetArray);
                arrayList.RemoveAt(arrayList.Count - 1);
                arrayList[arrayList.Count - 1] = DomainKeyWord.KW_Funtion.Enable + "    No";
                arrayList.Add(DomainKeyWord.KW_Funtion.Select + "    No");
                str_temp_TC_OpeProcedure = string.Join("\n", (string[])arrayList.ToArray(typeof(string)));
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if ((str_temp_TC_title.Contains("Options→Factory Reset") && str_temp_TC_title.Contains("Options→Factory Reset→"))
                || (str_temp_TC_title.Contains("Options→Set New Default") && str_temp_TC_title.Contains("Options→Set New Default→")))
            {
                string[] factoryResetArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(factoryResetArray);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    Factory Reset"))
                    {
                        arrayList.RemoveRange(i + 1, (arrayList.Count - i - 1));
                        if (str_temp_TC_title.Contains("Options→Factory Reset→Yes"))
                        {
                            arrayList.Add(DomainKeyWord.KW_Funtion.Enable + "    Yes");
                            arrayList.Add(DomainKeyWord.KW_Funtion.Select + "    Yes");
                            break;
                        }
                        if (str_temp_TC_title.Contains("Options→Factory Reset→No"))
                        {
                            arrayList.Add(DomainKeyWord.KW_Funtion.Enable + "    No");
                            arrayList.Add(DomainKeyWord.KW_Funtion.Select + "    No");
                            break;
                        }
                    }
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    Set New Default"))
                    {
                        arrayList.RemoveRange(i + 1, (arrayList.Count - i - 1));
                        if (str_temp_TC_title.Contains("Set New Default→Yes"))
                        {
                            arrayList.Add(DomainKeyWord.KW_Funtion.Enable + "    Yes");
                            arrayList.Add(DomainKeyWord.KW_Funtion.Select + "    Yes");
                            break;
                        }
                        if (str_temp_TC_title.Contains("Set New Default→No"))
                        {
                            arrayList.Add(DomainKeyWord.KW_Funtion.Enable + "    No");
                            arrayList.Add(DomainKeyWord.KW_Funtion.Select + "    No");
                            break;
                        }
                    }
                }
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
            }
            if (str_temp_TC_title.Contains("Options→File Split"))
            {
                if (str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_SoftInput.Input))
                {
                    return;
                }
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    [1-99]/1", "");
                if (!str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_SoftInput.Input) && !str_temp_TC_title.Contains("Options→File Split→"))
                {
                    string[] fileSplitOff = {
                        "メニュー_有効    File Split",
                        "メニュー_選択    File Split",
                        "メニュー_右下値チェック    File Split    Off",
                        "メニュー_選択    File Split"
                    };
                    List<string> fileSplitOffList = new List<string>(fileSplitOff);
                    string[] fileSplitArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> arrayList = new List<string>(fileSplitArray);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        if (arrayList[i].Contains(DomainKeyWord.KW_Funtion.Select + "    Options"))
                        {
                            arrayList.RemoveRange(i + 1, 4);
                            arrayList.Insert(i + 1, string.Join("\n", fileSplitOff));
                            str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                            break;
                        }
                    }
                }
                if (str_temp_TC_title.Contains("Options→File Split→Off"))
                {
                    string[] fileSplitArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> arrayList = new List<string>(fileSplitArray);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    File Split"))
                        {
                            arrayList.Insert(i + 1, DomainKeyWord.KW_Menu.Select + "    File Split");
                            str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                            break;
                        }
                    }
                }
                string[] fileSplit = {
                    "メニュー_選択    File Split",
                    "オプション_選択    Number of Documents",
                    "メニュー_右下値チェック    File Split    Number of Documents",
                    "メニュー_選択    Number of Documents",
                    "入力_クリーン",
                    "入力_入力    11",
                    "入力_コミット",
                    "メニュー_右下値チェック    Number of Documents    11",
                };
                List<string> fileSplitList = new List<string>(fileSplit);
                if (str_temp_TC_title.Contains("Options→File Split→") && !str_temp_TC_title.Contains("Off"))
                {
                    string[] fileSplitArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> arrayList = new List<string>(fileSplitArray);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    File Split"))
                        {
                            arrayList.RemoveRange(i + 1, arrayList.Count - i - 1);
                            arrayList.AddRange(fileSplitList);
                            str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                            break;
                        }
                    }
                    if (str_temp_TC_title.Contains("Number of Pages"))
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("Number of Documents", "Number of Pages");
                    }
                    if (str_temp_TC_title.Contains("Blank Page"))
                    {
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("Number of Documents", "Blank Page");
                    }
                }
            }
            if (str_temp_TC_title.Contains("Options→File Name→<Manual>"))
            {
                string[] fileNameArray = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(fileNameArray);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    File Name    <Manual>"))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_SoftInput.Clear + "\n"
                            + DomainKeyWord.KW_SoftInput.InputValue + "    aa\n"
                            + DomainKeyWord.KW_SoftInput.OK);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("File Name    <Manual>", "File Name    aa");
                        break;
                    }
                }
            }
            if (str_temp_TC_title.ToUpper().Contains("TCP/IP→BOOT Method→".ToUpper()))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if ((arrayList[i].ToUpper().Contains(DomainKeyWord.KW_Menu.SubValue + "    BOOT Method    ".ToUpper()))
                        && !str_temp_TC_OpeProcedure.Contains("Static"))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_Funtion.Select + "    OK");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Network→"))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("000.000.000.000", "0.0.0.0");
            }
            if (str_temp_TC_title.Contains("All Settings→Network→IPsec→"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    IPsec    "))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_PopUp.Click + "    Cancel");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Network→IPsec"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    IPsec    "))
                    {
                        arrayList.Remove(arrayList[i]);
                    }
                }
                str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
            }
            if (str_temp_TC_title.Contains("→Firmware Auto Check→On"))
            {
                string[] bootMethod = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(bootMethod);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Firmware Auto Check    "))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_PopUp.Click + "    Yes");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }

            /* 去除General Setup中的    Service和    Login Portal*/
            if (str_temp_TC_OpeProcedure.Contains("    Service") || str_temp_TC_OpeProcedure.Contains("    Login Portal"))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    Service", string.Empty).Replace("    Login Portal", string.Empty);
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            if ("All Settings→General Setup→Tray Setting".Equals(str_temp_TC_title))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace(DomainKeyWord.KW_Menu.List + "    Paper Type    Paper Size", string.Empty);
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                if (stream[stream.Length - 1].Length < 4)
                {
                    return;
                }
            }
            if ("All Settings→General Setup→Tray Setting".Equals(str_temp_TC_title)
                && str_temp_TC_OpeProcedure.Contains("Skip Tray") && str_temp_TC_OpeProcedure.Contains("Separator Tray"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Skip Tray==1&&Separator Tray==1";
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace(DomainKeyWord.KW_Menu.List
                    , DomainKeyWord.KW_Menu.List + "    Paper Type    Paper Size");
                if (!ConditionExist && !ManualModify)
                {
                    return;
                }
            }

            if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Skip Tray")
                || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Separator Tray"))
            {
                //Off    Tray 2    Tray 3    Tray 4
                if (str_temp_TC_OpeProcedure.Contains("Tray #2") && str_temp_TC_OpeProcedure.Contains("Tray #3")
                    && str_temp_TC_OpeProcedure.Contains("Tray #4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4&&Skip Tray==1&&Separator Tray==1";
                }
                if (str_temp_TC_OpeProcedure.Contains("Tray 2") && str_temp_TC_OpeProcedure.Contains("Tray 3")
                    && str_temp_TC_OpeProcedure.Contains("Tray 4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4&&Skip Tray==1&&Separator Tray==1";
                }
                else if (str_temp_TC_OpeProcedure.Contains("Tray 2") || str_temp_TC_OpeProcedure.Contains("Tray #2"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray>=2&&Skip Tray==1&&Separator Tray==1";
                }
                else if (str_temp_TC_OpeProcedure.Contains("Tray 3") || str_temp_TC_OpeProcedure.Contains("Tray #3"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray>=3&&Skip Tray==1&&Separator Tray==1";
                }
                else if (str_temp_TC_OpeProcedure.Contains("Tray 4") || str_temp_TC_OpeProcedure.Contains("Tray #4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4&&Skip Tray==1&&Separator Tray==1";
                }
                if (str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Skip Tray→Off")
                    || str_temp_TC_title.Contains("All Settings→General Setup→Tray Setting→Separator Tray→Off"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Skip Tray==1&&Separator Tray==1";
                }
            }
            if (str_temp_TC_title.Contains("Paper Low Notice") || str_temp_TC_title.Contains("Check Size")
                || str_temp_TC_title.Contains("Check Paper") || str_temp_TC_title.Contains("LCD Settings→Dim Timer")
                || str_temp_TC_title.Contains("Ecology→Sleep Time") || str_temp_TC_title.Contains("Journal Period")
                || (str_temp_TC_title.Contains("Printer→HP LaserJet→") && str_temp_TC_title.Contains("Margin"))
                || str_temp_TC_title.Contains("Printer→BR-Script 3") || str_temp_TC_title.Contains("Printer→PDF")
                || "All Settings→Printer".Equals(str_temp_TC_title) || "All Settings→Initial Setup→Station ID".Equals(str_temp_TC_title)
                || "Fax→Options".Equals(str_temp_TC_title) || "All Settings→Printer→Carbon Menu".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("General Setup→Delete Storage") || "All Settings→Fax→Setup Receive→Memory Receive".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("E-mail/IFAX→Setup Server→POP3/IMAP4"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            if (str_temp_TC_title.ToUpper().Contains("TCP/IP→BOOT Method".ToUpper()) && !str_temp_TC_title.ToUpper().Contains("BOOT Method→".ToUpper()))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    IP Boot Tries", string.Empty);
            }
            if (str_temp_TC_title.ToUpper().Contains("TCP/IP→BOOT Method→".ToUpper()))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }

            if ("All Settings→General Setup→Tray Setting→Paper Type".Equals(str_temp_TC_title)
                || "All Settings→General Setup→Tray Setting→Paper Size".Equals(str_temp_TC_title)
                || str_temp_TC_title.Contains("All Settings→Printer→Carbon Menu")
                || str_temp_TC_title.Contains("→Parts Life"))
            {
                //MP Tray    Tray #1    Tray #2    Tray #3    Tray #4
                if (str_temp_TC_OpeProcedure.Contains("Tray #1") && str_temp_TC_OpeProcedure.Contains("Tray #2")
                    && str_temp_TC_OpeProcedure.Contains("Tray #3") && str_temp_TC_OpeProcedure.Contains("Tray #4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4";
                }
                if (str_temp_TC_OpeProcedure.Contains("Tray 1") && str_temp_TC_OpeProcedure.Contains("Tray 2")
                    && str_temp_TC_OpeProcedure.Contains("Tray 3") && str_temp_TC_OpeProcedure.Contains("Tray 4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4";
                }
                if (str_temp_TC_OpeProcedure.Contains("PF Kit 1") && str_temp_TC_OpeProcedure.Contains("PF Kit 2")
                    && str_temp_TC_OpeProcedure.Contains("PF Kit 3") && str_temp_TC_OpeProcedure.Contains("PF Kit 4"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4";
                }
            }

            string[] tcTitleArray = str_temp_TC_title.Split('→');
            Array.Reverse(tcTitleArray);//反转排序字符串数组
            if ("MP>T1".Equals(tcTitleArray[0]) || "T1>MP".Equals(tcTitleArray[0]))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==1";
            }
            if ("MP>T1>T2".Equals(tcTitleArray[0]) || "MP>T2>T1".Equals(tcTitleArray[0])
             || "T1>T2>MP".Equals(tcTitleArray[0]) || "T2>T1>MP".Equals(tcTitleArray[0]))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==2";
            }
            if ("MP>T1>T2>T3".Equals(tcTitleArray[0]) || "MP>T3>T2>T1".Equals(tcTitleArray[0])
             || "T1>T2>T3>MP".Equals(tcTitleArray[0]) || "T3>T2>T1>MP".Equals(tcTitleArray[0]))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==3";
            }
            if ("MP>T1>T2>T3>T4".Equals(tcTitleArray[0]) || "MP>T4>T3>T2>T1".Equals(tcTitleArray[0])
             || "T1>T2>T3>T4>MP".Equals(tcTitleArray[0]) || "T4>T3>T2>T1>MP".Equals(tcTitleArray[0]))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = "Tray==4";
            }

            if (str_temp_TC_title.Contains("All Settings→Printer→Carbon Menu→"))
            {
                if (!str_temp_TC_title.Contains("Stream→"))
                {
                    string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> arrayList = new List<string>(stream);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue) && arrayList[i].Contains("Stream")
                             && arrayList[i].Contains("Off"))
                        {
                            arrayList.RemoveRange(i + 1, arrayList.Count - i - 1);
                            str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                            dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                            break;
                        }
                    }
                }
                if (str_temp_TC_title.Contains("Stream→"))
                {
                    string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                    List<string> arrayList = new List<string>(stream);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select) && arrayList[i].Contains("Stream"))
                        {
                            arrayList.RemoveRange(i + 1, 5);
                            arrayList.Insert(i + 1, DomainKeyWord.KW_PopUp.Click + "    OK");
                            break;
                        }
                    }
                    arrayList[arrayList.Count - 1] = DomainKeyWord.KW_PopUp.Click + "    OK";
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                    str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Printer→Carbon Menu"))
            {
                string[] carbonMenu = {
                "1. 共通_バックホーム",
                "1. 機能_選択    All Settings",
                "1. メニュー_選択    Printer",
                "1. メニュー_選択    Carbon Menu",
                "1. メニュー_選択    Carbon Copy",
                "1. メニュー_選択    On",
                "1. メニュー_選択    Copies",
                "1. 入力_クリーン",
                "1. 入力_入力    8",
                "1. 入力_コミット"
                };
                string printercarbonMenu = string.Empty;
                if (settingsButtonFlg == false)
                {
                    printercarbonMenu = string.Join("\n", carbonMenu);
                }
                else
                {
                    ArrayList arrayList = new ArrayList(carbonMenu);
                    arrayList.Insert(1, "1. 機能_選択    Settings");
                    printercarbonMenu = string.Join("\n", (string[])arrayList.ToArray(typeof(string)));
                }
                str_temp_TC_OpeProcedure = printercarbonMenu + "\n" + str_temp_TC_OpeProcedure;
                if (!str_temp_TC_title.Contains("Tray") || str_temp_TC_title.Contains("MP Tray"))
                {
                    dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                }
            }
            if ("All Settings→Fax→Setup Receive→PC Fax Receive→On".Equals(str_temp_TC_title))
            {
                string[] pcFaxReceive = {
                "1. 機能_選択    OK",
                "1. 機能_選択    OK",
                "1. タイトル_チェック    PC Fax Receive",
                "1. オプション_リストチェック    Backup Print: On    Backup Print: Off",
                "1. オプション_有効    Backup Print: On",
                "1. オプション_選択    Backup Print: On"
                };
                string pcFaxReceiveOn = string.Join("\n", pcFaxReceive);
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    PC Fax Receive"))
                    {
                        arrayList.Insert(i, pcFaxReceiveOn);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Fax→Setup Receive→PC Fax Receive"))
            {
                if (str_temp_TC_title.Contains("Backup Print: On"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", pcFaxReceiveBackupPrint);
                }
                else if (str_temp_TC_title.Contains("Backup Print: Off"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", pcFaxReceiveBackupPrint)
                        .Replace(DomainKeyWord.KW_Option.Enable + "    Backup Print: On", DomainKeyWord.KW_Option.Enable + "    Backup Print: Off")
                        .Replace(DomainKeyWord.KW_Option.Select + "    Backup Print: On", DomainKeyWord.KW_Option.Select + "    Backup Print: Off")
                        .Replace(DomainKeyWord.KW_Option.SelectValue + "    Backup Print: On", DomainKeyWord.KW_Option.SelectValue + "    Backup Print: Off");
                }
            }
            if (str_temp_TC_title.Contains("Setup Receive→Memory Receive→Fax Storage"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Memory Receive    Fax Storage"))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_Option.Select + "    Backup Print: Off");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                    else if (arrayList[arrayList.Count - 1].Substring(3).Equals(DomainKeyWord.KW_Menu.List + "    Backup Print"))
                    {
                        arrayList[arrayList.Count - 1] = DomainKeyWord.KW_Menu.List + "    Backup Print: On    Backup Print: Off";
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
                if (str_temp_TC_title.Contains("Backup Print") && str_temp_TC_title.Contains("Backup Print: Off"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", faxStorageBackupPrintOff);
                }
                if (str_temp_TC_title.Contains("Backup Print") && str_temp_TC_title.Contains("Backup Print: On"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", faxStorageBackupPrintOff);
                    str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure
                        .Replace(DomainKeyWord.KW_Option.Select + "    Backup Print: Off", DomainKeyWord.KW_Option.Select + "    Backup Print: On")
                        .Replace(DomainKeyWord.KW_Option.SelectValue + "    Backup Print: Off", DomainKeyWord.KW_Option.SelectValue + "    Backup Print: On");

                }
            }
            if (str_temp_TC_title.Contains("Memory Receive→Fax Storage→Backup Print")
                && !str_temp_TC_title.Contains("Backup Print: On") && !str_temp_TC_title.Contains("Backup Print: Off"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    Fax Storage"))
                    {
                        arrayList.RemoveRange(i + 1, 4);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }

            if ("All Settings→Fax→Setup Receive→Memory Receive→Fax Forward".Equals(str_temp_TC_title))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                str_temp_TC_OpeProcedure = string.Join("\n", faxMemoryReceiveFaxForward);
            }

            if (str_temp_TC_title.Contains("Initial Setup→Dial Prefix→On"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Dial Prefix    On"))
                    {
                        arrayList.Insert(i, DomainKeyWord.KW_Title.Title + "    Dial Prefix On\n"
                            + DomainKeyWord.KW_Menu.SubValue + "    Dial Prefix    9\n"
                            + DomainKeyWord.KW_Menu.Select + "    Dial Prefix\n"
                            + DomainKeyWord.KW_SoftInput.OK);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("Initial Setup→Dial Prefix") && str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_SoftInput.Input))
            {
                str_temp_TC_OpeProcedure = string.Join("\n", dialPrefix);
            }
            if (str_temp_TC_title.Contains("Initial Setup→Dial Prefix") && str_temp_TC_OpeProcedure.Contains(DomainKeyWord.KW_SoftInput.Type))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue + "    Dial Prefix    9"))
                    {
                        arrayList[i - 1] = DomainKeyWord.KW_Option.Select + "    On";
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Network→E-mail/IFAX→Setup Relay→Relay Domain→"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_SoftInput.OK))
                    {
                        arrayList.Insert(i + 1, DomainKeyWord.KW_Menu.Select + "    Relay Domain");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }

            if (str_temp_TC_title.Contains("Admin Settings→Home Screen Settings→Tabs→Rename→"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Title.Title + "    Tab"))
                    {
                        arrayList[i] = DomainKeyWord.KW_Title.Title + "    Tab Name";
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Contains("All Settings→Fax→Miscellaneous→Distinctive→Ring Pattern"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.Select + "    Distinctive"))
                    {
                        arrayList.Insert(i + 1, DomainKeyWord.KW_Menu.Select + "    Distinctive\n"
                            + DomainKeyWord.KW_Option.Select + "    On");
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }

            if (str_temp_TC_title.Contains("Password"))
            {
                string[] stream = str_temp_TC_OpeProcedure.Split(new string[] { "\n" }, StringSplitOptions.None);
                List<string> arrayList = new List<string>(stream);
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (arrayList[i].Contains(DomainKeyWord.KW_Menu.SubValue))
                    {
                        arrayList.RemoveRange(i, arrayList.Count - 1 - i);
                        str_temp_TC_OpeProcedure = string.Join("\n", arrayList.ToArray());
                        break;
                    }
                }
            }
            if (str_temp_TC_title.Equals("All Settings→Printer→HP LaserJet")
                || str_temp_TC_title.Contains("Printer→HP LaserJet→Font No.")
                || str_temp_TC_title.Contains("Printer→HP LaserJet→Font No."))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    Font Point", "");
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if (str_temp_TC_title.Contains("Printer→HP LaserJet→Font Pitch"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                string fontNo = null;
                if (settingsButtonFlg == false)
                {
                    fontNo = "共通_バックホーム\n機能_選択    All Settings\nメニュー_選択    Printer\nメニュー_選択    HP LaserJet\nメニュー_選択    Font No.\nメニュー_選択    Font No.\n入力_クリーン\n入力_入力    015\n入力_コミット\n";
                }
                else
                {
                    fontNo = "共通_バックホーム\n機能_選択    Settings\n機能_選択    All Settings\nメニュー_選択    Printer\nメニュー_選択    HP LaserJet\nメニュー_選択    Font No.\nメニュー_選択    Font No.\n入力_クリーン\n入力_入力    015\n入力_コミット\n";
                }
                str_temp_TC_OpeProcedure = fontNo + str_temp_TC_OpeProcedure;
            }
            if (str_temp_TC_title.Contains("Printer→HP LaserJet→Font Point"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                string fontNo = null;
                if (settingsButtonFlg == false)
                {
                    fontNo = "共通_バックホーム\n機能_選択    All Settings\nメニュー_選択    Printer\nメニュー_選択    HP LaserJet\nメニュー_選択    Font No.\nメニュー_選択    Font No.\n入力_クリーン\n入力_入力    020\n入力_コミット\n";
                }
                else
                {
                    fontNo = "共通_バックホーム\n機能_選択    Settings\n機能_選択    All Settings\nメニュー_選択    Printer\nメニュー_選択    HP LaserJet\nメニュー_選択    Font No.\nメニュー_選択    Font No.\n入力_クリーン\n入力_入力    020\n入力_コミット\n";
                }
                str_temp_TC_OpeProcedure = fontNo + str_temp_TC_OpeProcedure;
            }
            if (str_temp_TC_title.Equals("All Settings→Printer→HP LaserJet→Font No.") && CurTCModuleName.Contains("各画面確認"))
            {
                str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("    [I000-I109]/1", "").Replace("    Soft Font No.", "");
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
            }
            if ((str_temp_TC_title.Equals("All Settings→Printer→HP LaserJet→Font No.") && CurTCModuleName.Contains("オプション設定"))
                || str_temp_TC_title.Contains("HP LaserJet→Font No.→Soft Font No.") || str_temp_TC_title.Contains("Memory Receive→Fax Forward→File Type"))
            {
                return;
            }

            if (str_temp_TC_title.Contains("All Settings→Printer→Carbon Menu→Copies"))
            {
                dataRowTestCase[AddDoubleQuotes("前置条件")] = string.Empty;
                List<string> carbonMenuCopiesList = new List<string>(carbonMenuCopies);
                if (settingsButtonFlg)
                {
                    int index;
                    while ((index = carbonMenuCopiesList.FindIndex(o => o == "機能_選択    All Settings")) >= 0)
                    {
                        carbonMenuCopiesList[index] = "機能_選択    Settings\n" + "機能_選択    All Settings";
                    }
                }
                if (str_temp_TC_title.Contains("2"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "2");
                }
                else if(str_temp_TC_title.Contains("3"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "3");
                }
                else if(str_temp_TC_title.Contains("4"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "4");
                }
                else if(str_temp_TC_title.Contains("5"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "5");
                }
                else if(str_temp_TC_title.Contains("6"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "6");
                }
                else if(str_temp_TC_title.Contains("7"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "7");
                }
                else if(str_temp_TC_title.Contains("8"))
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray()).Replace("1", "8");
                }
                else
                {
                    str_temp_TC_OpeProcedure = string.Join("\n", carbonMenuCopiesList.ToArray());
                }
            }
            str_temp_TC_OpeProcedure = str_temp_TC_OpeProcedure.Replace("タイトル_チェック    Add Number", "タイトル_チェック    Broadcasting")
                .Replace("メニュー_リストチェック    Search:    Edit", "機能_有効    Edit");

            dataRowTestCase[AddDoubleQuotes("步骤")] = AddDoubleQuotes(str_temp_TC_OpeProcedure);

            /* "预期"の設定 */
            stepindex = 1;/* TC的预期结果的起始番号为1 */
            for (stepindex = 1; stepindex <= oneOpeProcedureInfo.Count; stepindex++)
            {
                if (1 == stepindex)
                {
                    str_temp_TC_ExpectResult = stepindex.ToString() + ". ";
                }
                else
                {
                    str_temp_TC_ExpectResult = str_temp_TC_ExpectResult + "\n" + stepindex.ToString() + ".";
                }
            }

            /*
             *补丁:優先度調整
             */
            foreach (System.Data.DataRow needReplacePathInfo in sheet1ManualModifyDT.Rows)
            {
                needReplacePath = needReplacePathInfo[0].ToString(); //第1列的值
                if (string.IsNullOrEmpty(needReplacePath)) { continue; }
                string operate = needReplacePathInfo["優先度調整"].ToString(); //優先度調整列的值
                if (string.IsNullOrEmpty(operate)) { continue; }
                if (str_temp_TC_title.Contains(needReplacePath) && (false == string.IsNullOrEmpty(operate)))
                {
                    if ("replace".Equals(operate.ToLower()))
                    {
                        string[] strArray = new string[needReplacePathInfo.ItemArray.Length];
                        needReplacePathInfo.ItemArray.CopyTo(strArray, 0);
                        List<string> strArrayList = new List<string>(strArray);
                        int strIndex = strArrayList.FindIndex(s => s == operate);
                        string content = strArrayList[strIndex + 1].Replace("\n", "").Replace("\r", ""); // 取出和Operate同行的下一列的值
                        CurTCPriority = int.Parse(content);
                        break;
                    }
                }
            }
            /* Admin Settings優先度調整,为了防止Admin Setting中的值设定会影响其他Tc的运行*/
            if (str_temp_TC_title.Contains("Admin Settings") && CurTCModuleName.Contains("各画面確認"))
            {
                CurTCPriority = 91;
            }
            if (str_temp_TC_title.Contains("Admin Settings") && CurTCModuleName.Contains("オプション設定"))
            {
                CurTCPriority = 90;
            }

            dataRowTestCase[AddDoubleQuotes("预期")] = AddDoubleQuotes(str_temp_TC_ExpectResult);
            str_temp_KeyWord = AddDoubleQuotes(CommonFTBXlsOperationNpoi.ftbMccAttri.continentcountrydatalist[Sel_Continent_Index].country_name[Sel_Country_Index]);
            dataRowTestCase[AddDoubleQuotes("关键词")] = str_temp_KeyWord;
            dataRowTestCase[AddDoubleQuotes("优先级")] = AddDoubleQuotes(CurTCPriority.ToString());
            dataRowTestCase[AddDoubleQuotes("适用阶段")] = str_temp_KeyWord;
            dataRowTestCase[AddDoubleQuotes("S")] = AddDoubleQuotes(oneOpeProcedureInfo.Count.ToString());

            if (false == ConditionExist)
            {
                if (false == ManualModify)
                {
                    dtScriptInfoNoCondition.Rows.Add(dataRowTestCase);
                }
                else
                {
                    dtScriptInfoNoCondition_NeedRevise.Rows.Add(dataRowTestCase);
                }
            }
            else
            {
                dtScriptInfoWithCondition.Rows.Add(dataRowTestCase);
            }
        }

        /* Tc excelとTc csvファイルを保存する */
        private void SaveScriptFile(DataTable dtScriptInfo,string strPreName,bool OutputExcel)
        {
            string srtSavePath_CSV = "";
            string srtSavePath_Excel = "";
            string srtSaveName = SheetName + "_" + strPreName;

            if (0 < dtScriptInfo.Rows.Count)
            {
                srtSavePath_CSV = FileSaveRootPath + "\\" + SheetName + "\\CSV\\";
                
                if (false == Directory.Exists(srtSavePath_CSV))
                {
                    Directory.CreateDirectory(srtSavePath_CSV);
                }

                CSVFileHelper.DataTable2CSV(dtScriptInfo, srtSavePath_CSV + srtSaveName + ".csv", Encoding.UTF8, false);

                if (true == OutputExcel)
                {
                    srtSavePath_Excel = FileSaveRootPath + "\\" + SheetName + "\\Excel\\";
                    if (false == Directory.Exists(srtSavePath_Excel))
                    {
                        Directory.CreateDirectory(srtSavePath_Excel);
                    }

                    CSVFileHelper.DataTable2Excel(dtScriptInfo, srtSavePath_Excel + srtSaveName + ".xlsx", Encoding.UTF8);
                }
            }
        }

        /* 画面IDにより、Design名を取得する */
        private string Get_DesignName_From_ScrnID(string strScrnID)
        {
            if (true == string.IsNullOrEmpty(strScrnID))
            {
                return "";
            }
            if (true == strScrnID.Equals("dummy"))
            {
                return "dummyDesign";
            }

            if ((null == allScreenInfo) || (0 == allScreenInfo.Count))
            {
                return "";
            }

            string strDesignName = allScreenInfo.Find(delegate (ScreenInfo info) { return info.screenID == strScrnID; }).designName;
            
            return strDesignName;
        }

        /* 字符串类型加双引号 */
        private string AddDoubleQuotes(string strTemp)
        {
            return "\"" + strTemp + "\"";
        }

        /* 追加共通步骤：AdminSettings的Input Password */
        private void AddCommonSetp_InputPSW()
        {
            if ((true == oneStepInfo.DomainKW.Equals(DomainKeyWord.KW_Funtion.Select)) && (true == oneStepInfo.us_word.Equals("Admin Settings")))
            {
                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.Input;
                /* design名設定 */
                oneStepInfo.designName =  "";
                oneStepInfo.us_word = DeviceInitPass;
                oneOpeProcedureInfo.Add(oneStepInfo);

                oneStepInfo.DomainKW = DomainKeyWord.KW_SoftInput.OK;
                /* design名設定 */
                oneStepInfo.designName = "";
                oneStepInfo.us_word = "";
                oneOpeProcedureInfo.Add(oneStepInfo);
            }
        }

        /* Condition情報を追加する */
        private void AddCommonConditionInfo(string strLevelName, int ConditionIndex)
        {
            if (null != oneOpeConditionInfo)
            {
                if (0 < ConditionIndex)
                {
                    string ConditionInfo = AllConditionList[ConditionIndex];
                    ConditionInfo = ConditionInfo.Replace("\n", "").Replace("\r", "");
                    if (ConditionInfo.Contains("TP Model")) //TBD暂定:目前平台支持GUI,不支持CUI,所以TP Model相关的tc的ConditionInfo置null
                    {
                        ConditionInfo = string.Empty;
                    }
                    //foreach (Condition_Replace tempReplaceInfo in allConditionReplaceInfo)
                    //{
                    //    if (true == tempReplaceInfo.OldCondition.Equals(ConditionInfo))
                    //    {
                    //        /* Conditionを置換 */
                    //        ConditionInfo = tempReplaceInfo.NewCondition;
                    //        break;
                    //    }
                    //}
                    readConditionReplaceContent(configurePath); //读取第3个sheet
                    foreach (ConditionReplaceInfo replaceConditionInfo in totalConditionReplaceInfo)
                    {
                        if (string.IsNullOrEmpty(replaceConditionInfo.conditionType)) { continue; }
                        if (string.IsNullOrEmpty(replaceConditionInfo.conditionReplaceValue)) { continue; }
                        if (("外部").Equals(replaceConditionInfo.conditionType) || ("外部").Equals(replaceConditionInfo.conditionType))
                        {
                            if (ConditionInfo.Equals(replaceConditionInfo.oldCondition))
                            {
                                ConditionInfo = replaceConditionInfo.conditionReplaceValue;
                                break;
                            }
                        }
                    }
                    /* 置換後のCondition情報が有効です */
                    if (false == string.IsNullOrEmpty(ConditionInfo))
                    {
                        string strTemp = "";
                        if (true == ConditionInfo.ToLower().StartsWith("#if")
                            || ConditionInfo.ToLower().StartsWith("#【laser】if"))
                        {
                            /* 置換前Condition情報の場合 */
                            strTemp = "[" + strLevelName + "]" + ":" + ConditionInfo;
                            strTemp = strTemp.Replace(",", "、");
                            strTemp = strTemp.Replace("\"", "'");
                        }
                        else
                        {
                            /* 置換後Condition情報の場合 */
                            strTemp = ConditionInfo.Replace(",", "、");
                        }

                        if (false == oneOpeConditionInfo.Contains(strTemp))
                        {
                            oneOpeConditionInfo.Add(strTemp);
                        }
                    }
                }
            }
        }

        /* 次階层画面の種別を取得する */
        private ScrnType CheckNextScrnType(LevelNode tempLevelNode)
        {
            bool LevelNodeExist = false;
            if (0 == tempLevelNode.levelnodes.Count)
            {
                return ScrnType.None;
            }
            else if (1 == tempLevelNode.levelnodes.Count)
            {
                if (tempLevelNode.levelnodes[0] is OptionNode)
                {
                    return CheckOptionScrnType((tempLevelNode.levelnodes[0] as OptionNode).ftboption.us_words);
                }
                else
                {
                    return ScrnType.List;
                }
            }
            else
            {
                for (int i = 0; i < tempLevelNode.levelnodes.Count; i++)
                {
                    if (tempLevelNode.levelnodes[i] is LevelNode)
                    {
                        LevelNodeExist = true;
                        break;
                    }
                }

                if (true == LevelNodeExist)
                {
                    return ScrnType.List;
                }
                else
                {
                    return CheckOptionScrnType((tempLevelNode.levelnodes[0] as OptionNode).ftboption.us_words);
                }
            }
        }

        /* オプション画面の種別を取得する */
        private ScrnType CheckOptionScrnType(string strValue)
        {
            string strTempValue = strValue.Trim();
            ScrnType curType = ScrnType.None;

            if (false == string.IsNullOrEmpty(strTempValue))
            {
                /* "Manual:<Limit:3,Charaset:TEL>" */
                if (Regex.IsMatch(strTempValue, "^(BRN)?Manual:", RegexOptions.IgnoreCase))
                {
                    curType = ScrnType.SoftKey;
                }
                /* "DispOnly:<Limit:255, Charaset:FTPF>" */
                else if (Regex.IsMatch(strTempValue, "^DispOnly:", RegexOptions.IgnoreCase))
                {
                    curType = ScrnType.MachinInfo;
                }
                else if (strTempValue.StartsWith("["))
                {
                    /* "[1-65535]/1" */
                    if (true == strTempValue.Contains("-") && true == strTempValue.Contains("/"))
                    {
                        curType = ScrnType.SoftKey;
                    }
                    else if (Regex.IsMatch(strTempValue, @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase))
                    {
                        /* "[000-255]/1.[000-255]/1.[000-255]/1.[000-255]/1" */
                        curType = ScrnType.SoftKey;
                    }
                    else
                    {
                        curType = ScrnType.List;
                    }
                }
                else
                {
                    curType = ScrnType.List;
                }
            }
            else
            {
                curType = ScrnType.Unknow;
            }

            return curType;
        }
    }
}
