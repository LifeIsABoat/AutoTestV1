using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    /*
     *  Description: Iterator manipulation interface
     */
    interface c
    {
        /*
         *  Description: Iterative start
         *  Return: 
         *  Exception: 
         *  Example: iterator.first();
         */
        void first();

        /*
         *  Description: Next iteration
         *  Return: 
         *  Exception: 
         *  Example: iterator.next();
         */
        void next();

        /*
         *  Description: Set the need object of import data
         *  Return: int Current iteration
         *  Exception: 
         *  Example: iterator.currentItem();
         */
        int currentItem();

        /*
         *  Description: Whether iteration to tail
         *  Return: bool, to tail is ture
         *  Exception: 
         *  Example: iterator.isDone();
         */
        bool isDone();
    }
}
