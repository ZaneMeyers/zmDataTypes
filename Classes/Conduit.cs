using System;
using System.Collections.Generic;
using zmDataTypes;
using zmDataTypes.Parsers;
using zmDataTypes.Shapes;

namespace zmDataTypes.Material
{
    public class ConduitType
    {
        public static string GetConduitTypeFromAlias(string alias)
        {
            if (!_conduitTypeAliasMap.ContainsKey(alias))
                throw new ArgumentException($"Conduit type {alias} not found in the alias map.");
            return _conduitTypeAliasMap[alias];
        }

        private static readonly Dictionary<string, string> _conduitTypeAliasMap = new Dictionary<string, string>
        {
            { "EMT",     "EMT"     },
            { "ENT",     "ENT"     },
            { "IMC",     "IMC"     },
            { "RMC",     "RMC"     },
            { "GRC",     "RMC"     },
            { "RMC-PVC", "RMC-PVC" },
            { "PVC",     "PVC-40"  },
            { "PVC-40",  "PVC-40"  },
            { "PVC-80",  "PVC-80"  },
            { "PVC-A",   "PVC-A"   },
            { "PVC-EB",  "PVC-EB"  },
            { "HDPE",    "HDPE"    },
            { "FMC",     "FMC"     },
            { "LFNC",    "LFNC-A"  },
            { "LFNC-A",  "LFNC-A"  },
            { "LFNC-B",  "LFNC-B"  },
            { "LFNC-C",  "LFNC-C"  },
            { "LFMC",    "LFMC"    }
        };
    }

    public class ConduitSize {

        public static int GetMetricDesignatorFromTradeSizeString(string tradeSizeString) {
            var tradeSize = MixedNumber.Parse(tradeSizeString);
            return GetMetricDesignatorFromTradeSizeDouble(tradeSize);
        }

        public static int GetMetricDesignatorFromTradeSizeDouble(double tradeSize) {
            if (!_tradeSizeToDesignatorMap.ContainsKey(tradeSize))
                throw new ArgumentException($"Trade size {tradeSize} not found in the designator map.");
            return _tradeSizeToDesignatorMap[tradeSize];
        }

        private static readonly Dictionary<double, int> _tradeSizeToDesignatorMap = new Dictionary<double, int>
        {
            { 0.375,  12 },
            { 0.500,  16 },
            { 0.750,  21 },
            { 1.000,  27 },
            { 1.250,  35 },
            { 1.500,  41 },
            { 2.000,  53 },
            { 2.500,  63 },
            { 3.000,  78 },
            { 3.500,  91 },
            { 4.000, 103 },
            { 5.000, 129 },
            { 6.000, 155 }
        };
    }

