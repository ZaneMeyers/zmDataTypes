using System;
using System.Collections.Generic;

namespace zmDataTypes.Shapes
{
    public class Tube
    {
        private double _majorRadius = 0;
        private double _minorRadius = 0;
        private double _materialDensity = 0;

        public double MajorRadius
        {
            get => _majorRadius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Major radius must be greater than zero.");
                if (_minorRadius > 0 && value <= _minorRadius)
                    throw new ArgumentException("Major radius must be greater than minor radius.");
                _majorRadius = value;
            }
        }

        public double MinorRadius
        {
            get => _minorRadius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Minor radius must be greater than zero.");
                if (_majorRadius > 0 && value >= _majorRadius)
                    throw new ArgumentException("Minor radius must be less than major radius.");
                _minorRadius = value;
            }
        }

        public double OuterDiameter
        {
            get => MajorRadius * 2;
            set => MajorRadius = value / 2;
        }

        public double InnerDiameter
        {
            get => MinorRadius * 2;
            set => MinorRadius = value / 2;
        }

        public double WallThickness
        {
            get => OuterDiameter - InnerDiameter;
        }

        public double OuterCircumference
        {
            get => Math.PI * OuterDiameter;
            set => MajorRadius = value / (2 * Math.PI);
        }

        public double InnerCircumference
        {
            get => Math.PI * InnerDiameter;
            set => MinorRadius = value / (2 * Math.PI);
        }

        public double OverallArea
        {
            get => Math.PI * OuterDiameter * MajorRadius;
            set => MajorRadius = value / (Math.PI * OuterDiameter);
        }

        public double InnerArea
        {
            get => Math.PI * InnerDiameter * MinorRadius;
            set => MinorRadius = value / (Math.PI * InnerDiameter);
        }

        public double WallArea
        {
            get => OverallArea - InnerArea;
        }

        public double MaterialDensity
        {
            get => _materialDensity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Material density must be greater than zero.");
                _materialDensity = value;
            }
        }

        public double LinearMassDensity
        {
            get => MaterialDensity * WallArea;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Linear mass density must be greater than zero.");
                MaterialDensity = value / WallArea;
            }
        }

        public Tube()
        {
        }

        public static Tube FromDiametersAndHundredWeight(double innerDiameter, double outerDiameter, double hundredWeight)
        {
            var linearMassDensity = hundredWeight / 100;
            var tube = new Tube
            {
                InnerDiameter = innerDiameter,
                OuterDiameter = outerDiameter,
                LinearMassDensity = linearMassDensity
            };
            return tube;
        }

        public double GetTotalWeight(double length)
        {
            return LinearMassDensity * length;
        }
    }
}