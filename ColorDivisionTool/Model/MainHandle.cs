using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using ColorDivisionTool.ViewModel;
using ColorDivisionTool.Operation;

namespace ColorDivisionTool.Model
{
    using ExcelData = Dictionary<string, Dictionary<string, CellBaseInfo>>;
    public class MainHandle
    {
        #region Evidence File Variable

        public ExcelHelper evidenceExeclHand { get; private set; }
        public ExcelHelper iconFontExcelHand { get; private set; }
        public ExcelData evidenceFileData { get; private set; }
        public ExcelData iconFontFileData { get; private set; }
        public Dictionary<string, int> pctCrtSwitch { get; private set; }
        string EvidenceFileName = null;

        #endregion

        // Evidence File Title collections
        public List<string> changeColortitles = new List<string>();
        public List<string> jpanTitles = new List<string>();

        //绑定View层Bindpath和控件属性
        BindItem ViewBindModel = new BindItem();

        public async void StartChangeFileColor(BindItem baseModel, List<string> ArgumentsLists)
        {
            this.ViewBindModel = baseModel;
            changeColortitles = IniOperation.GetSpecifySectionValues(ConfigConst.CHANGECOLORCOUNTRY, ConfigConst.CONFIGUREFILEPATH);
            jpanTitles = IniOperation.GetSpecifySectionValues(ConfigConst.JPANCOUNTRY, ConfigConst.CONFIGUREFILEPATH);

            await Task.Run(() =>
            {
                try
                {
                    //IconFont Data
                    iconFontExcelHand = null;
                    iconFontFileData = null;
                    //evidence File Data
                    evidenceExeclHand = null;
                    evidenceFileData = null;

                    //evidence file titles
                    List<string> allTitles = new List<string>();

                    //运行时界面操作框锁定不可写入
                    ViewBindModel.Btn_ColorStartCtl = false;
                    //开始执行时轻松一刻gif图
                    ViewBindModel.Show_GifStatus1 = "Visible";

                    #region Read IconFont File
                    // Open IconFont Excel
                    ViewBindModel.MWindowShowLog = "Open File : " + ConfigConst.ICONFONTFILE;
                    Console.WriteLine("Open File : " + ConfigConst.ICONFONTFILE);
                    iconFontExcelHand = new ExcelHelper(ConfigConst.ICONFONTFILE);
                    if (!iconFontExcelHand.OpenFile())
                    {
                        ViewBindModel.MWindowShowLog = "Open File IconFont.xlsx failed!";
                        Console.WriteLine("Open File IconFont.xlsx failed!");
                        return;
                    }
                    pctCrtSwitch = new Dictionary<string, int>();
                    if (!ReadIconFontFileData(iconFontExcelHand, pctCrtSwitch))
                    {
                        ViewBindModel.MWindowShowLog = " Read file failed : IconFont.xlsx! ";
                        Console.WriteLine(" Read file failed : IconFont.xlsx! ");
                        iconFontExcelHand.CloseFile();
                        return;
                    }
                    #endregion

                    #region Read Evidence File
                    // get Evidence File Name 
                    EvidenceFileName = Path.GetFileName(ViewBindModel.Excel_EvidenceFile);

                    // Open Evidence File
                    ViewBindModel.MWindowShowLog = "Open File : " + ViewBindModel.Excel_EvidenceFile;
                    evidenceExeclHand = new ExcelHelper(ViewBindModel.Excel_EvidenceFile);
                    if (!evidenceExeclHand.OpenFile())
                    {
                        ViewBindModel.MWindowShowLog = "Open File " + EvidenceFileName + " failed!";
                        Console.WriteLine("Open File " + EvidenceFileName + " failed!");
                        return;
                    }
                    //处理开始，显示loading的Gif图片
                    ViewBindModel.Show_GifStatus2 = "Visible";
                    //Read Evidence File Data and save
                    ViewBindModel.MWindowShowLog = "Read File : " + EvidenceFileName;
                    evidenceFileData = ReadTransCheckSheetData(evidenceExeclHand, allTitles);
                    if (evidenceFileData == null)
                    {
                        ViewBindModel.MWindowShowLog = "The Evidence File Data is empty, please check Evidence Data!";
                        Console.WriteLine("The Evidence File Data is empty, please check Evidence Data!");
                        evidenceExeclHand.CloseFile();
                        return;
                    }
                    #endregion

                    // 检查绘文字，处理EvidenceData
                    if (!CheckHandEvidenceFileData(evidenceFileData, allTitles))
                    {
                        ViewBindModel.MWindowShowLog = "File Color fill failure!";
                        Console.WriteLine("File Color fill failure!");
                        return;
                    }


                    // 获取处理后的MsgNo Color然后写入Evidence文件
                    if (!WriteDataToEvidenceFile(evidenceFileData, evidenceExeclHand, allTitles))
                    {
                        ViewBindModel.MWindowShowLog = "Write data to Evidence File Failed!";
                        Console.WriteLine("Write data to Evidence File Failed!");
                        return;
                    }

                    //保存Evidence文件
                    evidenceExeclHand.SaveExcelFile();
                    ViewBindModel.MWindowShowLog = "Color fill completed!";
                    Console.WriteLine("Color fill completed!");
                }
                catch (Exception e)
                {
                    ViewBindModel.MWindowShowLog = e.Message;
                }
                finally
                {
                    //Close Evidence File
                    if (evidenceExeclHand != null)
                    {
                        evidenceExeclHand.CloseFile();
                        evidenceExeclHand = null;
                    }
                    //Close IconFont File
                    if (iconFontExcelHand != null)
                    {
                        iconFontExcelHand.CloseFile();
                        iconFontExcelHand = null;
                    }
                    ViewBindModel.Btn_ColorStartCtl = true;
                    ViewBindModel.Show_GifStatus2 = "Hidden";
                    ViewBindModel.Show_GifStatus1 = "Hidden";
                    if (ArgumentsLists.Count > 2)
                    {
                        Console.WriteLine("色分けツール working Finished.");
                        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    }
                }
            });
        }

