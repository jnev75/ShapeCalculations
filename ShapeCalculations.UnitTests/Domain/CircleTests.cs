using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.Domain
{
    public class CircleTests
    {
        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateArea_ShouldReturnCorrectValue_ForVariousDiameters(double diameter)
        {
            var circle = new Circle(diameter);

            double expected = Math.PI * Math.Pow(diameter / 2, 2);
            ShapeTestHelper.AssertEqualWithTolerance(expected, circle.CalculateArea());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateBoundaryLength_ShouldReturnCorrectValue_ForVariousDiameters(double diameter)
        {
            var circle = new Circle(diameter);

            double expected = Math.PI * diameter;
            ShapeTestHelper.AssertEqualWithTolerance(expected, circle.CalculateBoundaryLength());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.InvalidLengths), MemberType = typeof(ShapeTestHelper))]
        public void Circle_WithInvalidDiameter_ShouldThrowArgumentOutOfRangeException(double diameter)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(diameter));
        }
    }
}