    public class Conduit {
        private static readonly Dictionary<string, Dictionary<int, double[]>> _conduitTypeSizeMap =
            new Dictionary<string, Dictionary<int, double[]>>
            {
                {
                    "EMT",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.622, 0.706,  0.300 ] },
                        {  21, [ 0.824, 0.922,  0.460 ] },
                        {  27, [ 1.049, 1.163,  0.670 ] },
                        {  35, [ 1.380, 1.510,  1.010 ] },
                        {  41, [ 1.610, 1.740,  1.160 ] },
                        {  53, [ 2.067, 2.197,  1.480 ] },
                        {  63, [ 2.731, 2.875,  2.160 ] },
                        {  78, [ 3.356, 3.500,  2.630 ] },
                        {  91, [ 3.834, 4.000,  3.490 ] },
                        { 103, [ 4.334, 4.500,  3.930 ] }
                    }
                },
                {
                    "IMC",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.660, 0.815,  0.620 ] },
                        {  21, [ 0.864, 1.029,  0.840 ] },
                        {  27, [ 1.105, 1.290,  1.190 ] },
                        {  35, [ 1.448, 1.638,  1.580 ] },
                        {  41, [ 1.683, 1.883,  1.940 ] },
                        {  53, [ 2.150, 2.360,  2.560 ] },
                        {  63, [ 2.557, 2.857,  4.410 ] },
                        {  78, [ 3.176, 3.476,  5.430 ] },
                        {  91, [ 3.671, 3.971,  6.290 ] },
                        { 103, [ 4.166, 4.466,  7.000 ] }
                    }
                },
                {
                    "RMC",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.632, 0.840,  0.820 ] },
                        {  21, [ 0.836, 1.050,  1.090 ] },
                        {  27, [ 1.063, 1.315,  1.610 ] },
                        {  35, [ 1.394, 1.660,  2.180 ] },
                        {  41, [ 1.624, 1.900,  2.630 ] },
                        {  53, [ 2.083, 2.375,  3.500 ] },
                        {  63, [ 2.489, 2.875,  5.590 ] },
                        {  78, [ 3.090, 3.500,  7.270 ] },
                        {  91, [ 3.570, 4.000,  8.800 ] },
                        { 103, [ 4.050, 4.500, 10.300 ] },
                        { 129, [ 5.073, 5.563, 14.000 ] },
                        { 155, [ 6.093, 6.625, 18.400 ] }
                    }
                },
                {
                    "RMC-PVC",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.632, 9.999,  0.850 ] },
                        {  21, [ 0.836, 9.999,  1.120 ] },
                        {  27, [ 1.063, 9.999,  1.640 ] },
                        {  35, [ 1.394, 9.999,  2.170 ] },
                        {  41, [ 1.624, 9.999,  2.680 ] },
                        {  53, [ 2.083, 9.999,  3.580 ] },
                        {  63, [ 2.489, 9.999,  5.460 ] },
                        {  78, [ 3.090, 9.999,  7.080 ] },
                        {  91, [ 3.570, 9.999,  8.510 ] },
                        { 103, [ 4.050, 9.999, 10.090 ] },
                        { 129, [ 5.073, 9.999, 13.370 ] },
                        { 155, [ 6.093, 6.705, 19.930 ] }
                    }
                },
                {
                    "PVC-40",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.602, 0.840,  0.180 ] },
                        {  21, [ 0.804, 1.050,  0.240 ] },
                        {  27, [ 1.029, 1.315,  0.330 ] },
                        {  35, [ 1.360, 1.660,  0.450 ] },
                        {  41, [ 1.590, 1.900,  0.560 ] },
                        {  53, [ 2.047, 2.375,  0.760 ] },
                        {  63, [ 2.445, 2.875,  1.260 ] },
                        {  78, [ 3.042, 3.500,  1.630 ] },
                        {  91, [ 3.521, 4.000,  1.970 ] },
                        { 103, [ 3.998, 4.500,  2.340 ] },
                        { 129, [ 5.016, 5.563,  3.190 ] },
                        { 155, [ 6.031, 6.625,  4.110 ] }
                    }
                },
                {
                    "PVC-80",
                    new Dictionary<int, double[]>
                    {
                        {  16, [ 0.526, 0.840,  0.220 ] },
                        {  21, [ 0.722, 1.050,  0.300 ] },
                        {  27, [ 0.936, 1.315,  0.420 ] },
                        {  35, [ 1.255, 1.660,  0.600 ] },
                        {  41, [ 1.476, 1.900,  0.720 ] },
                        {  53, [ 1.913, 2.375,  0.980 ] },
                        {  63, [ 2.290, 2.875,  1.600 ] },
                        {  78, [ 2.864, 3.500,  2.130 ] },
                        {  91, [ 3.326, 4.000,  2.560 ] },
                        { 103, [ 3.786, 4.500,  3.100 ] },
                        { 129, [ 4.768, 5.563,  4.300 ] },
                        { 155, [ 5.709, 6.625,  5.900 ] }
                    }
                }
            };

        public static Tube BuildTubeFromTradeSizeDoubleAndConduitType(double tradeSize, string conduitType) {

            var designator = ConduitSize.GetMetricDesignatorFromTradeSizeDouble(tradeSize);
            var lookupType = ConduitType.GetConduitTypeFromAlias(conduitType);

            if (!_conduitTypeSizeMap[lookupType].ContainsKey(designator))
                throw new ArgumentException($"Conduit size {designator} not found in the size map.");


            var size = _conduitTypeSizeMap[lookupType][designator];

            return new Tube
            {
                InnerDiameter = size[0],
                OuterDiameter = size[1],
                LinearMassDensity = size[2]
            };
        }
    }
}

// Stainless Steel 304: 490.060-503.170 lb/ft^3
// Stainless Steel 316: 491.308-503.794 lb/ft^3


