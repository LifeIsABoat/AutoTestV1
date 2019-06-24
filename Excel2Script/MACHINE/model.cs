using System;
using System.Collections.Generic;
using System.Text;

namespace FTBExcel2Script
{
    class model: CommonFTBXlsOperationNpoi
    {
        public model(MainForm form)
            : base()
        {
            this.form = form;
        }

        protected override void getModel()
        {
            int colIndex=0;
            string modelStr = "", tempmodelStr = "", tempStr = "";
            modelList = new List<string>();
            do
            {
				if (true == ProcessEnd)
                {
                    break;
                }
                form.setMessage(excelOperation.getSheetName() + " sheet:\nRead model info, please wait……");
                modelStr = excelOperation.getCellValue(modelStartPosition["row"] + 2, modelStartPosition["col"] + colIndex);
                if (tempmodelStr != modelStr && modelStr!="")
                {
                    modelList.Add(modelStr);
                    tempmodelStr = modelStr;
                }
                colIndex++;
                tempStr = excelOperation.getCellValue(modelStartPosition["row"], modelStartPosition["col"] + colIndex);
                if (tempStr == "Country")
                {
                    break;
                }
            }
            while (modelStr != "");
        }

        protected override void getContinent() 
        {
            int colIndex = 0;
            string continentStr = "", tempContinentStr = "";
            continentList = new List<ContinentInfo>();
            ContinentInfo continentInfo = new ContinentInfo();
            Dictionary<string, int> span = null;
            do
            {
				if (true == ProcessEnd)
                {
                    return;
                }
                form.setMessage(excelOperation.getSheetName() + " sheet:\nRead continent info, please wait……");
                continentStr = excelOperation.getCellValue(modelStartPosition["row"] + 1, modelStartPosition["col"] + modelList.Count + colIndex);
                if (tempContinentStr != continentStr && continentStr!="")
                {
                    span = excelOperation.getSpan(modelStartPosition["row"] + 1, modelStartPosition["col"] + modelList.Count + colIndex);
                    continentInfo.name = continentStr;
                    continentInfo.colSpan = span["colspan"];
                    if (continentList.Count == 0)
                    {
                        continentList.Add(continentInfo);
                    }
                    for (int index = 0; index < continentList.Count; index++)
                    {
                        if(continentList[index].name == continentInfo.name)
                        {
                            break;
                        }
                        else if (index == continentList.Count - 1)
                        {
                            continentList.Add(continentInfo);
                        }
                    }
                    tempContinentStr = continentStr;
                }
                colIndex++;
            }
            while (continentStr != "");
        }

        protected override void getCountry(int row, int col, int spanCol) 
        {
            countryList = new List<string>();
            int endCol = col + spanCol;
            string countryStr = "";
            
            for (; col < endCol; col++)
            {
				if (true == ProcessEnd)
                {
                    break;
                }
                form.setMessage(excelOperation.getSheetName() + " sheet:\nRead country info, please wait……");
				
                countryStr = excelOperation.getCellValue(row, col);
                if ((true == countryStr.Contains("(")) && (true == countryStr.Contains(")")))
                {
                    countryStr = countryStr.Remove(countryStr.IndexOf("("));
                }
                countryList.Add(countryStr);
            }
        }

        protected override void findMccInfo(FTBMccInfo ftbMccAttri)
        {
            ModelData modelData = new ModelData();
            ContinentCountryData continentCountryData = new ContinentCountryData();
            CountryData countryData = new CountryData();
            int countryColIndex = 0;
            int countryMccIndex;
            countryMccIndex = modelList.Count;
            
            for (int index = 0; index < continentList.Count || index < modelList.Count; index++)
            {
				if (true == ProcessEnd)
                {
                    return;
                }
                form.setMessage(excelOperation.getSheetName() + " sheet:\nRead mccInfo, please wait……");
                string mcc = "0000000000000000000000000000000000000000000000000000000000000000";
                StringBuilder sbModel = new StringBuilder(mcc);
                sbModel.Replace("0", "1", index, 1);
                //add model data to model_list
                if (index < modelList.Count)
                {
                    modelData.model_name = modelList[index];
                    modelData.model_mcc = Convert.ToInt64(sbModel.ToString(), 2) + "";
                    ftbMccAttri.modeldatalist.Add(modelData);
                }
                //add model data to continent_list
                if (index < continentList.Count)
                {
                    //getCountry(modelStartPosition["row"] + 3, modelStartPosition["col"] + countryColIndex, continentList[index].colSpan);
                    //countryColIndex=countryColIndex+ continentList[index].colSpan;
                    getCountry(modelStartPosition["row"] + 2, modelStartPosition["col"] + modelList.Count + countryColIndex, continentList[index].colSpan);
                    countryColIndex = countryColIndex + continentList[index].colSpan;
                    continentCountryData.country_name = new List<string>();
                    for (int countryIndex = 0; countryIndex < countryList.Count; countryIndex++)
                    {
                        StringBuilder sbCountry = new StringBuilder(mcc);
                        sbCountry.Replace("0", "1", countryMccIndex++, 1);
                        countryData.country_name = countryList[countryIndex];
                        countryData.country_mcc = Convert.ToInt64(sbCountry.ToString(), 2) + "";
                        ftbMccAttri.countrydatalist.Add(countryData);
                        continentCountryData.country_name.Add(countryList[countryIndex]);
                    }
                    continentCountryData.continent_name = continentList[index].name;
                    ftbMccAttri.continentcountrydatalist.Add(continentCountryData);
                }
            }
            ftbMccAttri.supportModelCountryDataList = findModelXCountry();
        }

