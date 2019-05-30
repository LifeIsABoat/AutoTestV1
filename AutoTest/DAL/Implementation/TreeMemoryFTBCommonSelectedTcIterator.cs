using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    class TreeMemoryFTBCommonSelectedTcIterator : IIterator
    {
         public int sortedTcIndex { get; set; }
        private TreeMemoryFTBCommonAggregate treeMemoryAggregate;

        public TreeMemoryFTBCommonSelectedTcIterator(TreeMemoryFTBCommonAggregate aggregate)
        {
            treeMemoryAggregate = aggregate;
            updateLevelList();
        }

        void IIterator.first()
        {
            sortedTcIndex = 0;
            updateLevelList();
        }

        void IIterator.next()
        {
            if (sortedTcIndex < TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes.Count)
            {
                sortedTcIndex++;
                if (sortedTcIndex < TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes.Count)
                {
                    updateLevelList();
                }
            }
        }
        bool IIterator.isDone()
        {
            if (sortedTcIndex >= TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes.Count)
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
            //return sortedTcIndex;
            return TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes[sortedTcIndex];
        }


        /*
         *  Description: Update the contents of the level index
         *  Return: 
         *  Exception: 
         *  Example: updateLevelList();
         */
        private void updateLevelList()
        {
            treeMemoryAggregate.levelCompositeIndexes.Clear();
            if (TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes.Count == 0)
            {
                return;
            }
            int tcIndex = TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes[sortedTcIndex];
            if(tcIndex < 0)
            {
                return;
            }
            for (AbstractScreenComponent node = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex];
                    node.parents != null; node = node.parents)
            {
                treeMemoryAggregate.levelCompositeIndexes.Add(node);
            }
            treeMemoryAggregate.levelCompositeIndexes.Reverse();
        }
    }
}
