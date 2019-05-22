using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenDictionary : AbstractScreenAggregate
    {
        private static List<string> tagWordsList = null;
        private Dictionary<string, Screen> screenDictionary;
        private int currentIndex;

        public static void setTagWords(List<string> tagWordsList)
        {
            if (null == tagWordsList)
                throw new FTBAutoTestException("Set tag words error by empty or null tagWordsList.");
            ScreenDictionary.tagWordsList = tagWordsList;
        }

        public static List<string> getTagWords()
        {
            return ScreenDictionary.tagWordsList;
        }

        public ScreenDictionary()
        {
            if (null == tagWordsList)
                throw new FTBAutoTestException("Create ScreenDictionary error, tagWordsList is null.");
            screenDictionary = new Dictionary<string, Screen>();
            currentIndex = 0;
        }

        public override Screen getFirstScreen()
        {
            if (tagWordsList.Count != screenDictionary.Count)
                throw new FTBAutoTestException("ScreenDictionary can't be read until being fill up.");
            return screenDictionary[tagWordsList.First()];
        }
        public override Screen getLastScreen()
        {
            if (tagWordsList.Count != screenDictionary.Count)
                throw new FTBAutoTestException("ScreenDictionary can't be read until being fill up.");
            return screenDictionary[tagWordsList.Last()];
        }
        public override int getCount()
        {
            if (tagWordsList.Count != screenDictionary.Count)
                throw new FTBAutoTestException("ScreenDictionary can't be read until being fill up.");
            return screenDictionary.Count;
        }

        public override void deleteScreen(int index)
        {
            if (index >= screenDictionary.Count || index < 0)
                throw new FTBAutoTestException("Delete screen of ScreenDictionary by index error, cause the index out of range.");
            screenDictionary.Remove(tagWordsList[index]);
        }
        public override Screen toFirstScreen(Screen screen)
        {
            return screen;
        }
        public override void appendScreen(Screen screen, int index = -1)
        {
            if (0 == screenDictionary.Count)
            {
                if (-1 != index && 0 != index)
                {
                    string expMsg = string.Format("Append ScreenDictionary is empty, index can't be {0}", index);
                    throw new FTBAutoTestException(expMsg);
                }
            }
            else
            {
                if (-1 == index)
                    index = currentIndex;
                if (screenDictionary.ContainsKey(tagWordsList[index]))
                    throw new FTBAutoTestException("Append ScreenDictionary error, cause the tagScreen is exsited.");
            }
            screenDictionary.Add(tagWordsList[currentIndex], screen);
            currentIndex++;
        }

        public override void moveToNextScreen(Screen screen, int index = -1)
        {
            //move to next screen
            List<AbstractControl> tmpControlList;
            tmpControlList = screen.getControlList(typeof(ControlTag));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found tagControl in currentScreen.");
            ControlButton tag = ((ControlTag)tmpControlList[0]).getTag(tagWordsList[currentIndex % tagWordsList.Count]);
            Position pos;
            if (null == tag)
            {
                //try for click arrow
                tmpControlList = screen.getControlList(typeof(ControlArrowLeftRight));
                if (null == tmpControlList)
                    throw new FTBAutoTestException("Append screen error by target tag is non-existent.");
                pos = ((ControlArrowLeftRight)tmpControlList[0]).getLastArrow().rect.getCenter();
                tpClicker.execute(pos);
                System.Threading.Thread.Sleep(1000);
                Screen tmpScreen = new Screen();
                fixedScreenLoader.execute(tmpScreen);
                //for alway first tag Home ,click arrow again
                tmpControlList = tmpScreen.getControlList(typeof(ControlArrowLeftRight));
                if (null == tmpControlList)
                    throw new FTBAutoTestException("Append screen error by target tag is non-existent.");
                pos = ((ControlArrowLeftRight)tmpControlList[0]).getLastArrow().rect.getCenter();
                tpClicker.execute(pos);
                System.Threading.Thread.Sleep(1000);
                tmpControlList = tmpScreen.getControlList(typeof(ControlTag));
                if (null == tmpControlList)
                    throw new FTBAutoTestException("Could not found tagControl in currentScreen.");
                tag = ((ControlTag)tmpControlList[0]).getTag(tagWordsList[currentIndex % tagWordsList.Count]);
                if (null == tag)
                    throw new FTBAutoTestException("Append screen error by target tag is non-existent.");
            }
            //click
            pos = tag.rect.getCenter();
            tpClicker.execute(pos);
            System.Threading.Thread.Sleep(1000);
        }

        public override bool isScreenContains(Screen screen)
        {
            //ScreenList Empty
            if (null == screen)
                return false;

            //loop the linkList
            //todo in custom1，custom2 the screen is all equals
            for (int i = 0; i < currentIndex; i++)
            {
                if (screen.EqualsByElementList(screenDictionary[tagWordsList[i]]))
                    return true;
            }

            return false;
        }
        public override Screen readScreen(int index)
        {
            //check for target index
            if (index>=currentIndex)
                throw new FTBAutoTestException("Get screen of screenDict by index error, cause the index out of range.");
            return screenDictionary[tagWordsList[index]];
        }
        public override void showScreen(int index)
        {
            //check for target index
            if (index >= currentIndex)
                throw new FTBAutoTestException("Get screen of screenDict by index error, cause the index out of range.");
            //get TargetScreen
            Screen savedScreen = screenDictionary[tagWordsList[index]];
            if (null == savedScreen)
                throw new FTBAutoTestException("Target Index DidNotExsited.");
            //show targetScreen
            List<AbstractControl> tmpControlList;
            tmpControlList = savedScreen.getControlList(typeof(ControlTag));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found tagControl in savedScreen.");
            if(((ControlTag)tmpControlList[0]).tagList.Count != tagWordsList.Count)
                throw new FTBAutoTestException("Could not fit tagControl to current ScreenDictionary.");
            tpClicker.execute(((ControlTag)tmpControlList[0]).tagList[index].rect.getCenter());

            StaticCurrentScreen.set(savedScreen);
        }
        public override Screen readScreen(string tagStr)
        {
            //check existed
            if (!screenDictionary.ContainsKey(tagStr))
                throw new FTBAutoTestException("Read screen by tagStr error, cause can't find tagStr in screenDicitinary");
            return screenDictionary[tagStr];  
        }
        public override void showScreen(string tagStr)
        {   
            //check existed
            if (!screenDictionary.ContainsKey(tagStr))
                throw new FTBAutoTestException("Read screen by tagStr error, cause can't find tagStr in screenDicitinary");
            if (!tagWordsList.Contains(tagStr))
                throw new FTBAutoTestException("Read screen by tagStr error, cause can't find tagStr in tagStrList");
            //get TargetScreen
            Screen savedScreen = screenDictionary[tagStr];
            if (null == savedScreen)
                throw new FTBAutoTestException("Target Index DidNotExsited.");
            //show targetScreen
            List<AbstractControl> tmpControlList;
            tmpControlList = savedScreen.getControlList(typeof(ControlTag));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found tagControl in savedScreen.");
            if (((ControlTag)tmpControlList[0]).tagList.Count != tagWordsList.Count)
                throw new FTBAutoTestException("Could not fit tagControl to current ScreenDictionary.");
            //getIndex
            int index = tagWordsList.FindIndex(x => x == tagStr);
            tpClicker.execute(((ControlTag)tmpControlList[0]).tagList[index].rect.getCenter());

            StaticCurrentScreen.set(savedScreen);
        }


        public override IIterator createReadIterator()
        {
            if (0 == screenDictionary.Count)
                throw new FTBAutoTestException("Create read iterator error, cause the screenDictionary was empty.");
            if (null == readIterator)
                this.readIterator = new ScreenDictionaryReadIterator(this);
            return readIterator;
        }
        public override IIterator createShowIterator()
        {
            if (0 == screenDictionary.Count)
                throw new FTBAutoTestException("Create read iterator error, cause the screenDictionary was empty.");
            if (null == readIterator)
                this.readIterator = new ScreenDictionaryShowIterator(this);
            return readIterator;
        }
    }
}
