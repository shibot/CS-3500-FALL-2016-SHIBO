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
        public delegate int Lookup(String v);

        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            //create two empty stack, one for values and one for operators.
            Stack<String> operatorStack = new Stack<String>();
            Stack<int> valueStack = new Stack<int>();
            //remove all leading and trailing white-space characters from the current String object.
            exp = exp.Trim();
            //split input string into tokens
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            //check if the String is empty, if it is empty, then throw an exception.
            if (exp.Equals(""))
            {
                throw new ArgumentException("No Expression enter!");
            }
            //using For-loop to check all elements in the substring
            for (int i = 0; i < substrings.Length; i++)
            {
                //put the token into a string, use Trim to remove the leading and traling white-space. 
                String element = substrings[i].Trim();
                //If the element is a space, then continue to the loop.
                if (element.Equals(""))
                {
                    continue;

                }
                //If the element is + or - symbol, use the while loop to check whether process operator.
                else if (element.Equals("+") || element.Equals("-"))
                {   //If the operator stack is not empty and operator peek element meet one of the symbol,evaluate the value.
                    while (operatorStack.Count != 0 && (operatorStack.Peek().Equals("+") || operatorStack.Peek().Equals("-")
                        || operatorStack.Peek().Equals("*") || operatorStack.Peek().Equals("/")))
                    {
                        //use helper method valueOperator to evaluate.
                        Evaluator.valueOperator(valueStack, operatorStack);
                    }
                    //push the element into operator stack.
                    operatorStack.Push(element);
                }
                //If the element is * and / symbol,use the while loop to check whether process operator.
                else if (element.Equals("*") || element.Equals("/"))
                {
                    while (operatorStack.Count != 0 && (operatorStack.Peek().Equals("*") || operatorStack.Peek().Equals("/")))
                    {
                        Evaluator.valueOperator(valueStack, operatorStack);
                    }
                    operatorStack.Push(element);
                }
                //If element meet the "(" symbol, simply push it into operatorStack.
                else if (element.Trim().Equals("("))
                {
                    operatorStack.Push("(");
                }
                //If the element meet the ")", we need to know if there is "(" symbol on the top of stack.
                else if (element.Trim().Equals(")"))
                {
                    try
                    {
                        while (!operatorStack.Peek().Equals("("))
                        {
                            Evaluator.valueOperator(valueStack, operatorStack);
                        }
                        operatorStack.Pop();
                    }
                    catch
                    {
                        throw new ArgumentException();
                    }
                }
                //If we meet a token except the "+" "-" "*" "/" "(" ")", we need to try to parse this token to the integer.
                else
                {
                    int value;
                    //if the value successful parse, push it into the value stack.
                    if (Int32.TryParse(element, out value))
                    {
                        valueStack.Push(value);
                    }
                    else
                    {
                        //otherwise try to find the value of this element and push the value into valueStack.
                        try
                        {
                            value = variableEvaluator(element);
                            valueStack.Push(value);
                        }
                        catch
                        {
                            throw new ArgumentException();
                        }
                    }
                }
            }
            while(operatorStack.Count != 0)
            {
                if(operatorStack.Count > 0 && valueStack.Count < 2)
                {
                    throw new ArgumentException("This expression is invalid");
                }
                Evaluator.valueOperator(valueStack, operatorStack);
            }

            if(valueStack.Count == 1 && operatorStack.Count == 0)
            {
                return valueStack.Pop();
            }
            else
            {
                throw new ArgumentException("Invalid Expression");
            }
        }

        public static void valueOperator(Stack<int> valueStack, Stack<string> operatorStack)
        {
                String Os = operatorStack.Pop();
                if (valueStack.Count < 2)
                    {
                        throw new ArgumentException();
                    }
                //Create two integers and pop the operator stack twice
                int Op1 = valueStack.Pop();
                int Op2 = valueStack.Pop();
                //If the token is "+", then Op2 + Op1
                if (Os.Equals("+"))
                {
                //Push the result into the operand stack
                    valueStack.Push(Op2 + Op1);
                }
                //If the token is "-", then Op2 - Op1
                else if (Os.Equals("-"))
                {
                    //Push the result into the operand stack
                    valueStack.Push(Op2 - Op1);
                }
                //If the token is "/", then Op2 / Op1
                else if (Os.Equals("/"))
                {
                    //If the denominator is zero, then throw an exception
                    if ((Op1 == 0 && Op2 != 0) || (Op1 != 0 && Op2 == 0))
                    {
                        throw new ArgumentException("Can not divided by zero");
                    }
                    //If not, push the result into the operand stack
                    else
                    {
                        valueStack.Push(Op2 / Op1);
                    }
                }
                //If the token is "*", then Op2 * Op1
                else if (Os.Equals("*"))
                {
                    //Push the result into the operand stack
                    valueStack.Push(Op2 * Op1);
                }
            }
    }
}
