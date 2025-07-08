using System;

namespace zmDataTypes.Geometry
{
    public class PointDistance
    {
        public double XDistance {
            get;
            set
            {
                Math.Abs(value);
            } 
        }
        public double YDistance {
            get;
            set
            {
                Math.Abs(value);
            }
        }

        public double GetEuclideanDistance()
        {
            return Math.Sqrt(Math.Pow(XDistance, 2) + Math.Pow(YDistance, 2));
        }

        // Refers to paths along axes, like Manhattan streets.
        // Good for run lengths that follow building lines (most of them).
        public double GetManhattanDistance()
        {
            return XDistance + YDistance;
        }

        // Refers to paths along axes or 45 degree diagonals, like circuit traces.
        // Good for approximating underground utility lengths.
        public double GetOctilinearDistance()
        {
            double max = Math.Max(XDistance, YDistance);
            double min = Math.Min(XDistance, YDistance);
            return max - min * (1 - Math.Sqrt(2));
        }

        // Not to be confused with Octilinear, which follows the same
        // chess-queen path but calculates diagonals with euclidean distance.
        // I'm including this method simply to make this distinction clear.
        // There is probably no practical use for it.
        public double GetChebyshevDistance()
        {
            return Math.Max(XDistance, YDistance);
        }
    }
}