        /// <summary>
        /// Read IconFont Excel Data
        /// </summary>
        /// <param name="excelHand"></param>
        /// <param name="switchCart"></param>
        /// <returns></returns>
        private bool ReadIconFontFileData(ExcelHelper excelHand, Dictionary<string, int> switchCart)
        {
            if (excelHand == null || switchCart == null)
            {
                ViewBindModel.MWindowShowLog = "Read IconFontFileData parameter Error!";
                Console.WriteLine("Read IconFontFileData parameter Error!");
                return false;
            }

            try
            {
                int startRow = 1;
                int startCol = 1;
                int endCol = 2;
                int endRow = 0;
                //Read IconFont Sheet
                if (!excelHand.CheckSheetName(ConfigConst.SHEETNAME_ICONFONTFILE))
                {
                    ViewBindModel.MWindowShowLog = "It's not current Sheet : Read Failed!";
                    Console.WriteLine("It's not current Sheet : Read Failed!");
                    return false;
                }

                // Get last Row of IconFont Sheet
                endRow = excelHand.GetEndRow(startRow, startCol);

                object[,] readData = null;
                excelHand.ReadExcel(startRow, endRow, startCol, endCol, ref readData);
                if (readData == null)
                {
                    ViewBindModel.MWindowShowLog = "Read characterSwitch Data Error!";
                    Console.WriteLine("Read characterSwitch Data Error!");
                    return false;
                }
                //读取每一行的置换数据
                for (int i = 2; i <= endRow; i++)
                {
                    string value = Convert.ToString(readData[i, 1]);
                    if (string.IsNullOrEmpty(value))
                        break;
                    if (switchCart.ContainsKey(value))
                        continue;
                    switchCart.Add(value, Convert.ToInt32(readData[i, 2]));
                }
                return true;
            }
            catch (Exception e)
            {
                ViewBindModel.MWindowShowLog = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Get all the language data about Evidence file from TransCheckSheet
        /// </summary>
        /// <param name="excelHand"></param>
        /// <param name="allTitles"></param>
        /// <returns></returns>
        private ExcelData ReadTransCheckSheetData(ExcelHelper excelHand, List<string> allTitles)
        {
            if (excelHand == null)
            {
                ViewBindModel.MWindowShowLog = "Read " + EvidenceFileName + " parameter Error!";
                Console.WriteLine("Read " + EvidenceFileName + " parameter Error!");
                return null;
            }

            try
            {
                int startRow = 1;
                int startCol = 1;
                int col = startCol;
                int titleRow = 0;

                ExcelData data = new Dictionary<string, Dictionary<string, CellBaseInfo>>();
                List<string> titles = new List<string>();

                // Read current sheet
                if (!excelHand.CheckSheetName(ConfigConst.SHEETNAME_TRANSLATEFILE))
                {
                    ViewBindModel.MWindowShowLog = "It's not current Sheet : Read Failed!";
                    Console.WriteLine("It's not current Sheet : Read Failed!");
                    return null;
                }

                //获取数据开始读取时的行号
                startRow = excelHand.GetTitleRow(startRow, col, ConfigConst.TITLE_MSGNO);
                titleRow = startRow - 1;
                if (startRow < 0)
                {
                    return null;
                }
                //Get end Column and end row
                int endCol = excelHand.GetTitleEndCol(titleRow, col);
                int endRow = excelHand.GetEndRow(titleRow, col);
                int row = startRow;

                //获取Evidence文件中所有的标题
                excelHand.ReadExcel(titleRow, col, endCol, ref allTitles);

                object[,] readData = null;
                double[,] wordColors = new double[endRow - startRow + 1, allTitles.Count];
                //读取所有的单元格内容存储到数组中
                excelHand.ReadExcel(startRow, endRow, startCol, endCol, ref readData);
                //excelHand.ColorIndex(startRow, endRow, ConfigConst.START_COL, ConfigConst.END_COL, ref wordColors);
                excelHand.GetColorIndex(startRow, endRow, ConfigConst.START_COL, endCol, ref wordColors);
                //double[,] cors = new double[endRow - startRow + 1, allTitles.Count];
                //for (int ii = 0; ii < (endRow - startRow + 1); ii++)
                //{
                //    for (int jj = 0; jj < allTitles.Count; jj++)
                //    {
                //        if (wordColors[ii, jj] != cors[ii, jj])
                //        {
                //            Console.Write("not-same");
                //            System.Threading.Thread.Sleep(2000);
                //        }
                //    }
                //}
                if (readData == null || wordColors == null)
                {
                    return null;
                }

                #region Read data processing
                while (true)
                {
                    string applyNo = string.Empty;
                    ViewBindModel.MWindowShowLog = "Get line No." + row + " Data!";
                    if (row <= endRow)
                    {
                        int line = row - titleRow;
                        Dictionary<string, CellBaseInfo> tempData = new Dictionary<string, CellBaseInfo>();
                        //一行行获取单元格的信息
                        tempData = GetCellDataFromEvidenceData(line, row, readData, wordColors, allTitles);
                        //获取申请No
                        applyNo = tempData[ConfigConst.TITLE_APPLYNO].ApplyNo;
                        if (string.IsNullOrEmpty(applyNo))
                        {
                            ViewBindModel.MWindowShowLog = "ApplyNo is null or empty!";
                            Console.WriteLine("ApplyNo is null or empty!");
                            break;
                        }
                        //数据保存到data中
                        data.Add(applyNo, tempData);
                        row++;
                    }
                    else
                    {
                        ViewBindModel.MWindowShowLog = "Get Evidence File Data Complete!";
                        Console.WriteLine("Get Evidence File Data Complete!");
                        break;
                    }
                }
                #endregion
                return data;
            }
            catch (Exception e)
            {
                ViewBindModel.MWindowShowLog = e.Message;
                return null;
            }
        }

        /// <summary>
        /// Get a cell information and save
        /// </summary>
        /// <param name="excelHand"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="msgNo"></param>
        /// <param name="titleName"></param>
        /// <returns></returns>
        private Dictionary<string, CellBaseInfo> GetCellDataFromEvidenceData(int line, int row, object[,] readData, double[,] wdColorList, List<string> titleName)
        {
            int temp = 0;
            Dictionary<string, CellBaseInfo> tempData = new Dictionary<string, CellBaseInfo>();

            //获取Excel中一整行数据信息
            for (int i = 1; i <= titleName.Count; i++)
            {
                CellBaseInfo tempCell = new CellBaseInfo();
                string title = titleName[i - 1];
                if (title == ConfigConst.TITLE_APPLYNO)
                {
                    //获取申请NO
                    tempCell.ApplyNo = Convert.ToString(readData[line, i]);
                }
                else if (title == ConfigConst.TITLE_MSGNO)
                {
                    //获取MsgNo
                    tempCell.MsgNo = Convert.ToString(readData[line, i]);
                }
                tempCell.Value = Convert.ToString(readData[line, i]);
                tempCell.Row = row;
                tempCell.Col = i;
                tempCell.Color = wdColorList[line - 1, i - 1];
                //在重复的title后面加上数字让title不重复
                if (tempData.ContainsKey(title))
                {
                    title += temp.ToString();
                    temp++;
                    //continue;
                }
                tempData.Add(title, tempCell);
            }
            return tempData;
        }

        /// <summary>
        /// 判断所有国的文言长度
        /// </summary>
        /// <param name="saveData"></param>
        /// <param name="alltitles"></param>
        /// <returns></returns>
        private bool CheckHandEvidenceFileData(ExcelData saveData, List<string> alltitles)
        {
            List<string> languageTitles = new List<string>();

            ViewBindModel.MWindowShowLog = "Change EvidenceFile msgNo color...";
            //获取各个国家的title
            foreach (string value in changeColortitles)
            {
                if ((value == ConfigConst.TITLE_MSGNO) || (value == ConfigConst.TITLE_APPLYNO))
                {
                    continue;
                }
                if (alltitles.Contains(value))
                    languageTitles.Add(value);
            }

            //判断和设置单元格颜色
            return ChangeEvidenceDataColors(saveData, languageTitles);
        }

        /// <summary>
        /// 处理各个国家的文言长度
        /// </summary>
        /// <param name="saveData"></param>
        /// <param name="countryTitles"></param>
        /// <returns></returns>
        private bool ChangeEvidenceDataColors(ExcelData saveData, List<string> countryTitles)
        {
            try
            {
                List<string> exceptJpnTitles = new List<string>();
                foreach (string tempCountry in countryTitles)
                {
                    if (!jpanTitles.Contains(tempCountry))
                        exceptJpnTitles.Add(tempCountry);
                }
                //循环读取saveData中的每一行
                foreach (string applyNo in saveData.Keys)
                {
                    if (saveData[applyNo][ConfigConst.TITLE_DESTINATION].Value == "JPNのみ")
                    {
                        //只处理日本国文言
                        GetLanguageLengthAndSetColors(saveData, jpanTitles, applyNo);
                    }
                    else if (saveData[applyNo][ConfigConst.TITLE_DESTINATION].Value == "JPN以外")
                    {
                        //处理日本语以外的文言
                        GetLanguageLengthAndSetColors(saveData, exceptJpnTitles, applyNo);
                    }
                    else
                    {
                        //处理所有国文言
                        GetLanguageLengthAndSetColors(saveData, countryTitles, applyNo);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get every country language length and compare it with max length to change msgNo colors. 
        /// </summary>
        /// <param name="saveData"></param>
        /// <param name="countries"></param>
        /// <param name="applyNo"></param>
        private void GetLanguageLengthAndSetColors(ExcelData saveData, List<string> countries, string applyNo)
        {
            bool redColorFlag = false;
            string jpnCountry = string.Empty;

            foreach (string jpn in jpanTitles)
            {
                if (jpn != ConfigConst.COUNTRY_JPE)
                    jpnCountry = jpn;
            }

            //循环读取每一行中的每一个国别的文言
            foreach (string title in countries)
            {
                //文言内容
                string crtValue = saveData[applyNo][title].Value;

                #region language is empty
                //文言为__(T_T)__HONYAKU，" "，N/A的时候则不翻译
                if ((crtValue == ConfigConst.EMPTYLANGUAGE) || (crtValue == " ") || (crtValue.Length == 0) || (crtValue == ConfigConst.NALANGUAGE))
                {
                    //文言内容为空，不做任何处理
                    if (title == ConfigConst.COUNTRY_UK || (title == ConfigConst.COUNTRY_JPE && countries.Count == 2))
                    {
                        saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_GREEN;
                        redColorFlag = false;
                        break;
                    }
                    else
                    {
                        if (title != ConfigConst.COUNTRY_UKR)
                        {
                            if (saveData[applyNo][ConfigConst.TITLE_DESTINATION].Value != "JPNのみ")
                            {
                                if (title == ConfigConst.COUNTRY_JPE || title == ConfigConst.COUNTRY_USA || title == jpnCountry)
                                {
                                    saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_GREEN;
                                    continue;
                                }
                            }
                            //如果文言为空且国别不为UKR，则把MsgNo标成红色
                            if (!redColorFlag)
                            {
                                redColorFlag = true;                
                            }
                        }
                        else
                        {
                            //如果国别为UKR，则直接跳过该条文言
                            continue;
                        }
                    }
                }
                else
                {//文言不为空先标记成绿色
                    saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_GREEN;
                }
                #endregion
                //如果有文字超出标记为红色
                if (saveData[applyNo][title].Color == ConfigConst.COLOR_RED)
                {
                    saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_RED;
                    break;
                }
                else
                {
                    if (saveData[applyNo][ConfigConst.TITLE_MSGNO].Color != ConfigConst.COLOR_GREEN)
                    {
                        saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_GREEN;
                        redColorFlag = false;
                    }
                }
            }

            if (redColorFlag)
            {
                saveData[applyNo][ConfigConst.TITLE_MSGNO].Color = ConfigConst.COLOR_RED;
                redColorFlag = false;
            }
        }

        /// <summary>
        /// Write Cell information to excel
        /// </summary>
        /// <param name="excelData"></param>
        /// <param name="excelHand"></param>
        /// <param name="evidenceTitles"></param>
        /// <returns></returns>
        private bool WriteDataToEvidenceFile(ExcelData excelData, ExcelHelper excelHand, List<string> evidenceTitles)
        {
            if (excelData == null || excelHand == null || evidenceTitles.Count < 1)
            {
                ViewBindModel.MWindowShowLog = "WriteDataToEvidenceFile parameter Error!";
                Console.WriteLine("WriteDataToEvidenceFile parameter Error!");
                return false;
            }
            try
            {
                int wStartRow = 0;
                int wStartCol = 0;
                int wEndRow = 0;
                List<double> writeColor = new List<double>();

                ViewBindModel.MWindowShowLog = "Start writeData to evidence file...";
                foreach (string applyNum in excelData.Keys)
                {
                    //获取一行数据准备写入
                    foreach (string title in evidenceTitles)
                    {
                        if (title == ConfigConst.TITLE_MSGNO && writeColor.Count == 0)
                        {
                            //获得一条文言的首行和首列
                            wStartRow = excelData[applyNum][title].Row;
                            wStartCol = excelData[applyNum][title].Col;
                        }
                        //获取最后一行和所有MsgNo列的颜色
                        wEndRow = excelData[applyNum][title].Row;
                    }
                    writeColor.Add(excelData[applyNum][ConfigConst.TITLE_MSGNO].Color);
                }
                excelHand.SetColorIndex(wStartRow, wEndRow, wStartCol, writeColor.ToArray());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文言的最大字符数
        /// </summary>
        /// <param name="excelData"></param>
        /// <param name="applyno"></param>
        /// <returns></returns>
        /*private int GetMaxStringLength(ExcelData excelData, string applyno)
        {
            try
            {
                int maxLength = 0;
                if (string.IsNullOrEmpty(excelData[applyno][ConfigConst.TITLE_SINGLENUM].Value) || string.IsNullOrEmpty(excelData[applyno][ConfigConst.TITLE_APPLYLINE].Value))
                {
                    return -1;
                }
                int num = int.Parse(excelData[applyno][ConfigConst.TITLE_SINGLENUM].Value);
                int line = int.Parse(excelData[applyno][ConfigConst.TITLE_APPLYLINE].Value);
                //判断申请字数和申请行数是否存在
                if (num == 0 || line == 0)
                {
                    //不存在则通过计算得出最大字符数
                    int temp_rstHeight = int.Parse(excelData[applyno][ConfigConst.TITLE_RSTHEIGHT].Value);
                    int temp_rstWidth = int.Parse(excelData[applyno][ConfigConst.TITLE_RSTWIDTH].Value);
                    int temp_fontHeight = int.Parse(excelData[applyno][ConfigConst.TITLE_FONTHEIGHT].Value);
                    //四舍五入计算行数
                    line = (int)Math.Round(((double)temp_rstHeight / temp_fontHeight), MidpointRounding.AwayFromZero);
                    //计算字数长度
                    if (temp_rstWidth % 6 == 0)
                    {
                        num = (temp_rstWidth / 6) % 2 == 0 ? temp_rstWidth / 6 : temp_rstWidth / 6 + 1;
                    }
                    else
                    {
                        num = (temp_rstWidth / 6) % 2 == 0 ? temp_rstWidth / 6 + 2 : temp_rstWidth / 6 + 1;
                    }
                    maxLength = num * line;
                    //存储申请字数和申请行数
                    excelData[applyno][ConfigConst.TITLE_SINGLENUM].Value = num.ToString();
                    excelData[applyno][ConfigConst.TITLE_APPLYLINE].Value = line.ToString();
                }
                else
                {
                    maxLength = num * line;
                }
                return maxLength;
            }
            catch
            {
                return -1;
            }
        }*/
    }
}
