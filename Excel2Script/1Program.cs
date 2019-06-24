using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ScriptExcel2ScriptCSV
{
    class Program
    {
        static int inum = 0;
        static int TCCountMax = 0;
        static void Main(string[] args)
        {
            if (0 >= args.Length)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                string strPath = args[0];
                TCCountMax = GetTCCountMax();
                if (true == File.Exists(strPath))
                {
                    File_ScriptExcel2ScriptCSV(strPath);
                }
                else if (true == Directory.Exists(strPath))
                {
                    Directory_ScriptExcel2ScriptCSV(strPath);

                    if (0 == inum)
                    {
                        Console.WriteLine("Excel file is not exist");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid args: " + strPath);
                }
            }

            Console.WriteLine("続行するには何かキーを押してください . . .");

            Console.ReadKey();
        }

        private static void File_ScriptExcel2ScriptCSV(string strFile)
        {
            string CSVStorePath = "";
            string CSVFileName = "";
            if ((true == Path.GetExtension(strFile).ToLower().Equals(".xls")) || (true == Path.GetExtension(strFile).ToLower().Equals(".xlsx")))
            {
                Console.WriteLine(strFile + " Convert to csv file");
                CSVStorePath = Path.GetDirectoryName(strFile) + "\\CSV";
                CSVFileName = Path.GetFileNameWithoutExtension(strFile);

                CSVFileHelper.Excel2CSV(strFile, CSVStorePath, CSVFileName, TCCountMax, Encoding.UTF8, false);

                inum++;
            }
            else if (true == Path.GetExtension(strFile).ToLower().Equals(".csv"))
            {
                /* Do nothing */
            }
            else
            {
                Console.WriteLine(strFile + " is not a excel file");
            }
        }

        private static void Directory_ScriptExcel2ScriptCSV(string strDir)
        {
            DirectoryInfo folder = new DirectoryInfo(strDir);

            if(false == Directory.Exists(strDir))
            {
                Console.WriteLine("DirectoryInfo is not exist:" + strDir);
                return;
            }

            foreach (FileInfo file in folder.GetFiles())
            {
                File_ScriptExcel2ScriptCSV(file.FullName);
            }

            foreach (DirectoryInfo tempfolder in folder.GetDirectories())
            {
                Directory_ScriptExcel2ScriptCSV(tempfolder.FullName);
            }
        }

        /*  每一个csv脚本文件中TC个数的最大值*/
        private static int GetTCCountMax()
        {
            int DefaultCountMax = 100;
            string strConfigFile = AppDomain.CurrentDomain.BaseDirectory + "setting.ini";
            if (false == File.Exists(strConfigFile))
            {
                Console.WriteLine("Warning:configuration file is not exist\n" + strConfigFile);
            }
            else
            {
                InitHelper iniSettings = new InitHelper(strConfigFile);
                int max = 0;
                string strCount = iniSettings.Get("INPUT", "tcCountMax");
                return (true == int.TryParse(strCount, out  max)) ? max : 100;
            }

            return DefaultCountMax;
        }
    }
}
