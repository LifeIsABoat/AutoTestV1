using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace WordingLink
{
    class HmiStridFile : FileHelper
    {
        public string filePath = string.Empty;
        public List<string> fileData = null;

        public HmiStridFile(string filePath)
        {
            this.filePath = filePath;
        }

        public bool ReadFileData()
        {
            bool readSucceed = false;
            fileData = new List<string>();
            try
            {
                string fileContent = ReadFile(filePath);
                if (fileContent != string.Empty)
                {
                    string[] dataArray = fileContent.Replace("\r\n", "\r").Replace("\t", string.Empty).Split('\r');
                    if (dataArray.Length != 0)
                    {
                        IEnumerable<string> list = dataArray.Where(n => n.StartsWith("HS"));
                        foreach (string str in list)
                        {
                            fileData.Add(str.Replace(",", string.Empty));
                        }
                    }
                }
                if (fileData.Count != 0)
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

        public bool CheckFileData(DataTable data)
        {
            bool updateSucceed = false;
            bool errFlag = false;
            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (!fileData.Contains(data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString()))    //HMIStringID不存在
                    {
                        if (errFlag == false)
                        {
                            errFlag = true;
                            LogFile.WriteLog("[" + data.Rows[i][CONST.EXCEL_WORDING_SERIES].ToString() + "] [" + data.Rows[i][CONST.EXCEL_WORDING_MODEL].ToString() + "] の" +
                                    CONST.FILENAME_HMISTRID + "で以下のIDがが存在しません");
                        }
                        LogFile.WriteLog("[" + data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString() + "]");
                    }
                }
                updateSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return updateSucceed;
        }
    }

    class StridLinkFile : FileHelper
    {
        public string filePath = string.Empty;
        public string fileContent = string.Empty;
        public Dictionary<string, string> fileData = null;
        private List<string> nAStrList = new List<string>() { "【N/A】", "「N/A」", "｢N/A｣", "[N/A]", "N/A" };

        public StridLinkFile(string filePath)
        {
            this.filePath = filePath;
        }

        public bool ReadFileData()
        {
            bool readSucceed = false;
            fileData = new Dictionary<string, string>();
            try
            {
                fileContent = ReadFile(filePath);
                if (fileContent != string.Empty)
                {
                    string[] dataArray = fileContent.Replace("\r\n", "\r").Replace(";", string.Empty).Split('\r');
                    if (dataArray.Length != 0)
                    {
                        IEnumerable<string> list = dataArray.Where(n => n.StartsWith("HS"));
                        foreach (string str in list)
                        {
                            string[] strAarry = str.Split(',');
                            if (strAarry.Length == 2)
                            {
                                if (!fileData.ContainsKey(strAarry[0]))
                                {
                                    fileData.Add(strAarry[0], strAarry[1]);
                                }
                            }
                        }
                    }
                }
                if (fileData.Count != 0)
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

        public bool UpdateFileData(string saveFolder, DataTable data, ref bool updated)
        {
            bool updateSucceed = false;
            bool addExist = false;
            string savePath = Path.Combine(saveFolder, CONST.FILENAME_STRIDLINK);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            try
            {
                if (data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        //HMI String IDが「N/A」登録の文言処理,Linkファイル処理を不要とする(FIRM_ALL_MODEL-29699)
                        string hmiStringID = data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString();
                        if (CONST.EXCEL_COMMON_NA.Equals(hmiStringID) || nAStrList.Find(o => o == hmiStringID) != null)
                        {
                            continue;
                        }
                        if (fileData.ContainsKey(data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString()))
                        {
                            if (!fileData[data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString()].Equals(data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString()))
                            {
                                fileData[data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString()] = data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString();
                                addExist = true;
                            }
                        }
                        else
                        {
                            fileData.Add(data.Rows[i][CONST.EXCEL_WORDING_HMISTRINGID].ToString(), data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString());
                            addExist = true;
                        }
                    }
                    if (addExist)
                    {
                        string prefix = fileContent.Split(new string[] { ";" + fileData.First().Key }, StringSplitOptions.None)[0];
                        string subfix = ";HMI_STRID_SENTINEL,DSP_DATA_END";
                        string str = string.Empty;
                        foreach (KeyValuePair<string, string> pair in fileData)
                        {
                            if ((pair.Key != string.Empty) && (pair.Value != string.Empty))
                            {
                                str += ";" + pair.Key + "," + pair.Value + "\r\n";
                            }
                            else
                            {
                                LogFile.WriteLog("[" + data.Rows[0][CONST.EXCEL_WORDING_SERIES].ToString() + "] [" + data.Rows[0][CONST.EXCEL_WORDING_MODEL].ToString() + "] の" +
                                    CONST.FILENAME_STRIDLINK + "で[" + pair.Key + "] [" + pair.Value + "] を書込み失敗しました.");
                            }
                        }
                        if (!Directory.Exists(saveFolder))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(saveFolder);
                            directoryInfo.Create();
                        }
                        WriteFile(savePath, prefix + str + subfix);
                        updated = true;
                    }
                }
                updateSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return updateSucceed;
        }
    }

    class HinagataFile : FileHelper
    {
        public string filePath = string.Empty;
        public string fileContent = string.Empty;
        public Dictionary<string, string> fileData = null;

        public HinagataFile(string filePath)
        {
            this.filePath = filePath;
        }

        public bool ReadFileData()
        {
            bool readSucceed = false;
            fileData = new Dictionary<string, string>();
            try
            {
                fileContent = ReadFile(filePath);
                if (fileContent != string.Empty)
                {
                    string[] dataArray = fileContent.Replace("\r\n", "\r").Split('\r');
                    if (dataArray.Length != 0)
                    {
                        IEnumerable<string> list = dataArray.Where(n => n.StartsWith("MsgNo"));
                        foreach (string str in list)
                        {
                            string[] strAarry = str.Split(';');
                            if (strAarry.Length == 2)
                            {
                                if (!fileData.ContainsKey(strAarry[0]))
                                {
                                    fileData.Add(strAarry[0].Replace("\t", string.Empty), strAarry[1]);
                                }
                            }
                        }
                    }
                }
                if (fileData.Count != 0)
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

        public bool UpdateFileData(string saveFolder, DataTable data,ref bool updated)
        {
            bool updateSucceed = false;
            bool addExist = false;
            string savePath = Path.Combine(saveFolder, CONST.FILENAME_HINAGATA);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (fileData.ContainsKey(data.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString()))    //MsgNo存在する
                    {
                        if (!fileData[data.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString()].Equals(data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString()))
                        {
                            //StringID不一致、log Fileに出力されます
                            LogFile.WriteLog("[" + data.Rows[i][CONST.EXCEL_WORDING_SERIES].ToString() + "] [" + data.Rows[i][CONST.EXCEL_WORDING_MODEL].ToString() + "] の" +
                                CONST.FILENAME_HINAGATA + "で[" + data.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString() + "] のStringIDは不一致");
                        }
                    }
                    else
                    {
                        if (fileData.ContainsValue(data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString()))
                        {
                            //同じのStringIDが存在し、log Fileに出力されます,Hinagata File Dataに追加しない
                            LogFile.WriteLog("[" + data.Rows[i][CONST.EXCEL_WORDING_SERIES].ToString() + "] [" + data.Rows[i][CONST.EXCEL_WORDING_MODEL].ToString() + "] の" +
                                CONST.FILENAME_HINAGATA + "で[" + data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString() + "] はもう存在しています");
                        }
                        else
                        {   // 同じのStringIDが存在しない、Hinagata File Dataに追加する
                        fileData.Add(data.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString(), data.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString());
                        addExist = true;
                        }
                    }
                }
                if (addExist)
                {
                    string prefix = fileContent.Split(new string[] { fileData.First().Key }, StringSplitOptions.None)[0];
                    string subfix = string.Empty;
                    Dictionary<string, string> dict = fileData.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);//MsgNo升序排序
                    foreach (KeyValuePair<string, string> pair in dict)
                    {
                        if ((pair.Key != string.Empty) && (pair.Value != string.Empty))
                        {
                            subfix += pair.Key + "\t" + ";" + pair.Value + "\r\n";
                        }
                        else
                        {
                            LogFile.WriteLog("[" + data.Rows[0][CONST.EXCEL_WORDING_SERIES].ToString() + "] [" + data.Rows[0][CONST.EXCEL_WORDING_MODEL].ToString() + "] の" +
                                CONST.FILENAME_HINAGATA + "で[" + pair.Key + "] ["+ pair.Value + "] を書込み失敗しました.");
                        }
                    }
                    if (!Directory.Exists(saveFolder))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(saveFolder);
                        directoryInfo.Create();
                    }
                    WriteFile(savePath, prefix + subfix);
                    updated = true;
                }
                updateSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return updateSucceed;
        }
    }
}
