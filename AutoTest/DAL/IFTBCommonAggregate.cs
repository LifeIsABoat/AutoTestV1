using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     *  Description: IFTBCommonAggregate class interface
     */
    interface IFTBCommonAggregate
    {
        /*
         *  Description: Loaded the Aggregateof data
         *  Return: 
         *  Exception: 
         *  Example:aggregate.import();
         */
        void importTree();
        void importScreenDict();

        /*
         *  Description: Create an iterator
         *  Param: type - iterator type
         *  Return: IIterator
         *  Exception: 
         *  Example: tcIterator = aggregate.createMccFilteredTcIterator(type);
         */
        IIterator createMccFilteredTcIterator();
        
        /*
         *  Description: Create an iterator
         *  Param: type - iterator type
         *  Return: IIterator
         *  Exception: 
         *  Example: levelIterator = aggregate.creatLevelIterator(type);
         */
        IIterator createLevelIterator();
        /*
         *  Description: Create an iterator
         *  Param: type - iterator type
         *  Return: IIterator
         *  Exception: 
         *  Example: tcIterator = aggregate.createSelectedTcIterator(type);
         */
        IIterator createSelectedTcIterator();
    }
}
