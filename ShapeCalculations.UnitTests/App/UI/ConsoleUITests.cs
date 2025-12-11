using Moq;
using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.UI;
using ShapeCalculations.App.Utilities;
using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.App.UI
{
    public class ConsoleUITests : IDisposable
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly ConsoleUI _ui;

        public ConsoleUITests()
        {
            _mockConsole = new Mock<IConsole>();
            _ui = new ConsoleUI(_mockConsole.Object);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void ShowHeader_WritesHeaderWithBorder()
        {
            _ui.ShowHeader();

            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()), Times.Exactly(3));
        }

        [Fact]
        public void ShowHeader_WritesCenteredHeader()
        {
            _ui.ShowHeader();

            _mockConsole.Verify(c =>
                c.WriteLine(It.Is<string>(s => s.Trim() == "Shape Calculator App"), ConsoleColorType.Header),
                Times.Once);
        }

        [Fact]
        public void ShowHeader_WritesBorderWithCorrectLength()
        {
            var calls = new List<string>();

            _mockConsole
                .Setup(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()))
                .Callback<string, ConsoleColorType>((s, _) => calls.Add(s));

            _ui.ShowHeader();

            string border = calls.First();

            Assert.All(border, ch => Assert.Equal('=', ch));
        }

        [Fact]
        public void ShowFooter_WritesFooterWithBorder()
        {
            _ui.ShowFooter();

            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()), Times.Exactly(3));
        }

        [Fact]
        public void ShowShapeMenu_WritesMenu()
        {
            _ui.ShowShapeMenu();

            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()), Times.AtLeast(7));
            _mockConsole.Verify(c => c.Write("Enter your choice: ", ConsoleColorType.Input), Times.Once);
        }

        [Fact]
        public void ShowShapeMenu_PrintsExpectedMenuStructure()
        {
            var calls = new List<string>();

            _mockConsole
                .Setup(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()))
                .Callback<string, ConsoleColorType>((s, _) => calls.Add(s));

            _ui.ShowShapeMenu();

            Assert.Contains(calls, s => s.Contains("Shape Menu"));
            Assert.Contains(calls, s => s.Contains("Key"));
            Assert.Contains(calls, s => s.Contains("Shape"));
            Assert.Contains(calls, s => s.Contains('c'));
            Assert.Contains(calls, s => s.Contains("Circle"));
            Assert.Contains(calls, s => s.Contains('s'));
            Assert.Contains(calls, s => s.Contains("Square"));
            Assert.Contains(calls, s => s.Contains('t'));
            Assert.Contains(calls, s => s.Contains("Triangle"));
        }

        [Fact]
        public void ShowInvalidSelectionMessage_WritesError()
        {
            _ui.ShowInvalidSelectionMessage();

            _mockConsole.Verify(c =>
                c.WriteLine("Invalid selection. Try again.", ConsoleColorType.Error),
                Times.Once);
        }

        [Fact]
        public void ShowShapeResults_UsesFormatter_ForLengthAreaAndBoundary()
        {
            var shape = new FakeShape(150, 20000, 150);

            _ui.ShowShapeResults(shape);

            _mockConsole.Verify(c => c.WriteLine("\nLength: 1.50 m", ConsoleColorType.Result), Times.Once);
            _mockConsole.Verify(c => c.WriteLine("Area: 2.00 m²", ConsoleColorType.Result), Times.Once);
            _mockConsole.Verify(c => c.WriteLine("Boundary Length: 1.50 m", ConsoleColorType.Result), Times.Once);
        }

        [Fact]
        public void ShowShapeResults_UsesCentimetersBelowThreshold()
        {
            var shape = new FakeShape(50, 5000, 80);

            _ui.ShowShapeResults(shape);

            _mockConsole.Verify(c => c.WriteLine("\nLength: 50.00 cm", ConsoleColorType.Result), Times.Once);
            _mockConsole.Verify(c => c.WriteLine("Area: 5000.00 cm²", ConsoleColorType.Result), Times.Once);
            _mockConsole.Verify(c => c.WriteLine("Boundary Length: 80.00 cm", ConsoleColorType.Result), Times.Once);
        }

        [Theory]
        [InlineData("y", true)]
        [InlineData("Y", true)]
        [InlineData("n", false)]
        [InlineData("N", false)]
        public void AskToContinue_ReturnsExpectedBool(string input, bool expected)
        {
            _mockConsole.Setup(c => c.ReadInput(ConsoleColorType.Input)).Returns(input);

            bool result = _ui.AskToContinue();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AskToContinue_HandlesWhitespace()
        {
            _mockConsole.Setup(c => c.ReadInput(ConsoleColorType.Input)).Returns("   y   ");

            Assert.True(_ui.AskToContinue());
        }

        [Fact]
        public void AskToContinue_RepeatsOnInvalidInput()
        {
            _mockConsole
                .SetupSequence(c => c.ReadInput(ConsoleColorType.Input))
                .Returns("bad")
                .Returns("y");

            bool result = _ui.AskToContinue();

            Assert.True(result);

            _mockConsole.Verify(c =>
                c.WriteLine("Please enter 'y' or 'n'.", ConsoleColorType.Error),
                Times.Once);
        }

        private class FakeShape(double length, double area, double boundary) : Shape(length)
        {
            private readonly double _area = area;
            private readonly double _boundary = boundary;

            public override double CalculateArea() => _area;
            public override double CalculateBoundaryLength() => _boundary;
        }
    }
}