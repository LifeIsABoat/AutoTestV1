using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Drawing;

namespace ColorDivisionTool.Operation
{
    public class Column
    {
        public int start { get; set; }
        public int end { get; set; }
        public Column(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }
    public class ExcelHelper
    {
        private Application excel = null;
        private Workbooks wbooks = null;
        private Workbook wbook = null;
        private Sheets sheets = null;
        private Worksheet sheet = null;
        private Range rng = null;
        private int threadFinishCheck = 0;
        private double[,] tmpColors = null;
        private int startColumn = 0;
        private int endColumn = 0;
        private int rowNum = 0;
        private int rowSeed = 0;
        private int colSeed = 0;

        private int countRow = 0;
        private string path;

        #region Constructor and Deconstructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path"></param>
        public ExcelHelper(string path)
        {
            this.path = path;
        }

        #endregion

        /// <summary>
        /// open file
        /// </summary>
        /// <returns></returns>
        public bool OpenFile()
        {
            bool openSucceed = false;
            try
            {
                excel = new Application();
                excel.Visible = false;

                wbooks = excel.Workbooks;
                excel.AlertBeforeOverwriting = false;
                excel.DisplayAlerts = false;

                //open file
                wbook = wbooks.Open(path);
                if (wbook.ReadOnly)
                {
                    System.Windows.MessageBox.Show("The file has been opened!");
                    CloseFile();
                    return false;
                }

                excel.DisplayAlerts = true;

                openSucceed = true;
            }
            catch
            {
                openSucceed = false;
            }
            return openSucceed;
        }

        /// <summary>
        /// check sheet name whether it is current sheet name
        /// if not, Jump to excelSheetName sheet
        /// </summary>
        /// <param name="excelSheetName"></param>
        /// <returns>
        /// Bool
        /// </returns>
        public bool CheckSheetName(string excelSheetName)
        {
            bool sheetValue = false;
            try
            {
                sheets = wbook.Worksheets;
                sheet = sheets.get_Item(excelSheetName);
                sheetValue = true;
            }
            catch
            {
                sheetValue = false;
            }
            return sheetValue;
        }

        /// <summary>
        /// get Excel data start Row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="titles"></param>
        /// <returns></returns>
        public int GetTitleRow(int row, int col, string aTitle)
        {
            while (row < 50)
            {
                row++;
                string cellValue = string.Empty;
                ReadExcel(row, col, ref cellValue);
                if (string.IsNullOrEmpty(cellValue))
                {
                    continue;
                }
                if (!cellValue.Contains(aTitle))
                {
                    continue;
                }
                break;
            }

            if (row >= 50)
            {
                return -1;
            }
            row++;
            return row;
        }

        /// <summary>
        /// Get the number of columns in a table
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetTitleEndCol(int row, int col)
        {
            while (true)
            {
                string cellValue = string.Empty;
                ReadExcel(row, col, ref cellValue);
                if (string.IsNullOrEmpty(cellValue))
                {
                    break;
                }
                col++;
            }
            return col - 1;
        }

        /// <summary>
        /// Get End Row Count
        /// </summary>
        /// <returns></returns>
        public int GetEndRow(int row, int col)
        {
            while (true)
            {
                string cellValue = string.Empty;
                ReadExcel(row, col, ref cellValue);
                if (string.IsNullOrEmpty(cellValue))
                {
                    break;
                }
                row++;
            }
            return row - 1;
        }

        /// <summary>
        /// read cell Value from excel
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="textValue"></param>
        public void ReadExcel(int row, int col, ref string textValue)
        {
            try
            {
                rng = (Range)sheet.Cells[row, col];
                textValue = rng.Text;
            }
            catch
            {
                textValue = null;
            }
        }

