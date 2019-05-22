using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenCircularDoublyLinkedListReadIterator:IIterator
    {
        private CircularLinkedListNode<Screen> readPointer;
        private int readIndex;
        private ScreenCircularDoublyLinkedList screenAggregate;
        public ScreenCircularDoublyLinkedListReadIterator(ScreenCircularDoublyLinkedList screenAggregate)
        {
            this.screenAggregate = screenAggregate;
        }
        void IIterator.first()
        {
            readPointer = screenAggregate.getFirstNode();
            readIndex = 0;
        }
        void IIterator.next()
        {
            readPointer = readPointer.Next;
            readIndex++;
        }
        int IIterator.currentItem()
        {
            return readIndex;
        }
        bool IIterator.isDone()
        {
            return readIndex >= screenAggregate.getCount();
        }
    }
}
