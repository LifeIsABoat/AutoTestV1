using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using Tool.DAL;
using Tool.BLL;
using System.Reflection;
using Tool.UI;
using NPOI.HSSF.UserModel;

namespace Tool.Report
{
    class CreateReportExcleNpoi : ICreateReport
    {
        private IWorkbook workbook = null;
        ISheet TcworkSheet = null;
        ISheet ScreenworkSheet = null;
        ICell cell = null;
        int mainPartStartRow = 0, screenMainStartrow = 0;
        int optionIndex = 0, optionRow = 0, okCount = 0, ngCount = 0, naCount = 0, tcCount = 0, outsideCount = 0, rowCount = 0, baseRow = 0, screenRowCount = 0, screenRow = 0;
        int opinionStsartCol = 0, optionSrartCol = 0, screenOprionStartCol = 0;
        int screenOkCount = 0, screenNgCount = 0, screenNaCount = 0, screenOutSideCount = 0;
        string allResult = "";
        int allResultFlag = -1;
        int optionCount;
        int colCount = 0;
        Dictionary<string, string> baseInfo;
        List<string> usWords, condition;
        List<int> excleOptionIndex;
        Dictionary<int, BLL.TestCheckResult> opinionResult;
        private object GlobalConfig;

        public CreateReportExcleNpoi()
        {
            screenMainStartrow = 22;
            baseRow = 1;
            screenOprionStartCol = 11;
            opinionStsartCol = 29;
            optionSrartCol = 21;
            mainPartStartRow = 24;
        }

        private void addInfo()
        {
            baseInfo = new Dictionary<string, string>();
            usWords = new List<string>();
            condition = new List<string>();
            opinionResult = new Dictionary<int, BLL.TestCheckResult>();
            //TransferImageLink
            baseInfo.Add("Firmware Ver", "XXX");
            baseInfo.Add("TC Ver", "XXX");
            baseInfo.Add("Tool Ver", "XXX");
            baseInfo.Add("Machine Type Select", "XXX");
            baseInfo.Add("Model", "XXX");
            baseInfo.Add("Continent", "XXX");
            baseInfo.Add("Country", "XXX");
            baseInfo.Add("Language", "XXX");
            baseInfo.Add("テストPC", "XXX");
            baseInfo.Add("実施者", "XXX");
            baseInfo.Add("実施日", "XXX");
            baseInfo.Add("総条数", "XXX");
            baseInfo.Add("OK数", "XXX");
            baseInfo.Add("NG数", "XXX");
            baseInfo.Add("NT数", "XXX");
            baseInfo.Add("NA数", "XXX");
            baseInfo.Add("測試消費時間", "XXX");
        }

        /* 
         *  Description:insert col
         *  Param:row-row index
         *  Param:col-col index
         *  Return:
         *  Exception:
         *  Example:insertCol(1, 1)
         */
        private void insertCol(int row, int col, ISheet sheet)
        {
            ICell cell1 = sheet.GetRow(row).GetCell(col - 1);
            ICellStyle style = workbook.CreateCellStyle() as XSSFCellStyle;
            cell = sheet.GetRow(row).CreateCell(col);
            style = cell1.CellStyle;
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            cell.CellStyle = style;
        }
        /* 
         *  Description:merge same value cell 
         *  Param:row-row index
         *  Param:col-col index
         *  Return:
         *  Exception:
         *  Example:checkMerge(1, 1)
         */
        private bool checkMerge(int row, int col)
        {
            ICell tempCell = null;
            cell = (XSSFCell)TcworkSheet.GetRow(row).GetCell(col);
            tempCell = (XSSFCell)TcworkSheet.GetRow(row - 1).GetCell(col);
            if (tempCell.ToString() != cell.ToString())
            {
                return true;
            }
            else
            {
                cell = (XSSFCell)TcworkSheet.GetRow(row).GetCell(col - 1);
                tempCell = (XSSFCell)TcworkSheet.GetRow(row - 1).GetCell(col - 1);
                if (tempCell.ToString() != cell.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //在饼状图后记录数据
        private void setBaseInfo(DAL.IFTBCommonAPI treeMemory, ISheet sheetName)
        {
            //获取版本号
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            GlobalConfig = version.ToString();

            FTBTestForm testForm = new FTBTestForm(null);

            int basrStartRow = 11, basrStartCol = 9;
            baseInfo["実施日"] = BLL.TestRuntimeAggregate.getCurrentTime();
            baseInfo["Model"] = treeMemory.getSelectModel();
            baseInfo["Continent"] = treeMemory.getSelectContinent();
            baseInfo["Country"] = treeMemory.getSelectCountry();
            baseInfo["Tool Ver"] = "V" + GlobalConfig as string;
            baseInfo["テストPC"] = System.Environment.MachineName;
            baseInfo["実施者"] = testForm.getUserName();
            baseInfo["Language"] = testForm.getLanguage();
            baseInfo["Machine Type Select"] = testForm.getMachineType();
            if (sheetName == TcworkSheet)
            {
                cell = (XSSFCell)sheetName.GetRow(basrStartRow).CreateCell(basrStartCol);
                cell.SetCellValue(outsideCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 1).CreateCell(basrStartCol);
                cell.SetCellValue((okCount + naCount + ngCount));
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 3).CreateCell(basrStartCol);
                cell.SetCellValue(okCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 4).CreateCell(basrStartCol);
                cell.SetCellValue(ngCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 5).CreateCell(basrStartCol);
                cell.SetCellValue(naCount);
            }
            else
            {
                cell = (XSSFCell)sheetName.GetRow(basrStartRow).CreateCell(basrStartCol);
                cell.SetCellValue(screenOutSideCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 1).CreateCell(basrStartCol);
                cell.SetCellValue((screenOkCount + screenNgCount + screenNaCount));
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 3).CreateCell(basrStartCol);
                cell.SetCellValue(screenOkCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 4).CreateCell(basrStartCol);
                cell.SetCellValue(screenNgCount);
                cell = (XSSFCell)sheetName.GetRow(basrStartRow + 5).CreateCell(basrStartCol);
                cell.SetCellValue(screenNaCount);
            }
        }
        /* 
         *  Description:merge cell 
         *  Param:row-row index
         *  Param:col-col index
         *  Return:
         *  Exception:
         *  Example:mergeCell(1, 1)
         */
        private void mergeCell(int startRow, int startCol, int endRow, int endCol, ISheet sheet)
        {
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(startRow, endRow, startCol, endCol));
        }

