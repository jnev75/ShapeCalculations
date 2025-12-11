using ShapeCalculations.App.Factories;
using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.App.Factories
{
    public class ShapeFactoryTests
    {
        [Theory]
        [InlineData(ShapeType.Circle, 5)]
        [InlineData(ShapeType.Square, 4)]
        [InlineData(ShapeType.Triangle, 3)]
        public void CreateShape_ReturnsCorrectShape_ForValidTypes(ShapeType type, double length)
        {
            var expectedTypes = new Dictionary<ShapeType, Type>
            {
                { ShapeType.Circle, typeof(Circle) },
                { ShapeType.Square, typeof(Square) },
                { ShapeType.Triangle, typeof(Triangle) }
            };

            var shape = ShapeFactory.CreateShape(type, length);

            Assert.IsType(expectedTypes[type], shape);
            Assert.Equal(length, shape.Length);
        }

        [Fact]
        public void CreateShape_Throws_WhenShapeTypeIsInvalid()
        {
            var invalidType = (ShapeType)99;
            var ex = Assert.Throws<NotSupportedException>(() => ShapeFactory.CreateShape(invalidType, 1));
            Assert.Contains("not supported", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(ShapeType.Circle, 0)]
        [InlineData(ShapeType.Circle, -1)]
        [InlineData(ShapeType.Square, 0)]
        [InlineData(ShapeType.Square, -5)]
        [InlineData(ShapeType.Triangle, 0)]
        [InlineData(ShapeType.Triangle, -10)]
        public void CreateShape_Throws_WhenLengthIsNonPositive(ShapeType type, double length)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ShapeFactory.CreateShape(type, length));
            Assert.Contains("Length must be positive", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}