using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace zmDataTypes.Material
{

    // TODO: Add XML documentation for the class and its methods.
    // TODO: Add better support for subobtimal text input:
        // - search the input string for possible gauges
            // - numbers prefixed with /#/,
            // - suffixed with / ?(AWG)|(KCMIL)|(MCM)/

    public class WireGauge : IComparable
    {
        public double DiameterInInches { get; }
        public double AreaInSquareInches => Math.PI * Math.Pow(DiameterInInches / 2, 2);

        private static readonly Dictionary<string, double> _gaugeToDiameterInches = new Dictionary<string, double>
        {
            { "18 AWG",     0.040302651 },
            { "16 AWG",     0.050820705 },
            { "14 AWG",     0.064083726 },
            { "12 AWG",     0.080808086 },
            { "10 AWG",     0.101897115 },
            { "8 AWG",      0.128489890 },
            { "6 AWG",      0.162022760 },
            { "4 AWG",      0.204306928 },
            { "3 AWG",      0.229422827 },
            { "2 AWG",      0.257626279 },
            { "1 AWG",      0.289296844 },
            { "1/0 AWG",    0.324860740 },
            { "2/0 AWG",    0.364796585 },
            { "3/0 AWG",    0.409641830 },
            { "4/0 AWG",    0.460000000 },
            { "250 kcmil",  0.500000000 },
            { "300 kcmil",  0.547722558 },
            { "350 kcmil",  0.591607978 },
            { "400 kcmil",  0.632455532 },
            { "500 kcmil",  0.707106781 },
            { "600 kcmil",  0.774596669 },
            { "700 kcmil",  0.836660027 },
            { "750 kcmil",  0.866025404 },
            { "800 kcmil",  0.894427191 },
            { "900 kcmil",  0.948683298 },
            { "1000 kcmil", 1.000000000 },
            { "1250 kcmil", 1.118033989 },
            { "1500 kcmil", 1.224744871 },
            { "1750 kcmil", 1.322875656 },
            { "2000 kcmil", 1.414213562 }
        };

        private WireGauge(double diameterInches)
        {
            DiameterInInches = diameterInches;
        }

        public static WireGauge FromDiameterInches(double diameterInches)
        {
            return new WireGauge(diameterInches);
        }

        private class AmericanWireGauge
        {
            private static readonly Regex _sizeRegex =
                new(@"^(#(?'gauge'\d{1,2}|\d\/0)|(?'gauge'\d{1,2}|\d\/0) AWG)$",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant);
            
            private static readonly Regex _stepRegex =
                new(@"^(?:(?'posStep'\d{1,2})|(?'aughtStep'\d)\/0)$",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant);

            public static string GetSizeFromDiameterInches(double diameterInches)
            {
                int step = (int)Math.Round(-39 * Math.Log(diameterInches / 0.005, 92) + 36);

                if (step < 1)
                {
                    int zeros = 1 + Math.Abs(step);
                    return $"{zeros}/0";
                }
                else
                {
                    return $"{step}";
                }
            }
            
            public static double Parse(string input)
            {

                if (input is null) throw new ArgumentNullException(nameof(input));

                var sizeMatch = _sizeRegex.Match(input);
                if (!sizeMatch.Success) throw new FormatException("Input is not a valid Awg size.");

                var stepMatch = _stepRegex.Match(sizeMatch.Groups["gauge"].Value);

                int step;
                if (stepMatch.Groups["posStep"].Success)
                {
                    // Handle the case where the input is in the form of "X"
                    //  #12; step = 12
                    step = int.Parse(stepMatch.Groups["posStep"].Value);
                }
                else if (stepMatch.Groups["aughtStep"].Success)
                {
                    // Handle the case where the input is in the form of "X/0"
                    //  #0 = #1/0; step =  0
                    // #00 = #2/0; step = -1
                    step = 1 - int.Parse(stepMatch.Groups["aughtStep"].Value);
                }
                else throw new FormatException("Input was a valid Awg size but could not be parsed. This should not happen.");


                return GetDiameterInchesFromAwgStep(step);
            }

            public static bool TryParse(string input, out double diameterInches)
            {
                if (input is null) throw new ArgumentNullException(nameof(input));

                var sizeMatch = _sizeRegex.Match(input);
                if (!sizeMatch.Success)
                {
                    diameterInches = 0;
                    return false;
                }

                var stepMatch = _stepRegex.Match(sizeMatch.Groups["gauge"].Value);

                int step;
                if (stepMatch.Groups["posStep"].Success)
                {
                    // Handle the case where the input is in the form of "X"
                    //  #12; step = 12
                    step = int.Parse(stepMatch.Groups["posStep"].Value);
                }
                else if (stepMatch.Groups["aughtStep"].Success)
                {
                    // Handle the case where the input is in the form of "X/0"
                    //  #0 = #1/0; step =  0
                    // #00 = #2/0; step = -1
                    step = 1 - int.Parse(stepMatch.Groups["aughtStep"].Value);
                }
                else throw new FormatException("Input was a valid Awg size but could not be parsed. This should not happen.");


                diameterInches = GetDiameterInchesFromAwgStep(step);
                return true;
            }

            public static double GetDiameterInchesFromAwgStep(int step)
            {
                return 0.005 * Math.Pow(92, (36 - step) / 39.0); // Formula from AWG standards
            }

        }

        private class CircularMilsWireGauge
        {
            private static readonly Regex _regex =
                new(@"^(?:#(?'gauge'\d{3,4})|(?'gauge'\d{3,4}) (?:kcmil|MCM))$",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant);

            public static double Parse(string input)
            {
                if (input is null) throw new ArgumentNullException(nameof(input));

                var match = _regex.Match(input);
                if (!match.Success) throw new FormatException("Input is not a valid kcmil size.");
                
                var areaKcmil = int.Parse(match.Groups["gauge"].Value);

                return GetDiameterInchesFromArea(areaKcmil);
            }

            public static bool TryParse(string input, out double diameterInches)
            {
                if (input is null) throw new ArgumentNullException(nameof(input));

                var match = _regex.Match(input);
                if (!match.Success)
                {
                    diameterInches = 0;
                    return false;
                }

                var areaKcmil = int.Parse(match.Groups["gauge"].Value);


                diameterInches = GetDiameterInchesFromArea(areaKcmil);
                return true;
            }

            public static double GetDiameterInchesFromArea(int areaKcmil)
            {
                return Math.Sqrt(areaKcmil / 1000.0);
            }

            public static int GetAreaFromDiameterInches(double diameterInches)
            {
                return (int)Math.Round(Math.Pow(diameterInches, 2) * 1000);
            }

            public static string GetSizeFromDiameterInches(double diameterInches)
            {
                int areaKcmil = GetAreaFromDiameterInches(diameterInches);
                return $"{areaKcmil} kcmil";
            }
        }

        public static WireGauge Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string cannot be null or empty.");
            
            double diameterInches;
            if (AmericanWireGauge.TryParse(input, out diameterInches))
                return new WireGauge(diameterInches);
            else if (CircularMilsWireGauge.TryParse(input, out diameterInches))
                return new WireGauge(diameterInches);
            else
                throw new FormatException("Input is not a valid wire gauge.");
        }

        public static WireGauge FromLookup(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Input string cannot be null or empty.");

            if (_gaugeToDiameterInches.TryGetValue(input, out var diameterInches))
            {
                return new WireGauge(diameterInches);
            }
            else throw new KeyNotFoundException($"Wire gauge '{input}' not found.");
        }

        public override string ToString()
        {
            if (DiameterInInches > 0.46)
            {
                return $"{CircularMilsWireGauge.GetSizeFromDiameterInches(DiameterInInches)} kcmil";
            }
            else
            {
                return $"{AmericanWireGauge.GetSizeFromDiameterInches(DiameterInInches)} AWG";
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is WireGauge))
            {
                throw new ArgumentException("Object must be of type WireGauge.");
            }

            var other = (WireGauge)obj;
            return DiameterInInches.CompareTo(other.DiameterInInches);
        }

        public bool Equals(WireGauge other)
        {
            if (other == null) return false;
            return DiameterInInches.Equals(other.DiameterInInches);
        }


    }

}