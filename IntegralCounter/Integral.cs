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
        private double upperLimit;
        private double lowerLimit;
        private string function;
        private PointF[] points;
        private double value;
        public double Value => value;
        public PointF[] Points
        {
            get => points;
        }

        public Integral(int upperLimit, int lowerLimit, string function)
        {
            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;
            this.function = function;
            Integrate(1000000);
        }

        private void Integrate(int intervals)
        {
            double deltaX = (upperLimit - lowerLimit) / intervals;
            double sum = 0.0;

            List<PointF> points = new List<PointF>();

            for (int i = 0; i < intervals; i++)
            {
                double x = lowerLimit + i * deltaX;
                double y = MathParser.Evaluate(function, x);
                sum += y * deltaX;

                if (i % 10000 == 0)
                {
                    float floatX = (float)x;
                    float floatY = (float)y;
                    points.Add(new PointF(floatX, floatY));
                }
            }

            this.points = points.ToArray();
            value = sum;
        }

        public override string ToString() => Value.ToString();
    }
}