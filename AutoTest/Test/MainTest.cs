using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Test
{
    class MainTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            AbstractTest test;
            //UI Test
            test = new FTBTestFormTest();
            test.TestMain(args);
        }
    }
}
