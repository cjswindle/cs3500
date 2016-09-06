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
            Console.Write(Evaluator.Evaluate("5+10", s=>0));  //15
            Console.Write(Evaluator.Evaluate("5-10", s => 0)); //-5
            Console.Write(Evaluator.Evaluate("5*10", s => 0)); //50
            Console.Write(Evaluator.Evaluate("5/10", s => 0));//0
            Console.Write(Evaluator.Evaluate("10/5", s => 0));//2
            //Console.Write(Evaluator.Evaluate("10/0", s => 0));//error


            //provided test expressions
            Console.Write(Evaluator.Evaluate("1+Z6-4", SimpleLookup)); // results in 17 or 7
            Console.Write(Evaluator.Evaluate("1+Z6-4", OtherLookup)); // results in 2

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
