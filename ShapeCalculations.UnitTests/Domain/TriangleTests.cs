using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.Domain
{
    public class TriangleTests
    {
        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateArea_ShouldReturnCorrectValue_ForVariousSideLengths(double side)
        {
            var triangle = new Triangle(side);

            double expected = (Math.Sqrt(3) / 4) * Math.Pow(side, 2);
            ShapeTestHelper.AssertEqualWithTolerance(expected, triangle.CalculateArea());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.ValidLengths), MemberType = typeof(ShapeTestHelper))]
        public void CalculateBoundaryLength_ShouldReturnCorrectValue_ForVariousSideLengths(double side)
        {
            var triangle = new Triangle(side);

            double expected = side * 3;
            ShapeTestHelper.AssertEqualWithTolerance(expected, triangle.CalculateBoundaryLength());
        }

        [Theory]
        [MemberData(nameof(ShapeTestHelper.InvalidLengths), MemberType = typeof(ShapeTestHelper))]
        public void Triangle_WithInvalidSideLength_ShouldThrowArgumentOutOfRangeException(double side)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Triangle(side));
        }
    }
}