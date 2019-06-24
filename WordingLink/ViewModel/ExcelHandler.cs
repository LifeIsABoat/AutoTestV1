using System;
using System.Data;
using System.Threading;
using InteropExcel = Microsoft.Office.Interop.Excel;

namespace WordingLink
{
    class WordingExcelHandler : InteropExcelHelper
    {
        public WordingExcelHandler(string fileName) : base(fileName){  }

        ///<param name = "data" > 读取的数据表 </ param >
        public bool ReadExcel(ref DataTable data)
        {
            bool readSucceed = false;

            try
            {
                int col = 1;
                int startCol = 0;
                int MsgNoCol = 0;
                string[] text = null;
                data = new DataTable();

                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SheetResult");
                int startRow = Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "ResultStartLine"));

                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);

                string cellValue = string.Empty;

                while (true)    //定位开始读取列
                {
                    ReadExcel(sheetName, startRow, col++, ref cellValue);
                    if (!string.IsNullOrEmpty(cellValue)) { break; }
                }
                startCol = --col;
                while (true)
                {
                    ReadExcel(sheetName, startRow, col++, ref cellValue);
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        break;
                    }
                    data.Columns.Add(cellValue);
                }

                MsgNoCol = data.Columns.IndexOf("申請No");

                while (true)
                {
                    startRow++;
                    ReadExcel(sheetName, startRow, startCol, startCol + data.Columns.Count - 1, ref text);
                    if (string.IsNullOrEmpty(text[MsgNoCol]))
                    {
                        break;
                    }

                    DataRow dr = data.NewRow();

                    for (int i = 0; i < text.Length; i++)
                    {
                        dr[i] = text[i];
                    }

                    data.Rows.Add(dr);
                }

                if (data.Rows.Count > 0)
                {
                    readSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "sSeries" > 查询的机种名 </ param >
        ///<param name = "sModel" > 查询的Model名 </ param >
        ///<param name = "sUK" > 查询的UK内容 </ param >
        ///<param name = "sUSA" > 查询的USA内容 </ param >
        ///<param name = "sJPN" > 查询的JPN内容 </ param >
        ///<param name = "sJPE" > 查询的JPE内容 </ param >
        public bool SearchExcel(DataRow data, bool tpModel)
        {
            bool searchSucceed = false;

            try
            {
                excel.DisplayAlerts = false;
                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SheetSearch");
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                RunExcelMacro(new Object[] { CONST.EXCEL_MACRO_CLEAR }); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchSeriesAddress"), data[CONST.EXCEL_WORDING_SERIES].ToString()); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchModelAddress"), data[CONST.EXCEL_WORDING_MODEL].ToString()); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchUKAddress"), data[CONST.EXCEL_WORDING_UK].ToString()); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchUSAAddress"), data[CONST.EXCEL_WORDING_US].ToString()); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchJPNAddress"), data[CONST.EXCEL_WORDING_JPN].ToString()); Thread.Sleep(100);
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchJPEAddress"), data[CONST.EXCEL_WORDING_JPE].ToString()); Thread.Sleep(100);
                if (tpModel)
                {
                    if (WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchApplyHeightAddress"), data[CONST.EXCEL_WORDING_APPLYHEIGHT].ToString()) &&
                        WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchApplyWidthAddress"), data[CONST.EXCEL_WORDING_APPLYWIDTH].ToString()) &&
                        RunExcelMacro(new object[] { CONST.EXCEL_MACRO_SEARCH }))
                    {
                        searchSucceed = true;
                    }
                }
                else
                {
                    if (WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchApplyLineAddress"), data[CONST.EXCEL_WORDING_LINENUM].ToString()) &&
                        RunExcelMacro(new object[] { CONST.EXCEL_MACRO_SEARCH }))
                    {
                        searchSucceed = true;
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return searchSucceed;
        }

        ///<param name = "sSeries" > 查询的机种名 </ param >
        ///<param name = "sModel" > 查询的Model名 </ param >
        ///<param name = "sUK" > 查询的UK内容 </ param >
        ///<param name = "sUSA" > 查询的USA内容 </ param >
        ///<param name = "sJPN" > 查询的JPN内容 </ param >
        ///<param name = "sJPE" > 查询的JPE内容 </ param >
        public bool SearchExcel(string msgNo)
        {
            bool searchSucceed = false;

            try
            {
                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SheetSearch");
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                if (RunExcelMacro(new Object[] { CONST.EXCEL_MACRO_CLEAR }) &&
                    WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SearchMsgNoAddress"), msgNo) &&
                    RunExcelMacro(new object[] { CONST.EXCEL_MACRO_SEARCH }))
                {
                    searchSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return searchSucceed;
        }

        public bool Login()
        {
            bool loginSucceed = false;
            try
            {
                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SheetUser");
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                WriteExcel(sheetName, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "UserInputAddress"), IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "UserName"));
                if (RunExcelMacro(new object[] { CONST.EXCEL_MACRO_LOGIN }))
                {
                    loginSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return loginSucceed;
        }

        public bool ClearResult()
        {
            bool clearSucceed = false;
            try
            {
                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "SheetResult");
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                int startRow = Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "ResultStartLine"));
                InteropExcel.Range StartRange = (InteropExcel.Range)sheet.Cells[startRow+1, 1];
                InteropExcel.Range EndRange = (InteropExcel.Range)sheet.Cells[sheet.UsedRange.Rows.Count, sheet.UsedRange.Columns.Count];
                sheet.get_Range(StartRange, EndRange).Value2 = null;

                clearSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return clearSucceed;
        }

        public void ClearProcessingForm()
        {
            try
            {
                Microsoft.Vbe.Interop.VBComponent oModule = wbook.VBProject.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);
                string sCode =
                    "Option Explicit\r\n" +
                    "Public Sub DeleteProcessingForm()\r\n" +
                    "   With ThisWorkbook.VBProject.VBComponents(\"B1Sheets\").CodeModule\r\n" +
                    "       .DeleteLines .ProcStartLine(\"ProcessingForm\", 0), .ProcCountLines(\"ProcessingForm\", 0)\r\n" +
                    "   End With\r\n" +
                    "End Sub\r\n";
                oModule.CodeModule.AddFromString(sCode);
                oModule.Name = "hello";
                this.RunExcelMacro(new object[] { "DeleteProcessingForm" });
                sCode =
                    "Public Sub ProcessingForm(ByVal pShowFlg As Boolean)\r\n" +
                    "   If Not FProcessing Is Nothing Then\r\n" +
                    "       Unload FProcessing\r\n" +
                    "   End If\r\n" +
                    "End Sub\r\n";
                oModule.CodeModule.AddFromString(sCode);
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }
    }

    class FinalExcelHandler : InteropExcelHelper
    {
        public FinalExcelHandler(string fileName) : base(fileName) { }

        ///<param name = "data" > 读取的数据表 </ param >
        public bool ReadExcel(ref DataTable data)
        {
            bool readSucceed = false;

            try
            {
                int col = 1;
                int startCol = 0;
                int MsgNoCol = 0;
                string[] text = null;
                data = new DataTable();

                string sheetName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "FinalExcelData", "WorkSheetName");
                int startRow = Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "FinalExcelData", "TitleLine"));

                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);

                string cellValue = string.Empty;

                while (true)    //定位开始读取列
                {
                    ReadExcel(sheetName, startRow, col++, ref cellValue);
                    if (!string.IsNullOrEmpty(cellValue)) { break; }
                }
                startCol = --col;
                while (true)
                {
                    ReadExcel(sheetName, startRow, col++, ref cellValue);
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        break;
                    }
                    data.Columns.Add(cellValue);
                }

                MsgNoCol = data.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO);

                while (true)
                {
                    startRow++;
                    ReadExcel(sheetName, startRow, startCol, startCol + data.Columns.Count - 1, ref text);
                    if (string.IsNullOrEmpty(text[MsgNoCol]))
                    {
                        break;
                    }

                    DataRow dr = data.NewRow();

                    for (int i = 0; i < text.Length; i++)
                    {
                        dr[i] = text[i];
                    }

                    data.Rows.Add(dr);
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "titleRow" > 写入开头行名 </ param >
        ///<param name = "data" > 写入内容 </ param >
        ///<param name = "appendEnd" > 是否从最后开始写入内容 </ param >
        public bool WriteExcel(string sheetName, int titleRow, DataTable data, bool appendEnd, int NotNullCol, int borderLine)
        {
            bool writeSucceed = false;
            try
            {
                sheets = wbook.Worksheets;
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                int startCol = 0;
                int startRow = 0;
                int col = 1;
                string cellValue = string.Empty;

                while (true)    //定位开始写入列
                {
                    ReadExcel(sheetName, titleRow, col++, ref cellValue);
                    if (!string.IsNullOrEmpty(cellValue)) { break; }
                }
                startCol = --col;

                //定位开始写入行
                if (appendEnd)
                {
                    startRow = sheet.UsedRange.Rows.Count;
                }
                else
                {
                    startRow = titleRow;
                }

                int line;
                for (line = startRow; line >= 0; line--)
                {
                    InteropExcel.Range rng = (InteropExcel.Range)sheet.Cells[line, startCol + NotNullCol];
                    string text = rng.Text + "";
                    if (text != string.Empty)
                    {
                        break;
                    }
                }

                startRow = line + 1;

                //写入数据
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        if (data.Rows[i][j].ToString() != string.Empty)
                        {
                            WriteExcel(sheetName, i + startRow, startCol + j, data.Rows[i][j].ToString());
                        }
                        InteropExcel.Range rng = (InteropExcel.Range)sheet.Cells[i + startRow, startCol + borderLine];
                        rng.Borders.LineStyle = InteropExcel.XlLineStyle.xlContinuous;
                    }
                }
                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return writeSucceed;
        }
    }
}
