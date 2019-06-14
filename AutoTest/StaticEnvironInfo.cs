using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tool
{
    static class StaticEnvironInfo
    {
        //Ocr flag
        private static bool ocrFlag = true;
        private static bool testTPFirmFlag = false;
        private static bool testTPBvboardFlag = false;

        private static bool menuItemTestFlag = false;

        //string check ignore case
        private static bool ignoreCase = false;
        //
        private static List<string> profileNameList = new List<string>();

        //project work path
        private static string modelPath = "";
        private static string filePath = "";
        private static string rootPath = "";
        private static string logPath = "";

        //Machine Use
        private static string machineConfigFileName = @"MFCTPConfig.ini";

        //Log Use
        private static string log4NetConfigFileName = @"log4net.config";

        //Report Use
        private static string reportSavedFileName = @"Reprot.xlsm";
        private static string reportTemplateFileName = @"ReprotTemplate.xlsm";
        //Engine Use
        private static string tcWordFileName = @"TC.txt";
        private static string conditionCalibratelFileName = @"ConditionCalibration";
        //ScreenTest Use
        private static string selectedStandardScreenFileName = @"SelectedStandardScreen";
        private static string totalStandardScreenFileName = @"TotalStandardScreen";
        private static string correspondingTableFileName = @"CorrespondingTable.txt";

        private static string menuTcOpinionFileName = @"MenuOpinionInfo.txt";
        private static string tempTcOpinionFileName = @"TempOpinionInfo.txt";

        private static string modelInfoFileName = @"ModelInfo.txt";
        private static string opinionBlackListFileName = @"OpinionBlackList.txt";

        private static string cameraShareMemoryName = "ShareMemoryToCameraApp";
        private static uint cameraShareMemorySize = 1024;
        private static string cameraReadSemaphoreName = "CameraAppToControlAppSEM";
        private static string cameraWriteSemaphoreName = "ControlAppToCameraAppSEM";
        private static string cameraConfigFileName = @"CameraConfig.xml";
        private static string currentCameraImagePath = "";
        private static int cameraImageIndex = 0;

        private static string ocrShareMemoryName = "ShareMemoryToOcrApp";
        private static uint ocrShareMemorySize = 4096 * 1024;
        private static string ocrReadSemaphoreName = "OcrAppToControlAppSEM";
        private static string ocrWriteSemaphoreName = "ControlAppToOcrAppSEM";
        private static string ocrConfigFileName = @"OCRConfig.txt";

        private const string documentShareMemoryName = "ShareMemoryToDocumentComparatorApp"; //sharememory name
        private const string documentWriteSmpName = "ControlAppToDocumentAppSEM";
        private const string documentReadSmpName = "DocumentAppToControlAppSEM";
        private static uint documentShareMemorySize = 1024;   //size of memory

        private static string ewsAndRspConfigfilename = "EWSAndRSPOptionOperator.ini";
        private static string countriesConfigName = "Countries74CodeConfig.ini";
        //standardButton image path
        private const string standardButtonImage = "StandardImage.png";

        public static void setTPFirmTestFlag(bool flag)
        {
            testTPFirmFlag = flag;
        }
        public static bool isTPFirmTested()
        {
            return testTPFirmFlag;
        }
        public static void setTPBvboardTestFlag(bool flag)
        {
            testTPBvboardFlag = flag;
        }
        public static bool isTPBvboardTested()
        {
            return testTPBvboardFlag;
        }
        public static void setOcrUsedFlag(bool enableFlag)
        {
            ocrFlag = enableFlag;
        }
        public static bool isOcrUsed()
        {
            return ocrFlag;
        }
        public static void setIgnoreCase(bool enableFlag)
        {
            ignoreCase = enableFlag;
        }
        public static bool isIgnoreCase()
        {
            return ignoreCase;
        }
        //setMenuItemTestFlag
        public static void setMenuItemTestFlag(bool flag)
        {
            menuItemTestFlag = flag;
        }
        public static bool isMenuItemTested()
        {
            return menuItemTestFlag;
        }
        //Machine Use
        public static string getMachineConfigFileName()
        {
            if (!File.Exists(modelPath + @"\" + machineConfigFileName))
                throw new FTBAutoTestException("Report template file isn't exsited.");
            return modelPath + @"\" + machineConfigFileName;
        }
        public static void setFilePath(string filePath)
        {
            StaticEnvironInfo.filePath = filePath;
        }
        public static string getIntPutFilePath()
        {
            return filePath;
        }
        //set work path
        public static void setPath(string modelPath, string rootPath)
        {
            StaticEnvironInfo.modelPath = modelPath;
            StaticEnvironInfo.rootPath = rootPath;

        }
        //Log Use
        public static string getLog4NetConfigFileName()
        {
            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);
            return modelPath + @"\" + log4NetConfigFileName;
        }
        public static string getLogPathRoot()
        {
            if (rootPath == null)
                throw new FTBAutoTestException("Get log path error by null root path.");
            //it while step in if rootPath equals "" or rootPath changed
            if (!logPath.Contains(rootPath))
            {
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                string model = BLL.TestRuntimeAggregate.getTreeMemory().getSelectModel();
                string country = BLL.TestRuntimeAggregate.getTreeMemory().getSelectCountry();
                logPath = rootPath + @"\Output\" + time
                    + @"_" + model.Trim(new Char[] { ' ' }) + @"_" + country;
                BLL.TestRuntimeAggregate.setCurrentTime(time);
            }
            return logPath;
        }
        public static string getMachineLogPath(int tcIndex)
        {
            string path = getLogPathRoot() + @"\MFCLog\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() + @"\TC_" + tcIndex;
            return path;
        }
        public static string getMachineLogPath()
        {
            string path = getLogPathRoot() + @"\MFCLog\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() + @"\TC_" 
                + BLL.TestRuntimeAggregate.getCurrentTcIndex();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        public static string getMachineLogFileName()
        {
            string path = getMachineLogPath();
            return path + @"\Level_" + BLL.TestRuntimeAggregate.getCurrentLevelIndex() + @".log";
        }
        public static string getCommandLogPath(int tcIndex)
        {
            string path = getLogPathRoot() + @"\CommandExecutorLog\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() 
                + @"\TC_" + tcIndex;
            return path;
        }
        public static string getCommandLogPath()
        {
            string path = getLogPathRoot() + @"\CommandExecutorLog\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() 
                + @"\TC_" + BLL.TestRuntimeAggregate.getCurrentTcIndex();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        public static string getCommandLogFileName()
        {
            string path = getCommandLogPath();
            return path + @"\Level_" + BLL.TestRuntimeAggregate.getCurrentLevelIndex() + @".log";
        }
        public static string getOcrLogPath(int tcIndex)
        {
            string path = getScreenImagePath(tcIndex).Replace("ScreenImage", "OCRLog");
            return path;
        }
        public static string getOcrLogPath()
        {
            string path = getScreenImagePath().Replace("ScreenImage", "OCRLog");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string getScreenImagePath(int tcIndex)
        {
            string path = logPath + @"\ScreenImage\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() 
                + @"\TC_" + tcIndex;
            return path;
        }
        public static string getScreenImagePath()
        {
            string path = logPath + @"\ScreenImage\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName() 
                + @"\TC_" + BLL.TestRuntimeAggregate.getCurrentTcIndex();
            if (BLL.TestOneTCStatus.Transfering == BLL.TestRuntimeAggregate.getCurrentTCStatus())
                path += @"\Level_" + BLL.TestRuntimeAggregate.getCurrentLevelIndex();
            else if (BLL.TestOneTCStatus.OptionChecking == BLL.TestRuntimeAggregate.getCurrentTCStatus())
                path += @"\Opinion_" + BLL.TestRuntimeAggregate.getCurrentOpinionIndex();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                cameraImageIndex = 0;
            }
            if (path != currentCameraImagePath)
            {
                currentCameraImagePath = path;
                cameraImageIndex = 0;
            }
            return path;
        }
        public static string getScreenImageFileName()
        {
            string path = getScreenImagePath();
            return path + @"\IMG_" + (cameraImageIndex++) + @".png";
        }

        public static string getReportLogFileName()
        {
            string reportPath = getLogPathRoot();
            if (!Directory.Exists(reportPath))
                Directory.CreateDirectory(reportPath);
            return reportPath + @"\Report.log";
        }
        

        //Report Use
        public static string getFlowGraphFullPath()
        {
            string path = getLogPathRoot() + @"\ScreenImage\" + BLL.TestRuntimeAggregate.getcurrentTcManagerName()
                + @"\TC_" + BLL.TestRuntimeAggregate.getCurrentTcIndex();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string getReportSaveFullFileName()
        {
            string reportSavedPath = getLogPathRoot();
            if (!Directory.Exists(reportSavedPath))
                Directory.CreateDirectory(reportSavedPath);
            return reportSavedPath + @"\" + reportSavedFileName;
        }
        public static string getReportTemplateFullFileName()
        {
            string fileName = rootPath + @"\Output\" + reportTemplateFileName;
            if (!File.Exists(fileName))
                throw new FTBAutoTestException("Report template file isn't exsited.");
            return fileName;
        }

        //Engine Use
        public static string getTcWordFullFileName()
        {
            return modelPath + @"\" + tcWordFileName;
        }
        public static string getConditionCalibrationFullFileName()
        {
            return modelPath + @"\ConditionCalibration\" + conditionCalibratelFileName;
        }
        public static string getMenuTcOpinionFullFileName()
        {
            return modelPath + @"\" + menuTcOpinionFileName;
        }
        public static string getTempTcOpinionFullFileName()
        {
            return modelPath + @"\" + tempTcOpinionFileName;
        }
        public static string getModelInfoFullFileName()
        {
            return modelPath + @"\" + modelInfoFileName;
        }
        public static string getOpinionBlackListFullFileName()
        {
            return modelPath + @"\" + opinionBlackListFileName;
        }

        public static string getCameraShareMemoryName()
        {
            return cameraShareMemoryName;
        }
        public static uint getCameraShareMemorySize()
        {
            return cameraShareMemorySize;
        }
        public static string getCameraReadSemaphoreName()
        {
            return cameraReadSemaphoreName;
        }
        public static string getCameraWriteSemaphoreName()
        {
            return cameraWriteSemaphoreName;
        }
        public static string getCameraConfigFileName()
        {
            if (!File.Exists(modelPath + @"\" + cameraConfigFileName))
                throw new FTBAutoTestException("Camera config file isn't exsited.");
            return modelPath + @"\" + cameraConfigFileName;
        }

        public static string getOcrShareMemoryName()
        {
            return ocrShareMemoryName;
        }
        public static uint getOcrShareMemorySize()
        {
            return ocrShareMemorySize;
        }
        public static string getOcrReadSemaphoreName()
        {
            return ocrReadSemaphoreName;
        }
        public static string getOcrWriteSemaphoreName()
        {
            return ocrWriteSemaphoreName;
        }
        public static string getOcrConfigFileName()
        {
            if (!File.Exists(modelPath + @"\" + ocrConfigFileName))
                throw new FTBAutoTestException("OCR config file isn't exsited.");
            return modelPath + @"\" + ocrConfigFileName;
        }

        public static string getDocumentShareMemoryName()
        {
            return documentShareMemoryName;
        }
        public static uint getDocumentShareMemorySize()
        {
            return documentShareMemorySize;
        }
        public static string getDocumentReadSemaphoreName()
        {
            return documentReadSmpName;
        }
        public static string getDocumentWriteSemaphoreName()
        {
            return documentWriteSmpName;
        }
        
        public static string getIntPutModelPath()
        {
            return modelPath;
        }
        public static string getDocumentewsAndRspConfigfilename()
        {
            return ewsAndRspConfigfilename;
        }

        public static string getStandardButtonImage()
        {
            return modelPath + @"\" + standardButtonImage;
        }
        //countriesConfigName
        public static string getCountriesConfigName()
        {
            return countriesConfigName;
        }
        public static string getSelectedStandardScreenFileName()
        {
            string model = BLL.TestRuntimeAggregate.getTreeMemory().getSelectModel();
            string country = BLL.TestRuntimeAggregate.getTreeMemory().getSelectCountry();
            string[] args = BLL.TestRuntimeAggregate.getFormArguments();
            if (args != null)
            {
                List<string> list = new List<string>(args);
                for (int r = 0; r < list.Count(); r++)
                {
                    if (list[r].Contains("SelectedStandardScreen"))
                    {
                        return list[r];
                    }
                }
            }
            return modelPath + @"\SelectedStandardScreen\" + model.Trim(new Char[] { ' ' })
                + @"-" + country + @"-" + selectedStandardScreenFileName + ".txt";
        }
        public static string getTotalStandardScreenFileName()
        {
            return modelPath + @"\TotalStandardScreen\" + totalStandardScreenFileName;
        }
        public static string getCorrespondingTableFileName()
        {
            return modelPath + @"\" + correspondingTableFileName;
        }
        public static void setProfileName(List<string> list)
        {
            profileNameList.AddRange(list);
        }
        public static List<string> getProfileName()
        {
            return profileNameList;
        }
    }
}
