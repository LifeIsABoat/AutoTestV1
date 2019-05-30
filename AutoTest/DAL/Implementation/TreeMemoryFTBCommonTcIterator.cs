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
    class TreeMemoryFTBCommonTcIterator :IIterator
    {
        public int tcIndex { get; set; }
        private TreeMemoryFTBCommonAggregate treeMemoryAggregate;

        public TreeMemoryFTBCommonTcIterator(TreeMemoryFTBCommonAggregate aggregate)
        {
            treeMemoryAggregate = aggregate;
            updateLevelList_2();
        }

        void IIterator.first()
        {
            tcIndex = 0;
            updateLevelList_2();
        }

        void IIterator.next()
        {
            if (tcIndex < TreeMemoryFTBCommonAggregate.tcLeafIndexes.Count)
            {
                tcIndex++;
                if (tcIndex < TreeMemoryFTBCommonAggregate.tcLeafIndexes.Count)
                {
                    updateLevelList_2();
                }
            }
        }
        bool IIterator.isDone()
        {
            if (tcIndex >= TreeMemoryFTBCommonAggregate.tcLeafIndexes.Count)
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
            return tcIndex;
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
            for (AbstractScreenComponent node = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex];
                    node.parents != null; node = node.parents)
            {
                treeMemoryAggregate.levelCompositeIndexes.Add(node);
            }
            treeMemoryAggregate.levelCompositeIndexes.Reverse();
        }

        /*
         *  Description: Update the contents of the level index
         *  Return: 
         *  Exception: 
         *  Example: updateLevelList();
         */
        private void updateLevelList_2()
        {
            treeMemoryAggregate.levelCompositeIndexes.Clear();
            OptionInfo ftboption = (OptionInfo)(TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex].ftbButton);
            if (ftboption.ftbMcc.isMccValid() == true)
            {
                for (AbstractScreenComponent node = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex];
                     node.parents != null; node = node.parents)
                {
                    treeMemoryAggregate.levelCompositeIndexes.Add(node);
                }
                treeMemoryAggregate.levelCompositeIndexes.Reverse();
            }
            else
            {
                tcIndex++;
                if (tcIndex < TreeMemoryFTBCommonAggregate.tcLeafIndexes.Count)
                {
                    updateLevelList_2();
                }
            }
        }

    }
}
