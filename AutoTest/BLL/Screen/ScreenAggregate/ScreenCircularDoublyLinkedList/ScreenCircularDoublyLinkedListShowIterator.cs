using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{ 
    class ScreenCircularDoublyLinkedListShowIterator:IIterator
    {
        private CircularLinkedListNode<Screen> showPointer;
        private int showIndex;
        private ScreenCircularDoublyLinkedList screenAggregate;
        private AbstractCommandExecutor clicker;

        public ScreenCircularDoublyLinkedListShowIterator(ScreenCircularDoublyLinkedList screenAggregate,
                                                          AbstractCommandExecutor clicker)
        {
            this.screenAggregate = screenAggregate;
            this.clicker = clicker;
        }
        void IIterator.first()
        {
            showIndex = 0;
            screenAggregate.showScreen(showIndex);
            showPointer = screenAggregate.getFirstNode();
        }
        void IIterator.next()
        {
            Screen savedScreen = showPointer.Value;
            AbstractControl tmpControlArrow;
            tmpControlArrow = savedScreen.getScrollControl();
            if (null == tmpControlArrow)
                throw new FTBAutoTestException("Could not found arrowControl in savedScreen.");
            Position pos;
            pos = ((AbstractControlArrow)tmpControlArrow).getLastArrow().rect.getCenter();
            //do move operation
            clicker.execute(pos);
            showPointer = showPointer.Next;
            StaticCurrentScreen.set(showPointer.Value);
            showIndex++;
        }
        int IIterator.currentItem()
        {
            return showIndex;
        }
        bool IIterator.isDone()
        {
            return showIndex >= screenAggregate.getCount();
        }
    }
}
