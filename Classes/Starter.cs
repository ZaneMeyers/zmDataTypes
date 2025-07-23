using System;
using System.Collections.Generic;

namespace zmDataTypes.Equipment
{
    public class Starter
    {
        private static readonly Dictionary<string, double> _nemaSizeToMaxContinuousAmperes = new Dictionary<string, double>
        {
            { "00",   9.00 },
            { "0",   18.00 },
            { "1",   27.00 },
            { "2",   45.00 },
            { "3",   90.00 },
            { "4",  135.00 },
            { "5",  270.00 },
            { "6",  540.00 },
            { "7",  810.00 },
            { "8", 1215.00 },
            { "9", 2250.00 },
        };

        public static double GetMaxContinuousAmperes(string nemaSize)
        {
            if (_nemaSizeToMaxContinuousAmperes.TryGetValue(nemaSize, out var maxAmperes))
            {
                return maxAmperes;
            }
            else
            {
                throw new ArgumentException($"Invalid NEMA size: {nemaSize}");
            }
        }

        public static double GetMaxPowerInWatts(string nemaSize, double voltage)
        {
            if (_nemaSizeToMaxContinuousAmperes.TryGetValue(nemaSize, out var maxAmperes))
            {
                return maxAmperes * voltage * Math.Sqrt(3); // Power (Watts) = Amperes * Volts * sqrt(3) for 3-phase
            }
            else
            {
                throw new ArgumentException($"Invalid NEMA size: {nemaSize}");
            }
        }

        public static double GetMaxPowerInHorsepower(string nemaSize, double voltage)
        {
            var maxPowerInWatts = GetMaxPowerInWatts(nemaSize, voltage);
            return maxPowerInWatts / 746; // Horsepower = Watts / 746
        }

        public static string GetMinimumNemaSizeFromContinuousAmperes(double amperes)
        {
            var roundedAmperes = Math.Ceiling(amperes);
            foreach (var kvp in _nemaSizeToMaxContinuousAmperes)
            {
                if (roundedAmperes <= kvp.Value)
                {
                    return kvp.Key;
                }
            }
            throw new ArgumentException($"No NEMA size found for {amperes} amperes.");
        }

        public static string GetMinimumNemaSizeFromWattsAndVoltage(double watts, double voltage)
        {
            var maxAmperes = watts / (voltage * Math.Sqrt(3));
            return GetMinimumNemaSizeFromContinuousAmperes(maxAmperes);
            // watts = √3 × amps × lineToLineVoltage
            // amps = watts / (√3 × lineToLineVoltage)
        }

        public static string GetMinimumNemaSizeFromHorsepowerAndVoltage(double horsepower, double voltage)
        {
            var watts = horsepower * 746; // Convert horsepower to watts
            return GetMinimumNemaSizeFromWattsAndVoltage(watts, voltage);
        }
    }
}