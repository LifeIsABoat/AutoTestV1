using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    class TreeMemoryFTBCommonScreenIterator : IIterator
    {
        public int ScreenIndex { get; set; }
        private ScreenMemoryCommonAggregate screenMemoryAggregate;

        public TreeMemoryFTBCommonScreenIterator(ScreenMemoryCommonAggregate aggregate)
        {
            screenMemoryAggregate = aggregate;
        }

        void IIterator.first()
        {
            ScreenIndex = 0;
        }

        void IIterator.next()
        {
            if (ScreenIndex < screenMemoryAggregate.standardScreen.Count)
            {
                ScreenIndex++;
            }
        }
        bool IIterator.isDone()
        {
            if (ScreenIndex >= screenMemoryAggregate.standardScreen.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int IIterator.currentItem()
        {
            return ScreenIndex;
        }

    }
}
