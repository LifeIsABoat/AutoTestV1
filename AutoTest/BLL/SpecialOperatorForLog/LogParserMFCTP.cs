using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    /*
     *  Description: Log parser specific implementation class
     */
    class LogParserMFCTP
    {
        public const string UnKnown = "UnKnown";
        public const string TS_PN_KEY = "TS:PN:KEY";
        public const string TS_PN_TCH = "TS:PN:TCH";
        public const string TS_PR = "TS:PR";
        public const string TS_ER = "TS:ER";
        public const string TS_DP_KEY = "TS:DP:KEY";
        public const string TS_DP_TCH = "TS:DP:TCH";
        public const string TS_SC_SCID = "TS:SC:SCID";
        public const string TS_SC_SN = "TS:SC:SN";
        public const string TS_WD_BTN = "TS:WD:BTN";
        public const string TS_LS_USL = "TS:LS:USL";
        public const string TS_SCER_StrID = "TS:SCER:StrID";
        public const string TS_SCER_TRNone = "TS:SCER:TRNone";
        public const string TS_SCER_StrOver = "TS:SCER:StrOver";
        public const string TS_SC_INFO = "TS:SC:INFO";
        public const string TS_SC_INFO_Part1 = "TS:SC:INFO:Part1";
        public const string TS_SC_INFO_Part2 = "TS:SC:INFO:Part2";
        public const string TS_SC_INFO_Part3 = "TS:SC:INFO:Part3";
        public const string TS_SC_INFO_Part4 = "TS:SC:INFO:Part4";
        public const string TS_SDUI_SND = "TS:SDUI:SND";

        /*
         *  Description: 
         *  Param: buf - 
         *  Return: bool - yes is true, no is false 
         *  Exception: 
         *  Example: if(!isScrInfoFinished(buf));
         */
        public static bool isScrInfoFinished(string buf)
        {
            bool ret = false;
            int index = 0;
            string[] lineData = buf.Split('\n');
            while (index < lineData.Length)
            {
                if (lineData[index].Length > 1 && lineData[index].Substring(0, 2).Contains(")]"))
                {
                    ret = true;
                }
                index++;
            }
            return ret;
        }
        /*
         *  Description: 
         *  Param: buf - 
         *  Return: bool - yes is true, no is false 
         *  Exception: 
         *  Example: if(!isScrIdFinished(buf));
         */
        public static bool isScrIdFinished(string buf)
        {
            bool ret = false;
            int index = 0;
            string pattern = @"\[" + TS_SC_SCID + @"\((\w+),(\d+)\)\]";
            string[] lineData = buf.Split('\n');
            while (index < lineData.Length)
            {
                if (Regex.IsMatch(lineData[index], pattern))
                {
                    ret = true;
                }
                index++;
            }
            return ret;
        }
        /*
         *  Description: 
         *  Param: buf - 
         *  Return: bool - yes is true, no is false 
         *  Exception: 
         *  Example: if(!isScrNameFinished(buf));
         */
        public static bool isScrNameFinished(string buf)
        {
            bool ret = false;
            int index = 0;
            string pattern = @"\[" + TS_SC_SN + @"\((\w+),(\d+)\)\]";
            string[] lineData = buf.Split('\n');
            while (index < lineData.Length)
            {
                if (Regex.IsMatch(lineData[index], pattern))
                {
                    ret = true;
                }
                index++;
            }
            return ret;
        }

        public void parse(string logContent, LogScreen logScreen)
        {
            string[] rowsString;
            rowsString = logContent.Split('\n');
            if (!parseScrInfo(ref logScreen, rowsString))
            {
                throw new FTBAutoTestException("The log information that needs to be parsed is incomplete");
            }
        }

        /*
         *  Description: parsing screen info log,fill in the to log information class
         *  Param: logScreenInfo - A reference to the log information class
         *  Param: logContent - Log content by line distinguish
         *  Return: bool Filled successfully true, Filled failed false
         *  Exception: 
         *  Example:parseScrInfo(ref logScreenInfo,rowString);
         */
        private bool parseScrInfo(ref LogScreen logScreenInfo, string[] logContent)
        {
            string text_line = null;
            string[] splitWords;
            int contentLen = logContent.Length;
            int index = 0;
            ushort parseValueUInt16 = 0;
            int parseValueInt32 = 0;

            logScreenInfo.scrid = getScrName(logContent, ref index);

            while (index < contentLen)//Judgment begins
            {
                text_line = logContent[index];
                index++;
                if (text_line.Contains(@"[" + TS_SC_INFO + @"2("))
                    break;
            }
            if (index == contentLen)  // no screen info
            {
                return false;
            }
            //*
            //extract button number in button list
            text_line = logContent[index];
            index++;
            if (index == contentLen)
            {
                return false;
            }
            splitWords = text_line.Split(',');
            if (splitWords.Length != 2)
            {
                return false;
            }

            if (false == Int32.TryParse(splitWords[0], out parseValueInt32))
                return false;
            logScreenInfo.btn_num_inlist = parseValueInt32;

            //*
            //break for useless line
            index++;

            //*
            //parse controls
            int ctlIndex = 0;
            logScreenInfo.ctls = new List<LogControl>();
            string pattern = @"^<([\w\W]+)>";
            while (index < contentLen)
            {
                text_line = logContent[index];
                index++;
                if (text_line.Contains(@")]"))
                    return true;
                //fill list
                if (Regex.IsMatch(text_line, pattern))
                {
                    Match m1 = Regex.Match(text_line, pattern);
                    string tmp_str = m1.Groups[1].Value;
                    splitWords = tmp_str.Split(',');
                    if (10 != splitWords.Count())
                        return false;
                    logScreenInfo.ctls.Add(new LogControl());

                    if (false == UInt16.TryParse(splitWords[0], out parseValueUInt16))
                        return false;
                    logScreenInfo.ctls[ctlIndex].layerID = parseValueUInt16;
                    if (false == UInt16.TryParse(splitWords[1], out parseValueUInt16))
                        return false;
                    logScreenInfo.ctls[ctlIndex].dataHolderID = parseValueUInt16;

                    logScreenInfo.ctls[ctlIndex].ctl_type = splitWords[2];

                    if (false == Int32.TryParse(splitWords[3], out parseValueInt32))
                        return false;
                    logScreenInfo.ctls[ctlIndex].x = parseValueInt32;
                    if (false == Int32.TryParse(splitWords[4], out parseValueInt32))
                        return false;
                    logScreenInfo.ctls[ctlIndex].y = parseValueInt32;
                    if (false == Int32.TryParse(splitWords[5], out parseValueInt32))
                        return false;
                    logScreenInfo.ctls[ctlIndex].w = parseValueInt32;
                    if (false == Int32.TryParse(splitWords[6], out parseValueInt32))
                        return false;
                    logScreenInfo.ctls[ctlIndex].h = parseValueInt32;

                    logScreenInfo.ctls[ctlIndex].btnStatus = splitWords[7];
                    if (splitWords[8] == "None")
                    {
                        logScreenInfo.ctls[ctlIndex].istrHMIid = 0;
                    }
                    else
                    {
                        if (false == UInt16.TryParse(splitWords[8], out parseValueUInt16))
                            return false;
                        logScreenInfo.ctls[ctlIndex].istrHMIid = parseValueUInt16;
                    }
                    if (splitWords[9] == "None")
                    {
                        logScreenInfo.ctls[ctlIndex].text = "";
                    }
                    else
                    {
                        string text = UnicodeToString(splitWords[9]);
                        if(null == text)
                            return false;
                        logScreenInfo.ctls[ctlIndex].text = text.Replace("\r\n", " ");
                    }
                    ctlIndex++;
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        /*
         *  Description: get current screen id
         *  Param: logContent - Log content by line distinguish
         *  Param: index - A reference to the log content index
         *  Return: string screen id
         *  Exception: 
         *  Example:scrid = getScrid(rowString, ref index);
         */
        private string getScrId(string[] logContent, ref int index)
        {
            //string pattern = @"^\[TS:SC:SCID\((\w+),(\d+)\)\]";
            string pattern = @"\[" + TS_SC_SCID + @"\((\w+),(\d+)\)\]";
            string scrid = null;
            string text_line;
            int contentLen = logContent.Length;

            while (index < contentLen)
            {
                text_line = logContent[index];
                index++;
                if (Regex.IsMatch(text_line, pattern))
                {
                    Match m1 = Regex.Match(text_line, pattern);
                    scrid = m1.Groups[1].Value;
                    break;
                }
            }

            return scrid;
        }
        private string getScrName(string[] logContent, ref int index)
        {
            //string pattern = @"^\[TS:SC:SCID\((\w+),(\d+)\)\]";
            string pattern = @"\[" + TS_SC_SN + @"\((\w+),(\d+)\)\]";
            string scrid = null;
            string text_line;
            int contentLen = logContent.Length;

            while (index < contentLen)
            {
                text_line = logContent[index];
                index++;
                if (Regex.IsMatch(text_line, pattern))
                {
                    Match m1 = Regex.Match(text_line, pattern);
                    scrid = m1.Groups[1].Value;
                    break;
                }
            }

            return scrid;
        }

        /*
         *  Description: Unicode to string
         *  Param: srcText - The Unicode type to convert
         *  Return: string Converts the resulting string type
         *  Exception: 
         *  Example:str = UnicodeToString(srcText);
         */


        //todo Change Parse to TryParse
        private string UnicodeToString(string srcText)
        {
            string dst = "";
            if (0 != srcText.Length % 4)
                return null;

            int len = srcText.Length / 4;
            for (int i = 0; i < len; i++)
            {
                string str = "";
                str = srcText.Substring(0, 4);
                srcText = srcText.Substring(4);
                byte[] bytes = new byte[2];
                byte parseValueByte = 0;

                if (false == Byte.TryParse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out parseValueByte))
                    return null;
                bytes[1] = parseValueByte;//byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                if (false == Byte.TryParse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out parseValueByte))
                    return null;
                bytes[0] = parseValueByte; //byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());

                try
                {
                    dst += Encoding.Unicode.GetString(bytes);
                }
                catch(ArgumentException)
                {
                    return null;
                }
            }
            return dst;
        }
    }

}
