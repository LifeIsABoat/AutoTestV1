using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    interface IExcelOperation
    {
        /* 
         *  Description:open Excel
         *  Param:filePath - file Path
         *  Return:bool
         *  Exception:
         *  Example:openExcel(filePath);
         */
        bool openExcel(string filePath);
        /* 
         *  Description:move To Sheet
         *  Param:sheetName - sheet Name
         *  Return:bool
         *  Exception:
         *  Example:moveToSheet(sheetName);
         */
        bool moveToSheet( string sheetName);
        /* 
         *  Description:get current Sheet Name
         *  Param:
         *  Return:string
         *  Exception:
         *  Example:getSheetName();
         */
        //string getSheetName();
        /* 
         *  Description:get All Sheet Name
         *  Param:
         *  Return: List<string>
         *  Exception:
         *  Example:getAllSheetName();
         */
        List<string> getAllSheetName();
        /* 
         *  Description:get Cell Color
         *  Param:row,col- coordinate
         *  Return: string
         *  Exception:
         *  Example:getCellColor(row, col);
         */
        string getCellColor(int row, int col);
        /* 
         *  Description:get Cell Value
         *  Param:row,col- coordinate
         *  Return: string
         *  Exception:
         *  Example:getCellValue(row, col);
         */
        string getCellValue( int row, int col);
        /* 
         *  Description:get cell Span
         *  Param:row,col- coordinate
         *  Return: Dictionary<string, int>
         *  Exception:
         *  Example:getSpan(row, col);
         */
        Dictionary<string, int> getSpan(int row,int col);
        /* 
         *  Description:get Cell Top Row
         *  Param:row,col- coordinate
         *  Return: int
         *  Exception:
         *  Example:getCellTopRow(row, col);
         */
        int getCellTopRow(int row,int col);
        /* 
         *  Description:close
         *  Param:
         *  Return: 
         *  Exception:
         *  Example:close();
         */
        void close();

    }
}
