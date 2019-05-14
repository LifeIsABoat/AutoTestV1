using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AutoTestTool.BLL
{
    class CaptureWord
    {
        public Rectangle rect;
        public string text;
        public CaptureWord()
        {
            rect = new Rectangle();
        }

        public CaptureWord(int x, int y, int w, int h)
        {
            rect = new Rectangle(x, y, w, h);
        }

        public static CaptureWord operator +(CaptureWord word, Position post)
        {
            word.rect.x += post.x;
            word.rect.y += post.y;
            return word;
        }
    }

    class CaptureText
    {
        public List<CaptureWord> words;
        public Rectangle rect;
        public string text;
        public CaptureText()
        {
            words = new List<CaptureWord>();
            rect = new Rectangle();
        }

        public CaptureText(int x, int y, int w, int h)
        {
            words = new List<CaptureWord>();
            rect = new Rectangle(x, y, w, h);
        }
        public static CaptureText operator +(CaptureText text, Position post)
        {
            int count;

            text.rect.x += post.x;
            text.rect.y += post.y;

            for(count = 0;count < text.words.Count;count++)
            {
                text.words[count] = text.words[count] + post;
            }

            return text;
        }
    }

    class CaptureImage
    {
        public Rectangle rect;
        public long hash = 0;
        public CaptureImage()
        {
            rect = new Rectangle();
        }

        public CaptureImage(int x, int y, int w, int h)
        {
            rect = new Rectangle(x, y, w, h);
        }

        public static CaptureImage operator +(CaptureImage image, Position post)
        {
            image.rect.x += post.x;
            image.rect.y += post.y;

            return image;
        }
    }
    class CaptureLine
    {
        public CaptureText text;
        public CaptureImage image;
        public Rectangle rect;

        public CaptureLine()
        {
            text = new CaptureText();
            image = new CaptureImage();
            rect = new Rectangle();
        }

        public CaptureLine(int x, int y, int w, int h)
        {
            text = new CaptureText();
            image = new CaptureImage();
            rect = new Rectangle(x, y, w, h);
        }

        public static CaptureLine operator +(CaptureLine line, Position post)
        {

            line.rect.x += post.x;
            line.rect.y += post.y;
            line.text = line.text + post;
            line.image = line.image + post;

            return line;
        }
    }

    class CaptureBlock
    {
        public List<CaptureLine> lines;
        public Rectangle rect;
        public string property = null;
        public CaptureBlock()
        {
            lines = new List<CaptureLine>();
            rect = new Rectangle();
        }

        public CaptureBlock(int x, int y, int w, int h)
        {
            lines = new List<CaptureLine>();
            rect = new Rectangle(x, y, w, h);
        }

        public static CaptureBlock operator +(CaptureBlock block, Position post)
        {
            int count;

            block.rect.x += post.x;
            block.rect.y += post.y;

            for (count = 0; count < block.lines.Count; count++)
            {
                block.lines[count] = block.lines[count] + post;
            }

            return block;
        }
    }

    class CaptureScreen
    {
        public List<CaptureBlock> captureControlList;

        public CaptureScreen()
        {
            captureControlList = new List<CaptureBlock>();
        }

        public List<int> CheckInvalidArea()
        {
            int count;
            List<int> result = new List<int>();

            for (count = 0; count < captureControlList.Count; count++)
            {
                if (captureControlList[count].property == "invalidArea")
                {
                    result.Add(count);
                }
            }
            return result;
        }
    }
}