        /* 
         *  Description:insert Row
         *  Param:starRow-start row index
         *  Param:rows-add row count
         *  Param:optionStyle-is option cell
         *  Return:
         *  Exception:
         *  Example:insertRow(20,5,true)
         */
        private void insertRow(int starRow, int rows, bool optionStyle, ISheet sheet)
        {
            if (starRow + 1 > sheet.LastRowNum)
            {
                sheet.CreateRow(sheet.LastRowNum + 1);
            }
            sheet.ShiftRows(starRow + 1, sheet.LastRowNum, rows, true, true);
            for (int i = 0; i < rows; i++)
            {
                XSSFRow sourceRow = null;
                XSSFRow targetRow = null;
                XSSFCell sourceCell = null;
                XSSFCell targetCell = null;
                sourceRow = (XSSFRow)sheet.GetRow(starRow);
                targetRow = (XSSFRow)sheet.CreateRow(starRow + i + 1);
                targetRow.HeightInPoints = sourceRow.HeightInPoints;
                for (short insertCol = (short)sourceRow.FirstCellNum; insertCol < sourceRow.LastCellNum; insertCol++)
                {
                    sourceCell = (XSSFCell)sourceRow.GetCell(insertCol);
                    targetCell = (XSSFCell)targetRow.CreateCell(insertCol);
                    //if option part
                    if (optionStyle)
                    {
                        if (insertCol != (short)sourceRow.FirstCellNum)
                        {
                            targetCell.CellStyle.CloneStyleFrom(sourceCell.CellStyle);
                            targetCell.CellStyle.BorderTop = BorderStyle.Thin;
                            targetCell.CellStyle.BorderRight = BorderStyle.Thin;
                            targetCell.CellStyle.BorderLeft = BorderStyle.Thin;
                            targetCell.CellStyle.BorderBottom = BorderStyle.Thin;
                        }
                        else
                        {
                            targetCell.CellStyle = sourceCell.CellStyle;
                            targetCell.CellStyle.BorderTop = BorderStyle.Thin;
                            targetCell.CellStyle.BorderRight = BorderStyle.Thin;
                            targetCell.CellStyle.BorderLeft = BorderStyle.Thin;
                            targetCell.CellStyle.BorderBottom = BorderStyle.Thin;
                        }
                    }
                    else
                    {
                        targetCell.CellStyle.CloneStyleFrom(sourceCell.CellStyle);
                        targetCell.CellStyle.BorderTop = BorderStyle.Thin;
                        targetCell.CellStyle.BorderRight = BorderStyle.Thin;
                        targetCell.CellStyle.BorderLeft = BorderStyle.Thin;
                        targetCell.CellStyle.BorderBottom = BorderStyle.Thin;
                        targetCell.CellStyle.Alignment = HorizontalAlignment.Left;
                        targetCell.CellStyle.VerticalAlignment = VerticalAlignment.Top;
                        targetRow.Height = 20 * 20;
                    }
                }
            }
        }

        /* 
         *  Description:write Level
         *  Param:usWords-usword
         *  Param:condition-condition
         *  Return:
         *  Exception:
         *  Example:writeLevel("usword","condition")
         */
        public void writeLevel(DAL.IFTBCommonAPI treeMemory)
        {
            string condition = treeMemory.getTotalCondition(treeMemory.getLevelCondition());
            string ukWords = treeMemory.getLevelButtonWord();
            string usWords = treeMemory.getLevelButtonWord(-1,-1,-1,"US");
            
            cell = (XSSFCell)TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(1 + colCount);
            cell.SetCellValue(condition);
            cell = (XSSFCell)TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(2 + colCount);
            if (ukWords != usWords)
            {
                //cell.CellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                XSSFCellStyle fCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                XSSFFont ffont = (XSSFFont)workbook.CreateFont();
                ffont.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
                fCellStyle.SetFont(ffont);
                fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
                fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                cell.CellStyle = fCellStyle;
                cell.SetCellValue(ukWords);
            }
            else
            {
                cell.SetCellValue(ukWords);
            }
        }

