using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralCounter
{
    delegate decimal Function(decimal x);
    public class Integral
    {
        private Function function;
        private decimal upperLimit;
        private decimal lowerLimit;
        private Function differential;
        public decimal Value
        {
            get
            {
                if (differential == null)
                {
                    return Simpson();
                }
                else return 1;
            }
        }
        public decimal UpperLimit => upperLimit;
        public decimal LowerLimit => lowerLimit;

        private const int intervals = 2000;

        public Integral(decimal lowerLimit, decimal upperLimit, string function)
        {
            if (lowerLimit > upperLimit)
            {
                var temp = upperLimit;
                upperLimit = lowerLimit;
                lowerLimit = temp;
            }

            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;
            this.function = x => { return MathParser.Evaluate(function, x); };
        }

        public Integral(decimal lowerLimit, decimal upperLimit, string function, string differential)
        {
            if (lowerLimit > upperLimit)
            {
                var temp = upperLimit;
                upperLimit = lowerLimit;
                lowerLimit = temp;
            }

            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;
            this.function = x => { return MathParser.Evaluate(function, x); };
            this.differential = x => { return MathParser.Evaluate(differential, x); };
        }

        public decimal Rectangle()
        {
            decimal deltaX = (upperLimit - lowerLimit) / intervals;
            decimal sum = 0;

            for (int i = 0; i < intervals; i++)
            {
                decimal x = lowerLimit + i * deltaX;
                try
                {
                    decimal y = function(x);
                    sum += y * deltaX;
                }
                catch (OutOfScopeException)
                {
                    sum += 0;
                }
            }
            return sum;
        }

        public decimal Trapezoidal()
        {
            decimal h = (upperLimit - lowerLimit) / intervals;
            decimal sum;
            try
            {
                sum = 0.5m * (function(upperLimit) + function(lowerLimit));
            }
            catch (OutOfScopeException)
            {
                sum = 0;
            }
            for (int i = 1; i < intervals; i++)
            {
                try
                {
                    decimal x = lowerLimit + i * h;
                    sum += function(x);
                }
                catch (OutOfScopeException)
                {
                    sum += 0;
                }

            }
            return h * sum;
        }

        public decimal Simpson()
        {
            if (intervals % 2 != 0)
                throw new ArgumentException("n must be even");
            decimal h = (upperLimit - lowerLimit) / intervals;
            decimal sum;
            try
            {
                sum = function(upperLimit) + function(lowerLimit);

            }
            catch (OutOfScopeException)
            {
                sum = 0;
            }
            for (int i = 1; i < intervals; i++)
            {
                try
                {
                    decimal x = lowerLimit + i * h;
                    sum += 2 * function(x) * (1 + i % 2);
                }
                catch (OutOfScopeException)
                {
                    sum += 0;
                }
            }
            return h / 3 * sum;
        }

        public decimal SimsonWithDiff()
        {
            decimal h = (upperLimit - lowerLimit) / intervals;
            decimal[] x = new decimal[intervals + 1];
            decimal[] y = new decimal[intervals + 1];
            for (int i = 0; i <= intervals; i++)
            {
                x[i] = lowerLimit + i * h;
                y[i] = function(x[i]) * differential(x[i]);
            }
            decimal value = 0;
            for (int i = 0; i < intervals - 1; i += 2)
            {
                value += (y[i] + 4 * y[i + 1] + y[i + 2]) * h / 3;
            }
            return value;
        }

        public override string ToString() => Value.ToString();
    }
}