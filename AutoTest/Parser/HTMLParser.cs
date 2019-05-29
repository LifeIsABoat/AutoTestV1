using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;
using HtmlAgilityPack;

namespace Tool.Parser
{
    class HTMLParser
    {
        private const int BLOCK = 0;
        private const int LINE = 1;
        private const int WORD = 2;
        private static HtmlDocument htmlDocument;

        public static CaptureScreen getCaptureScreen(string path, string language)
        {
            CaptureScreen capture = new CaptureScreen();

            //Starting with page, parse HTML
            htmlDocument = new HtmlDocument();
            htmlDocument.Load(path, Encoding.UTF8);
            HtmlNodeCollection pageCollection = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='ocr_page']").ChildNodes;

            getHtmlParserResult(capture, language,pageCollection);
            
            return capture;
        }

        private static void getHtmlParserResult(CaptureScreen screen,string language,HtmlNodeCollection collection)
        {
            string titlevlaue;
            foreach (HtmlNode node in collection)
            {
                //Remove excess items
                if (node.Name == "#text")
                {
                    continue;
                }

                if (node.Attributes["class"].Value == "ocr_par")
                {
                    titlevlaue = node.Attributes["title"].Value;
                    stringParser(screen, titlevlaue, BLOCK);
                }
                else if (node.Attributes["class"].Value == "ocr_line")
                {
                    titlevlaue = node.Attributes["title"].Value;
                    stringParser(screen,titlevlaue, LINE);
                }
                else if (node.Attributes["class"].Value == "ocrx_word")
                {
                    titlevlaue = node.Attributes["title"].Value;
                    stringParser(screen,titlevlaue, WORD);
                    titlevlaue = System.Net.WebUtility.HtmlDecode(node.InnerText);
                    getTextbyWordAndLine(screen, language,titlevlaue);
                }

                //Minimum node is words
                if (node.ChildNodes.Count > 1)
                {
                    getHtmlParserResult(screen, language,node.ChildNodes);
                }
            }
        }

        //Gets text for words and lines
        private static void getTextbyWordAndLine(CaptureScreen screen,string language, string text)
        {
            string textVlaue;
            int blockIndex,lineIndex, wordIndex;

            textVlaue = text;
            blockIndex = screen.captureControlList.Count - 1;
            lineIndex = screen.captureControlList[blockIndex].lines.Count - 1;
            wordIndex = screen.captureControlList[blockIndex].lines[lineIndex].text.words.Count - 1;

            screen.captureControlList[blockIndex].lines[lineIndex].text.words[wordIndex].text = textVlaue;
            if ((language == "eng")&& (screen.captureControlList[blockIndex].lines[lineIndex].text.text!=null))
            {
                screen.captureControlList[blockIndex].lines[lineIndex].text.text += " ";
            }
            screen.captureControlList[blockIndex].lines[lineIndex].text.text += textVlaue;
        }

        //Parsing coordinate information in titles
        private static void stringParser(CaptureScreen screen, string titlevlaue, int mode)
        {
            try
            {
                Rectangle rect = new Rectangle();
                int index = titlevlaue.IndexOf(';');
                if (index != -1)
                {
                    titlevlaue = titlevlaue.Substring(0, index);
                }

                string[] strings = titlevlaue.Split(' ');
                rect.x = Int32.Parse(strings[1]);
                rect.y = Int32.Parse(strings[2]);
                rect.w = Int32.Parse(strings[3]) - Int32.Parse(strings[1]);
                rect.h = Int32.Parse(strings[4]) - Int32.Parse(strings[2]);

                if (mode == BLOCK)
                {
                    CaptureBlock block = new CaptureBlock(rect.x, rect.y, rect.w, rect.h);
                    screen.captureControlList.Add(block);
                }
                else  if (mode == LINE)
                {
                    CaptureLine line = new CaptureLine();
                    line.text.rect = rect;
                    int blockCount = screen.captureControlList.Count - 1;
                    screen.captureControlList[blockCount].lines.Add(line);
                }
                else if (mode == WORD)
                {
                    CaptureWord word = new CaptureWord(rect.x, rect.y, rect.w, rect.h);
                    int blockCount = screen.captureControlList.Count - 1;
                    int lineIndex = screen.captureControlList[blockCount].lines.Count - 1;
                    screen.captureControlList[blockCount].lines[lineIndex].text.words.Add(word);
                }
                else
                {
                    /* do Nothing */
                }

            }
            catch
            {
                throw new Exception("Error in bbox coordinate format!");
            }

        }
    }
}
