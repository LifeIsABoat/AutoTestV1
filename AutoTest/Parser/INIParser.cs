using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;
using System.IO;
using Tool.Engine;

namespace Tool.Parser
{
    class ImageParser
    {
        private const int SINGLE = 0;
        private const int WHOLE = 1;
        public static CaptureScreen getCaptureScreen(string imgPath, string describePath)
        {
            string temp, cutScreenPath, destParam, hocrPath, txtPath;
            string value = getFileText(describePath);
            CaptureScreen childCapture;
            CaptureScreen capture = Newtonsoft.Json.JsonConvert.DeserializeObject<CaptureScreen>(value);

            //Init System
            //EngineCommunicatorByShareMemory engineCommunicator = new EngineCommunicatorByShareMemory(StaticEnvironInfo.getOcrShareMemoryName(),
            //                                                                                         StaticEnvironInfo.getOcrWriteSemaphoreName(),
            //                                                                                         StaticEnvironInfo.getOcrReadSemaphoreName(),
            //                                                                                         StaticEnvironInfo.getOcrShareMemorySize());
            //EngineOCR.setEngineCommunicator(engineCommunicator);
            EngineOCR ocr = new EngineOCR();

            for (int count = 0; count < capture.captureControlList.Count; count++)
            {
                temp = "_" + count.ToString() + ".";
                cutScreenPath = imgPath.Replace(".", temp);
                destParam = cutScreenPath.Substring(0, cutScreenPath.IndexOf('.'));
                hocrPath = destParam + ".hocr";
                txtPath = destParam + ".txt";
                //cut screen
                if (File.Exists(cutScreenPath))
                {
                    File.Delete(cutScreenPath);
                }
                bool ret = ocr.cutScreen(imgPath, cutScreenPath, capture.captureControlList[count].rect);
                if (!ret)
                {
                    throw new Exception("CutScreen fail!");
                }

                //get hocr
                if (File.Exists(hocrPath))
                {
                    File.Delete(hocrPath);
                }
                ret = ocr.getOcrResult(cutScreenPath, destParam, "eng"/*"chi_sim"*/, capture.captureControlList[count].property,"hocr");
                if (!ret)
                {
                    throw new Exception("Create hocr fail!");
                }
               
                childCapture = HTMLParser.getCaptureScreen(hocrPath,"eng");


                //get txt
                if (File.Exists(txtPath))
                {
                    File.Delete(txtPath);
                }
                ret = ocr.getOcrResult(cutScreenPath, destParam, "eng"/*"chi_sim"*/, capture.captureControlList[count].property);
                if (!ret)
                {
                    throw new Exception("Create Txt fail!");
                }
                addTxtInfoCaptureScreen(capture, count,childCapture, txtPath);
                childCapture.captureControlList.Clear();
            }
            
            return capture;
        }

        private static string getFileText(string filepath)
        {
            using (FileStream fread = new FileStream(filepath, FileMode.Open))
            {
                int fsLen = (int)fread.Length;
                byte[] heByte = new byte[fsLen];
                int r = fread.Read(heByte, 0, heByte.Length);
                string fileText = System.Text.Encoding.UTF8.GetString(heByte);
                return fileText;
            }
        }

        private static List<string> txtParser(string text)
        {
            string[] strings = text.Split(Environment.NewLine.ToCharArray());
            List<string> list = new List<string>();

            for(int num=0;num< strings.Length;num++)
            {
                if (strings[num] != null && !strings[num].Equals(""))
                {
                    list.Add(strings[num]);
                }
            }
            return list;
        }
        private static CaptureScreen addTxtInfoCaptureScreen(CaptureScreen parentCapture, int blockCount, CaptureScreen childCapture,string txtPath)
        {
            
            CaptureBlock block;
            string text = getFileText(txtPath);
            List<string> txtList = new List<string>();
            txtList = txtParser(text);

            if (txtList.Count == 0)
            {
                return childCapture;
            }

            if (childCapture.captureControlList.Count == 0)
            {
                block = parentCapture.captureControlList[blockCount];
                for (int i = 0; i < txtList.Count; i++)
                {
                    CaptureLine line = new CaptureLine();
                    line.text.rect = parentCapture.captureControlList[blockCount].rect;
                    line.text.text = txtList[i];
                    block.lines.Add(line);
                }
                childCapture.captureControlList.Add(block);
            }
            else
            {
                block = childCapture.captureControlList[0];
                for (int i = 0; i < block.lines.Count; i++)
                {
                    if (block.lines[i].text.text != txtList[i])
                    {
                        block.lines[i].text.text = txtList[i];
                    }
                }
                mergeCaptureScreen(parentCapture, blockCount, childCapture);
            }

            return childCapture;
        }
        private static CaptureScreen mergeCaptureScreen(CaptureScreen parentCapture, int blockCount, CaptureScreen childCapture)
        {

            CaptureBlock block = parentCapture.captureControlList[blockCount];
            string property = block.property;
            Position post = new Position(block.rect.x, block.rect.y);

            for (int j = 0; j < childCapture.captureControlList.Count; j++)
            {
                for (int i = 0; i < childCapture.captureControlList[j].lines.Count; i++)
                {
                    CaptureLine line = childCapture.captureControlList[j].lines[i] + post;
                    if (line.text.text != " ")
                    {
                        block.lines.Add(line);
                    }   
                }
            }

            return parentCapture;
        }

    }
}
