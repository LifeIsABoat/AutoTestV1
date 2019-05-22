using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenDictionaryReadIterator:IIterator
    {
        private int readIndex;
        private ScreenDictionary screenAggregate;

        public ScreenDictionaryReadIterator(ScreenDictionary screenAggregate)
        {
            this.screenAggregate = screenAggregate;
        }

        void IIterator.first()
        {
            readIndex = 0;
        }
        void IIterator.next()
        {
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