        protected override void getModelInfoColumn()
        {
            int tempCols = 0;
            int index = 0;
            Sel_Model_Column = Sel_Model_Index;

            foreach (ContinentInfo AreaInfo in continentList)
            {
                if (index == Sel_Continent_Index)
                {
                    break;
                }
                index++;

                tempCols += AreaInfo.colSpan;
            }

            Sel_Country_Column = modelList.Count + tempCols + Sel_Country_Index;
        }

        protected override void getCondition(List<string> conditionList)
        {
            conditionList.Add("no condition");
            string strTempCondition = "";
            List<string> tempconditionlist = new List<string>();
            for (int rowIndex = levelStartPosition["row"] + 3; rowIndex < endPosition["row"]; rowIndex++)
            {
                form.setMessage(excelOperation.getSheetName() + " sheet:\nRead condition " + Math.Round(100.0 * (rowIndex + 1) / endPosition["row"], 2) + "%");
                for (int colIndex = levelStartPosition["col"]; colIndex <= optionPosition["col"]; colIndex = colIndex + 5)
                {
					if (true == ProcessEnd)
					{
						return;
					}

                    strTempCondition = excelOperation.getCellValue(rowIndex, colIndex);
                    if (true == string.IsNullOrEmpty(strTempCondition))
                    {
                        continue;
                    }

                    if (false == conditionList.Contains(strTempCondition))
                    {
                        conditionList.Add(strTempCondition);
                    }
                }
            }
            form.setMessage(excelOperation.getSheetName() + " sheet:\nRead condition end, please wait……");
        }

