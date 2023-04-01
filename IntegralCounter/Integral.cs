using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralCounter
{
    public class Integral
    {
        private decimal upperLimit;
        private decimal lowerLimit;
        private string function;
        private decimal value;
        public decimal Value => value;
        public Integral(decimal upperLimit, decimal lowerLimit, string function)
        {
            if (lowerLimit > upperLimit)
            {
                var temp = upperLimit;
                upperLimit = lowerLimit;
                lowerLimit = temp;
            }

            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;


            this.function = function;
            Integrate(10000);
        }
        private void Integrate(int intervals)
        {
            decimal deltaX = (upperLimit - lowerLimit) / intervals;
            decimal sum = 0;

            for (int i = 0; i < intervals; i++)
            {
                decimal x = lowerLimit + i * deltaX;
                try
                {
                    decimal y = MathParser.Evaluate(function, x);
                    sum += y * deltaX;
                }
                catch(OutOfScopeException)
                {
                    sum += 0;
                }
            }
            value = sum;
        }

        public override string ToString() => Value.ToString();
    }
}