using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace FTBExcel2Script
{
    class ExcelOperationNpoi : IExcelOperation
    {
        private FileStream fs = null;
        IWorkbook workBook = null;
        ISheet workSheet = null;
        public int count = 0;
        static MergeClas Merge = new MergeClas();
        public bool openExcel(string filePath)
        {
            bool flag = false;
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (filePath.IndexOf(".xlsm") > 0 || filePath.IndexOf(".xlsx") > 0 || filePath.IndexOf(".xls") > 0)
            {
                workBook = new XSSFWorkbook(fs);
                flag = true;
            }
            return flag;
        }
        //拿到所有的sheet名
        public List<string> getAllSheetName()
        {
            List<string> sheetName = new List<string>();
            string tempSheetName = "";
            for (int index = 0; index < workBook.NumberOfSheets; index++)
            {
                tempSheetName = workBook.GetSheetName(index);
                if (tempSheetName != "")
                {
                    sheetName.Add(tempSheetName);
                }
            }
            return sheetName;
        }
        //迁移到指定工作sheet
        public bool moveToSheet(string sheetName)
        {
            if (workBook != null)
            {
                workSheet = workBook.GetSheet(sheetName);
                if (workSheet == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("workBook is null");
            }
        }
        public string getSheetName()
        {
            if (workSheet == null)
            {
                return "";
            }
            else
            {
                return workSheet.SheetName;
            }
        }
        //获取当前cell的颜色
        public string getCellColor(int row, int col)
        {
            ICell cell = null;
            IRow irow = workSheet.GetRow(row);
            if (irow == null)
            {
                return "";
            }
            cell = (XSSFCell)irow.GetCell(col);
            if (cell.CellStyle.FillForegroundColor == IndexedColors.Automatic.Index)
            {
                return "Automatic";
            }
            else if (cell.CellStyle.FillForegroundColor == IndexedColors.Black.Index)
            {
                return "Black";
            }
            else if (cell.CellStyle.FillForegroundColor == IndexedColors.LightYellow.Index)
            {
                return "LightYellow";
            }
            else
            {
                return null;
            }
        }
        //获取当前cell的value
        public string getCellValue(int row, int col)
        {
            string str = "";
            ICell cell = null;
            IRow irow = workSheet.GetRow(row);
            if (irow == null)
            {
                return "";
            }
            cell = (XSSFCell)irow.GetCell(col);
            if (cell == null)
            {
                return "";
            }

            if (cell.CellType == CellType.Formula)
            {
                return cell.StringCellValue;
            }

            if ((cell.ToString() == "") && (cell.IsMergedCell))
            {
                Merge.GdeleValue += Merge.getCellValue;
                str = (string)MergedRegionAnalysis(row, col);
                Merge.GdeleValue -= Merge.getCellValue;
                return str;
            }
            else
            {
                return cell.ToString();
            }
        }
        //获取合并单元的长宽
        public Dictionary<string, int> getSpan(int row, int col)
        {
            ICell cell = null;
            Dictionary<string, int> span = new Dictionary<string, int>();
            cell = (XSSFCell)workSheet.GetRow(row).GetCell(col);
            count++;
            if (cell.IsMergedCell)
            {
                Merge.GdeleValue += Merge.getSpanValue;
                span = (Dictionary<string, int>)MergedRegionAnalysis(row, col);
                Merge.GdeleValue -= Merge.getSpanValue;
                return span;
            }
            else
            {
                span.Add("rowspan", 1);
                span.Add("colspan", 1);
                return span;
            }
        }
        //获取单元格顶部坐标
        public int getCellTopRow(int row, int col)
        {
            ICell cell = null;
            int leftTop = -1;
            IRow irow = workSheet.GetRow(row);
            if (null == irow)
            {
                return leftTop;
            }
            cell = (XSSFCell)irow.GetCell(col);
            if (cell.ToString() == "" && cell.IsMergedCell)
            {
                Merge.GdeleValue += Merge.getFirstRowValue;
                leftTop = (int)MergedRegionAnalysis(row, col);
                Merge.GdeleValue -= Merge.getFirstRowValue;
                return leftTop;
            }
            else
            {
                leftTop = row;
                return leftTop;
            }
        }
        public void close()
        {
            if (workBook != null)
            {
                workBook.Close();
            }
        }

        //
        private object MergedRegionAnalysis(int row, int col)
        {
            object temp;
            int regionsCount = workSheet.NumMergedRegions;
            for (int index = 0; index < regionsCount; index++)
            {
                CellRangeAddress range = workSheet.GetMergedRegion(index);
                if ((range.FirstRow <= row && row <= range.LastRow) && (range.FirstColumn <= col  && col <= range.LastColumn))
                {
                    temp = Merge.InvokeEvent(workSheet, range);
                    return temp;
                }
            }
            return null;
        }

    }
    ////定义有返回值的委托 
    public delegate object GenricDelegate(ISheet workSheet, CellRangeAddress range);

    public class MergeClas
    {
        //声明委托
        public GenricDelegate GdeleValue;
        public object getCellValue(ISheet workSheet, CellRangeAddress range)
        {
            ICell cell = (XSSFCell)workSheet.GetRow(range.FirstRow).GetCell(range.FirstColumn);
            return cell.ToString();
        }
        public object getSpanValue(ISheet workSheet, CellRangeAddress range)
        {
            Dictionary<string, int> span = new Dictionary<string, int>();
            span.Add("rowspan", range.LastRow - range.FirstRow + 1);
            span.Add("colspan", range.LastColumn - range.FirstColumn + 1);
            return span;
        }
        public object getFirstRowValue(ISheet workSheet, CellRangeAddress range)
        {
            return range.FirstRow;
        }

        public object InvokeEvent(ISheet workSheet, CellRangeAddress range)
        {
            if (GdeleValue != null)
            {
                //调用委托
                return GdeleValue(workSheet, range);
            }
            else
            {
                return null;
            }
        }
    }

}
