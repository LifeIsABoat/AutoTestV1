using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tool.BLL;

namespace Tool.Engine
{
    //OCR Recognize
    class TextInfo
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string str { get; set; }
    }

    class OCRAnalyzeParam
    {
        public string imageDir { get; set; }
        public List<TextInfo> textList { get; set; }
    }

    //Image Difference
    class OCRDifferenceParam
    {
        public string standard { get; set; }
        public string current { get; set; }
    }

    //Cut Image 
    class ScreenParam
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string srcPath { get; set; }
        public string destPath { get; set; }
    }

    //Create hocr
    class OCRParam
    {
        public string srcPath { get; set; }
        public string destPath { get; set; }
        public string lang { get; set; }
        public string property { get; set; }
        public string resultType { get; set; }
        
    }

    //get Screen Similarity
    class ScreenSimilarity
    {
        public string path1 { get; set; }
        public string path2 { get; set; }
    }

    class ReverseRectangleCheck
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string imagePath { get; set; }
    }

    //Button Check
    class ImageInfo
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string status { get; set; }
    }

    class OCRBtnCheckParam
    {
        public string imagePath { get; set; }
        public List<ImageInfo> imageInfoList { get; set; }
    }

    //Check Result
    class ResultInfo
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string standard { get; set; }
        public string current { get; set; }
    }

    class OCRCheckResultParam
    {
        public string imagePath { get; set; }
        public List<ResultInfo> textList { get; set; }
    }

    class EngineOCR
    {
        private const string _STATUS_UNKNOW = "Unknow";
        private const string _STATUS_VALID = "Valid";
        private const string _STATUS_SELECTED = "Selected";
        private const string _STATUS_INVALID = "Invalid";
        static protected AbstractEngineCommunicator engineCommunicator;

        public EngineOCR()
        {
            if (null == engineCommunicator)
                throw new FTBAutoTestException("Create ocr engine error by null ");
        }

        public static void setEngineCommunicator(AbstractEngineCommunicator engineCommunicator)
        {
            EngineOCR.engineCommunicator = engineCommunicator;
        }

        public static AbstractEngineCommunicator getEngineCommunicator()
        {
            return engineCommunicator;
        }

        public static void start(string configFilePath,int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetConfigFile";
            cmd.param = configFilePath;
            string result = engineCommunicator.execute(cmd,timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Start ocr engine failed.");
        }

        public static void setLanguage(string languageCode, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetLanguageCode";
            cmd.param = languageCode;
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Set language to ocr engine failed.");
        }

        public static void setScreenSize(Size screenSize, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetScreenSize";
            cmd.param = screenSize;
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Set screen size to ocr engine failed.");
        }

        public static void setStandardButtons(string imageFilePath, List<ControlButton> buttonList, int timeout = -1)
        {
            if (null == imageFilePath || 0 == buttonList.Count)//todo
                throw new FTBAutoTestException("Check button status failed by invalid param.");

            OCRBtnCheckParam param = new OCRBtnCheckParam();
            param.imagePath = imageFilePath;
            param.imageInfoList = new List<ImageInfo>();
            foreach (ControlButton button in buttonList)
            {
                if (null == button.imageList || 0 == button.imageList.Count)
                    throw new FTBAutoTestException("Check button status failed by invalid param.");
                ImageInfo buttonInfo = new ImageInfo();
                buttonInfo.x = button.imageList[0].rect.x;
                buttonInfo.y = button.imageList[0].rect.y;
                buttonInfo.w = button.imageList[0].rect.w;
                buttonInfo.h = button.imageList[0].rect.h;

                switch (button.statusShow)
                {
                    case ControlButtonStatus.Unknow:  //todo
                        buttonInfo.status = "Unknow";
                        break;
                    case ControlButtonStatus.Valid:
                        buttonInfo.status = "Valid";
                        break;
                    case ControlButtonStatus.Selected:
                        buttonInfo.status = "Selected";
                        break;
                    case ControlButtonStatus.Invalid:
                        buttonInfo.status = "Invalid";
                        break;
                    default:
                        throw new FTBAutoTestException("Button recognition failed.");
                }
                param.imageInfoList.Add(buttonInfo);
            }

            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetStandardButtonStatus";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Button recognition failed.");
        }

        public static void stop(int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "HaltOcrApp";
            cmd.param = "";
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Stop ocr engine failed.");
        }

        public List<string> analyzeWords(string imageFilePath, List<ElementString> strElementList, int timeout = -1)
        {
            if (null == imageFilePath || null == strElementList)
                throw new FTBAutoTestException("OCR engine analyze failed by invalid param.");
            if (0 == strElementList.Count)
                return null;

            OCRAnalyzeParam param = new OCRAnalyzeParam();
            param.imageDir = imageFilePath;
            param.textList = new List<TextInfo>();
            foreach (ElementString strElement in strElementList)
            {
                if (null == strElement.rect || null == strElement.str)
                    throw new FTBAutoTestException("OCR engine analyze failed by invalid param.");
                TextInfo textInfo = new TextInfo();
                textInfo.x = strElement.rect.x;
                textInfo.y = strElement.rect.y;
                textInfo.w = strElement.rect.w;
                textInfo.h = strElement.rect.h;
                textInfo.str = strElement.str;
                param.textList.Add(textInfo);
            }

            EngineCommand cmd = new EngineCommand();
            cmd.name = "WordsAnalyzeRequest";
            cmd.param = param;

            object rawResult = engineCommunicator.execute(cmd, timeout);
            OCRAnalyzeParam result = null;
            try { result = Newtonsoft.Json.JsonConvert.DeserializeObject<OCRAnalyzeParam>(rawResult.ToString()); }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }
            if (null != result && null != result.textList && strElementList.Count == result.textList.Count)
            {
                List<string> strList = new List<string>();
                for (int i = 0; i < strElementList.Count; i++)
                {
                    strList.Add(result.textList[i].str);
                }
                return strList;
            }
            throw new FTBAutoTestException("OCR engine analyze failed.");
        }

        public bool screenDiff(string standardPath, string currentPath, int timeout = -1)
        { 
            if (null == standardPath || null == currentPath)
                throw new FTBAutoTestException("OCR engine difference failed by invalid param.");

            OCRDifferenceParam param = new OCRDifferenceParam();
            param.standard = standardPath;
            param.current = currentPath;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "ScreenDiff";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if ("OK" == result)
            {
                return true;
            }
            else if("NG" == result)
            {
                return false;
            }
            throw new FTBAutoTestException("Differential recognition failed.");
        }

        public void setWorkSpacePath(string path, int timeout = -1)
        {
            if(null == path)
                throw new FTBAutoTestException("OCR engine setWorkSpacePath failed by invalid param.");

            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetWorkSpacePath";
            cmd.param = path;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;

            throw new FTBAutoTestException("SetWorkSpacePath failed.");
        }

        public bool cleanImgArea(string srcPath, string destPath, Rectangle roiRect, int timeout = -1)
        {
            if (null == srcPath || null == destPath || roiRect == null)
                throw new FTBAutoTestException("Clean image area failed by invalid param.");

            ScreenParam param = new ScreenParam();
            param.srcPath = srcPath;
            param.destPath = destPath;
            param.x = roiRect.x;
            param.y = roiRect.y;
            param.w = roiRect.w;
            param.h = roiRect.h;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "CleanImgArea";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if ("OK" == result)
            {
                return true;
            }
            else if ("NG" == result)
            {
                return false;
            }
            throw new FTBAutoTestException("Clean image area failed.");
        }

        public bool cutScreen(string srcPath, string destPath, Rectangle roiRect, int timeout = -1)
        {
            if (null == srcPath ||null == destPath || roiRect == null)
                throw new FTBAutoTestException("Cut Screen failed by invalid param.");

            ScreenParam param = new ScreenParam();
            param.srcPath = srcPath;
            param.destPath = destPath;
            param.x = roiRect.x;
            param.y = roiRect.y;
            param.w = roiRect.w;
            param.h = roiRect.h;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "CutScreen";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if ("OK" == result)
            {
                return true;
            }
            else if ("NG" == result)
            {
                return false;
            }
            throw new FTBAutoTestException("Cut Screen failed.");
        }

        public bool getOcrResult(string srcPath,string destPath,string lang, string property, string resultType = "txt",int timeout = -1)
        {
            
            if (null == srcPath || null == destPath || null == lang || (("SingleLine" == property) && ("MultiLine" == property)))
                throw new FTBAutoTestException("Get Hocr failed by invalid param.");

            OCRParam param = new OCRParam();
            param.srcPath = srcPath;
            param.destPath = destPath;
            param.lang = lang;
            param.property = property;
            param.resultType = resultType;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "GetOcrResult";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if ("OK" == result)
            {
                return true;
            }
            else if ("NG" == result)
            {
                return false;
            }
            throw new FTBAutoTestException("Get Hocr failed.");

        }

        public bool screenConsistencyCheck(string path1, string path2, int timeout = -1)
        {
            if (path1 == null || path2 == null)
                throw new FTBAutoTestException("screen consistency Check failed by invalid param.");

            ScreenSimilarity param = new ScreenSimilarity();
            param.path1 = path1;
            param.path2 = path2;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "ScreenConsistencyCheck";
            cmd.param = param;
            string result = engineCommunicator.execute(cmd, timeout) as string;

            if (result == "OK")
            {
                return true;
            }
            else if (result == "NG")
            {
                return false;
            }

            throw new FTBAutoTestException("get screen similarity failed.");
        }

        public float getScreenSimilarity(string path1,string path2, int timeout = -1)
        {
            if (path1 == null || path2 == null)
                throw new FTBAutoTestException("Get screen similarity failed by invalid param.");

            ScreenSimilarity param = new ScreenSimilarity();
            param.path1 = path1;
            param.path2 = path2;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "GetScreenSimilarity";
            cmd.param = param;
            string result = engineCommunicator.execute(cmd, timeout) as string;

            if (result != "NG")
            {
                float similarity = float.Parse(result);
                return similarity;
            }
           
            throw new FTBAutoTestException("get screen similarity failed.");
        }

        public bool reverseRectangleCheck(string imagePath, Rectangle rect, int timeout = -1)
        {
            if (imagePath == null || rect == null)
                throw new FTBAutoTestException("Reverse Rectangle Check failed by invalid param.");

            ReverseRectangleCheck param = new ReverseRectangleCheck();
            param.imagePath = imagePath;
            param.x = rect.x;
            param.y = rect.y;
            param.w = rect.w;
            param.h = rect.h;

            EngineCommand cmd = new EngineCommand();
            cmd.name = "ReverseRectangleCheck";
            cmd.param = param;
            string result = engineCommunicator.execute(cmd, timeout) as string;

            if (result == "OK")
            {
                return true;
            }
            else if (result == "NG")
            {
                return false;
            }

            throw new FTBAutoTestException("Check Reverse Rectangle failed.");
        }

        public List<ControlButtonStatus> analaysButtonStatus(string imageFilePath, List<ControlButton> buttonList, int timeout = -1)
        {
            if (null == imageFilePath || 0 == buttonList.Count)
                throw new FTBAutoTestException("Get button status failed by invalid param.");
           
            OCRBtnCheckParam param = new OCRBtnCheckParam();
            param.imagePath = imageFilePath;
            param.imageInfoList = new List<ImageInfo>();
            foreach (ControlButton button in buttonList)
            {
                if (null == button.imageList || 1!= button.imageList.Count)
                    throw new FTBAutoTestException("Get button status failed by invalid param.");

                ImageInfo buttonInfo = new ImageInfo();
                buttonInfo.x = button.imageList[0].rect.x;
                buttonInfo.y = button.imageList[0].rect.y;
                buttonInfo.w = button.imageList[0].rect.w;
                buttonInfo.h = button.imageList[0].rect.h;
                buttonInfo.status = null;
                param.imageInfoList.Add(buttonInfo);
            }

            EngineCommand cmd = new EngineCommand();
            cmd.name = "GetCurrentButtonStatus";
            cmd.param = param;

            object rawResult = engineCommunicator.execute(cmd, timeout);
            OCRBtnCheckParam result = null;
            try { result = Newtonsoft.Json.JsonConvert.DeserializeObject<OCRBtnCheckParam>(rawResult.ToString()); }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }

            List<ControlButtonStatus> buttonStatusList = new List<ControlButtonStatus>();

            if (null != result && null != result.imageInfoList && buttonList.Count == result.imageInfoList.Count)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    switch (result.imageInfoList[i].status)
                    {
                        case _STATUS_UNKNOW:
                            buttonStatusList.Add(ControlButtonStatus.Unknow);
                            break;
                        case _STATUS_VALID:
                            buttonStatusList.Add(ControlButtonStatus.Valid);
                            break;
                        case _STATUS_SELECTED:
                            buttonStatusList.Add(ControlButtonStatus.Selected);
                            break;
                        case _STATUS_INVALID:
                            buttonStatusList.Add(ControlButtonStatus.Invalid);
                            break;
                        default:
                            break;
                            throw new FTBAutoTestException("Button recognition failed.");
                    }
                }
                return buttonStatusList;
            }
            throw new FTBAutoTestException("Button recognition failed.");
        }
       
        public void ShowStringCheckResult(string imageFilePath,
                                         List<string> standardList, 
                                         List<Rectangle> rectList,
                                         List<string> currentList, 
                                         int timeout = -1)
        {
            if (false == File.Exists(imageFilePath)
                || 0 == standardList.Count
                || 0 == currentList.Count
                || standardList.Count != currentList.Count)//todo
            {
                throw new FTBAutoTestException("Show check Text result failed by invalid param.");
            }

            OCRCheckResultParam param = new OCRCheckResultParam();
            param.imagePath = imageFilePath;
            param.textList = new List<ResultInfo>();
            for (int i=0;i< standardList.Count;i++)
            {
                if (   null == standardList
                    || null == rectList
                    || null == currentList)
                    throw new FTBAutoTestException("Show check Text result failed by invalid param.");//todo
                ResultInfo textInfo = new ResultInfo();
                textInfo.x = rectList[i].x;
                textInfo.y = rectList[i].y;
                textInfo.w = rectList[i].w;
                textInfo.h = rectList[i].h;
                textInfo.standard = standardList[i];
                textInfo.current = currentList[i];
                param.textList.Add(textInfo);
            }

            EngineCommand cmd = new EngineCommand();
            cmd.name = "ShowStringCheckResult";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Show check Text result failed.");
        }

        public void ShowButtonCheckResult(string imageFilePath,
                                    List<string> standardList,
                                    List<Rectangle> rectList,
                                    List<string> currentList,
                                    int timeout = -1)
        {
            if (false == File.Exists(imageFilePath)
                || 0 == standardList.Count
                || 0 == currentList.Count
                || standardList.Count != currentList.Count)//todo
                throw new FTBAutoTestException("Show check button result failed by invalid param.");

            OCRCheckResultParam param = new OCRCheckResultParam();
            param.imagePath = imageFilePath;
            param.textList = new List<ResultInfo>();
            for (int i = 0; i < standardList.Count; i++)
            {
                if (null == rectList[i]
                    || null == currentList[i]
                    || null == standardList[i])
                    throw new FTBAutoTestException("Show check button result failed by invalid param.");//todo
                ResultInfo textInfo = new ResultInfo();
                textInfo.x = rectList[i].x;
                textInfo.y = rectList[i].y;
                textInfo.w = rectList[i].w;
                textInfo.h = rectList[i].h;
                textInfo.standard = standardList[i];
                textInfo.current = currentList[i];
                param.textList.Add(textInfo);
            }

            EngineCommand cmd = new EngineCommand();
            cmd.name = "ShowButtonCheckResult";
            cmd.param = param;

            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Show check button result failed.");
        }
    }
}
