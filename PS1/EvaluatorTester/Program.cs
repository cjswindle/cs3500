using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluatorTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Evaluator.Evaluate("1+Z6-4", SimpleLookup); // results in 17

            Evaluator.Evaluate("1+Z6-4", OtherLookup); // results in 7
        }
        
        //Faux delegate method to simulate lookup for testing
        public static int SimpleLookup(string v)
        {
            // Do anything here. Decide whether or not this delegate has a value for v, and return its value, or throw if it doesn't.

            if (v == "Z6")
                return 20;
            else if (v == ...)
                //...
            else
               // throw ...
        }

        //second faux delegate used to hold another look up method
        public static int OtherLookup(string v)
        {
            if (v == "Z6")
                return 10;
            else
               // throw ...
        }
    }
}