        /* 
         *  Description:write Option
         *  Param:option-option list
         *  Return:
         *  Exception:
         *  Example:writeOption(optionlist)
         */
        public void writeOption(List<string> option)
        {
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol).SetCellValue(option[0]);
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol + 1).SetCellValue(option[1]);
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol + 2).SetCellValue(option[2]);
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol + 3).SetCellValue(option[3]);
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol + 4).SetCellValue(option[4]);
            TcworkSheet.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(optionSrartCol + 5).SetCellValue(option[5]);
        }

        /* 
         *  Description:写图片路径
         *  Param:imageLink-imageLink
         *  Return:
         *  Exception:
         *  Example:writeImageLink("imageLink")
         */
        public void writeImageLink(Dictionary<int, List<string>> tcPath, ISheet sheetName)
        {
            if (sheetName == TcworkSheet)
            {
                for (int index = 0; index < tcPath.Count; index++)
                {
                    for (int pathIndex = 0; pathIndex < 4; pathIndex++)
                    {
                        cell = sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(28 + (optionIndex * 4) + pathIndex * tcPath.Count + index);
                        if (tcPath[index][pathIndex] != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (tcPath[index][pathIndex])
                            };
                            cell.Hyperlink = hssfHyperlink;
                            if (tcPath[index][pathIndex].Split('_')[tcPath[index][pathIndex].Split('_').Length - 1] != "")
                            {
                                cell.SetCellValue("TC-" + tcPath[index][pathIndex].Split('_')[tcPath[index][pathIndex].Split('_').Length - 1]);
                            }
                        }
                    }
                }
            }
            else if (sheetName == ScreenworkSheet)
            {
                for (int index = 0; index < tcPath.Count; index++)
                {
                    for (int pathIndex = 0; pathIndex < 4; pathIndex++)
                    {
                        cell = (XSSFCell)ScreenworkSheet.GetRow(23 + excleOptionIndex.Count * 2 + screenRow - 1).GetCell(11 + (excleOptionIndex.Count) * 4 + pathIndex * tcPath.Count + index);
                        if (tcPath[index][pathIndex] != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                 Address = (tcPath[index][pathIndex])
                            };
                            cell.Hyperlink = hssfHyperlink;
                            if (tcPath[index][pathIndex].Split('_')[tcPath[index][pathIndex].Split('_').Length - 1] != "")
                            {
                                cell.SetCellValue("Screen-" + tcPath[index][pathIndex].Split('_')[tcPath[index][pathIndex].Split('_').Length - 1]);
                            }
                        }
                    }
                }
            }
        }

        /* 
         *  Description:write OptionList
         *  Param:detail-option detail
         *  Param:range-option range
         *  Return:
         *  Exception:
         *  Example:writeOptionList("detail","range")
         */
        public void writeOptionList(string opinionName,string detail, string range, ISheet sheetName)
        {
            //补齐模板后的观点及log
            insertRow(optionRow - 3, 2, true, sheetName);
            mergeCell(optionRow - 2, 1, optionRow - 1, 2, sheetName);
            mergeCell(optionRow - 2, 3, optionRow - 1, 6, sheetName);
            mergeCell(optionRow - 2, 7, optionRow - 1, 10, sheetName);
            cell = (XSSFCell)sheetName.GetRow(optionRow - 2).GetCell(1);
            cell.SetCellValue(excleOptionIndex[optionIndex - 1]);
            cell = (XSSFCell)sheetName.GetRow(optionRow - 2).GetCell(3);
            cell.SetCellValue(detail);
            cell = (XSSFCell)sheetName.GetRow(optionRow - 2).GetCell(7);
            cell.SetCellValue(range);
            //写入excel中NG，NA，OK，NT上面的观点的index和name
            if (optionIndex == 1 && sheetName == TcworkSheet)
            {
                cell = (XSSFCell)sheetName.GetRow((mainPartStartRow - 2) + optionIndex * 2).GetCell(opinionStsartCol-1);
                cell.SetCellValue("テスト観点" + excleOptionIndex[optionIndex - 1] + "\r\n" + opinionName);
            }
            if (optionIndex > 1 && sheetName == TcworkSheet)
            {
                //添加观点测试结果上的观点名
                for (int i = 0; i < 4; i++)
                {
                    insertCol((mainPartStartRow - 2) + optionIndex * 2, opinionStsartCol + i + (optionIndex - 2) * 4 + 3, sheetName);//等差数列
                    sheetName.SetColumnWidth(opinionStsartCol + i + (optionIndex - 2) * 4 + 2, sheetName.GetColumnWidth(26));
                }
                mergeCell((mainPartStartRow - 2) + optionIndex * 2, opinionStsartCol + (optionIndex - 2) * 4 + 3, (mainPartStartRow - 2) + optionIndex * 2, opinionStsartCol + 3 + (optionIndex - 2) * 4 + 3, sheetName);
                cell = (XSSFCell)sheetName.GetRow((mainPartStartRow - 2) + optionIndex * 2).GetCell(opinionStsartCol + (optionIndex - 2) * 4 + 3);
                cell.SetCellValue("テスト観点" + excleOptionIndex[optionIndex - 1]+"\r\n"+ opinionName);
                for (int i = 0; i < 4; i++)
                {
                    insertCol((mainPartStartRow - 1) + optionIndex * 2, opinionStsartCol + i + (optionIndex - 2) * 4 + 3, sheetName);
                    //workSheet.SetColumnWidth(27 + i + 2 + ((optionIndex - 1) * (optionIndex - 2) / 2) * 3, workSheet.GetColumnWidth(26));
                    switch (i)
                    {
                        case 0:
                            sheetName.GetRow((mainPartStartRow - 1) + optionIndex * 2).GetCell(opinionStsartCol + i + (optionIndex - 2) * 4 + 3).SetCellValue("NG");
                            break;
                        case 1:
                            sheetName.GetRow((mainPartStartRow - 1) + optionIndex * 2).GetCell(opinionStsartCol + i + (optionIndex - 2) * 4 + 3).SetCellValue("NA");
                            break;
                        case 2:
                            sheetName.GetRow((mainPartStartRow - 1) + optionIndex * 2).GetCell(opinionStsartCol + i + (optionIndex - 2) * 4 + 3).SetCellValue("OK");
                            break;
                        case 3:
                            sheetName.GetRow((mainPartStartRow - 1) + optionIndex * 2).GetCell(opinionStsartCol + i + (optionIndex - 2) * 4 + 3).SetCellValue("NT");
                            break;
                    }
                }
            }
            if (optionIndex == excleOptionIndex.Count && sheetName == TcworkSheet)
            {
                for (int imgIndex = 0; imgIndex < 4; imgIndex++)
                {
                    for (int optionCountIndex = 0; optionCountIndex < excleOptionIndex.Count; optionCountIndex++)
                    {
                        insertCol(mainPartStartRow + 2 * (excleOptionIndex.Count) - 2, opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex, TcworkSheet);
                        //insertCol(screenMainStartrow + 2 * (excleOptionIndex.Count) - 1, 12 + (excleOptionIndex.Count) * 3 + imgIndex * 2, ScreenworkSheet);
                        insertCol(mainPartStartRow + 2 * (excleOptionIndex.Count) - 1, opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex, TcworkSheet);
                        cell = (XSSFCell)TcworkSheet.GetRow(mainPartStartRow + 2 * (excleOptionIndex.Count) - 1).GetCell(opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex);
                        cell.SetCellValue("観点" + (excleOptionIndex[optionCountIndex]));
                    }
                    mergeCell(mainPartStartRow + 2 * (excleOptionIndex.Count) - 2, opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + excleOptionIndex.Count * imgIndex, mainPartStartRow + 2 * (excleOptionIndex.Count) - 2, opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + excleOptionIndex.Count * imgIndex + excleOptionIndex.Count - 1, TcworkSheet);
                    cell = (XSSFCell)TcworkSheet.GetRow(mainPartStartRow + 2 * (excleOptionIndex.Count) - 2).GetCell(opinionStsartCol - 1 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count);
                    switch (imgIndex)
                    {
                        case 0:
                            cell.SetCellValue("ImgPath");
                            break;
                        case 1:
                            cell.SetCellValue("RunTime - Log");
                            break;
                        case 2:
                            cell.SetCellValue("OCR-Log");
                            break;
                        case 3:
                            cell.SetCellValue("MFC-Log");
                            break;
                    }
                }
            }
            if (optionIndex == 1 && sheetName == ScreenworkSheet)
            {
                cell = (XSSFCell)sheetName.GetRow((screenMainStartrow) + optionIndex * 2 - 1).GetCell(screenOprionStartCol);
                cell.SetCellValue("テスト観点" + excleOptionIndex[optionIndex - 1] + "\r\n" + opinionName);
            }
            if (optionIndex > 1 && sheetName == ScreenworkSheet)
            {
                for (int i = 0; i < 4; i++)
                {
                    insertCol((screenMainStartrow) + optionIndex * 2 - 1, screenOprionStartCol + i + (optionIndex - 1) * 4, sheetName);//等差数列
                    sheetName.SetColumnWidth(screenOprionStartCol + i + (optionIndex - 1) * 4, sheetName.GetColumnWidth(26));
                }
                mergeCell((screenMainStartrow) + optionIndex * 2 - 1, screenOprionStartCol + (optionIndex - 1) * 4, (screenMainStartrow) + optionIndex * 2 - 1, screenOprionStartCol + 3 + (optionIndex - 1) * 4, sheetName);
                cell = (XSSFCell)sheetName.GetRow((screenMainStartrow) + optionIndex * 2 - 1).GetCell(screenOprionStartCol + (optionIndex - 1) * 4);
                cell.SetCellValue("テスト観点" + excleOptionIndex[optionIndex - 1] + "\r\n" + opinionName);
                for (int i = 0; i < 4; i++)
                {
                    insertCol((screenMainStartrow) + optionIndex * 2, screenOprionStartCol + i + (optionIndex - 1) * 4, sheetName);
                    //workSheet.SetColumnWidth(27 + i + 2 + ((optionIndex - 1) * (optionIndex - 2) / 2) * 3, workSheet.GetColumnWidth(26));
                    switch (i)
                    {
                        case 0:
                            sheetName.GetRow((screenMainStartrow) + optionIndex * 2).GetCell(screenOprionStartCol + i + (optionIndex - 1) * 4).SetCellValue("NG");
                            break;
                        case 1:
                            sheetName.GetRow((screenMainStartrow) + optionIndex * 2).GetCell(screenOprionStartCol + i + (optionIndex - 1) * 4).SetCellValue("NA");
                            break;
                        case 2:
                            sheetName.GetRow((screenMainStartrow) + optionIndex * 2).GetCell(screenOprionStartCol + i + (optionIndex - 1) * 4).SetCellValue("OK");
                            break;
                        case 3:
                            sheetName.GetRow((screenMainStartrow) + optionIndex * 2).GetCell(screenOprionStartCol + i + (optionIndex - 1) * 4).SetCellValue("NT");
                            break;
                    }
                }
            }
            if (optionIndex == excleOptionIndex.Count && sheetName == ScreenworkSheet)
            {
                for (int imgIndex = 0; imgIndex < 4; imgIndex++)
                {
                    for (int optionCountIndex = 0; optionCountIndex < excleOptionIndex.Count; optionCountIndex++)
                    {
                        insertCol(screenMainStartrow + 2 * (excleOptionIndex.Count) - 1, 11 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex, ScreenworkSheet);
                        //insertCol(screenMainStartrow + 2 * (excleOptionIndex.Count) - 1, 12 + (excleOptionIndex.Count) * 3 + imgIndex * 2, ScreenworkSheet);
                        insertCol(screenMainStartrow + 2 * (excleOptionIndex.Count), 11 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex, ScreenworkSheet);
                        cell = (XSSFCell)ScreenworkSheet.GetRow(screenMainStartrow + 2 * (excleOptionIndex.Count)).GetCell(11 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count + optionCountIndex);
                        cell.SetCellValue("観点" + excleOptionIndex[optionCountIndex]);
                    }
                    mergeCell(screenMainStartrow + 2 * (excleOptionIndex.Count) - 1, 11 + (excleOptionIndex.Count) * 4 + excleOptionIndex.Count * imgIndex, screenMainStartrow + 2 * (excleOptionIndex.Count) - 1, 11 + (excleOptionIndex.Count) * 4 + excleOptionIndex.Count * imgIndex + excleOptionIndex.Count - 1, ScreenworkSheet);
                    cell = (XSSFCell)ScreenworkSheet.GetRow(screenMainStartrow + 2 * (excleOptionIndex.Count) - 1).GetCell(11 + (excleOptionIndex.Count) * 4 + imgIndex * excleOptionIndex.Count);
                    switch (imgIndex)
                    {
                        case 0:
                            cell.SetCellValue("ImgPath");
                            break;
                        case 1:
                            cell.SetCellValue("RunTime-Log");
                            break;
                        case 2:
                            cell.SetCellValue("OCR-Log");
                            break;
                        case 3:
                            cell.SetCellValue("MFC-Log");
                            break;
                    }
                }
            }
        }

        /* 
         *  Description:write BaseInfo
         *  Param:baseInfoDetail-baseInfo Detail
         *  Return:
         *  Exception:
         *  Example:writeBaseInfo("baseInfoDetail")
         */
        public void writeBaseInfo(string baseInfoDetail, ISheet sheetName)
        {
            int totalCountStartRow = 12;
            string time;
            cell = (XSSFCell)sheetName.GetRow(baseRow).GetCell(3);
            if (baseRow >= totalCountStartRow)
            {
                //在excel的baseInfo位置中写入数据
                //get tcStartTime
                string startTime = BLL.TestRuntimeAggregate.getTcStartTime();
                //NowTimeToString
                string endTime = DateTime.Now.ToString();
                //endtime - starttime
                DateTime beginTime = DateTime.Parse(startTime);
                DateTime stopTime = DateTime.Parse(endTime);
                time = (stopTime - beginTime).Days.ToString() + "day:" +
                    (stopTime - beginTime).Hours.ToString() + "hour:" +
                    (stopTime - beginTime).Minutes.ToString() + "Mins";

                if (sheetName == TcworkSheet)
                {
                    switch (baseRow)
                    {
                        case 12:
                            //cell.SetCellValue(okCount + naCount + ngCount + outsideCount);
                            //cell.CellFormula = "SUM($D$14:$E$17)";
                            break;
                        case 17:
                            cell.SetCellValue(time);
                            break;
                    }
                }
                else
                {
                    switch (baseRow)
                    {
                        case 12:
                            //cell.SetCellValue(screenNaCount + screenNgCount + screenOkCount + screenOutSideCount);
                            //cell.CellFormula = "SUM($D$14:$E$17)";
                            break;
                        case 17:
                            cell.SetCellValue(time);
                            break;
                    }
                }
            }
            else
            {
                cell.SetCellValue(baseInfoDetail);
            }
        }


        public void save(string path)
        {
            throw new NotImplementedException();
        }

        /* 
         *  Description:start method
         *  Param:treeMemory-source
         *  Param:templetExclePath-templet excle path
         *  Param:savePath-save excle Path
         *  Return:
         *  Exception:
         *  Example:create(treeMemory,"@"C:\Users\lingru\Desktop\2.xlsx"","@"C:\Users\lingru\Desktop\23.xlsx"")
         */
        public void create(DAL.IFTBCommonAPI treeMemory, IScreenCommonAPI screenMemory, string templetExclePath, string savePath)
        {
            FileStream fs = null;
            List<string> getTestOpinion = new List<string>();
            addInfo();
            //拿到观点总数
            optionCount = BLL.TestRuntimeAggregate.getOpinionCount();
            //Get iterators
            IIterator tcIterator = treeMemory.createMccFilteredTcIterator();
            IIterator levelIterator = treeMemory.createLevelIterator();
            //打开模板
            fs = new FileStream(templetExclePath, FileMode.Open, FileAccess.ReadWrite);

            if (templetExclePath.IndexOf(".xlsx") > 0 || templetExclePath.IndexOf(".xlsm") > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else
            {
                throw new Exception("not xlsx file");
            }
            //初始化sheet
            if (workbook != null)
            {
                TcworkSheet = workbook.GetSheet("TC Test Report");
                ScreenworkSheet = workbook.GetSheet("Screen Test Report");
            }
            else
            {
                throw new Exception("can't find sheet");
            }
            
            tcTestReport(treeMemory);
            if (screenMemory != null)
            {
                screenTestReport(screenMemory);
            }
            //读取 baseInfo
            setBaseInfo(treeMemory, TcworkSheet);
            setBaseInfo(treeMemory, ScreenworkSheet);
            foreach (KeyValuePair<string, string> bif in baseInfo)
            {
                //写入baseInfo
                writeBaseInfo(bif.Value, TcworkSheet);
                baseRow++;
            }
            baseRow = 1;
            foreach (KeyValuePair<string, string> bif in baseInfo)
            {
                writeBaseInfo(bif.Value, ScreenworkSheet);
                baseRow++;
            }
            FileStream saveFilePath = new FileStream(savePath, FileMode.Create);
            workbook.Write(saveFilePath);
            saveFilePath.Flush();
            saveFilePath.Close();
        }
        public void writeConclusionAndOpinionResult(Dictionary<int, BLL.TestCheckResult> optionResult, int opinionIndex, ISheet sheetName, Dictionary<int, List<string>> tcPath)
        {
            string NAstr = "△(", OKstr = "○(", NGstr = "×(",NTstr="◇(";
            List<int> NA = new List<int>();
            List<int> OK = new List<int>();
            List<int> NG = new List<int>();
            List<int> NT = new List<int>();
            if (optionResult.Keys.Count > 0)
            {
                //根据优先级计算最后的result
                foreach (KeyValuePair<int, BLL.TestCheckResult> bif in optionResult)
                {
                    if (bif.Value == BLL.TestCheckResult.NG)
                    {
                        NG.Add(bif.Key);
                        allResult = "NG";
                        allResultFlag = 0;
                    }
                    else if (bif.Value == BLL.TestCheckResult.OK)
                    {
                        OK.Add(bif.Key);
                        if (allResult != "NG" && allResult != "NT")
                        {
                            allResult = "OK";
                        }
                        allResultFlag = 0;
                    }
                    else if (bif.Value == BLL.TestCheckResult.NA)
                    {
                        NA.Add(bif.Key);
                        if (allResult != "NG" && allResult != "NT" && allResult != "OK")
                        {
                            allResult = "NA";
                        }
                        allResultFlag = 0;
                    }
                    else if (bif.Value == BLL.TestCheckResult.NT)
                    {
                        NT.Add(bif.Key);
                        if (allResult != "NG")
                        {
                            allResult = "NT";
                        }
                        allResultFlag = 0;
                    }
                }
            }
            //当遍历到最后一个观点时
            if (opinionIndex == excleOptionIndex.Count)
            {
                //if allResultFlag=-1 is outside
                if (allResultFlag == -1)
                {
                    allResult = "対象外";
                }

                if (sheetName == TcworkSheet)
                {
                    sheetName.GetRow(mainPartStartRow + excleOptionIndex.Count * 2 + rowCount).GetCell(27).SetCellValue(allResult);
                }
                //？
                else
                {
                    sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1+ screenRow - 1).GetCell(screenOprionStartCol - 2).SetCellValue(allResult);
                }
            }
            //写入数据，如OK（2,3）
            for (int i = 0; i < NA.Count; i++)
            {
                NAstr = NAstr + NA[i];
                if (i != NA.Count - 1)
                    NAstr = NAstr + ",";
                if (i == NA.Count - 1)
                    NAstr = NAstr + ")";
            }
            for (int i = 0; i < OK.Count; i++)
            {
                OKstr = OKstr + OK[i];
                if (i != OK.Count - 1)
                    OKstr = OKstr + ",";
                if (i == OK.Count - 1)
                    OKstr = OKstr + ")";
            }
            for (int i = 0; i < NG.Count; i++)
            {
                NGstr = NGstr + NG[i];
                if (i != NG.Count - 1)
                    NGstr = NGstr + ",";
                if (i == NG.Count - 1)
                    NGstr = NGstr + ")";
            }
            for (int i = 0; i < NT.Count; i++)
            {
                NTstr = NTstr + NT[i];
                if (i != NT.Count - 1)
                    NTstr = NTstr + ",";
                if (i == NT.Count - 1)
                    NTstr = NTstr + ")";
            }

            string flowChartPath = getFlowChartPath(tcPath[opinionIndex - 1][0]);
            
            //当前sheet为Tc时
            if (sheetName == TcworkSheet)
            {
                if (allResultFlag != -1)
                {
                    if (NG.Count != 0)
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol - 1 + (opinionIndex - 1) * 4).SetCellValue(NGstr);

                        cell = sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol - 1 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol - 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (NA.Count != 0)
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + (opinionIndex - 1) * 4).SetCellValue(NAstr);
                        cell = sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (OK.Count != 0)
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 1 + (opinionIndex - 1) * 4).SetCellValue(OKstr);
                        cell = sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 1 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (NT.Count != 0)
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 2 + (opinionIndex - 1) * 4).SetCellValue(NTstr);
                        cell = sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 2 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 2 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                }
                else
                {
                    sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol - 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(mainPartStartRow + optionIndex * 2 + rowCount).GetCell(opinionStsartCol + 2 + (opinionIndex - 1) * 4).SetCellValue("-");
                }
            }
            //当前sheet为Screen时
            else
            {
                if (allResultFlag != -1)
                {
                    if (NG.Count != 0)
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1+ screenRow - 1).GetCell(screenOprionStartCol + (opinionIndex - 1) * 4).SetCellValue(NGstr);
                        cell = sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (NA.Count != 0)
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 1 + (opinionIndex - 1) * 4).SetCellValue(NAstr);
                        cell = sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 1 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (OK.Count != 0)
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 2 + (opinionIndex - 1) * 4).SetCellValue(OKstr);
                        cell = sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 2 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 2 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                    if (NT.Count != 0)
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 3 + (opinionIndex - 1) * 4).SetCellValue(NTstr);
                        cell = sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 3 + (opinionIndex - 1) * 4);
                        if (flowChartPath != null)
                        {
                            XSSFHyperlink hssfHyperlink = new XSSFHyperlink(HyperlinkType.Unknown)
                            {
                                Address = (flowChartPath)
                            };
                            cell.Hyperlink = hssfHyperlink;
                        }
                    }
                    else
                    {
                        sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 3 + (opinionIndex - 1) * 4).SetCellValue("-");
                    }
                }
                else
                {
                    sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 1 + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 2 + (opinionIndex - 1) * 4).SetCellValue("-");
                    sheetName.GetRow(screenMainStartrow + excleOptionIndex.Count * 2 + 1 + screenRow - 1).GetCell(screenOprionStartCol + 3 + (opinionIndex - 1) * 4).SetCellValue("-");
                }
            }
        }

        private string getFlowChartPath(string tcPath)
        {
            //tcPath = tcPath[opinionIndex - 1][0]
            if (tcPath == "")
                return null;
            int pathIndex = tcPath.LastIndexOf("ScreenImage");
            int pathLength = tcPath.Length - pathIndex;
            string path = tcPath.Substring(pathIndex, pathLength);

            int tcIndex = tcPath.LastIndexOf("TC_") + 3;
            int tclength = tcPath.Length - tcIndex;

            string currentTC = tcPath.Substring(tcIndex, tclength);
            string flowChartPath = path + @"\Tc-" + currentTC + ".jpg";
            return flowChartPath;
        }

        private void screenTestReport(IScreenCommonAPI screenMemory)
        {
            List<string> getTestOpinion = new List<string>();
            optionIndex = 0;
            optionRow = 20;
            excleOptionIndex = new List<int>();
            //添加type为screen的观点
            for (int index = 0; index < optionCount; index++)
            {
                if (BLL.TestRuntimeAggregate.getOpinionType(index) == "Screen")
                {
                    excleOptionIndex.Add(index);
                }
            }
            //观点的基本信息写入excel
            if (excleOptionIndex.Count != 0)
            {
                cell = (XSSFCell)ScreenworkSheet.GetRow(screenMainStartrow - 1).GetCell(screenOprionStartCol);
                cell.SetCellValue("テスト観点" + excleOptionIndex[0]);
                for (int index = 0; index < excleOptionIndex.Count; index++)
                {
                    getTestOpinion = BLL.TestRuntimeAggregate.getOpinionContent(excleOptionIndex[index]);
                    if (getTestOpinion == null)
                    {
                        continue;
                    }
                    optionIndex++;
                    optionRow = optionRow + 2;
                    writeOptionList(BLL.TestRuntimeAggregate.getOpinionName(excleOptionIndex[index]),getTestOpinion[0], getTestOpinion[1], ScreenworkSheet);
                    //add imgLink
                }
                //write Screen Test Report
                IIterator screenIterator = screenMemory.createScreenIterator();
                int startrow = 23 + excleOptionIndex.Count * 2, startCol = 1;
                int countRow = 0;
                try
                {
                    for (screenIterator.first(); !screenIterator.isDone(); screenIterator.next())
                    {
                        screenRow++;
                        countRow++;
                        //插入行
                        insertRow(startrow - 2 + screenRow, 1, false, ScreenworkSheet);
                        //merge body cell
                        for (int mergeCellIndex = 0; mergeCellIndex < 10; mergeCellIndex = mergeCellIndex + 2)
                        {
                            mergeCell(startrow + screenRow - 1, startCol + mergeCellIndex, startrow + screenRow - 1, startCol + mergeCellIndex + 1, ScreenworkSheet);
                        }
                        //写入 condition
                        cell = (XSSFCell)ScreenworkSheet.GetRow(startrow + screenRow - 1).GetCell(startCol);
                        List<string> screenConditionList = screenMemory.getScreenCondition(screenIterator.currentItem());
                        string condition = "";
                        if (screenConditionList == null || screenConditionList.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            for (int screenConditionIndex = 0; screenConditionIndex < screenConditionList.Count; screenConditionIndex++)
                            {
                                condition = condition + screenConditionList[screenConditionIndex] + "\r\n";
                            }
                        }
                        cell.SetCellValue(condition);
                        //写入 path
                        cell = (XSSFCell)ScreenworkSheet.GetRow(startrow + screenRow - 1).GetCell(startCol + 2);
                        List<string> screenPathList = screenMemory.getScreenPath(screenIterator.currentItem());
                        string path = "";
                        if (screenPathList == null || screenPathList.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            for (int screenPAthIndex = 0; screenPAthIndex < screenPathList.Count; screenPAthIndex++)
                            {
                                path = path + "\"" + screenPathList[screenPAthIndex] + "\",";
                            }
                            path = path.Substring(0, path.Length - 1);
                        }
                        cell.SetCellValue(path);
                        //写入 standardWords
                        cell = (XSSFCell)ScreenworkSheet.GetRow(startrow + screenRow - 1).GetCell(startCol + 4);
                        List<string> screenWordsList = screenMemory.getScreenWords(screenIterator.currentItem());
                        string screenWords = "";
                        if (screenWordsList == null || screenWordsList.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            for (int screenWordsIndex = 0; screenWordsIndex < screenWordsList.Count; screenWordsIndex++)
                            {
                                screenWords = screenWords + "\"" + screenWordsList[screenWordsIndex] + "\",";
                            }
                            screenWords = screenWords.Substring(0, screenWords.Length - 1);
                        }
                        cell.SetCellValue(screenWords);
                        ////写入 result
                        Dictionary<int, List<string>> tcPath = new Dictionary<int, List<string>>();
                        //写入 log
                        for (int index = 0; index < excleOptionIndex.Count; index++)
                        {
                            List<string> oneOpinionPath = new List<string>();
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getScreenImagePath(screenIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getCommandLogPath(screenIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getOcrLogPath(screenIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getMachineLogPath(screenIterator.currentItem(), excleOptionIndex[index]));
                            tcPath.Add(index, oneOpinionPath);
                        }
                        //写入 result
                        for (int optionResultIndex = 1; optionResultIndex <= excleOptionIndex.Count; optionResultIndex++)
                        {
                            Dictionary<int, TestCheckResult> resultDic = TestRuntimeAggregate.getCheckResult(screenIterator.currentItem(), excleOptionIndex[optionResultIndex - 1]);
                            if (resultDic == null)
                            {
                                continue;
                            }
                            //计算当前case的总体结果
                            writeConclusionAndOpinionResult(resultDic, optionResultIndex, ScreenworkSheet, tcPath);
                        }
                   
                        writeImageLink(tcPath, ScreenworkSheet);
                        allResultFlag = -1;
                        allResult = "";
                        //写入 cureentWords
                        cell = (XSSFCell)ScreenworkSheet.GetRow(startrow+ screenRow - 1).GetCell(startCol + 6);
                        List<string> cureenrWordsList = TestRuntimeAggregate.getScreenCheckCurrentWords(screenIterator.currentItem(), screenPathList.Count);
                        string currentWords = "";
                        if (cureenrWordsList == null || cureenrWordsList.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            for (int currentWordsIndex = 0; currentWordsIndex < cureenrWordsList.Count; currentWordsIndex++)
                            {
                                currentWords = currentWords + "\"" + cureenrWordsList[currentWordsIndex] + "\",";
                            }
                            currentWords = currentWords.Substring(0, currentWords.Length - 1);
                        }
                        cell.SetCellValue(currentWords);//to do
                        Console.Write("Write Screen-" + screenIterator.currentItem() + " to excel.\r");
                    }
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("Reason:Screen Process interrupt---" + ex.Message + "\r\n");
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }

                //merge screen Test Report cell
                int mergeStartRow = screenMainStartrow + 1 + optionIndex * 2;
                //计算OK，NA，NG，对象外总数
                for (int row = mergeStartRow; row < screenRow + mergeStartRow; row++)
                {

                    if (ScreenworkSheet.GetRow(row).GetCell(9).ToString() == "OK")
                    {
                        screenOkCount++;
                    }
                    if (ScreenworkSheet.GetRow(row).GetCell(9).ToString() == "NA")
                    {
                        screenNaCount++;
                    }
                    if (ScreenworkSheet.GetRow(row).GetCell(9).ToString() == "NG")
                    {
                        screenNgCount++;
                    }
                    if (ScreenworkSheet.GetRow(row).GetCell(9).ToString() == "対象外")
                    {
                        screenOutSideCount++;
                    }
                }

                //write na ng ok count
                int okRow = 14, okCol = 9, optionCountRow = 13, optionCountCol = 7;
                cell = (XSSFCell)ScreenworkSheet.GetRow(okRow).GetCell(okCol);
                cell.SetCellValue(screenOkCount);
                cell = (XSSFCell)ScreenworkSheet.GetRow(okRow + 1).GetCell(okCol);
                cell.SetCellValue(screenNgCount);
                cell = (XSSFCell)ScreenworkSheet.GetRow(okRow + 2).GetCell(okCol);
                cell.SetCellValue(screenNaCount);
                //write screen option count in excel
                cell = (XSSFCell)ScreenworkSheet.GetRow(optionCountRow).GetCell(optionCountCol);
                cell.SetCellValue(excleOptionIndex.Count);
                cell = (XSSFCell)ScreenworkSheet.GetRow(optionCountRow).GetCell(optionCountCol + 1);
                cell.SetCellValue(countRow);
            }
        }
        private void tcTestReport(DAL.IFTBCommonAPI treeMemory)
        {
            List<string> getTestOpinion = new List<string>();
            optionIndex = 0;
            optionRow = 20;
            excleOptionIndex = new List<int>();
            //记录观点type为TC的观点
            for (int index = 0; index < optionCount; index++)
            {
                if (BLL.TestRuntimeAggregate.getOpinionType(index) == "TC")
                    excleOptionIndex.Add(index);
            }
            if (excleOptionIndex.Count != 0)
            {
                //set opinion1 words
                cell = (XSSFCell)TcworkSheet.GetRow(mainPartStartRow - 2).GetCell(opinionStsartCol - 1);
                cell.SetCellValue("テスト観点" + excleOptionIndex[0]);
                //写入观点信息
                try
                {
                    for (int index = 0; index < excleOptionIndex.Count; index++)
                    {
                        getTestOpinion = BLL.TestRuntimeAggregate.getOpinionContent(excleOptionIndex[index]);
                        if (getTestOpinion == null)
                        {
                            continue;
                        }
                        optionIndex++;
                        optionRow = optionRow + 2;
                        writeOptionList(BLL.TestRuntimeAggregate.getOpinionName(excleOptionIndex[index]), getTestOpinion[0], getTestOpinion[1], TcworkSheet);
                    }
                    IIterator tcIterator = treeMemory.createMccFilteredTcIterator();
                    IIterator levelIterator = treeMemory.createLevelIterator();
                    int countRow = 0;
                    //遍历树
                    for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
                    {
                        countRow++;
                        Dictionary<int, List<string>> tcPath = new Dictionary<int, List<string>>();
                        insertRow((mainPartStartRow - 1) + optionIndex * 2 + rowCount, 1, false, TcworkSheet);
                        //遍历level
                        for (levelIterator.first(); levelIterator.currentItem() < treeMemory.getLevelCount(); levelIterator.next())
                        {
                            writeLevel(treeMemory);
                            colCount = colCount + 2;
                        }
                        List<string> option = new List<string>();
                        //获取当前TC option的信息
                        option.Add(treeMemory.getTotalCondition(treeMemory.getLevelCondition()));
                        option.Add(treeMemory.getLevelButtonWord());
                        option.Add(treeMemory.getTcFactorySetting());
                        option.Add(treeMemory.getTcComment());
                        option.Add(treeMemory.getTcRsp());
                        option.Add(treeMemory.getTcEws());
                        writeOption(option);
                        //获取log信息
                        for (int index = 0; index < excleOptionIndex.Count; index++)
                        {
                            List<string> oneOpinionPath = new List<string>();
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getScreenImagePath(tcIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getCommandLogPath(tcIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getOcrLogPath(tcIterator.currentItem(), excleOptionIndex[index]));
                            oneOpinionPath.Add(BLL.TestRuntimeAggregate.getMachineLogPath(tcIterator.currentItem(), excleOptionIndex[index]));
                            //index 观点下标 oneOpinionPath此观点的四个log路径
                            tcPath.Add(index, oneOpinionPath);
                        }
                        //写入当前Tc的测试结果
                        for (int Index = 1; Index <= excleOptionIndex.Count; Index++)
                        {
                            opinionResult = BLL.TestRuntimeAggregate.getCheckResult(tcIterator.currentItem(), excleOptionIndex[Index - 1]);
                            if (opinionResult == null)
                            {
                                continue;
                            }
                            writeConclusionAndOpinionResult(opinionResult, Index, TcworkSheet, tcPath);
                        }
                        //计算结果中NA，NG，OK，NT总数
                        //目前而言，这些数据的统计在Excel中进行
                        switch (allResult)
                        {
                            case "OK":
                                okCount++;
                                break;
                            case "NG":
                                ngCount++;
                                break;
                            case "対象外":
                                outsideCount++;
                                break;
                            case "NA":
                                naCount++;
                                break;
                        }

                        writeImageLink(tcPath, TcworkSheet);
                        //
                        Console.Write("Write TC-" + tcIterator.currentItem() + " to excel.\r");
                        rowCount++;
                        colCount = 0;
                        tcCount++;
                        allResult = "";
                        allResultFlag = -1;
                    }

                    //合并单元格
                    int mergeStartRow = mainPartStartRow + 1 + optionIndex * 2;
                    for (int col = 1; col < optionSrartCol; col = col + 2)
                    {
                        int tempRow = mergeStartRow - 1;
                        for (int row = mergeStartRow; row < rowCount - 1 + mergeStartRow; row++)
                        {
                            if (TcworkSheet.GetRow(tempRow).GetCell(col + 1).ToString() == "")
                                tempRow = row - 1;
                            if (checkMerge(row, col + 1))
                            {
                                //button合并
                                mergeCell(tempRow, col + 1, row - 1, col + 1, TcworkSheet);
                                //condition合并
                                mergeCell(tempRow, col, row - 1, col, TcworkSheet);
                                tempRow = row;
                            }
                            else if (row == rowCount - 2 + mergeStartRow && TcworkSheet.GetRow(tempRow).GetCell(col + 1).ToString() != "")
                            {
                                mergeCell(tempRow, col + 1, row, col + 1, TcworkSheet);
                                mergeCell(tempRow, col, row, col, TcworkSheet);
                            }
                        }
                    }
                    int optionCountRow = 13, optionCountCol = 7;
                    //write screen option count in excel
                    cell = (XSSFCell)TcworkSheet.GetRow(optionCountRow).GetCell(optionCountCol);
                    cell.SetCellValue(excleOptionIndex.Count);
                    cell = (XSSFCell)TcworkSheet.GetRow(optionCountRow).GetCell(optionCountCol + 1);
                    cell.SetCellValue(countRow);
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("Reason:TC Process interrupt---" + ex.Message + "\r\n");
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }
            }
                
        }
    }
}
