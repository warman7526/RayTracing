using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Interval
    {
        public double min, max;

        public Interval() //default interval is universal
        {
            max = double.PositiveInfinity;
            min = double.NegativeInfinity;
        }
        public Interval(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public double Size()
        {
            return max - min;
        }

        public bool Contains(double value) { return min <= value && value <= max; }
        public bool Surrounds(double value) { return min < value && value < max; }

        public double Clamp(double value) { return min > value ? min : (max < value ? max : value); }

        public static Interval empty = new Interval(double.PositiveInfinity, double.NegativeInfinity);
        public static Interval universe = new Interval();
    }
}
