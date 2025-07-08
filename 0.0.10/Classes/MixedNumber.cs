using System;
using System.Text.RegularExpressions;

namespace zmDataTypes.Parsers
{

    /// <summary>
    /// Represents a mixed number, which is a combination of a whole number and a fraction.
    /// </summary>
    /// <remarks>
    /// This class provides methods to:
    ///   - parse mixed number strings,
    ///   - convert them to double values
    /// The class can parse fractions of any denominator,
    /// but it will only output dyadic fractions (powers of 2).
    /// </remarks>
    public class MixedNumber
    {
        /// <summary>
        /// Encapsulates the fractional functionality of the MixedNumber class.
        /// </summary>
        private class Fraction {

            public static double Parse(string input)
            {
                var match = Regex.Match(input, @"(\d+)/(\d+)");
                if (!match.Success)
                {
                    throw new ArgumentException($"Invalid fraction string: {input}");
                }

                var numerator = double.Parse(match.Groups[1].Value);
                var denominator = double.Parse(match.Groups[2].Value);

                return numerator / denominator;
            }

            private static int GreatestCommonDenominator(int a, int b)
            {
                while (a != 0 && b != 0)
                {
                    if (a > b)
                        a %= b;
                    else
                        b %= a;
                }
                return a | b;
            }

            /// <summary>
            /// Converts a double to a dyadic fraction string.
            /// </summary>
            /// <param name="value">The double value to convert.</param>
            /// <returns>A string representing the dyadic fraction.</returns>
            /// <remarks>
            /// This method converts a double value to a dyadic fraction string in the form of "numerator/denominator".
            /// It assumes 1/32 precision, which is precise enough for most applications,
            /// and imprecise enough to avoid floating point errors.
            /// </remarks>
            public static string ConvertDoubleToString(double value)
            {
                // Note that negative values happen to work correctly (e.g. -0.5 = -1/2),
                // as do improper fractions (e.g. 1.5 = 3/2),
                // however this may be undesirable in some cases.
                if ( value == 0 || double.IsInteger(value) || !double.IsNormal(value) )
                {
                    return value.ToString();
                }

                int denominator = (int)Math.Pow(2, 5); // 2^5 = 32
                int numerator = (int)Math.Truncate(value * denominator);
                int gcd = GreatestCommonDenominator(numerator, denominator);

                numerator /= gcd;
                denominator /= gcd;

                return $"{numerator}/{denominator}";
            }
        }

        /// <summary>
        /// Parses a mixed number string into a double value.
        /// </summary>
        /// <param name="input">The mixed number string to parse.</param>
        /// <returns>The double value of the mixed number.</returns>
        /// <exception cref="ArgumentException">Thrown when the input string is not a valid mixed number.</exception>
        /// <remarks>
        /// This method supports mixed numbers in the format "whole-fraction" or "whole fraction".
        /// It also supports "whole" and "fraction" formats.
        /// </remarks>
        /// <example>
        /// // <code>
        /// var mixedNumber = MixedNumber.Parse("2-3/4");
        /// Console.WriteLine(mixedNumber); // Output: 2.75
        /// // </code>
        /// </example>
        public static double Parse(string input)
        {
            if (!Regex.IsMatch(input, @"\d*(?:[ \-]\d+/\d+)?"))
            {
                throw new ArgumentException($"Invalid mixed number string: {input}");
            }

            double value;

            if (double.TryParse(input, out value))
            {
                return value;
            }

            return ParseMixedNumber(input);
        }

        private static double ParseMixedNumber(string input)
        {
            var parts = input.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                return Fraction.Parse(parts[0]);
            }
            else if (parts.Length == 2)
            {
                var whole = double.Parse(parts[0]);
                var fraction = Fraction.Parse(parts[1]);
                return whole + fraction;
            }
            else
            {
                throw new ArgumentException($"Invalid mixed number string: {input}");
            }
        }

        public static string ConvertDoubleToString(double value)
        {
            if ( double.IsInteger(value) || !double.IsNormal(value)  )
            {
                return value.ToString();
            }

            if (double.IsNegative(value))
            {
                return "-" + ConvertDoubleToString(-value);
            }

            if (value < 1)
            {
                return Fraction.ConvertDoubleToString(value);
            }

            var whole = double.Truncate(value);
            var fraction = value - whole;

            return $"{whole}-{Fraction.ConvertDoubleToString(fraction)}";
        }
    }
}
