using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordingLink
{
    public class FileHelper
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        public string ReadFile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return string.Empty;
            }
            try
            {
                using (FileStream fs = File.OpenRead(filepath))
                {
                    StreamReader reader = new StreamReader(fs, Encoding.Default);
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        public void WriteFile(string filepath, string content)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                fs.SetLength(0);
                sw.Write(content);
                sw.Flush();
                fs.Close();
            }
        }
    }

    public class LogFile
    {
        public static bool writeAlready = false;

        public static void WriteLog(string msg)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,CONST.FILEPATH_LOG);

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    if (!writeAlready)
                    {
                        writeAlready = true;
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine("********************" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "********************");
                    }

                    sw.WriteLine(DateTime.Now.ToString("[HH:mm:ss]") + " " + msg);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine();
                    sw.WriteLine("**************************************************");
                    sw.WriteLine("日志记录异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }
}
