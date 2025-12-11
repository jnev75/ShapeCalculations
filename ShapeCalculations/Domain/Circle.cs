namespace ShapeCalculations.Domain
{
    /// <summary>
    /// Circle defined by diameter.
    /// </summary>
    public class Circle(double diameter) : Shape(diameter)
    {
        public override double CalculateArea()
        {
            double radius = Length / 2;
            return Math.PI * Math.Pow(radius, 2);
        }

        public override double CalculateBoundaryLength() =>
            Math.PI * Length;
    }
}