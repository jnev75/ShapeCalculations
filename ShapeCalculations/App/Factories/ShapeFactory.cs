using ShapeCalculations.Domain;

namespace ShapeCalculations.App.Factories
{
    /// <summary>
    /// Factory for creating concrete <see cref="Shape"/> instances
    /// based on a selected <see cref="ShapeType"/>.
    /// </summary>
    public static class ShapeFactory
    {
        /// <summary>
        /// Creates a shape of the specified type using the given length.
        /// </summary>
        /// <param name="type">The shape type to construct.</param>
        /// <param name="length">A positive numeric value used as the shape's defining dimension.</param>
        /// <returns>A concrete <see cref="Shape"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="length"/> is zero or negative.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when the provided <paramref name="type"/> is not recognized.
        /// </exception>
        public static Shape CreateShape(ShapeType type, double length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be positive.");

            return type switch
            {
                ShapeType.Circle => new Circle(length),
                ShapeType.Square => new Square(length),
                ShapeType.Triangle => new Triangle(length),
                _ => throw new NotSupportedException($"Shape type '{type}' is not supported.")
            };
        }
    }
}