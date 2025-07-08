using System;

namespace zmDataTypes.Shapes
{
    class Box {
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        
        private Box() {

        }

        public Box SetWidth(double width) {
            Width = width;
            return this;
        }

        public Box SetHeight(double height) {
            Height = height;
            return this;
        }

        public Box SetDepth(double depth) {
            Depth = depth;
            return this;
        }

        public bool IsInitialized() {
            return Width > 0 && Height > 0 && Depth > 0;
        }

        public double Volume() {
            if (!IsInitialized()) {
                throw new InvalidOperationException("Box is not initialized with width, height, and depth.");
            }
            return Width * Height * Depth;
        }

        public double SurfaceArea() {
            if (!IsInitialized()) {
                throw new InvalidOperationException("Box is not initialized with width, height, and depth.");
            }
            return 2 * (Width * Height + Width * Depth + Height * Depth);
        }
    }

}