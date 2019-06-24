using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FTBExcel2Script
{
    class CommonFTBXlsOperationNpoi
    {
        protected struct ContinentInfo
        {
            public string name;
            public int colSpan;
        }

        protected MainForm form;
        protected ExcelOperationNpoi excelOperation = null;
        protected List<string> allSheetname = null;

        protected int rowIndex = 2;

        protected List<string> modelList = null;
        //List<string> totalPath = null;
        protected int Sel_Model_Index;
        protected int Sel_Model_Column;   /* FTBExcel中、選択モデルの所在列 */

        protected List<ContinentInfo> continentList = null;
        protected static int Sel_Continent_Index;

        protected List<string> countryList = null;
        protected int Sel_Country_Index;
        protected int Sel_Country_Column;   /* FTBExcel中、選択国の所在列 */

        protected Dictionary<string, int> modelStartPosition = null;
        protected Dictionary<string, int> endPosition = null;
        protected Dictionary<string, int> levelStartPosition = null;
        protected Dictionary<string, int> optionPosition = null;
        protected List<string> conditionList = null;
        protected List<string> totalConditionList = null;

        protected List<string> allSheetManualPathList = null;
        public static FTBMccInfo ftbMccAttri = null;
        protected static LevelNode levelNode = null;
        protected static FTBConditionInfo ftbCondition = null;
        protected static FTBOneSheetInfo ftbOneSheetInfo = null;
        protected static FTBlanguageInfo ftbLangAttri = null;

        TCScriptCreat ScriptCreat = null;
        //NodeInfoHelper nodeInfoHelper = null;
        protected static int levels = 0, yellowCell = -1;

        private bool ftb2JSON = false;

        /* 実行終了Flag */
        protected static bool ProcessEnd = false;

        public CommonFTBXlsOperationNpoi()
        {
            excelOperation = new ExcelOperationNpoi();
            ScriptCreat = new TCScriptCreat();
            //nodeInfoHelper = new NodeInfoHelper();
            totalConditionList = new List<string>();
            allSheetManualPathList = new List<string>();
        }

        public bool UserCancelProcessEndFlag
        {
            set { ProcessEnd = value; }
            get { return ProcessEnd; }
        }

        public bool ftb2JSONFlag
        {
            set { ftb2JSON = value; }
        }

        public int SelModelIndex
        {
            set { Sel_Model_Index = value; }
        }

        public int SelContinentIndex
        {
            set { Sel_Continent_Index = value; }
        }

        public int SelCountryIndex
        {
            set { Sel_Country_Index = value; }
        }

        public FTBMccInfo CurFTBMccInfo
        {
            get { return ftbMccAttri; }
        }

        public FTBlanguageInfo CurLangMccInfo
        {
            get { return ftbLangAttri; }
        }

        /* 
         *  Description:open Excle
         *  Param:filePath - file Path
         *  Return:bool
         *  Exception:
         *  Example:openExcle(filePath);
         */
        public bool openExcle(string Path)
        {
            return excelOperation.openExcel(Path);
        }

        /* 
         *  Description:get Model Name Postion
         *  Param:
         *  Return:Dictionary<string, int>
         *  Exception:
         *  Example:getModelNamePostion() 
         */
        protected Dictionary<string, int> getModelNamePostion()
        {
            Dictionary<string, int> position = new Dictionary<string, int>();
            string strTemp = "";
            //100 is #MODELNAME# possible max row
            for (int rowIndex = 0; rowIndex < 100; rowIndex++)
            {
                //10 is #MODELNAME# possible max col
                for (int colIndex = 0; colIndex < 10; colIndex++)
                {
                    if (true == ProcessEnd)
                    {
                        return null;
                    }
                    strTemp = excelOperation.getCellValue(rowIndex, colIndex);
                    if (strTemp == "#MODELNAME#")
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                    else if (strTemp == "モデル")//ECモデルから、新しいMasterFTB
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                }
            }
            return null;
        }

        /* 
         *  Description:get End Postion
         *  Param:
         *  Return:Dictionary<string, int>
         *  Exception:
         *  Example:getEndPostion()
         */
        protected Dictionary<string, int> getEndPostion(int colIndex)
        {
            Dictionary<string, int> position = new Dictionary<string, int>();
            //5000 is "#EOF#" possible max row
            for (int index = 50; index < 5000; index++)
            {
                if (true == ProcessEnd)
                {
                    break;
                }
                if (excelOperation.getCellValue(index, colIndex) == "#EOF#" || excelOperation.getCellValue(index, colIndex) == "")
                {
                    position.Add("row", index);
                    position.Add("col", colIndex);
                    return position;
                }
            }
            return null;
        }
        /* 
         *  Description:get Level Start Postion
         *  Param:
         *  Return:Dictionary<string, int>
         *  Exception:
         *  Example:getLevelStartPostion()
         */
        protected Dictionary<string, int> getLevelStartPostion(int startRow, int startCol)
        {
            Dictionary<string, int> position = new Dictionary<string, int>();
            string strTemp = "";
            //100 is #LEVEL# possible max row
            for (int rowIndex = startRow; rowIndex < 100; rowIndex++)
            {
                if (startCol > 200)
                {
                    throw new Exception("LEVEL start column is greater than 200！");
                }
                //200 is #LEVEL# possible max col
                for (int colIndex = startCol; colIndex < 200; colIndex++)
                {
                    if (true == ProcessEnd)
                    {
                        return null;
                    }
                    strTemp = excelOperation.getCellValue(rowIndex, colIndex);
                    if (strTemp == "#LEVEL#")
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                    else
                    {
                        if (true == excelOperation.getSheetName().Contains("Menu"))
                        {
                            if (strTemp == "LEVEL_1")//ECモデルから、新しいMasterFTB
                            {
                                position.Add("row", rowIndex - 1);
                                position.Add("col", colIndex);
                                return position;
                            }
                        }
                        else
                        {
                            if (strTemp == "LEVEL_2")//ECモデルから、新しいMasterFTB
                            {
                                position.Add("row", rowIndex - 1);
                                position.Add("col", colIndex);
                                return position;
                            }
                        }
                    }
                }
            }
            return null;
        }
        /* 
         *  Description:get Option Position
         *  Param:
         *  Return:Dictionary<string, int>
         *  Exception:
         *  Example:getOptionPosition()
         */
        protected Dictionary<string, int> getOptionPosition(int startRow, int startCol)
        {
            Dictionary<string, int> position = new Dictionary<string, int>();
            string strTemp = "";
            //100 is #OPTIONS# possible max row
            for (int rowIndex = startRow; rowIndex < 100; rowIndex++)
            {
                for (int colIndex = startCol; colIndex < 500; colIndex++)
                {
                    if (true == ProcessEnd)
                    {
                        return null;
                    }
                    strTemp = excelOperation.getCellValue(rowIndex, colIndex);
                    if (strTemp == "#OPTIONS#")
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                    else if (strTemp == "OPTIONS")//ECモデルから、新しいMasterFTB
                    {
                        position.Add("row", rowIndex - 1);
                        position.Add("col", colIndex);
                        return position;
                    }
                }
            }
            return null;
        }
        /* 
         *  Description:get FunSetFlag Position
         *  Param:
         *  Return:Dictionary<string, int>
         *  Exception:
         *  Example:getOptionPosition()
         */
        protected Dictionary<string, int> getFunSetFlagPosition()
        {
            Dictionary<string, int> position = new Dictionary<string, int>();
            string strTemp = "";
            for (int rowIndex = 0; rowIndex < 100; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 500; colIndex++)
                {
                    strTemp = excelOperation.getCellValue(rowIndex, colIndex);
                    if (strTemp == "#FUNCSETFLAG#")
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                    else if (strTemp == "Laser")
                    {
                        position.Add("row", rowIndex);
                        position.Add("col", colIndex);
                        return position;
                    }
                }
            }
            return null;
        }
        /* 
         *  Description:get Levels count
         *  Param:
         *  Return:int
         *  Exception:
         *  Example:getLevels() 
         */
        protected int getLevels()
        {
            string cellValue = null, tempValue = null;
            int col = levelStartPosition["col"];
            while (true)
            {
                col++;
                cellValue = excelOperation.getCellValue(levelStartPosition["row"] + 1, col);
                if (cellValue.Length > "LEVEL_".Length && "LEVEL_" == cellValue.Substring(0, "LEVEL_".Length))
                {
                    if (cellValue != tempValue)
                    {
                        tempValue = cellValue;
                        levels++;
                    }
                }
                else
                    break;
            }
            return levels;
        }
        /* 
         *  Description:create Sheet Txt File
         *  Param:fileName-save json txt name
         *  Return:
         *  Exception:
         *  Example:ReadAllSheetInfo()
         */
        protected void ReadAllSheetInfo()
        {
            allSheetname = excelOperation.getAllSheetName();
            //去除不需要的sheetName
            for (int index = 0; index < allSheetname.Count; index++)
            {
                string sheetname = allSheetname[index];
                //sheetName 长度小于9的不进行操作
                if (sheetname.Length <= 9)
                {
                    allSheetname.RemoveAt(index);
                    index--;
                    continue;
                }
                //sheetName 不是Settings的不进行操作
                if (sheetname.Length >= 10)
                {
                    //if (sheetname.Substring(sheetname.Length - 8, 8) != "Settings" && sheetname.Substring(sheetname.Length - 4, 4) != "Temp" && sheetname.Substring(sheetname.Length - 13, 13) != "Temp Settings" && sheetname.Substring(sheetname.Length - 13, 13) != "Menu Settings")
                    if (sheetname.Substring(sheetname.Length - 8, 8) != "Settings")
                    {
                        allSheetname.RemoveAt(index);
                        index--;
                        continue;
                    }
                }
            }
        }

        /* 
         *  Description:Delete invalid Sheet
         *  Param:none
         *  Return:
         *  Exception:
         *  Example:RemoveInvalidSheet()
         */
        protected void RemoveInvalidSheet()
        {
            allSheetname = new List<string>();
            allSheetname = excelOperation.getAllSheetName();
            //去除不需要的sheetName
            for (int index = 0; index < allSheetname.Count; index++)
            {
                string sheetname = allSheetname[index];
                //sheetName 长度小于9的不进行操作
                if (sheetname.Length <= 9)
                {
                    allSheetname.RemoveAt(index);
                    index--;
                    continue;
                }
                //sheetName 不是Menu和Temp的不进行操作
                if (sheetname.Length >= 10)
                {
                    //if (sheetname.Substring(sheetname.Length - 8, 8) != "Settings" && sheetname.Substring(sheetname.Length - 4, 4) != "Temp" && sheetname.Substring(sheetname.Length - 13, 13) != "Temp Settings" && sheetname.Substring(sheetname.Length - 13, 13) != "Menu Settings")
                    if (sheetname.Substring(sheetname.Length - 8, 8) != "Settings"
                        || sheetname.Contains("Direct Print"))
                    {
                        allSheetname.RemoveAt(index);
                        index--;
                        continue;
                    }
                }
            }
        }

        /* 
         *  Description:find MCCstring
         *  Param:
         *  Return:string
         *  Exception:
         *  Example:getMcc(row,startCol,endCol,str)
         */
        protected string getMcc(int row, int startCol, int endCol, List<string> rightStrList, List<string> wrongStrList)
        {
            int col;
            string modelCountry = "";
            for (col = startCol; col <= endCol; col++)
            {
                if (wrongStrList.Contains(excelOperation.getCellValue(row, col)))
                {
                    modelCountry = modelCountry + "0";
                }
                else if (rightStrList.Contains(excelOperation.getCellValue(row, col)))
                {
                    modelCountry = modelCountry + "1";
                }
                else
                {
                    throw new Exception("Model x Country right or wrong sign can't find");
                }
            }
            for (; col <= 64; col++)
            {
                modelCountry = modelCountry + "0";
            }
            return Convert.ToInt64(modelCountry, 2) + "";
        }
        /* 
         *  Description:find ModelXCountry
         *  Param:
         *  Return:List<ModelCountry>
         *  Exception:
         *  Example:findModelXCountry()
         */
        protected List<ModelCountry> findModelXCountry()
        {
            string model = "";
            int modelStartRow = 0, modelCol = 0, startCol = 0, endCol = 0;
            List<string> rightStrLis = new List<string>();
            List<string> wrongStrLis = new List<string>();
            List<ModelCountry> modelXCountryList = new List<ModelCountry>();
            ModelCountry modelXCountry = new ModelCountry();
            string sheetName = excelOperation.getSheetName();
            rightStrLis.Add("");
            rightStrLis.Add("〇");//〇
            rightStrLis.Add("○");//○
            rightStrLis.Add("O");//O
            wrongStrLis.Add("x");//x
            wrongStrLis.Add("×");//×
            wrongStrLis.Add("ｘ");//ｘ
            wrongStrLis.Add("X");//X
            bool flag = excelOperation.moveToSheet("Model x Country");
            if (flag == true)
            {
                for (int row = 0; row < 100; row++)
                {
                    for (int col = 0; col < 20; col++)
                    {
                        if (excelOperation.getCellValue(row, col) == "#MODELNAME#")
                        {
                            modelCol = col;
                            modelStartRow = row + 1;
                            startCol = col + 1;
                            break;
                        }
                    }
                }
                endCol = startCol;
                while (excelOperation.getCellValue(modelStartRow - 1, endCol) != "")
                {
                    endCol++;
                }

                for (int row = modelStartRow; excelOperation.getCellValue(row, modelCol) != ""; row++)
                {
                    model = excelOperation.getCellValue(row, modelCol);
                    modelXCountry.model_name = model;
                    modelXCountry.modelxcounty = getMcc(row, startCol, endCol, rightStrLis, wrongStrLis);
                    modelXCountryList.Add(modelXCountry);
                    model = "";
                }
            }
            else
            {
                excelOperation.moveToSheet(sheetName);
                int row = modelStartPosition["row"] + 4;
                int col = 0;
                string modelXCountryStr = "";
                int colCount = (levelStartPosition["col"] - 1) / modelList.Count;
                for (int modelIndex = 0; modelIndex < modelList.Count; modelIndex++)
                {
                    for (col = 0; col < (levelStartPosition["col"] - 1) / modelList.Count; col++)
                    {
                        if (excelOperation.getCellValue(row, col + (colCount * modelIndex)) == "x")
                        {
                            modelXCountryStr = modelXCountryStr + "0";
                        }
                        else
                        {
                            modelXCountryStr = modelXCountryStr + "1";
                        }
                    }
                    for (; col < 64; col++)
                    {
                        modelXCountryStr = modelXCountryStr + "0";
                    }
                    modelXCountry.model_name = modelList[modelIndex];
                    modelXCountry.modelxcounty = Convert.ToInt64(modelXCountryStr, 2) + "";
                    modelXCountryList.Add(modelXCountry);
                    modelXCountryStr = "";
                }
            }
            excelOperation.moveToSheet(sheetName);
            return modelXCountryList;
        }
        /* 
         *  Description:get LanguageInfo
         *  Param:
         *  Return:int
         *  Exception:
         *  Example:getLanguageInfo()
         */
        protected int getLanguageInfo()
        {
            int row, title_row;
            int col, Category_col = 0, Country_col = 0, Target_col = 0, Language_col = 0;
            int Category_start_row, Category_rowcount;
            int start_row, rowcount;
            string cell_value;
            ftbLangAttri = new FTBlanguageInfo();
            List<string> model = new List<string>();
            model.Add("MFC");
            model.Add("DCP");
            model.Add("MFC/DCP");
            model.Add("PRN");
            model.Add("ES");
            excelOperation.moveToSheet("Language List");

            title_row = 3;
            //5 is "Local Language" possible max row
            for (int i = 0; i < 5; i++)
            {
                cell_value = excelOperation.getCellValue(i, 0).Trim();
                if (true == cell_value.Equals("Local Language"))
                {
                    title_row = i + 2;  //title line
                    break;
                }
            }

            col = 0;
            int endRow = 0;
            while (excelOperation.getCellValue(endRow, 0) != "#EOF#")
            {
                endRow++;
            }
            while ("" != (cell_value = excelOperation.getCellValue(title_row, col)))
            {
                if ("Product Category" == cell_value)
                {
                    Category_col = col;
                }
                else if ("Country" == cell_value)
                {
                    Country_col = col;
                }
                else if ("Target" == cell_value)
                {
                    Target_col = col;
                }
                else if ("Language" == cell_value)
                {
                    Language_col = col;
                }
                //else if ("Default" == cell_value)               
                //else if ("Language DB File" == cell_value)  
                else
                {

                }
                col++;
            }

            //add mcc data to FTBMccInfo
            for (int i = 0; i < model.Count; i++)
            {
                LangData langdateinfo = new LangData();
                row = title_row + 1;
                cell_value = null;
                Category_start_row = 0;
                Category_rowcount = 0;
                while (row < endRow)
                {
                    cell_value = excelOperation.getCellValue(row, Category_col);
                    start_row = excelOperation.getCellTopRow(row, Category_col);
                    rowcount = excelOperation.getSpan(row, Category_col)["rowspan"];
                    if (model[i] == cell_value)
                    {
                        langdateinfo.model_name = model[i];
                        Category_start_row = start_row;
                        Category_rowcount = rowcount;
                        //Console.WriteLine("yejx {0} Category_start_row:{1},Category_rowcount{2}\n", cell_value, Category_start_row, Category_rowcount);
                        break;
                    }
                    else
                    {
                        row = row + rowcount;
                    }
                }

                if ((Category_start_row == 0) && (Category_rowcount == 0))
                {
                    continue;
                }

                langdateinfo.supportlanguageinfolist = new List<SupportLanguageDate>();

                row = Category_start_row;
                rowcount = 1;
                cell_value = null;
                while (row < Category_start_row + Category_rowcount)
                {
                    SupportLanguageDate supportlanguagedatelist = new SupportLanguageDate();

                    cell_value = excelOperation.getCellValue(row, Country_col);
                    if ((true == cell_value.Contains("(")) && (true == cell_value.Contains(")")))
                    {
                        cell_value = cell_value.Remove(cell_value.IndexOf("("));
                    }
                    supportlanguagedatelist.country_name = cell_value;

                    rowcount = excelOperation.getSpan(row, Country_col)["rowspan"];

                    supportlanguagedatelist.support_language_name = new List<string>();

                    int lang_row = row;
                    int lang_rowcount = 1;
                    cell_value = null;
                    while (lang_row < row + rowcount)
                    {
                        cell_value = excelOperation.getCellValue(lang_row, Language_col);
                        supportlanguagedatelist.support_language_name.Add(cell_value);
                        lang_rowcount = excelOperation.getSpan(lang_row, Language_col)["rowspan"];
                        lang_row = lang_row + lang_rowcount;
                    }
                    langdateinfo.supportlanguageinfolist.Add(supportlanguagedatelist);
                    row = row + rowcount;
                }
                ftbLangAttri.langdatelist.Add(langdateinfo);
            }
            return 0;
        }

        /* 
         *  Description:write json to txt
         *  Param: fileName-file name
         *  Param: ftbMccAttri,ftbCondition,levelNodel-levelNode param
         *  Param: index-sheet name index
         *  Return:
         *  Exception:
         *  Example:write(fileName,ftbMccAttri,ftbCondition,levelNodel,index)
         */
        protected void write2Json(FTBMccInfo ftbMccAttri, FTBConditionInfo ftbCondition, LevelNode levelNodel, int index)
        {
            string strFileStoreFolder = TCScriptCreat.FileSaveRootPath + "\\Json";
            string strFileFullName = TCScriptCreat.FileSaveRootPath + "\\Json\\" + allSheetname[index] + ".json";

            string jsonText = "";

            /* 文件夹是否存在 */
            if (false == Directory.Exists(strFileStoreFolder))
            {
                Directory.CreateDirectory(strFileStoreFolder);
            }

            /* 文件是否存在存在 */
            if (true == File.Exists(strFileFullName))
            {
                File.Delete(strFileFullName);
            }

            FileStream fileNewStream = new FileStream(strFileFullName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriteStream = new StreamWriter(fileNewStream);
            ftbOneSheetInfo = new FTBOneSheetInfo(ftbMccAttri, ftbCondition, levelNodel);
            jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(ftbOneSheetInfo);
            //write json
            fileWriteStream.Write(jsonText);
            fileWriteStream.Flush();
            fileWriteStream.Close();

            fileNewStream.Close();
        }
        /* 
         *  Description:write Language
         *  Param: fileName-file name
         *  Return:
         *  Exception:
         *  Example:writeLanguage(fileName)
         */
        protected void writeLanguage()
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string jsonText = "";
            fs = new FileStream(TCScriptCreat.FileSaveRootPath + "\\" + "language.txt", FileMode.Create, FileAccess.Write);
            fs.Close();
            sw = new StreamWriter(TCScriptCreat.FileSaveRootPath + "\\" + "language.txt", true);

            jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(ftbLangAttri);
            sw.Write("\r\n" + jsonText + "\r\n");
            sw.Flush();
            sw.Close();
        }

        /* 
         *  Description:main method
         *  Param: fileName-file name
         *  Return:
         *  Exception:
         *  Example:ReadSelTestInfo()
         */
        public bool ReadSelTestInfo()
        {
            bool actResult = false;

            ReadAllSheetInfo();
            if (0 >= allSheetname.Count)
            {
                return false;
            }

            ftbMccAttri = new FTBMccInfo();
            ftbCondition = new FTBConditionInfo();
            actResult = excelOperation.moveToSheet(allSheetname[0]);
            if (false == actResult)
            {
                return false;
            }

            modelStartPosition = getModelNamePostion();
            if (null == modelStartPosition)
            {
                return false;
            }

            levelStartPosition = getLevelStartPostion(modelStartPosition["row"], modelStartPosition["col"]);
            if (null == levelStartPosition)
            {
                return false;
            }

            getModel();
            getContinent();
            findMccInfo(ftbMccAttri);

            getLanguageInfo();
            //writeLanguage("XXX");

            //not use
            Console.WriteLine("get mcc end");
            return true;
        }

        /* 
         *  Description:main method
         *  Param: fileName-file name
         *  Return:
         *  Exception:
         *  Example:ReadFTBInfo(fileName)
         */
        public void ReadFTBDetailInfo(string fileSavePath)
        {
            RemoveInvalidSheet();
            bool actResult = false;
            ScriptCreat.TCCount_NoCondition = 0;
            ScriptCreat.TCCount_WithCondition = 0;
            for (int sheetIndex = 0; sheetIndex < allSheetname.Count; sheetIndex++)
            {
                if (true == ProcessEnd)
                {
                    break;
                }

                ftbMccAttri = new FTBMccInfo();
                levelNode = new LevelNode();
                ftbCondition = new FTBConditionInfo();
                conditionList = new List<string>();
                actResult = excelOperation.moveToSheet(allSheetname[sheetIndex]);
                if (false == actResult)
                {
                    continue;
                }

                modelStartPosition = getModelNamePostion();
                if (null == modelStartPosition)
                {
                    Trace.WriteLine(DateTime.Now + ":" + allSheetname[sheetIndex] + " sheet,get modelStartPosition error");
                    continue;
                }

                levelStartPosition = getLevelStartPostion(modelStartPosition["row"], modelStartPosition["col"]);
                if (null == levelStartPosition)
                {
                    Trace.WriteLine(DateTime.Now + ":" + allSheetname[sheetIndex] + " sheet,get getLevelStartPostion error");
                    continue;
                }

                endPosition = getEndPostion(modelStartPosition["col"]);
                if (null == endPosition)
                {
                    Trace.WriteLine(DateTime.Now + ":" + allSheetname[sheetIndex] + " sheet,get endPosition error");
                    continue;
                }

                optionPosition = getOptionPosition(levelStartPosition["row"], levelStartPosition["col"]);
                if (null == optionPosition)
                {
                    Trace.WriteLine(DateTime.Now + ":" + allSheetname[sheetIndex] + " sheet,get getOptionPosition error");
                    continue;
                }

                getModel();
                getContinent();
                findMccInfo(ftbMccAttri);

                //not use
                Console.WriteLine("get mcc end");

                getModelInfoColumn();

                getCondition(conditionList);
                /* 输出所有的conditionList */
                totalConditionList.AddRange(conditionList.Distinct().ToList());

                foreach (string strtemp in conditionList)
                {
                    Trace.WriteLine(strtemp + "\n");
                    //writeExcel(strtemp, Directory.GetCurrentDirectory() + "\\Configure.xlsx", conditionSheetIndex);
                    //nodeInfoHelper.openExcel(Directory.GetCurrentDirectory() + "\\Configure.xlsx");
                    //nodeInfoHelper.writeTotalConditionListToExcel(totalConditionList);
                }

                getButton(levelNode, conditionList);

                ftbCondition.conditions_list = conditionList;

                
                //nodeInfoHelper.readFromExcelFile(configurePath);
                //nodeInfoHelper.addLevelNodeInfo(levelNode); //补全信息用
                //allSheetManualPathList.AddRange(nodeInfoHelper.getOneSheetTotalManualPathList());

                //System.Data.DataTable modelRelateDT = CSVFileHelper.excelToDataTable(configurePath, 1);

                //string configurePath = Directory.GetCurrentDirectory() + "\\Configure.xlsx";
               

                ///nodeInfoHelper.writeToExcel(configurePath, nodeInfoHelper.getOneSheetTotalManualPathList(), conditionList);
                if (true == ftb2JSON)
                {
                    write2Json(ftbMccAttri, ftbCondition, levelNode, sheetIndex);
                }

                ScriptCreat.CurSheetName = allSheetname[sheetIndex];
                if (true == allSheetname[sheetIndex].Contains("Menu"))
                {
                    ScriptCreat.TempSet = false;
                }
                else
                {
                    ScriptCreat.TempSet = true;
                }

                form.setMessage(allSheetname[sheetIndex] + " sheet:\nScript file is creating, please wait……");

                ScriptCreat.write2Script(ftbMccAttri, levelNode, ftbCondition);

                form.setMessage(allSheetname[sheetIndex] + " sheet:\nScript file create done");

                //not use
                Console.WriteLine("     " + System.DateTime.Now);
            }

            //writeListToTextFile(allSheetManualPathList.Distinct().ToList(), Directory.GetCurrentDirectory() + "\\totalManualList.txt");
            writeListToTextFile(totalConditionList.Distinct().ToList(), Directory.GetCurrentDirectory() + "\\totalConditionList.txt");
            ////nodeInfoHelper.writeToExcel(Directory.GetCurrentDirectory() + "\\Configure.xlsx", conditionSheetIndex);
            //not use
            Console.Write("Language end  " + System.DateTime.Now);

            form.setMessage("TC number of no condition = " + ScriptCreat.TCCount_NoCondition.ToString() +
                            "\nTC number of with condition = " + ScriptCreat.TCCount_WithCondition.ToString());
        }
        public List<string> readTxtFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string tmp = sr.ReadLine();
            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return list;
        }

        public void writeListToTextFile(List<string> list, string txtFile)
        {
            FileStream fs = new FileStream(txtFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < list.Count; i++) sw.WriteLine(list[i].Replace("\n", "").Replace("\r", ""));
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        ////private void creatTree(FTBMccInfo ftbMccAttri, FTBConditionInfo ftbCondition, LevelNode levelNodel, int index)
        ////{
        ////    string strFileStoreFolder = TCScriptCreat.FileSaveRootPath + "\\Json";
        ////    string strFileFullName = TCScriptCreat.FileSaveRootPath + "\\Json\\" + allSheetname[index] + ".json";

        ////    List<string> manualPathList = new List<string>();

        ////    TreeMemoryFTBCommonAggregate.setImporter(new TreeMemoryFTBCommonImportFormJson(strFileFullName));
        ////    IFTBCommonAPI treeMemory;
        ////    treeMemory = new TreeMemoryFTBCommonAggregate();
        ////    treeMemory.importTree();
        ////    treeMemory.importScreenDict();
        ////    totalPath = treeMemory.getTotalPath();

        ////    foreach (string onPathStr in totalPath)
        ////    {
        ////        string[] stringArray = onPathStr.Split('/');
        ////        Array.Reverse(stringArray);//反转排序字符串数组
        ////        if ((Regex.IsMatch(stringArray[0], "^(BRN)?Manual:", RegexOptions.IgnoreCase))
        ////            || (Regex.IsMatch(stringArray[0], @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase)))
        ////        {
        ////            //Array.Reverse(aqqqa);
        ////            //string str = string.Join("->", aqqqa);
        ////            manualPathList.Add(onPathStr);
        ////        }
        ////    }
        ////}
        ////将数据写入已存在Excel
        //public void writeExcel(string conditioStr, string excelFilePath, int sheetIndex)
        //{
        //    Application xApp = new Application();
        //    Workbook xBook = xApp.Workbooks.Open(excelFilePath,
        //                          Missing.Value, Missing.Value, Missing.Value, Missing.Value,
        //                          Missing.Value, Missing.Value, Missing.Value, Missing.Value,
        //                          Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //    Worksheet xSheet = (Worksheet)xBook.Sheets[sheetIndex];
        //    if (sheetIndex == 3)
        //    {
        //        xSheet.Cells[1, 1] = "FTBCondition";
        //    }
        //    if (sheetIndex == 2)
        //    {
        //        xSheet.Cells[1, 1] = "TC_Input_Value_Path";
        //        xSheet.Cells[1, 2] = "ManualRange";
        //        xSheet.Cells[1, 2] = "ManualValue";
        //    }
        //    ((Range)xSheet.Columns["A:A", System.Type.Missing]).ColumnWidth = 100; //设置列宽为100
        //    xSheet.Cells[1][rowIndex] = conditioStr;
        //    rowIndex++;

        //    xBook.Save();
        //    xSheet = null;
        //    xBook.Close();
        //    xBook = null;
        //    xApp.DisplayAlerts = false;
        //    xApp.Quit();
        //    xApp = null;
        //}
        /* 
         *  Description:get all Model name 
         *  Param:
         *  Return:
         *  Exception:
         *  Example:getModel()
         */
        protected virtual void getModel() { }
        /* 
         *  Description:get Continent
         *  Param:
         *  Return:
         *  Exception:
         *  Example:getContinent()
         */
        protected virtual void getContinent() { }
        /* 
         *  Description:get Country
         *  Param:row,col-coordinate
         *  Param:spanCol-continent cell span col
         *  Return:
         *  Exception:
         *  Example:getCountry(row,col,spanCol)
         */
        protected virtual void getCountry(int row, int col, int spanCol) { }
        /* 
         *  Description:find Mcc Info
         *  Param:ftbMccAttri-mcc infoLrb123
         *  Return:
         *  Exception:
         *  Example:findMccInfo(ftbMccAttri)
         */
        protected virtual void findMccInfo(FTBMccInfo ftbMccAttri) { }

        /* 
         *  Description:get ModelInfo Column
         *  Param:none
         *  Return:
         *  Exception:
         *  Example:getModelInfoColumn()
         */
        protected virtual void getModelInfoColumn() { }

        /* 
         *  Description:get Condition List
         *  Param:conditionList-add condition into list
         *  Return:
         *  Exception:
         *  Example:getCondition(conditionList)
         */
        protected virtual void getCondition(List<string> conditionList) { }
        /* 
         *  Description:get Button
         *  Param:level node-add button info into level
         *  Param:conditionlist-condition list
         *  Return:
         *  Exception:
         *  Example:getButton(levelnode,conditionlist)
         */
        protected virtual void getButton(LevelNode levelnode, List<string> conditionlist) { }
    }
}
