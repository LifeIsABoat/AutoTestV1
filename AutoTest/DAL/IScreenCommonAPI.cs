using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    interface IScreenCommonAggregate
    {
        /*
        *  Description: Loaded the Aggregateof data
        *  Return: 
        *  Exception: 
        *  Example:aggregate.import();
        */
        void importScreen(string path);

        /*
         *  Description: Create an iterator
         *  Param: type - iterator type
         *  Return: IIterator
         *  Exception: 
         *  Example: screenIterator = aggregate.createSelectedTcIterator(type);
         */
        IIterator createScreenIterator();
    }
}
