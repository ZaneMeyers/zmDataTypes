using System;

namespace zmDataTypes.Shapes
{
    public class Cylinder
    {
        public double Height { get; }
        public double Radius { get; }

        public double Diameter => Radius * 2;
        public double Circumference => Math.PI * Diameter;
        public double Area => Math.PI * Math.Pow(Radius, 2);
        public double Volume => Area * Height;
        public double SurfaceArea => 2 * Math.PI * Radius * (Radius + Height);

        public Cylinder()
        {

        }

        public Cylinder SetRadius(double radius)
        {
            Radius = radius;
            return this;
        }

        public Cylinder SetDiameter(double diameter)
        {
            Radius = diameter / 2;
            return this;
        }

        public Cylinder SetCircumference(double circumference)
        {
            Radius = circumference / (2 * Math.PI);
            return this;
        }

        public Cylinder SetArea(double area)
        {
            Radius = Math.Sqrt(area / Math.PI);
            return this;
        }

        public Cylinder SetHeight(double height)
        {
            Height = height;
            return this;
        }
    }
}