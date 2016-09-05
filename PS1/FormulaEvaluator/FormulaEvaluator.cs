using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    public static class Evaluator
    {
        public static Stack<int> valueStack;
        public static Stack<char> operandStack;
        public static string expression;
        public delegate int Lookup(String v);

        /// <summary>
        /// this is the constructor for the static Evaluator class 
        /// </summary>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            //TO DO
            //Creates an array of substings
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            //use regex to check for patterns to check variables
            //check if substrings are valid, if valid place in proper stack.  If unvalid, throw exception
            //if substring is a variable, check to make sure it is proper type...if proper pass to delegate

            //perform math based on condition of stacks

            //once operandStack is empty return value of valueStack
            //if operand stack is not empty and valueStack does have value throw exception

            return 0;
        }


    }
}
