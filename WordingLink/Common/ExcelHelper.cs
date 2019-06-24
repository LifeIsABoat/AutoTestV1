using System;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InteropExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

namespace WordingLink
{
    public class NPOIExcelHelper
    {
        private IWorkbook workbook = null;
        private string fileName = string.Empty;

        public NPOIExcelHelper(string fileName)
        {
            this.fileName = fileName;
        }

        public bool OpenExcel()
        {
            bool openSucceed = false;

            try
            {
                FileStream fileStreamIn = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                GC.Collect();
                workbook = new XSSFWorkbook(fileStreamIn);
                fileStreamIn.Close();
                fileStreamIn = null;
                openSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return openSucceed;
        }

        public void CloseExcel()
        {
            try
            {
                if (workbook != null)
                {
                    workbook.Close();
                }
            }
            finally
            {
                workbook = null;
            }

        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "startRow" > 读取开始行 </ param >
        ///<param name = "endCol" > 读取截止列 </ param >
        ///<param name = "data" > 读取的数据表 </ param >
        public bool ReadExcel(string sheetName, int startRow, int endCol, ref DataTable data)
        {
            bool readSucceed = false;
            IRow row = null;
            ISheet sheet = null;

            try
            {
                data = new DataTable();
                sheet = workbook.GetSheet(sheetName);

                row = sheet.GetRow(startRow);
                for (int i = 0; i < endCol; i++)
                {
                    //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());  
                    //将第一列作为列表头  
                    data.Columns.Add(row.GetCell(i).ToString());
                }
                
                for (int j = startRow; j <= sheet.LastRowNum; j++)
                {
                    row = sheet.GetRow(j);
                    DataRow dr = data.NewRow();

                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        for (int k = 0; k < endCol; k++)
                        {
                            //读取该行的第j列数据
                            ICell cell = row.GetCell(k);
                            if (cell == null)
                            {
                                dr[k] = null;
                            }
                            else
                            {
                                dr[k] = cell.ToString();
                            }
                        }
                        data.Rows.Add(dr);
                    }
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                data = null;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "startRow" > 开始读取行 </ param >
        ///<param name = "data" > 读取的数据表 </ param >
        public bool ReadExcel(string sheetName, int startRow, ref DataTable data)
        {
            bool readSucceed = false;
            ISheet sheet = null;
            IRow row = null;
            string stringTemp = string.Empty;
            ICell cellTemp = null;

            try
            {
                data = new DataTable();
                sheet = workbook.GetSheet(sheetName);
                int endCol = sheet.GetRow(startRow).LastCellNum;

                for (int i = 0; i < endCol; i++)
                {
                    cellTemp = sheet.GetRow(startRow).GetCell(i);
                    if (cellTemp != null)
                    {
                        stringTemp = cellTemp.ToString();
                    }
                    else
                    {
                        stringTemp = string.Empty;
                    }
                    data.Columns.Add(stringTemp);
                }

                for (int j = startRow+1; j <= sheet.LastRowNum; j++)
                {
                    row = sheet.GetRow(j);
                    DataRow dr = data.NewRow();

                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        for (int k = 0; k < endCol; k++)
                        {
                            //读取该行的第j列数据
                            ICell cell = row.GetCell(k);
                            if (cell == null)
                            {
                                dr[k] = null;
                            }
                            else
                            {
                                dr[k] = cell.ToString();
                            }
                        }
                        data.Rows.Add(dr);
                    }
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                data = null;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "startRow" > 读取开始行 </ param >
        ///<param name = "endCol" > 读取截止列 </ param >
        ///<param name = "data" > 读取的多维数组 </ param >
        public bool ReadExcel(string sheetName, int startRow, ref object[,] data)
        {
            bool readSucceed = false;
            ISheet sheet = null;
            IRow row = null;

            try
            {
                sheet = workbook.GetSheet(sheetName);
                int endCol = sheet.GetRow(startRow).LastCellNum;
                data = new object[sheet.LastRowNum - startRow + 1, endCol];

                for (int j = startRow; j <= sheet.LastRowNum; j++)
                {
                    row = sheet.GetRow(j);

                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        for (int k = 0; k < endCol; k++)
                        {
                            //读取该行的第j列数据
                            ICell cell = row.GetCell(k);
                            if (cell == null)
                            {
                                data[j - startRow, k] = null;
                            }
                            else
                            {
                                data[j - startRow, k] = cell.ToString();
                            }
                        }
                    }
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                data = null;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "startRow" > 读取开始行 </ param >
        ///<param name = "endCol" > 读取截止列 </ param >
        ///<param name = "data" > 读取的多维数组 </ param >
        public bool ReadExcel(string sheetName, int startRow, int endCol, ref object[,] data)
        {
            bool readSucceed = false;
            IRow row = null;
            ISheet sheet = null;

            try
            {
                sheet = workbook.GetSheet(sheetName);

                data = new object[sheet.LastRowNum-startRow+1, endCol];

                for (int j = startRow; j <= sheet.LastRowNum; j++)
                {
                    row = sheet.GetRow(j);

                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        for (int k = 0; k < endCol; k++)
                        {
                            //读取该行的第j列数据
                            ICell cell = row.GetCell(k);
                            if (cell == null)
                            {
                                data[j-startRow,k] = null;
                            }
                            else
                            {
                                data[j-startRow, k] = cell.ToString();
                            }
                        }
                    }
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                data = null;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }

        ///<param name = "excelName" > Excel名 </ param >
        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "titleRow" > 写入Title行 </ param >
        ///<param name = "data" > 要写入的数据 </ param >
        public bool UpdateExcel(string excelName, string sheetName, int titleRow, DataTable data, bool appendEnd)
        {
            bool writeSucceed = false;
            IRow row = null;
            ISheet sheet = null;
            ICell cellTemp = null;
            FileStream fileStreamOut = null;
            int startCol = 0;
            int startRow = 0;

            try
            {
                sheet = workbook.GetSheet(sheetName);

                row = sheet.GetRow(titleRow);
                //定位开始列
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    cellTemp = row.GetCell(i);
                    if (cellTemp != null)
                    {
                        startCol = i;
                        break;
                    }
                }

                if (appendEnd)
                {
                    startRow = sheet.LastRowNum + 1;
                }
                else
                {
                    startRow = titleRow + 1;
                }

                for (int i= 0; i< data.Rows.Count; i++)
                {
                    row = sheet.CreateRow(startRow+i);
                    for(int j = 0; j < data.Columns.Count; j++)
                    {
                        cellTemp = row.CreateCell(j+startCol);
                        cellTemp.SetCellValue(data.Rows[i][j].ToString());
                    }
                }

                fileStreamOut = new FileStream(excelName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

                workbook.Write(fileStreamOut);
                fileStreamOut.Close();
                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return writeSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 读取行 </ param >
        ///<param name = "startCol" > 开始读取截止列 </ param >
        public bool GetStartCol(string sheetName, int row,ref int startCol)
        {
            bool readSucceed = false;
            IRow iRow = null;
            ISheet sheet = null;
            ICell cellTemp = null;

            try
            {
                sheet = workbook.GetSheet(sheetName);

                iRow = sheet.GetRow(row);
                //定位开始列
                for (int i = 0; i < iRow.LastCellNum; i++)
                {
                    cellTemp = iRow.GetCell(i);
                    if (cellTemp != null)
                    {
                        startCol = i;
                        break;
                    }
                }
                readSucceed = true;
            }
            catch (Exception e)
            {
                startCol = -1;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }
    }

    public class InteropExcelHelper
    {
        public InteropExcel.Application excel = null;
        public InteropExcel.Workbooks wbooks = null;
        public InteropExcel.Workbook wbook = null;
        public InteropExcel.Sheets sheets = null;
        public string fileName = string.Empty;

        public InteropExcelHelper(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// open file
        /// </summary>
        /// <returns></returns>
        public bool OpenExcel(bool macroEnable)
        {
            bool openSucceed = false;
            try
            {
                excel = new InteropExcel.Application();
                excel.Visible = false;
                excel.AlertBeforeOverwriting = false;
                excel.DisplayAlerts = false;
                excel.AskToUpdateLinks = false;
                MsoAutomationSecurity securityTemp = excel.AutomationSecurity;
                
                if (macroEnable)
                {
                    excel.AutomationSecurity = MsoAutomationSecurity.msoAutomationSecurityLow;
                }
                else
                {
                    excel.AutomationSecurity = MsoAutomationSecurity.msoAutomationSecurityForceDisable;
                }
                

                wbooks = excel.Workbooks;
                wbook = wbooks.Open(fileName);

                sheets = wbook.Worksheets;
                excel.AutomationSecurity = securityTemp;
                openSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return openSucceed;
        }

        /// <summary>
        /// save file
        /// </summary>
        /// <param name="param"></param>
        public void SaveExcel()
        {
            try
            {
                wbook.Save();
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        /// <summary>
        /// close file
        /// </summary>
        public void CloseExcel()
        {
            try
            {
                if (sheets != null)
                    Marshal.ReleaseComObject(sheets);
            }
            finally { sheets = null; }

            try  //release wbook
            {
                if (wbook != null)
                {
                    wbook.Close();
                    Marshal.ReleaseComObject(wbook);
                }
            } finally { wbook = null; }

            try //release wbooks 
            {
                if (wbooks != null)
                    Marshal.ReleaseComObject(wbooks);
            }
            finally { wbooks = null; }

            try //release excel
            {
                if (excel != null)
                {
                    excel.Quit();
                    Marshal.ReleaseComObject(excel);
                }
            }
            finally { excel = null; }

            GC.Collect();
        }

        /// <summary>
        /// Execution of the macro in Excel
        /// </summary>
        public bool RunExcelMacro(object[] parameters)
        {
            bool openSucceed = false;
            try
            {
                excel.DisplayAlerts = false;
                excel.GetType().InvokeMember(
                                                "Run",
                                                System.Reflection.BindingFlags.Default |
                                                System.Reflection.BindingFlags.InvokeMethod,
                                                null,
                                                excel,
                                                parameters
                                            );
                openSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return openSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 读取行 </ param >
        ///<param name = "col" > 读取列 </ param >
        ///<param name = "text" > 读取的数据 </ param >
        public bool ReadExcel(string sheetName, int row, int col, ref string text)
        {
            bool readSucceed = false;

            try
            {
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                InteropExcel.Range rng = (InteropExcel.Range)sheet.Cells[row, col];
                text = rng.Text + "";

                readSucceed = true;
            }
            catch (Exception e)
            {
                text = null;
                LogFile.WriteLog(e.Message);
            }
            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 读取行 </ param >
        ///<param name = "startCol" > 读取开始列 </ param >
        ///<param name = "endCol" > 读取结束列 </ param >
        ///<param name = "text" > 读取的数据 </ param >
        public bool ReadExcel(string sheetName, int row, int startCol, int endCol, ref string[] text)
        {
            bool readSucceed = false;
            try
            {
                text = new string[endCol-startCol+1];
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                InteropExcel.Range StartRange = (InteropExcel.Range)sheet.Cells[row, startCol];
                InteropExcel.Range EndRange = (InteropExcel.Range)sheet.Cells[row, endCol];
                var buffer = sheet.get_Range(StartRange, EndRange).Value2;
                for (int i = 1; i <= (endCol - startCol + 1); i++)
                    text[i - 1] = Convert.ToString(buffer[1, i]);
                readSucceed = true;
            }
            catch (Exception e)
            {
                text = null;
                LogFile.WriteLog(e.Message);
            }
            return readSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "address" > 写入地址 </ param >
        ///<param name = "text" > 写入内容 </ param >
        public bool WriteExcel(string sheetName, string address, string text)
        {
            bool writeSucceed = false;

            try
            {
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                InteropExcel.Range rng = sheet.get_Range(address);
                rng.Value2 = text;

                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return writeSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 写入行地址 </ param >
        ///<param name = "col" > 写入列地址 </ param >
        ///<param name = "text" > 写入内容 </ param >
        public bool WriteExcel(string sheetName, int row, int col, string text)
        {
            bool writeSucceed = false;
            try
            {
                excel.DisplayAlerts = false;
                sheets = wbook.Worksheets;
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                InteropExcel.Range rng = (InteropExcel.Range)sheet.Cells[row, col];
                rng.Borders.LineStyle = InteropExcel.XlLineStyle.xlContinuous;
                rng.Value2 = text;
                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return writeSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 写入行地址 </ param >
        ///<param name = "startCol" > 写入开始列 </ param >
        /// <param name = "endCol" > 写入截止列 </ param >
        ///<param name = "data" > 写入内容 </ param >
        public bool WriteExcel(string sheetName, int row, int startCol, int endCol, string[] data)
        {
            bool writeSucceed = false;
            try
            {
                sheets = wbook.Worksheets;
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                InteropExcel.Range StartRange = (InteropExcel.Range)sheet.Cells[row, startCol];
                InteropExcel.Range EndRange = (InteropExcel.Range)sheet.Cells[row, endCol];
                sheet.get_Range(StartRange, EndRange).Value2 = data;
                sheet.get_Range(StartRange, EndRange).Borders.LineStyle = InteropExcel.XlLineStyle.xlContinuous;
                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return writeSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "titleRow" > 写入开头行名 </ param >
        ///<param name = "data" > 写入内容 </ param >
        public bool WriteExcel(string sheetName, int titleRow, Dictionary<int[], String> data)
        {
            bool writeSucceed = false;
            try
            {
                sheets = wbook.Worksheets;
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                InteropExcel.Range rng = null;

                foreach (var itemDict in data)
                {
                    rng = (InteropExcel.Range)sheet.Cells[itemDict.Key[0], itemDict.Key[1]];
                    //rng.Borders.LineStyle = InteropExcel.XlLineStyle.xlContinuous;
                    if (rng.Value2 != null && rng.Value2 != "")
                    {
                        continue;
                    }
                    else
                    {
                        rng.Value2 = itemDict.Value;
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

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "titleRow" > 写入开头行名 </ param >
        ///<param name = "data" > 写入内容 </ param >
        ///<param name = "appendEnd" > 是否从最后开始写入内容 </ param >
        public bool WriteExcel(string sheetName, int titleRow, DataTable data, bool appendEnd)
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
                    startRow = sheet.UsedRange.Rows.Count + 1;
                }
                else
                {
                    startRow = titleRow + 1;
                }

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    //string[] rowString = new string[data.Columns.Count];
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        //rowString[j] = data.Rows[i][j].ToString();
                        if (data.Rows[i][j].ToString() != string.Empty)
                        {
                            WriteExcel(sheetName, i + startRow, startCol + j, data.Rows[i][j].ToString());
                        }
                    }
                    //WriteExcel(sheetName, i + startRow, startCol, startCol + data.Columns.Count, rowString);
                }
                writeSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return writeSucceed;
        }

        ///<param name = "sheetName" > Excel工作表名 </ param >
        ///<param name = "row" > 读取行 </ param >
        ///<param name = "startCol" > 开始读取截止列 </ param >
        public bool GetStartCol(string sheetName, int row, ref int startCol)
        {
            bool readSucceed = false;

            try
            {
                sheets = wbook.Worksheets;
                InteropExcel.Worksheet sheet = sheets.get_Item(sheetName);
                sheet.Activate();
                string cellValue = string.Empty;
                int col = 1;

                while (true)    //定位开始读取列
                {
                    ReadExcel(sheetName, row, col++, ref cellValue);
                    if (!string.IsNullOrEmpty(cellValue)) { break; }
                }
                startCol = --col;

                readSucceed = true;
            }
            catch (Exception e)
            {
                startCol = -1;
                LogFile.WriteLog(e.Message);
            }

            return readSucceed;
        }
    }
}
