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
        public delegate int Lookup(String v);

        /// <summary>
        /// this is the constructor for the static Evaluator class 
        /// </summary>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            int value;

            //TO DO
            //Creates an array of substings
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<int> valueStack = new Stack<int>();
            Stack<char> operandStack = new Stack<char>();

            foreach (string t in substrings)
            {
                //If t is Int
                if (int.TryParse(t, out value))
                {

                    if (StackExtensions.isOnTop<char>(operandStack, '*' ) )
                    {
                        value = value * valueStack.Pop();
                    }
                    if (StackExtensions.isOnTop<char>(operandStack, '/'))
                    {
                        value = value * valueStack.Pop();
                    }

                    valueStack.Push(value);
                }
                
                //if t is a variable

                //if t + or -

                //if t is * or /

                //if t is (

                //if t is )

                //matches for variable
                Regex.IsMatch(t, @"^\s*[a-zA-Z]+\d+\s*$");
                   
            }

            //use regex to check for patterns to check variables
            //check if substrings are valid, if valid place in proper stack.  If unvalid, throw exception
            //if substring is a variable, check to make sure it is proper type...if proper pass to delegate

            //perform math based on condition of stacks

            //once operandStack is empty return value of valueStack
            //if operand stack is not empty and valueStack does have value throw exception

            return valueStack.Pop();
        }




    }

    /// <summary>
    /// A class for holding extensions to Stack
    /// </summary>
    static class StackExtensions
    {
        /// <summary>
        /// An extention for a Stack that determines if the given value is at the top of the stack.
        /// Peek is insufficient because it will throw if the stack is empty.
        /// </summary>
        /// <typeparam name="T">The generic type placeholder</typeparam>
        /// <param name="s">The stack (this)</param>
        /// <param name="val">The value to look for on the top of the stack</param>
        /// <returns>Returns whether or not val is on the top of the stack</returns>
        public static bool isOnTop<T>(this Stack<T> s, T val)
        {
            //check is empty, then peek and return bool
            if (s.Count >= 1)
            {
                return val.Equals(s.Peek());
            }
            return false;
        }
    }
}
