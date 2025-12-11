using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.Domain
{
    public class SquareTests
    {
        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateArea_ShouldReturnCorrectValue_ForVariousSideLengths(double side)
        {
            var square = new Square(side);

            double expected = Math.Pow(side, 2);
            ShapeTestHelper.AssertEqualWithTolerance(expected, square.CalculateArea());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateBoundaryLength_ShouldReturnCorrectValue_ForVariousSideLengths(double side)
        {
            var square = new Square(side);

            double expected = side * 4;
            ShapeTestHelper.AssertEqualWithTolerance(expected, square.CalculateBoundaryLength());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.InvalidLengths), MemberType = typeof(ShapeTestHelper))]
        public void Square_WithInvalidSideLength_ShouldThrowArgumentOutOfRangeException(double side)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Square(side));
        }
    }
}