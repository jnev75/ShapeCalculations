using ShapeCalculations.App.UI;

namespace ShapeCalculations.UnitTests.App.UI
{
    public class ConsoleFormatterTests
    {
        [Fact]
        public void Center_ReturnsSameText_WhenTextIsLongerThanWidth()
        {
            string text = new('A', 100);
            string result = ConsoleFormatter.Center(text, 50);

            Assert.Equal(text, result);
        }

        [Fact]
        public void Center_PadsCorrectly()
        {
            string result = ConsoleFormatter.Center("Test", 10);

            Assert.Equal("   Test", result);
        }

        [Fact]
        public void FormatLength_ReturnsCentimetersBelowThreshold()
        {
            string result = ConsoleFormatter.FormatLength(50);

            Assert.Equal("50.00 cm", result);
        }

        [Fact]
        public void FormatLength_ReturnsMetersAboveThreshold()
        {
            string result = ConsoleFormatter.FormatLength(150);

            Assert.Equal("1.50 m", result);
        }

        [Fact]
        public void FormatArea_ReturnsCm2BelowThreshold()
        {
            string result = ConsoleFormatter.FormatArea(5000);

            Assert.Equal("5000.00 cm²", result);
        }

        [Fact]
        public void FormatArea_ReturnsM2AboveThreshold()
        {
            string result = ConsoleFormatter.FormatArea(20000);

            Assert.Equal("2.00 m²", result);
        }
    }
}