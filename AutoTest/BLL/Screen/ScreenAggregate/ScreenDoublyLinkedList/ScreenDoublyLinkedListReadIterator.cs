using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenDoublyLinkedListReadIterator : IIterator
    {
        private LinkedListNode<Screen> readPointer;
        private int readIndex;
        private ScreenDoublyLinkedList screenAggregate;
        public ScreenDoublyLinkedListReadIterator(ScreenDoublyLinkedList screenAggregate)
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
