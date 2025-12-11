namespace ShapeCalculations.Domain
{
    /// <summary>
    /// Square defined by side length.
    /// </summary>
    public class Square(double side) : Shape(side)
    {
        public override double CalculateArea() => Math.Pow(Length, 2);

        public override double CalculateBoundaryLength() => Length * 4;
    }
}