using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     *  Description: Iterator manipulation interface of Implementation class
     */
    class TreeMemoryFTBCommonLevelIterator : IIterator
    {
        public int levelIndex { get; set; }
        private TreeMemoryFTBCommonAggregate treeMemoryAggregate;

        public TreeMemoryFTBCommonLevelIterator(TreeMemoryFTBCommonAggregate aggregate)
        {
            treeMemoryAggregate = aggregate;
        }

        void IIterator.first()
        {
            levelIndex = 0;
        }

        void IIterator.next()
        {
            if (levelIndex < treeMemoryAggregate.levelCompositeIndexes.Count)
            {
                levelIndex++;
            }
        }
        bool IIterator.isDone()
        {
            if (levelIndex >= treeMemoryAggregate.levelCompositeIndexes.Count)
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
            return levelIndex;
        }
    }
}
