using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenDoublyLinkedList : AbstractScreenAggregate
    {
        private LinkedList<Screen> screenLinkList;

        //Create screenList as initial
        public ScreenDoublyLinkedList()
        {
            this.screenLinkList = new LinkedList<Screen>();
        }

        private LinkedListNode<Screen> readNodeByIndex(int index)
        {
            if (index >= screenLinkList.Count)
                throw new FTBAutoTestException("Get screen of DoublyLinkList by index error, cause the index out of range.");
            LinkedListNode<Screen> targetNode = screenLinkList.First;
            for (int i = 0; i < index; i++)
                targetNode = targetNode.Next;
            return targetNode;
        }
        private int getScreenIndex(Screen targetScreen)
        {
            if (null == targetScreen)
                return -1;
            //loop the linkList
            int index = 0;
            LinkedListNode<Screen> node = screenLinkList.First;
            for (; null != node; node = node.Next, index++)
            {
                if (targetScreen.EqualsByElementList(node.Value))
                    return index;
            }
            return -1;
        }

        public LinkedListNode<Screen> getFirstNode()
        {
            return screenLinkList.First;
        }
        public LinkedListNode<Screen> getLastNode()
        {
            return screenLinkList.Last;
        }

        public override Screen getFirstScreen()
        {
            return screenLinkList.First.Value;
        }
        public override Screen getLastScreen()
        {
            return screenLinkList.Last.Value;
        }
        public override int getCount()
        {
            return screenLinkList.Count;
        }

        public override void deleteScreen(int index)
        {
            if (index >= screenLinkList.Count || index < 0)
                throw new FTBAutoTestException("Delete screen of DoublyLinkList by index error, cause the index out of range.");

            LinkedListNode<Screen> targetNode = readNodeByIndex(index);
            screenLinkList.Remove(targetNode);
        }
        public override Screen toFirstScreen(Screen screen)
        {
            Screen nowScreen = screen;
            List<AbstractControl> tmpControlList;
            //move to next screen
            tmpControlList = screen.getControlList(typeof(AbstractControlArrow));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found arrowControl in currentScreen.");
            Position pos = ((AbstractControlArrow)tmpControlList[0]).getFirstArrow().rect.getCenter();
            while (true)
            {
                tpClicker.execute(pos);
                System.Threading.Thread.Sleep(1000);
                Screen preScreen = nowScreen;
                nowScreen = new Screen();
                rawScreenLoader.execute(nowScreen);
                //fixedScreenLoader.execute(screen);
                if(nowScreen.EqualsByElementList(preScreen) == true)
                {
                    break;
                }
            }
            if (nowScreen.EqualsByElementList(screen) == false)
            {
                nowScreen = new Screen();
                fixedScreenLoader.execute(nowScreen);
                return nowScreen;
            }
            else
            {
                return screen;
            }
        }

        public override void appendScreen(Screen screen, int index = -1)
        {
            if (0 == screenLinkList.Count)
            {
                if (-1 != index && 0 != index)
                {
                    string expMsg = string.Format("Append screenLinkList is empty, index can't be {0}", index);
                    throw new FTBAutoTestException(expMsg);
                }
                screenLinkList.AddFirst(screen);
            }
            else
            {
                LinkedListNode<Screen> targetNode;
                if (-1 == index)
                    targetNode = screenLinkList.Last;
                else
                    targetNode = readNodeByIndex(index);
                screenLinkList.AddAfter(targetNode, screen);
            }
        }

        public override void moveToNextScreen(Screen screen, int index = -1)
        {
            //move to next screen
            List<AbstractControl> tmpControlList;
            tmpControlList = screen.getControlList(typeof(AbstractControlArrow));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found arrowControl in currentScreen.");
            Position pos = ((AbstractControlArrow)tmpControlList[0]).getLastArrow().rect.getCenter();
            tpClicker.execute(pos);
            System.Threading.Thread.Sleep(1000);
        }

        public override bool isScreenContains(Screen screen)
        {
            //ScreenList Empty
            if (null == screen)
                return false;
            //loop the linkList
            LinkedListNode<Screen> node;
            for (node = screenLinkList.First; null != node; node = node.Next)
            {
                if (screen.EqualsByElementList(node.Value))
                    return true;
            }
            return false;
        }

        //Read Screen 
        public override Screen readScreen(int index)
        {
            return readNodeByIndex(index).Value;
        }

        public override void showScreen(int index)
        {
            //check for target index
            if (index >= screenLinkList.Count)
                throw new FTBAutoTestException("Get screen of DoublyLinkList by index error, cause the index out of range.");
            //get Current Index
            Screen currentScreen = new Screen();
            rawScreenLoader.execute(currentScreen);
            int currentIndex = getScreenIndex(currentScreen);
            if (-1 == currentIndex)
                throw new FTBAutoTestException("Could not found current screen in aggregate.");
            //already stay targetIndex
            if (currentIndex == index)
                return;
            //decide how to move to target screen
            LinkedListNode<Screen> savedNode = readNodeByIndex(currentIndex);
            Screen savedScreen = savedNode.Value;
            List<AbstractControl> tmpControlList;
            tmpControlList = savedScreen.getControlList(typeof(AbstractControlArrow));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found arrowControl in savedScreen.");
            if (index - currentIndex < 0)
            {
                int clickTimes = currentIndex - index;
                Position pos = ((AbstractControlArrow)tmpControlList[0]).getFirstArrow().rect.getCenter();
                //do move operation
                for (int i = 0; i < clickTimes; i++)
                {
                    tpClicker.execute(pos);
                    savedNode = savedNode.Previous;
                    StaticCurrentScreen.set(savedNode.Value);
                }
            }
            else
            {
                int clickTimes = index - currentIndex;
                Position pos = ((AbstractControlArrow)tmpControlList[0]).getLastArrow().rect.getCenter();
                //do move operation
                for (int i = 0; i < clickTimes; i++)
                {
                    tpClicker.execute(pos);
                    savedNode = savedNode.Next;
                    StaticCurrentScreen.set(savedNode.Value);
                }
            }
        }

        public override IIterator createReadIterator()
        {
            if (0 == screenLinkList.Count)
                throw new FTBAutoTestException("Create read iterator error, cause the screenLinkList was empty.");
            if (null == readIterator)
                this.readIterator = new ScreenDoublyLinkedListReadIterator(this);
            return readIterator;
        }

        public override IIterator createShowIterator()
        { 
            if (0 == screenLinkList.Count)
                throw new FTBAutoTestException("Create show iterator error, cause the screenLinkList was empty.");
            if (null == showIterator)
                this.showIterator = new ScreenDoublyLinkedShowIterator(this,tpClicker);
            return showIterator;
        }
    }
}
