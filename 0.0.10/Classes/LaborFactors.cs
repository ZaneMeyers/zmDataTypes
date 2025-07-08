using System;
using System.Collections.Generic;
using System.Linq;

namespace zmDataTypes.LaborFactors
{
    public interface ILaborFactor
    {
        string Dimension { get; }
        double Value { get; }
        double Factor { get; }
        string Warning { get; }
    }

    public sealed class CompoundLaborFactor : ILaborFactor
    {
        public string Dimension => "Composite Factor";
        public double Value => 0; // Not applicable in this composite
        public double Factor { get; }
        public string Warning { get; }
        public IReadOnlyList<ILaborFactor> Factors { get; }

        public CompoundLaborFactor(params ILaborFactor[] factors)
        {
            Factors = factors;

            double product = 1.0;
            var warningList = new List<string>();

            foreach (var factor in Factors)
            {
                product *= factor.Factor;
                if (!string.IsNullOrWhiteSpace(factor.Warning))
                {
                    warningList.Add(factor.Warning);
                }
            }

            Factor = product;
            Warning = warningList.Count > 0 ? string.Join("; ", warningList) : string.Empty;
        }

        public static CompoundLaborFactor Combine(params CompoundLaborFactor[] compoundFactors)
        {
            var allChildFactors = compoundFactors.SelectMany(cf => cf.Factors).ToArray();
            return new CompoundLaborFactor(allChildFactors);
        }
    }

    public sealed class MountingHeight : ILaborFactor
    {
        public string Dimension => "Mounting Height";
        public double Value { get; }
        public double Factor { get; }
        public string Warning { get; }

        public MountingHeight(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Mounting height cannot be negative.");

            Value = value;
            Factor = (2.0 / 5.0) * Math.Pow(value, 0.3);
            Warning = value > 50
                ? $"Mounting height {value} exceeds recommended range (>50)."
                : string.Empty;
        }
    }

    public sealed class ParallelRuns : ILaborFactor
    {
        public string Dimension => "Parallel Runs";
        public double Value { get; }
        public double Factor { get; }
        public string Warning { get; }

        public ParallelRuns(double value)
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value), "Number of parallel runs must be at least 1.");

            Value = value;
            Factor = (6.0 / 5.0) * Math.Pow(value, -0.115);
            Warning = string.Empty; 
        }
    }

    public sealed class AmbientTemperature : ILaborFactor
    {
        public string Dimension => "Ambient Temperature";
        public double Value { get; }
        public double Factor { get; }
        public string Warning { get; }

        public AmbientTemperature(double value)
        {
            Value = value;
            Factor = 1; 
            Warning = "Ambient temperature factor calculation is not implemented yet.";
        }
    }

    public sealed class AmbientRelativeHumidity : ILaborFactor
    {
        public string Dimension => "Ambient Relative Humidity";
        public double Value { get; }
        public double Factor { get; }
        public string Warning { get; }

        public AmbientRelativeHumidity(double value)
        {
            Value = value;
            Factor = 1; 
            Warning = "Ambient relative humidity factor calculation is not implemented yet.";
        }
    }
}
