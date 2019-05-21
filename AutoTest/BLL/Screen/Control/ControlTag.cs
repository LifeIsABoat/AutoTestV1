using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlTag : AbstractControl , IAggregateCreateAPI
    {
        public List<ControlButton> tagList;

        public ControlTag(List<ControlButton> tagList)
            :base()
        {
            if (null == tagList)
                throw new FTBAutoTestException("Create tag failed by input empty tagList");

            //left-->right
            //sort by position
            tagList.Sort((tag1, tag2) =>
            {
                return tag1.rect.x - tag2.rect.x;
            });

            this.tagList = tagList;
        }

        public override void hasFixed()
        {
            if (null != tagList)
            {
                foreach (ControlButton tag in tagList)
                    tag.hasFixed();
            }
        }

        public List<string> getStringList()
        {
            List<string> tagStrList = new List<string>();
            foreach (ControlButton tag in tagList)
                tagStrList.Add(tag.stringList[0].str);
            return tagStrList;
        }

        public ControlButton getTag(string tagWord)
        {
            foreach (ControlButton tag in tagList)
            {
                if (tag.getIdentify().btnWordsStr == tagWord)
                    return tag;
            }
            return null;
        }

        public AbstractScreenAggregate createAggregate()
        {
            return new ScreenDictionary();
        }

        public override string ToString()
        {
            string str = "";

            str += "Tag:{";
            if (null == rect)
                str += "null,\r\n";
            else
                str += rect.ToString() + ",\r\n";
            str += "{";
            string tmpStr = "";
            foreach (ControlButton arrow in tagList)
                tmpStr += "\r\n" + arrow.ToString();
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};";

            return str;
        }
        public override bool Equals(AbstractControl targetControl)
        {
            ControlTag tag = targetControl as ControlTag;
            if (null == tag)
                return false;
            if (tagList.Count != tag.tagList.Count)
                return false;
            if (0 != tagList.Where((a, i) => a != tag.tagList[i]).Count())
                return false;
            return true;
        }
    }
}
