namespace ShapeCalculations.UnitTests.Domain
{
    public class ShapeTestHelper
    {
        public static void AssertEqualWithTolerance(double expected, double actual, int precision = 5)
        {
            Assert.Equal(expected, actual, precision);
        }

        public static IEnumerable<object[]> ValidLengths =>
            [
                [0.01],
                [5],
                [10000]
            ];

        public static IEnumerable<object[]> InvalidLengths =>
            [
                [0],
                [-1],
                [-100]
            ];
    }
}