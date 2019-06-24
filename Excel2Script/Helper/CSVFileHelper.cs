using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace FTBExcel2Script
{
    public class CSVFileHelper
	{
        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fullPath">CSV的文件完整路径</param>
        /// <param name="encoding">CSV编码格式</param>
        /// <param name="utf8_WithBom">UTF8格式写文件时指定是否有BOM</param>
        public static void DataTable2CSV(DataTable dt, string fullPath, Encoding encoding, bool utf8_WithBom)
		{
			FileInfo fi = new FileInfo(fullPath);
			if (false == fi.Directory.Exists)
			{
				fi.Directory.Create();
			}

            if (true == File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            var encodInfo = Encoding.Default;
            if (encoding == Encoding.UTF8)
            {
                encodInfo = new UTF8Encoding(utf8_WithBom);
            }
            else
            {
                encodInfo = encoding;
            }
            
            FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs, encodInfo);
			string data = "";
			//写出列名称
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				data += dt.Columns[i].ColumnName.ToString();
				if (i < dt.Columns.Count - 1)
				{
					data += ",";
				}
			}
			sw.WriteLine(data);
			//写出各行数据
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				data = "";
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string str = dt.Rows[i][j].ToString();
					//str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
					//if (str.Contains(',') || str.Contains('"') 
					//	|| str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
					//{
					//	str = string.Format("\"{0}\"", str);
					//}

					data += str;
					if (j < dt.Columns.Count - 1)
					{
						data += ",";
					}
				}
				sw.WriteLine(data);
			}
			sw.Close();
			fs.Close();
		}

        /// <summary>
        /// 将DataTable中数据写入到Excel文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fullPath">Excel的文件完整路径</param>
        /// <param name="encoding">Excel编码格式</param>
        public static void DataTable2Excel(DataTable dt, string fullPath, Encoding encoding)
        {
            string strTempFullPath_CSV = fullPath.Replace(".xlsx", ".csv");
            DataTable2CSV(dt, strTempFullPath_CSV, encoding, true);
            if (true == File.Exists(strTempFullPath_CSV))
            {
                CSV2Excel(strTempFullPath_CSV);
                File.Delete(strTempFullPath_CSV);
            }
        }

        /// <summary>
        /// 将Excel文件转换为DataTable
        /// </summary>
        /// <param name="ExcelFilePath">Excel文件的完整路径</param>
        /// <param name="CsvFilePath">CSV文件的完整路径</param>
        /// <param name="encoding">CSV的编码</param>
        /// <param name="utf8_WithBom">是否有Bom头</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable Excel2DataTable(string ExcelFilePath)
        {
            string sheetName = "";
            bool isFirstRowColumn = true;
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            FileStream fs = new FileStream(ExcelFilePath, FileMode.Open, FileAccess.Read);
            //excel工作表
            NPOI.SS.UserModel.ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = new XSSFWorkbook(fs);
                if (0 >= workbook.NumberOfSheets)
                {
                    return null;
                }
                sheetName = workbook.GetSheetName(0);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;

                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// 将CSV文件的数据读取到DataTable中
		/// </summary>
		/// <param name="fileName">CSV文件路径</param>
		/// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable CSV2DataTable(string filePath)
		{
			Encoding encoding = GetType(filePath); //Encoding.ASCII;//
			DataTable dt = new DataTable();
			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			
			//StreamReader sr = new StreamReader(fs, Encoding.UTF8);
			StreamReader sr = new StreamReader(fs, encoding);
			//string fileContent = sr.ReadToEnd();
			//encoding = sr.CurrentEncoding;
			//记录每次读取的一行记录
			string strLine = "";
			//记录每行记录中的各字段内容
			string[] aryLine = null;
			string[] tableHead = null;
			//标示列数
			int columnCount = 0;
			//标示是否是读取的第一行
			bool IsFirst = true;
			//逐行读取CSV中的数据
			while ((strLine = sr.ReadLine()) != null)
			{
				if (IsFirst == true)
				{
					tableHead = strLine.Split(',');
					IsFirst = false;
					columnCount = tableHead.Length;
					//创建列
					for (int i = 0; i < columnCount; i++)
					{
						DataColumn dc = new DataColumn(tableHead[i]);
						dt.Columns.Add(dc);
					}
				}
				else
				{
					aryLine = strLine.Split(',');
					DataRow dr = dt.NewRow();
					for (int j = 0; j < columnCount; j++)
					{
						dr[j] = aryLine[j];
					}
					dt.Rows.Add(dr);
				}
			}
			if (aryLine != null && aryLine.Length > 0)
			{
				dt.DefaultView.Sort = tableHead[0] + " " + "asc";
			}
			
			sr.Close();
			fs.Close();
			return dt;
		}
		
		/// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
		/// <param name="FILE_NAME">文件路径</param>
		/// <returns>文件的编码类型</returns>
		 
		public static Encoding GetType(string FILE_NAME)
		{
			FileStream fs = new FileStream(FILE_NAME, FileMode.Open,FileAccess.Read);
			Encoding r = GetType(fs);
			fs.Close();
			return r;
		}
		 
		/// 通过给定的文件流，判断文件的编码类型
		/// <param name="fs">文件流</param>
		/// <returns>文件的编码类型</returns>
		public static Encoding GetType(FileStream fs)
		{
			byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
			byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
			byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
			Encoding reVal = Encoding.Default;
		 
			BinaryReader r = new BinaryReader(fs, Encoding.Default);
			int i;
			int.TryParse(fs.Length.ToString(), out i);
			byte[] ss = r.ReadBytes(i);
			if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
			{
				reVal = Encoding.UTF8;
			}
			else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
			{
				reVal = Encoding.BigEndianUnicode;
			}
			else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
			{
				reVal = Encoding.Unicode;
			}
			r.Close();
			return reVal;
		}
		 
		/// 判断是否是不带 BOM 的 UTF8 格式
		/// <param name="data"></param>
		/// <returns></returns>
		private static bool IsUTF8Bytes(byte[] data)
		{
			int charByteCounter = 1;　 //计算当前正分析的字符应还有的字节数
			byte curByte; //当前分析的字节.
			for (int i = 0; i < data.Length; i++)
			{
				curByte = data[i];
				if (charByteCounter == 1)
				{
					if (curByte >= 0x80)
					{
						//判断当前
						while (((curByte <<= 1) & 0x80) != 0)
						{
							charByteCounter++;
						}
						//标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
						if (charByteCounter == 1 || charByteCounter > 6)
						{
							return false;
						}
					}
				}
				else
				{
					//若是UTF-8 此时第一位必须为1
					if ((curByte & 0xC0) != 0x80)
					{
						return false;
					}
					charByteCounter--;
				}
			}
			if (charByteCounter > 1)
			{
				throw new Exception("非预期的byte格式");
			}
			return true;
		}

        /// <summary>
        /// 将Csv文件转换为XLS文件
        /// </summary>
        /// <param name="FilePath">文件全路路径</param>
        /// <returns>返回转换后的Xls文件名</returns>
        public static string CSV2Excel(string FilePath)
        {
            //QuertExcel();
            string _NewFilePath = "";

            Excel.Application excelApplication;
            Excel.Workbooks excelWorkBooks = null;
            Excel.Workbook excelWorkBook = null;
            Excel.Worksheet excelWorkSheet = null;

            try
            {
                excelApplication = new Excel.Application();
                excelWorkBooks = excelApplication.Workbooks;
                excelWorkBook = ((Excel.Workbook)excelWorkBooks.Open(FilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets[1];
                excelApplication.Visible = false;
                excelApplication.DisplayAlerts = false;
                _NewFilePath = FilePath.Replace(".csv", ".xlsx");
                excelWorkBook.SaveAs(_NewFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close();
                //QuertExcel();
                // ExcelFormatHelper.DeleteFile(FilePath);
                //可以不用杀掉进程QuertExcel();


                GC.Collect(System.GC.GetGeneration(excelWorkSheet));
                GC.Collect(System.GC.GetGeneration(excelWorkBook));
                GC.Collect(System.GC.GetGeneration(excelApplication));

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

            finally
            {
                GC.Collect();
            }

            return _NewFilePath;
        }



        /// <summary>
        /// 将xls文件转换为csv文件
        /// </summary>
        /// <param name="FilePath">文件全路路径</param>
        /// <returns>返回转换后的csv文件名</returns>
        public static string Excel2CSV(string FilePath)
        {
            QuertExcel();
            string _NewFilePath = "";

            Excel.Application excelApplication;
            Excel.Workbooks excelWorkBooks = null;
            Excel.Workbook excelWorkBook = null;
            Excel.Worksheet excelWorkSheet = null;

            try
            {
                excelApplication = new Excel.Application();
                excelWorkBooks = excelApplication.Workbooks;
                excelWorkBook = ((Excel.Workbook)excelWorkBooks.Open(FilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets[1];
                excelApplication.Visible = false;
                excelApplication.DisplayAlerts = false;
                _NewFilePath = FilePath.Replace(".xlsx", ".csv");

                excelWorkBook.SaveAs(_NewFilePath, Excel.XlFileFormat.xlCSV, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                QuertExcel();
                //ExcelFormatHelper.DeleteFile(FilePath);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
            return _NewFilePath;
        }



        /// <summary>
        /// 删除一个指定的文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        private static bool DeleteFile(string FilePath)
        {
            try
            {
                bool IsFind = File.Exists(FilePath);
                if (IsFind)
                {
                    File.Delete(FilePath);
                }
                else
                {
                    throw new IOException("指定的文件不存在");
                }
                return true;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

        }


        /// <summary>
        /// 执行过程中可能会打开多个EXCEL文件 所以杀掉
        /// </summary>
        private static void QuertExcel()
        {
            Process[] excels = Process.GetProcessesByName("EXCEL");
            foreach (var item in excels)
            {
                item.Kill();
            }
        }

        /// <summary>
        /// 将Excel文件转换为DataTable,基于SheetIndex
        /// </summary>
        /// <param name="ExcelFilePath">Excel文件的完整路径</param>
        /// <param name="sheetIndex">SheetIndex</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable excelToDataTableBasedSheetIndex(string ExcelFilePath, int sheetIndex)
        {
            string sheetName = "";
            bool isFirstRowColumn = true;
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            FileStream fs = new FileStream(ExcelFilePath, FileMode.Open, FileAccess.Read);
            //excel工作表
            NPOI.SS.UserModel.ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = new XSSFWorkbook(fs);
                if (0 >= workbook.NumberOfSheets)
                {
                    return null;
                }
                sheetName = workbook.GetSheetName(sheetIndex);
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(sheetIndex);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(sheetIndex);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;

                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
