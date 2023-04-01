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

        private static readonly List<string> functions = new List<string>() { "sin", "cos", "tg", "ctg", "ln", "sqrt" };
        private static readonly List<string> operators = new List<string>() { "+", "-", "*", "/", "^" };


        private static bool isNumber(string token) => double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
        private static bool isFunction(string token) => functions.Contains(token);
        private static bool isOperator(string token) => operators.Contains(token);
        private static bool isVariable(string token)
        {
            if (isFunction(token)) return false;
            foreach (char c in token)
            {
                if (!char.IsLetter(c) || !((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
                {
                    return false;
                }
            }
            return true;
        }
        private static byte GetPriority(string @operator)
        {
            switch (@operator)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                case "~":
                    return 4;
                default:
                    return 5;
            }
        }
        private static IEnumerable<string> Tokenization(string input)
        {
            string pattern = $@"(\d+([.,]\d+)?)|([a-zA-Z]{{1,4}})|(({string.Join("|", functions)})(?=\())|([\+\-\*/\(\)^])";

            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                yield return match.Value;
            }
        }
        private static string[] TransformInfixToPostfixNotation(string infixString)
        {

            var stack = new Stack<string>();
            var outputQueue = new Queue<string>();

            string? prevToken = null;

            foreach (var token in Tokenization(infixString))
            {
                if (isNumber(token) || isVariable(token))
                {
                    outputQueue.Enqueue(token);
                }
                if (isFunction(token))
                {
                    stack.Push(token);
                }
                if (token == ",")
                {
                    while (stack.Peek() != "(")
                    {
                        outputQueue.Enqueue(stack.Pop());
                        if (stack.Count == 0) throw new PolishNotationException("Стек закончился до того, как был встречен токен открывающая скобка - в выражении пропущен разделитель аргументов функции (запятая), либо пропущена открывающая скобка.");
                    }

                }
                if (isOperator(token))
                {
                    string op = token;

                    if (op == "-" && (prevToken == null || operators.Contains(prevToken) || prevToken == "("))
                    {
                        op = "~";
                    }

                    while (stack.Count > 0 && GetPriority(op) <= GetPriority(stack.Peek()))
                    {
                        outputQueue.Enqueue(stack.Pop());
                    }
                    stack.Push(op);
                }
                if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        outputQueue.Enqueue(stack.Pop());
                        if (stack.Count == 0) throw new PolishNotationException("стек закончился до того, как был встречен токен открывающая скобка. В выражении пропущена скобка");
                    }

                    stack.Pop();

                    if (isFunction(stack.Peek()))
                    {
                        outputQueue.Enqueue(stack.Pop());
                    }
                }
                prevToken = token;
            }

            while (stack.Count > 0)
            {
                if (stack.Peek() == "(") throw new PolishNotationException("Токен оператор на вершине стека — открывающая скобка, в выражении пропущена скобка");
                outputQueue.Enqueue(stack.Pop());
            }

            return outputQueue.ToArray();

        }

        //Служебные Функции Evaluate
        private static decimal ApplyOperator(decimal left, decimal right, string @operator)
        {
            try
            {
                switch (@operator)
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
                        return (decimal)Math.Pow((double)left, (double)right);

                    default:
                        throw new PolishNotationException("Invalid operator: " + @operator);
                }
            }
            catch
            {
                throw new OutOfScopeException();
            }
           
        }
        private static decimal ApplyFunction(decimal value, string function)
        {
            try
            {
                switch (function)
                {
                    case "sin":
                        return (decimal)Math.Sin((double)value);
                    case "cos":
                        return (decimal)Math.Cos((double)value);
                    case "tg":
                        return (decimal)Math.Tan((double)value);
                    case "ctg":
                        return 1 / (decimal)Math.Tan((double)value);
                    case "ln":
                        return (decimal)Math.Log((double)value);
                    case "sqrt":
                        return (decimal)Math.Sqrt((double)value);
                    default:
                        throw new PolishNotationException("Invalid operator: " + value);
                }
            }
            catch
            {
                throw new OutOfScopeException();
            }
        }

        //Всё Ради Этого 
        public static decimal Evaluate(string function, decimal x)
        {
            Stack<decimal> stack = new Stack<decimal>();

            decimal right, left, value, result;

            foreach (var token in TransformInfixToPostfixNotation(function))
            {
                if (isNumber(token))
                {
                    stack.Push(Convert.ToDecimal(token));
                }

                else if (isVariable(token))
                {
                    stack.Push(x);
                }

                else if (isOperator(token))
                {
                    right = stack.Pop();
                    left = stack.Pop();
                    result = ApplyOperator(left, right, token);
                    stack.Push(result);
                }

                else if (token == "~")
                {
                    stack.Push(-1 * stack.Pop());
                }

                else if (isFunction(token))
                {
                    value = stack.Pop();
                    result = ApplyFunction(value, token);
                    stack.Push(result);
                }
            }

            return stack.Pop();
        }

    }

    public class PolishNotationException : Exception
    {
        public PolishNotationException() : base() { }
        public PolishNotationException(string message) : base(message) { }
    }
    public class OutOfScopeException : Exception
    {
        public OutOfScopeException() : base() { }
        public OutOfScopeException(string message) : base(message) { }
    }
}