        protected override void getButton(LevelNode levelNode, List<string> conditionList)
        {
            OptionNode optionNode = new OptionNode();
            List<LevelNode> levelList = new List<LevelNode>();
            int ret=0, leveln = 0, lastIndexFlag = -1;
            levels = getLevels();
            levelList.Add(levelNode);
            List<string> tempconditionlist = new List<string>();
            Dictionary<string, int> funcSetFlagPostion = getFunSetFlagPosition();

            Dictionary<string, int> ItemSpan;
            bool ItemVisible = false;

            string conditionvalue = "";

            string sheetName = excelOperation.getSheetName();

            //new count levels LevelNode
            for (int levelIndex = 1; levelIndex < levels + 1; levelIndex++)
            {
                LevelNode templevel = new LevelNode();
                levelList.Add(templevel);
            }
            if (yellowCell == 0)
            {
                
            }
            //yellowcell=-1
            else
            {
                lastIndexFlag = -1;
                for (int row = levelStartPosition["row"] + 3; row < endPosition["row"]; row++)
                {
					if (true == ProcessEnd)
					{
						return;
					}
                    form.setMessage(excelOperation.getSheetName() + " sheet:\nRead button info " + Math.Round(100.0 * (row + 1) / endPosition["row"], 2) + "%");
                    leveln = 1;
                    ret = 0;
                    for (int i = levelStartPosition["col"]; i <= optionPosition["col"]; i = i + 5)
                    {
                        if (true == ProcessEnd)
						{
							return;
						}
                        //if us_words != "" or col = option_start_col
                        if (excelOperation.getCellValue(row, i + 1) != "" || i == optionPosition["col"])
                        {
                            /************************/
                            /* 非表示の項目をスキップ */
                            /*↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓*/
                            ItemSpan = excelOperation.getSpan(row, i + 1);

                            ItemVisible = false;
                            for (int tempIndex = 0; tempIndex < ItemSpan["rowspan"]; tempIndex++)
                            {
                                if (true == excelOperation.getCellValue(row + tempIndex, Sel_Model_Column).Equals("Y"))
                                {
                                    ItemVisible = true;
                                    break;
                                }
                            }

                            /* モデル下非表示の場合 */
                            if (false == ItemVisible)
                            {
                                row = row + ItemSpan["rowspan"] - 1;
                                break;
                            }

                            ItemVisible = false;
                            for (int tempIndex = 0; tempIndex < ItemSpan["rowspan"]; tempIndex++)
                            {
                                if (true == excelOperation.getCellValue(row + tempIndex, Sel_Country_Column).Equals("Y"))
                                {
                                    ItemVisible = true;
                                    break;
                                }
                            }

                            /* Country下非表示の場合 */
                            if (false == ItemVisible)
                            {
                                row = row + ItemSpan["rowspan"] - 1;
                                continue;
                            }

                            /*↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑*/
                            /* 非表示の項目をスキップ */
                            /************************/

                            //this tc not a MergeCells
                            if (levelList[leveln].ftbbutton.us_words == "" || excelOperation.getCellTopRow(row, i + 1) == row)
                            {
                                /* options以外 */
                                if (i != optionPosition["col"])
                                {
                                    levelList[leveln] = new LevelNode();

                                    conditionvalue = excelOperation.getCellValue(row, i);//condition info
                                    if (false == string.IsNullOrEmpty(conditionvalue))
                                    {
                                        levelList[leveln].ftbbutton.condition_index = conditionList.FindIndex(s => s.Equals(conditionvalue));
                                    }
                                    levelList[leveln].ftbbutton.us_words = excelOperation.getCellValue(row, i + 1).Replace("\n", " ");
                                    levelList[leveln].ftbbutton.string_id = excelOperation.getCellValue(row, i + 2);
                                    levelList[leveln].ftbbutton.next_scrn_id = excelOperation.getCellValue(row, i + 3);
                                    levelList[leveln].ftbbutton.cur_Level = leveln;

                                    if (1 < leveln)
                                    {
                                        levelList[leveln].ftbbuttontitle.us_words = levelList[leveln - 1].ftbbutton.us_words.Replace("\n", " ");
                                        levelList[leveln].ftbbuttontitle.string_id = levelList[leveln - 1].ftbbutton.string_id;
                                        levelList[leveln].ftbbuttontitle.cur_scrn_id = levelList[leveln - 1].ftbbutton.next_scrn_id;
                                    }

                                    levelList[leveln - 1].Add(levelList[leveln]);
                                }
                                else
                                {
                                    optionNode = new OptionNode();
                                    string mcc = "";
                                    for (int l = 0; l < modelList.Count; l++)
                                    {
                                        mcc = mcc + "1";
                                    }
                                    conditionvalue = excelOperation.getCellValue(row, i);//condition info
                                    if (false == string.IsNullOrEmpty(conditionvalue))
                                    {
                                        //optionNode.ftboption.condition_index = conditionList.FindIndex(delegate (string s) { return s == tempconditionlist[index].Substring(2, tempconditionlist[index].Length - 2); }) + "";
                                        optionNode.ftboption.condition_index = conditionList.FindIndex(s => s.Equals(conditionvalue));
                                    }
                                    optionNode.ftboption.us_words = excelOperation.getCellValue(row, i + 1).Replace("\n", " ");
                                    optionNode.ftboption.string_id = excelOperation.getCellValue(row, i + 2);
                                    optionNode.ftboption.factory_setting = excelOperation.getCellValue(row, i + 4);
                                    optionNode.ftboption.comment = excelOperation.getCellValue(row, i + 6);
                                    optionNode.ftboption.rsp = excelOperation.getCellValue(row, funcSetFlagPostion["col"]);
                                    optionNode.ftboption.ews = excelOperation.getCellValue(row, funcSetFlagPostion["col"] + 1);
                                    optionNode.ftboption.cur_scrn_id = levelList[leveln - 1].ftbbutton.next_scrn_id;

                                    optionNode.ftbbuttontitle.us_words = levelList[leveln - 1].ftbbutton.us_words.Replace("\n", " ");
                                    optionNode.ftbbuttontitle.string_id = levelList[leveln - 1].ftbbutton.string_id;
                                    optionNode.ftbbuttontitle.cur_scrn_id = levelList[leveln - 1].ftbbutton.next_scrn_id;
                                    //set mcc info
                                    for (int j = 0; j <= levelStartPosition["col"] - 2; j++)
                                    {
                                        if (excelOperation.getCellValue(row, j) == "N")
                                        {
                                            mcc = mcc + "0";
                                        }
                                        else
                                        {
                                            mcc = mcc + "1";
                                        }
                                    }
                                    for (int j = 0; j < 64 - modelList.Count - (levelStartPosition["col"] - 2) - 1; j++)
                                    {
                                        mcc = mcc + "0";
                                        optionNode.ftboption.tc_mcc = Convert.ToInt64(mcc, 2) + "";
                                    }
                                    if (lastIndexFlag == 0)
                                    {
                                        optionNode.ftboption.cur_Level = leveln;
                                        levelList[leveln].Add(optionNode);
                                    }
                                    else
                                    {
                                        optionNode.ftboption.cur_Level = leveln - 1;
                                        levelList[leveln - 1].Add(optionNode);
                                    }
                                }
                            }
                            if (i != optionPosition["col"])
                            {
                                leveln++;
                                if (leveln == levelList.Count)
                                {
                                    leveln--;
                                    lastIndexFlag = 0;
                                }
                            }
                        }
                        else
                        {
                            //add option
                            if (ret == 1)
                                continue;
                            i = optionPosition["col"] - 5;
                            ret++;
                        }

                    }
                    lastIndexFlag = -1;
                }
            }
            form.setMessage(excelOperation.getSheetName() + " sheet:\nRead button info end, please wait……");
        }
    }
}
