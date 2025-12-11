namespace ShapeCalculations.Domain
{
    /// <summary>
    /// Equilateral triangle defined by side length.
    /// </summary>
    public class Triangle(double side) : Shape(side)
    {
        public override double CalculateArea() => (Math.Sqrt(3) / 4) * Math.Pow(Length, 2);

        public override double CalculateBoundaryLength() => Length * 3;
    }
}