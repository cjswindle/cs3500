using FormulaEvaluator;
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
            //my test expressions
            Console.WriteLine(Evaluator.Evaluate("5+   (10)-  7", s=>0));  //8
            Console.WriteLine(Evaluator.Evaluate("(5+3)+ 5 * 10", s => 0)); //58
            Console.WriteLine(Evaluator.Evaluate("5+ 3* (5 - 10)", s => 0)); //-10
            Console.WriteLine(Evaluator.Evaluate("10/ (5- (1+2))", s => 0));//5
            Console.WriteLine(Evaluator.Evaluate("10", s => 0));//10
            Console.WriteLine(Evaluator.Evaluate("aBeR 1 5 0", s => 666));//150


            //provided test expressions
            Console.WriteLine(Evaluator.Evaluate("(2+Z5) /4", SimpleLookup)); // results in 3
            Console.WriteLine(Evaluator.Evaluate("5 * (Z6-4  )", OtherLookup)); // results in 5

            Console.Read();
        }
        
        //Faux delegate method to simulate lookup for testing
        public static int SimpleLookup(string v)
        {
            // Do anything here. Decide whether or not this delegate has a value for v, and return its value, or throw if it doesn't.

            if (v == "Z6")
                return 20;
            else if (v == "Z5")
                return 10;
            else
                throw new ArgumentException("Has no value.");
        }

        //second faux delegate used to hold another look up method
        public static int OtherLookup(string v)
        {
            if (v == "Z6")
                return 5;
            else
                throw new ArgumentException("Has no value.");
        }
    }
}
