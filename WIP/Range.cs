using System;
using System.Collections.Generic;

namespace zmDataTypes {

    public class Range : IComparable<Range>
    {
        public double Min;
        public double Max;

        public Range(double numberA, double numberB)
        {
            Min = Math.Min(numberA, numberB)
            Max = Math.Max(numberA, numberB)
        }

        public static Range FromMax(double number)
        {
            return new InRange(double.NegativeInfinity, number);;
        }

        public static Range FromMin(double number)
        {
            return new InRange(number, double.PositiveInfinity);
        }

        public double Mean => (Min + Max) / 2;
        public double Range => Max - Min;

        public bool Contains(double number)
        {
            return number >= Min && number <= Max;
        }

        public bool Contains(Range range)
        {
            return range.Min >= Min && range.Max <= Max;
        }

        public bool Overlaps(Range range)
        {
            return range.Min <= Max && range.Max >= Min;
        }

        // Range and double

        public static Range operator +(double number, Range range)
        {
            return new Range(number + range.Min, number + range.Max);
        }

        public static Range operator +(Range range, double number)
        {
            return new Range(range.Min + number, range.Max + number);
        }

        public static Range operator -(double number, Range range)
        {
            return new Range(number - range.Max, number - range.Min);
        }

        public static Range operator -(Range range, double number)
        {
            return new Range(range.Min - number, range.Max - number);
        }

        public static Range operator *(double number, Range range)
        {
            return new Range(number * range.Min, number * range.Max);
        }

        public static Range operator *(Range range, double number)
        {
            return new Range(range.Min * number, range.Max * number);
        }

        public static Range operator /(double number, Range range)
        {
            return new Range(number / range.Max, number / range.Min);
        }

        public static Range operator /(Range range, double number)
        {
            return new Range(range.Min / number, range.Max / number);
        }

        // Range and Range

        public static Range operator *(Range rangeA, Range rangeB)
        {
            return new Range(rangeA.Min * rangeB.Min, rangeA.Max * rangeB.Max);
        }

        public static Range operator /(Range rangeA, Range rangeB)
        {
            return new Range(rangeA.Min / rangeB.Max, rangeA.Max / rangeB.Min);
        }

        public static Range operator +(Range rangeA, Range rangeB)
        {
            return new Range(rangeA.Min + rangeB.Min, rangeA.Max + rangeB.Max);
        }

        public static Range operator -(Range rangeA, Range rangeB)
        {
            return new Range(rangeA.Min - rangeB.Max, rangeA.Max - rangeB.Min);
        }

        public int CompareTo(object obj)
        {
            if (obj is Range other)
            {
                if (Min < other.Min) return -1;
                if (Min > other.Min) return 1;
                if (Max < other.Max) return -1;
                if (Max > other.Max) return 1;
                return 0;
            }
            else if (obj is double number)
            {
                if (Min < number) return -1;
                if (Max > number) return 1;
                return 0;
            }
            else
            {
                // Not comparable
                throw new ArgumentException("Object is not a supported type");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Range other)
            {
                return Min == other.Min && Max == other.Max;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        public override string ToString()
        {
            if (Min == double.NegativeInfinity && Max == double.PositiveInfinity)
            {
                return "[-∞ <= x <= ∞]";
            }
            else if (Min == double.NegativeInfinity)
            {
                return $"[-∞ <= x <= {Max}]";
            }
            else if (Max == double.PositiveInfinity)
            {
                return $"[{Min} <= x <= ∞]";
            }
            return $"[{Min} <= x <= {Max}]";
        }
    }

}
