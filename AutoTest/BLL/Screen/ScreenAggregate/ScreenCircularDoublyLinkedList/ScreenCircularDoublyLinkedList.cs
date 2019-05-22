using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class CircularLinkedListNode<T>
    {
        public CircularLinkedListNode(T value)
        {
            this.Value = value;
            this.Next = null;
            this.Previous = null;
        }
        public CircularLinkedListNode<T> Next { get; set; }
        public CircularLinkedListNode<T> Previous { get; set; }
        public T Value { get; set; }
    }
    class ScreenCircularDoublyLinkedList : AbstractScreenAggregate
    {
        private CircularLinkedListNode<Screen> first,last;
        private int count;

        public ScreenCircularDoublyLinkedList()
        {
            first = null;
            last = null;
            count = 0;
        }

        private void addFirst(Screen screen)
        {
            if (0 == count)
            {
                first = new CircularLinkedListNode<Screen>(screen);
                first.Next = first;
                first.Previous = first;
                last = first;
            }
            else
            {
                CircularLinkedListNode<Screen> tmpfirst;
                tmpfirst = new CircularLinkedListNode<Screen>(screen);
                tmpfirst.Next = first;
                tmpfirst.Previous = last;
                last.Next = tmpfirst;
                first.Previous = tmpfirst;
                first = tmpfirst;
            }
            count++;
        }
        private void addLast(Screen screen)
        {
            if (0 == count)
            {
                last = new CircularLinkedListNode<Screen>(screen);
                last.Next = last;
                last.Previous = last;
                first = last;
            }
            else
            {
                CircularLinkedListNode<Screen> tmplast;
                tmplast = new CircularLinkedListNode<Screen>(screen);
                tmplast.Next = first;
                tmplast.Previous = last;
                last.Next = tmplast;
                first.Previous = tmplast;
                last = tmplast;
            }
            count++;
        }
        private void addAfter(CircularLinkedListNode<Screen> current, 
                              Screen screen)
        {
            if (current.Previous == null || current.Next == null)
                throw new FTBAutoTestException("Add node to CircularLinkList error, please check input current node.");

            if (last == current)
            {
                addLast(screen);
            }
            else
            {
                CircularLinkedListNode<Screen> tmpNode;
                tmpNode = new CircularLinkedListNode<Screen>(screen);
                tmpNode.Next = current.Next;
                tmpNode.Previous = current;
                current.Next = tmpNode;
                tmpNode.Next.Previous = tmpNode;
                count++;
            }
        }
        private CircularLinkedListNode<Screen> readNodeByIndex(int index)
        {
            if (index >= count)
                throw new FTBAutoTestException("Get screen of CircularDoublyLinkList by index error, cause the index out of range.");
            CircularLinkedListNode<Screen> targetNode = first;
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
            CircularLinkedListNode<Screen> node = first;
            for (; null != node; node = node.Next, index++)
            {
                if (index >= count)
                {
                    //equal screen not existed
                    break;
                }
                if (targetScreen.EqualsByElementList(node.Value))
                    return index;
            }
            return -1;
        }

        public CircularLinkedListNode<Screen> getFirstNode()
        {
            return first;
        }
        public CircularLinkedListNode<Screen> getLastNode()
        {
            return last;
        }

        public override Screen getFirstScreen()
        {
            return first.Value;
        }
        public override Screen getLastScreen()
        {
            return last.Value;
        }
        public override int getCount()
        {
            return count;
        }

        public override void deleteScreen(int index)
        {
            CircularLinkedListNode<Screen> node = readNodeByIndex(index);
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.Previous = null;
            node.Next = null;
        }
        public override Screen toFirstScreen(Screen screen)
        {
            return screen;
        }
        public override void appendScreen(Screen screen, int index = -1)
        {
            if (0 == count)
            {
                if (-1 != index && 0 != index)
                {
                    string expMsg = string.Format("Append screenCircularLinkList is empty, index can't be {0}", index);
                    throw new FTBAutoTestException(expMsg);
                }
                addFirst(screen);
            }
            else
            {
                CircularLinkedListNode<Screen> targetNode;
                if (-1 == index)
                    targetNode = last;
                else
                    targetNode = readNodeByIndex(index);
                addAfter(targetNode, screen);
            }

            //move to current fixed screen (to fix <home screen>)
            index = getScreenIndex(screen);
            showScreen(index);
            //StaticCurrentScreen.set(screen);

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
            CircularLinkedListNode<Screen> node = first;
            for (int i = 0; i < count; i++, node = node.Next)
            {
                if (screen.EqualsByElementList(node.Value))
                    return true;
            }
            return false;
        }

        public override Screen readScreen(int index)
        {
            return readNodeByIndex(index).Value;
        }
        public override void showScreen(int index)
        {
            //check for target index
            if (index >= count)
                throw new FTBAutoTestException("Get screen of CircularDoublyLinkList by index error, cause the index out of range.");
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
            CircularLinkedListNode<Screen> saveNode = readNodeByIndex(currentIndex);
            Screen savedScreen = saveNode.Value;
            List<AbstractControl> tmpControlList;
            tmpControlList = savedScreen.getControlList(typeof(AbstractControlArrow));
            if (null == tmpControlList)
                throw new FTBAutoTestException("Could not found arrowControl in savedScreen.");
            int clickTimesL = currentIndex - index;
            int clickTimesR = index - currentIndex;
            if (clickTimesL < 0)
                clickTimesL += count;
            else
                clickTimesR += count;

            if (clickTimesL < clickTimesR)
            {
                Position pos = ((AbstractControlArrow)tmpControlList[0]).getFirstArrow().rect.getCenter();
                //do move operation
                for (int i = 0; i < clickTimesL; i++)
                {
                    tpClicker.execute(pos);
                    saveNode = saveNode.Previous;
                    StaticCurrentScreen.set(saveNode.Value);
                }
            }
            else
            {
                Position pos = ((AbstractControlArrow)tmpControlList[0]).getLastArrow().rect.getCenter();
                for (int i = 0; i < clickTimesR; i++)
                {
                    tpClicker.execute(pos);
                    saveNode = saveNode.Next;
                    StaticCurrentScreen.set(saveNode.Value);
                }
            }
        }

        public override IIterator createReadIterator()
        {
            if (0 == count)
                throw new FTBAutoTestException("Create read iterator error, cause the screenCircularLinkList was empty.");
            if (null == readIterator)
                this.readIterator = new ScreenCircularDoublyLinkedListReadIterator(this);
            return readIterator;
        }
        public override IIterator createShowIterator()
        {
            if (0 == count)
                throw new FTBAutoTestException("Create show iterator error, cause the screenLinkList was empty.");
            if (null == showIterator)
                this.showIterator = new ScreenCircularDoublyLinkedListShowIterator(this, tpClicker);
            return showIterator;
        }
    }
}
