/*
 * File Name: Leaf.cs
 * Creat Date: 2017-3-22
 * Description: 
 * Modify Content:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     * Description: The leaf of the tree
     */
    public class OptionLeaf : AbstractScreenComponent
    {
        public Dictionary<string,string> tcPath;

        //todo remove
        public bool tcStatus;

        //todo
        public List<short> tcTestResult;
        //public static Dictionary<string, string> testOpinion = new Dictionary<string, string>();

        public OptionLeaf()
            : base()
        {
        }

        /* 
         *  Description:The constructor
         *  Param:ftbButton - Objects of type FTBButtonInfo assignment to ftbButton
         *  Return:
         *  Exception: FTBAutoException
         *  Example:
         */
        public OptionLeaf(ButtonInfo ftbButton)
            : base(ftbButton)
        {
        }
        public override void addScreenComponent(AbstractScreenComponent screenComponent)
        {
            throw new FTBAutoTestException("Leaf cannot add child nodes");
        }
    }//class
}
