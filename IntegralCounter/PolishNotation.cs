using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntegralCounter
{
    public static class MathParser
    {
        private static string[] Tokenize(string input)
        {
            string pattern = @"([\+\-\*\/\^])|((\d+(\.\d+)?|\.\d+)(x)?)|(x)|(sin)|(cos)|(tg)|(ctg)|(ln)";
            List<string> tokens = new List<string>();

            foreach (Match match in Regex.Matches(input, pattern))
            {
                tokens.Add(match.Value);
            }
            return tokens.ToArray();
        }
        public static double Evaluate(string expression, double x)
        {
            string[] tokens = Tokenize(expression);
            Stack<double> stack = new Stack<double>();

            double right, left, value, result;

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double v))
                {
                    stack.Push(v);
                }

                else if (token == "x")
                {
                    stack.Push(x);
                }

                else if (IsOperator(token))
                {
                    right = stack.Pop();
                    left = stack.Pop();
                    result = ApplyOperator(left, right, token);
                    stack.Push(result);
                }

                else if (IsFunction(token))
                {
                    value = stack.Pop();
                    result = ApplyFunction(value, token);
                    stack.Push(result);
                }
            }

            return stack.Pop();
        }

        private static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }
        private static bool IsFunction(string token)
        {
            return token == "sin" || token == "cos" || token == "tg" || token == "ctg" || token == "ln";
        }
        private static double ApplyOperator(double left, double right, string op)
        {
            switch (op)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
                case "^":
                    return Math.Pow(left, right);
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }
        private static double ApplyFunction(double value, string function)
        {
            switch (function)
            {
                case "sin":
                    return Math.Sin(value);
                case "cos":
                    return Math.Cos(value);
                case "tg":
                    return Math.Tan(value);
                case "ctg":
                    return 1 / Math.Tan(value);
                case "ln":
                    return Math.Log(value);
                default:
                    throw new ArgumentException("Invalid operator: " + value);
            }
        }


    }
}