        /// <summary>
        /// read cell Value from excel
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="textValue"></param>
        public string ReadExcel(int row, int col)
        {
            try
            {
                rng = (Range)sheet.Cells[row, col];
                return rng.Text;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a column of data from excel
        /// </summary>
        /// <param name="row"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <param name="text"></param>
        public void ReadExcel(int row, int startCol, int endCol, ref string[] text)
        {
            try
            {
                Range startRange = (Range)sheet.Cells[row, startCol];
                Range endRange = (Range)sheet.Cells[row, endCol];

                var buffer = sheet.get_Range(startRange, endRange).Value2;
                for (int i = 1; i <= (endCol - startCol + 1); i++)
                    text[i - 1] = Convert.ToString(buffer[1, i]);
            }
            catch
            {
                text = null;
            }
        }

        /// <summary>
        /// Read a range of data from excel
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool ReadExcel(int startRow, int endRow, int startCol, int endCol, ref object[,] text)
        {
            try
            {
                Range startRange = (Range)sheet.Cells[startRow, startCol];
                Range endRange = (Range)sheet.Cells[endRow, endCol];
                //cell-type text value
                text = sheet.get_Range(startRange, endRange).Value2;
                return true;
            }
            catch
            {
                text = null;
                return false;
            }
        }

        /// <summary>
        /// Read a column of data from excel
        /// </summary>
        /// <param name="row"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <param name="textList"></param>
        public void ReadExcel(int row, int startCol, int endCol, ref List<string> textList)
        {
            try
            {
                Range startRange = (Range)sheet.Cells[row, startCol];
                Range endRange = (Range)sheet.Cells[row, endCol];

                var buffer = sheet.get_Range(startRange, endRange).Value2;
                for (int i = 1; i <= (endCol - startCol + 1); i++)
                {
                    textList.Add(Convert.ToString(buffer[1, i]));
                }
            }
            catch
            {
                textList = null;
            }
        }

        /// <summary>
        /// Read a column of colors data from excel
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="col"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        public bool GetColorIndex(int startRow, int endRow, int col, ref double[] colors)
        {
            try
            {
                List<double> tmpColors = new List<double>();
                for (int row = startRow; row <= endRow; row++)
                {
                    tmpColors.Add(((Range)sheet.Cells[row, col]).Interior.Color);
                }
                colors = tmpColors.ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取单元格文字颜色，返回代表一个颜色的double值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public double GetFontColor(int row, int col)
        {
            rng = (Range)sheet.Cells[row, col];
            string text = rng.Text;
            double tempValue = rng.Interior.Color;
            if (tempValue == ConfigConst.COLOR_ORANGE || tempValue == ConfigConst.COLOR_GOLDEN)
            {
                int textLength = 0;
                if (text.Contains("\n"))
                {
                    string[] arr = text.Split(new string[] { "\n" }, StringSplitOptions.None);
                    foreach (string txt in arr)
                    {
                        if (txt.Length == 0) { textLength += 1; continue; }
                        textLength += txt.Length;
                        if (rng.Characters[textLength, 1].Font.Color == ConfigConst.COLOR_RED)
                        {//检查文字颜色是否有红色，如果有则记录下来
                            return rng.Characters[textLength, 1].Font.Color;
                        }
                        textLength += 1;
                    }
                    return 0;
                }
                else
                {
                    return rng.Characters[text.Length, 1].Font.Color;
                }
            }
            else
            {
                //申请行数超出的时候
                if (tempValue == ConfigConst.COLOR_AQUAGREEN)
                {
                    return ConfigConst.COLOR_RED;
                }
                return 0;
            }
        }

        private void search(object column)
        {
            Column newColumn = (Column)column;
            int start = newColumn.start;
            int end = newColumn.end;
            int mid = (start + end) / 2;
            int temp;
            if (start == end)
            {
                try
                {
                    temp = (int)(sheet.Cells[rowNum, start].Font.Color);
                    tmpColors[countRow, start - 1] = temp;
                    threadFinishCheck++;
                    return;
                }
                catch
                {
                    temp = (int)GetFontColor(rowNum, start);
                    tmpColors[countRow, start - 1] = temp;
                    threadFinishCheck++;
                    return;
                }
            }
            else
            {
                try
                {
                    temp = (int)(sheet.get_Range((Range)sheet.Cells[rowNum, start], (Range)sheet.Cells[rowNum, end]).Font.Color);
                    for (int j = start; j <= end; j++)
                    {
                        tmpColors[countRow, start - 1] = temp;
                    }
                    threadFinishCheck += (end - start + 1);
                    return;
                }
                catch
                {
                    Column column_1 = new Column(start, mid);
                    ThreadPool.QueueUserWorkItem(search, column_1);
                    mid = mid + 1;
                    if (mid <= end)
                    {
                        Column column_2 = new Column(mid, end);
                        ThreadPool.QueueUserWorkItem(search, column_2);
                    }
                }
            }
        }
        /// <summary>
        /// 获取Excel的背景色
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <param name="wordColors"></param>
        /// <returns></returns>
        public bool GetColorIndex(int startRow, int endRow, int startCol, int endCol, ref double[,] wordColors)
        {
            try
            {
                var rr = sheet.Cells[15, 26].Font.Color;
                var tq = sheet.get_Range((Range)sheet.Cells[15, 26], (Range)sheet.Cells[15, 27]).Font.Color;
                var tt = sheet.get_Range((Range)sheet.Cells[15, 26], (Range)sheet.Cells[15, 26]).Font.Color;
                var ta = sheet.get_Range((Range)sheet.Cells[startRow+1, 19], (Range)sheet.Cells[startRow+1, 20]).Font.Color;

                countRow = 0;
                startColumn = startCol;
                endColumn = endCol;
                tmpColors = new double[endRow - startRow + 1, endColumn];
                for (int row = startRow; row <= endRow; row++)
                {
                    DateTime dt1 = DateTime.Now;
                    rowNum = row;
                    threadFinishCheck = 0;
                    ThreadPool.SetMaxThreads(32, 32);
                    Column column = new Column(startCol, endCol);
                    ThreadPool.QueueUserWorkItem(search, column);
                    while (threadFinishCheck != column.end - column.start + 1)
                    {
                        Thread.Sleep(15);
                    }
                    wordColors = (double[,])tmpColors.Clone();
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts = dt2.Subtract(dt1);
                    Console.WriteLine("time {0} ms/line", ts.TotalMilliseconds);
                    countRow++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void Calculate(object threadNum)
        {
            int i = (int)threadNum + 1;
            int countCol = startColumn - 1;
            for (int col = (startColumn + (11 * (i - 1))); col <= ((startColumn + 10) + (11 * (i - 1))); col++)
            {
                int a = countCol++;
                get(rowNum, col, a);
            }
        }
        protected void get(int row, int col, int countCol)
        {
            tmpColors[countRow, countCol] = GetFontColor(row, col);
        }

        public bool ColorIndex(int startRow, int endRow, int startCol, int endCol, ref double[,] wordColors)
        {
            try
            {
                int countRow = 0;
                for (int row = startRow; row <= endRow; row++)
                {
                    DateTime dt1 = DateTime.Now;
                    int countCol = startCol - 1;
                    for (int col = startCol; col <= endCol; col++)
                    {
                        wordColors[countRow, countCol++] = GetFontColor(row, col);
                    }
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts = dt2.Subtract(dt1);
                    Console.WriteLine("time {0} ms/line", ts.TotalMilliseconds);
                    countRow++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get cell color
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetColorIndex(int row, int col)
        {
            int colorIndex = -1;

            try
            {
                rng = (Range)sheet.Cells[row, col];
                colorIndex = rng.Interior.Color;
            }
            catch
            {
            }

            return colorIndex;
        }

        /// <summary>
        /// write a column of color data to excel
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="col"></param>
        /// <param name="colorIndexData"></param>
        public void SetColorIndex(int startRow, int endRow, int col, double[] colorIndexData)
        {
            try
            {
                int count = 0;
                for (int row = startRow; row <= endRow; row++)
                {
                    rng = (Range)sheet.Cells[row, col];
                    rng.Interior.Color = colorIndexData[count++];
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// write a column of cell values to excel
        /// </summary>
        /// <param name="row"></param>
        /// <param name="startCol"></param>
        /// <param name="endCol"></param>
        /// <param name="data"></param>
        /// <param name="writeFlg"></param>
        /// <returns></returns>
        public bool WriteExcel(int row, int startCol, int endCol, string[] data, bool writeFlg = false)
        {
            bool writeSucceed = false;
            try
            {
                if (writeFlg)
                {
                    //Insert Row
                    string msgNoValue = sheet.Cells[row, 1].Value2;
                    if (msgNoValue == string.Empty || msgNoValue == null)
                    {
                        Range range = (Range)sheet.Rows[row];
                        range.Insert(XlInsertShiftDirection.xlShiftDown);
                    }
                }
                Range StartRange = (Range)sheet.Cells[row, startCol];
                Range EndRange = (Range)sheet.Cells[row, endCol];

                sheet.get_Range(StartRange, EndRange).Value2 = data;

                writeSucceed = true;
            }
            catch
            {
                writeSucceed = false;
            }
            return writeSucceed;
        }

        /// <summary>
        /// save Excel File
        /// </summary>
        public void SaveExcelFile()
        {
            try
            {
                excel.DisplayAlerts = false;
                wbook.SaveAs(path);
                excel.DisplayAlerts = true;
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        /// <summary>
        /// close file
        /// </summary>
        /// <returns>
        /// false:close failed
        /// true: close succeed
        ///  </returns>
        public bool CloseFile()
        {
            bool closeSucceed = false;
            try
            {
                excel.AlertBeforeOverwriting = false;
                excel.DisplayAlerts = false;

                ReleaseAllObject();
                closeSucceed = true;
            }
            //Close File Failed
            catch
            {
                closeSucceed = false;
            }
            return closeSucceed;
        }

        /// <summary>
        /// release all resource
        /// </summary>
        private void ReleaseAllObject()
        {
            try
            {   //release rng
                if (rng != null)
                    Marshal.ReleaseComObject(rng);
            }
            finally
            {
                rng = null;
            }
            try
            {   //release sheet
                if (sheet != null)
                    Marshal.ReleaseComObject(sheet);
            }
            finally
            {
                sheet = null;
            }
            try
            {   //release sheets
                if (sheets != null)
                    Marshal.ReleaseComObject(sheets);
            }
            finally
            {
                sheets = null;
            }
            try  //release wbook
            {
                if (wbook != null)
                {
                    wbook.Close();
                    Marshal.ReleaseComObject(wbook);
                }
            }
            finally
            {
                wbook = null;
            }
            try //release wbooks 
            {
                if (wbooks != null)
                    Marshal.ReleaseComObject(wbooks);
            }
            finally
            {
                wbooks = null;
            }
            try //release excel
            {
                if (excel != null)
                {
                    excel.Quit();
                    Marshal.ReleaseComObject(excel);
                }
            }
            finally
            {
                excel = null;
            }
            GC.Collect();
        }
    }
}
