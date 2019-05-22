using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ScreenDictionaryShowIterator:IIterator
    {
        private int showIndex;
        private ScreenDictionary screenAggregate;

        public ScreenDictionaryShowIterator(ScreenDictionary screenAggregate)
        {
            this.screenAggregate = screenAggregate;
        }

        void IIterator.first()
        {
            showIndex = 0;
            screenAggregate.showScreen(0);
        }
        void IIterator.next()
        {
            screenAggregate.showScreen(showIndex);
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
