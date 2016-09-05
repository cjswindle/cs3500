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
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            return 0;
        }


    }
}
