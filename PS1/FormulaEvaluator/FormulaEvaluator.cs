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
        //public static Stack<int> valueStack;
        //public static Stack<char> operandStack;
        public delegate int Lookup(String v);

        /// <summary>
        /// this is the constructor for the static Evaluator class 
        /// </summary>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            int value;
            char operand;

            //remove whitespace between digits and operands only
            //exp = Regex.Replace(exp, @"(?<=\b\d+)\s+(?=\d+\b)", "");
            exp = exp.Replace(" ", ""); 
            //Creates an array of substings
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<int> valueStack = new Stack<int>();
            Stack<char> operandStack = new Stack<char>();

            //check if substrings are valid, if valid place in proper stack.  If unvalid, throw exception
            foreach (string t in substrings)
            {
                //If t is Int
                if (int.TryParse(t, out value))
                {
                    if (StackExtensions.isOnTop<char>(operandStack, '*'))
                    {
                        value = Multiply(value, valueStack, operandStack);
                    }
                    else if (StackExtensions.isOnTop<char>(operandStack, '/'))
                    {
                        value = Divide(value, valueStack, operandStack);
                    }

                    valueStack.Push(value);
                }
                //if t is not an int or variable
                else if (char.TryParse(t, out operand))
                {
                    //if addition or subtraction
                    if (operand == '+' || operand == '-')
                    {
                        //if addition operator is on stack, add
                        if (StackExtensions.isOnTop<char>(operandStack, '+'))
                        {
                            value = Add(valueStack, operandStack);
                        }
                        //if subtraction operator is on stack, subtract
                        else if (StackExtensions.isOnTop<char>(operandStack, '-'))
                        {
                            value = Subtract(valueStack, operandStack);
                        }
                        operandStack.Push(operand);
                    }
                    //if t is * or / or (
                    else if (operand == '*' || operand == '/' || operand == '(')
                    {
                        operandStack.Push(operand);
                    }
                    //if t is )
                    else if (operand == ')')
                    {
                        //add inside parenthesis
                        if (StackExtensions.isOnTop<char>(operandStack, '+'))
                        {
                            value = Add(valueStack, operandStack);
                            valueStack.Push(value);
                        }
                        //subtract inside parenthesis
                        else if (StackExtensions.isOnTop<char>(operandStack, '-'))
                        {
                            value = Subtract(valueStack, operandStack);
                            valueStack.Push(value);
                        }

                        //check matching ( is ontop of operand stack
                        if (!StackExtensions.isOnTop<char>(operandStack, '('))
                        {
                            throw new ArgumentException("This is an invalid expression!");
                        }
                        else
                        {
                            operandStack.Pop();
                        }

                        //check operand stack for * or /
                        if (StackExtensions.isOnTop<char>(operandStack, '*') || StackExtensions.isOnTop<char>(operandStack, '/'))
                        {
                            if (valueStack.Count <2)
                            {
                                throw new ArgumentException("This is an invalid expression!");
                            }

                            value = valueStack.Pop();

                            if (StackExtensions.isOnTop<char>(operandStack, '*'))
                            {
                                value = Multiply(value, valueStack, operandStack);
                                valueStack.Push(value);
                            }
                            else if (StackExtensions.isOnTop<char>(operandStack, '/'))
                            {
                                value = Divide(value, valueStack, operandStack);
                                valueStack.Push(value);
                            }  
                        }
                        
                    }
                    
                }
                //if t is a variable
                //use regex to check for patterns to check variables
                else if (Regex.IsMatch(t, @"^\s*[a-zA-Z]+\d+\s*$"))
                {
                    value = variableEvaluator(t);

                    if (StackExtensions.isOnTop<char>(operandStack, '*'))
                    {
                        value = Multiply(value, valueStack, operandStack);
                    }
                    else if (StackExtensions.isOnTop<char>(operandStack, '/'))
                    {
                        value = Divide(value, valueStack, operandStack);
                    }

                    valueStack.Push(value);
                }
                else
                {
                    throw new ArgumentException("This expression contains invalid operators or variables.");
                }
            }

            if (operandStack.Count != 0)
            {
                //Should only be one operand left in expression
                if (operandStack.Count > 1)
                {
                    throw new ArgumentException("Too many operands in this expression!");
                }
                //Perform addition or subtraction
                if (StackExtensions.isOnTop<char>(operandStack, '+'))
                {
                    value = Add(valueStack, operandStack);
                }
                else if (StackExtensions.isOnTop<char>(operandStack, '-'))
                {
                    value = Subtract(valueStack, operandStack);
                }
                //Operand left was not + or - meaning you have to many operands!
                else
                {
                    throw new ArgumentException("Too many operands in this expression!");
                }

                valueStack.Push(value);
            }

            if (valueStack.Count != 1)
            {
                    throw new ArgumentException("You have to many values leftover!");
            }

            //once operandStack is empty return value of valueStack
            return valueStack.Pop();
        }

        /// <summary>
        /// Helper method called whenever Subtraction is required
        /// </summary>
        /// <param name="valueStack">Value Stack</param>
        /// <param name="operandStack">Operand Stack</param>
        /// <returns>Returns the value of the partial expression</returns>
        private static int Subtract(Stack<int> valueStack, Stack<char> operandStack)
        {
            if (valueStack.Count < 2)
            {
                throw new ArgumentException("This is an invalid expression, there is nothing to subtract!");
            }
            int value = valueStack.Pop();
            operandStack.Pop();

            value = valueStack.Pop() - value;
            return value;
        }

        /// <summary>
        /// Helper method called whenever Addition is required
        /// </summary>
        /// <param name="valueStack">Value Stack</param>
        /// <param name="operandStack">Operand Stack</param>
        /// <returns>Returns the value of the partial expression</returns>
        private static int Add(Stack<int> valueStack, Stack<char> operandStack)
        {
            if(valueStack.Count < 2)
            {
                throw new ArgumentException("This is an invalid expression, there is nothing to add!");
            }
            int value;
            operandStack.Pop();
            value = valueStack.Pop() + valueStack.Pop();
            return value;
        }

        /// <summary>
        /// Helper method called whenever Division is required
        /// </summary>
        /// <param name="valueStack">Value Stack</param>
        /// <param name="operandStack">Operand Stack</param>
        /// <returns>Returns the value of the partial expression</returns>
        private static int Divide(int value, Stack<int> valueStack, Stack<char> operandStack)
        {
            if(value == 0)
                                {
                throw new DivideByZeroException("Cannot divide by zero!");
            }
            if (operandStack.Count == 0)
            {
                throw new ArgumentException("This is an invalid Expression!");
            }
            operandStack.Pop();
            value = valueStack.Pop() / value;
            return value;
        }

        /// <summary>
        /// Helper method called whenever Multiplication is required
        /// </summary>
        /// <param name="valueStack">Value Stack</param>
        /// <param name="operandStack">Operand Stack</param>
        /// <returns>Returns the value of the partial expression</returns>
        private static int Multiply(int value, Stack<int> valueStack, Stack<char> operandStack)
        {
            if(operandStack.Count == 0)
            {
                throw new ArgumentException("This is an invalid Expression!");
            }
            operandStack.Pop();
            value = value * valueStack.Pop();
            return value;
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
