namespace ShapeCalculations.Domain
{
    /// <summary>
    /// Base class for shapes defined by a primary length.
    /// </summary>
    public abstract class Shape(double length)
    {
        /// <summary>
        /// Primary length (must be positive).
        /// </summary>
        public double Length { get; } = length > 0
            ? length
            : throw new ArgumentOutOfRangeException(nameof(length), "Length must be positive.");

        public abstract double CalculateArea();

        public abstract double CalculateBoundaryLength();
    }
}