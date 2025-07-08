using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace zmDataTypes.Material
{
    /// <summary>
    /// Represents standard imperial rebar.
    /// </summary>
    /// <remarks>
    /// see https://en.wikipedia.org/wiki/Rebar
    /// </remarks>
    public class Rebar {
        public double NominalDiameterInInches { get; }

        public double NominalAreaInSquareInches { get { return Math.PI * Math.Pow(NominalDiameterInInches / 2, 2); } }

        /// <summary>
        /// Gets the linear mass density in pounds per foot.
        /// </summary>
        /// <remarks>
        /// based on a regression of the wikipedia values.
        /// </remarks>
        public double LinearMassDensityInPoundsPerFoot { get { return NominalAreaInSquareInches * 3.40223; } }

        /// <summary>
        /// Maps the rebar size to its nominal diameter in inches.
        /// </summary>
        /// <remarks>
        /// This dictionary maps the rebar size (e.g., #2, #3, etc.) to its nominal diameter in inches.
        /// The diameter is generally the integer number of 1/8th inches. (e.g., #3 = 3/8").
        /// 14J and 18J are non-standard sizes.
        /// </remarks>
        private static readonly Dictionary<string, double> _barSizeDiameterMap = new Dictionary<string, double>
        {
            {   "#2", 0.250 },
            {   "#3", 0.375 },
            {   "#4", 0.500 },
            {   "#5", 0.625 },
            {   "#6", 0.750 },
            {   "#7", 0.875 },
            {   "#8", 1.000 },
            {   "#9", 1.128 },
            {  "#10", 1.270 },
            {  "#11", 1.410 },
            {  "#14", 1.693 },
            { "#14J", 1.880 },
            {  "#18", 2.257 },
            { "#18J", 2.340 }
        };

        private Rebar(double diameterInches)
        {
            NominalDiameterInInches = diameterInches;
        }

        public static Rebar FromImperialBarSize(string imperialBarSize)
        {
            if (string.IsNullOrEmpty(imperialBarSize))
                throw new ArgumentException("Input string cannot be null or empty.");

            var diameter = 0.0;
            if (_barSizeDiameterMap.TryGetValue(imperialBarSize, out diameter))
            {
                return new Rebar(diameter);
            }
            else
            {
                throw new ArgumentException($"Invalid rebar size string: {imperialBarSize}");
            }
        }

        public double GetTotalWeightInPounds(double lengthInFeet)
        {
            return LinearMassDensityInPoundsPerFoot * lengthInFeet;
        }
    }
}