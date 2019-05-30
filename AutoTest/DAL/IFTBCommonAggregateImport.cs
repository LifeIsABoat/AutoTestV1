using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     *  Description: Import data manipulation interface
     */
    interface IFTBCommonAggregateImport
    {
        /*
         *  Description: Set the need object of import data
         *  Param: aggregate - Need object of import data
         *  Return: 
         *  Exception: 
         *  Example:commonAggregateImport.import(aggregate);
         */
        void import(IFTBCommonAggregate aggregate);
    }
}
