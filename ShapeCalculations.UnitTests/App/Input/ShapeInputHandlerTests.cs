using Moq;
using ShapeCalculations.App.Input;
using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;
using ShapeCalculations.Domain;

namespace ShapeCalculations.UnitTests.App.Input
{
    public class ShapeInputHandlerTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly ShapeInputHandler _handler;

        public ShapeInputHandlerTests()
        {
            _mockConsole = new Mock<IConsole>();
            _handler = new ShapeInputHandler(_mockConsole.Object);
        }

        [Fact]
        public void Constructor_InitializesCorrectly()
        {
            var console = new Mock<IConsole>().Object;
            var handler = new ShapeInputHandler(console);

            Assert.NotNull(handler);
        }

        [Theory]
        [InlineData("c", ShapeType.Circle)]
        [InlineData("C", ShapeType.Circle)]
        [InlineData("s", ShapeType.Square)]
        [InlineData("S", ShapeType.Square)]
        [InlineData("t", ShapeType.Triangle)]
        [InlineData("T", ShapeType.Triangle)]
        public void GetShapeType_ReturnsExpectedShape(string input, ShapeType expected)
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>())).Returns(input);

            ShapeType? result = _handler.GetShapeType();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("  c  ", ShapeType.Circle)]
        [InlineData("\tS\t", ShapeType.Square)]
        [InlineData("\nT\n", ShapeType.Triangle)]
        public void GetShapeType_TrimsWhitespace_ReturnsExpectedShape(string input, ShapeType expected)
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>())).Returns(input);

            ShapeType? result = _handler.GetShapeType();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData(null)]
        [InlineData("x")]
        [InlineData("%")]
        [InlineData("1")]
        [InlineData("circle")]
        [InlineData("square")]
        [InlineData("triangle")]
        [InlineData("CIRCLE")]
        [InlineData("SQUARE")]
        [InlineData("TRIANGLE")]
        [InlineData("Circle")]
        [InlineData("Square")]
        [InlineData("Triangle")]
        public void GetShapeType_InvalidInput_ReturnsNull(string input)
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>())).Returns(input);

            ShapeType? result = _handler.GetShapeType();

            Assert.Null(result);
        }

        [Fact]
        public void GetLength_WritesPrompt()
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>())).Returns("123");

            _handler.GetLength();

            _mockConsole.Verify(c => c.Write("Enter the length: ", ConsoleColorType.Input), Times.Once);
        }

        [Fact]
        public void GetLength_ReturnsValidInput()
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>())).Returns("123.45");

            double length = _handler.GetLength();

            Assert.Equal(123.45, length);
        }

        [Fact]
        public void GetLength_AcceptsScientificNotation()
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns("1e2");

            double result = _handler.GetLength();

            Assert.Equal(100, result);
        }

        [Fact]
        public void GetLength_TrimsWhitespaceAroundValidNumber()
        {
            _mockConsole
                .Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns("   123   ");

            double result = _handler.GetLength();

            Assert.Equal(123, result);
        }

        [Fact]
        public void GetLength_RepeatsOnNonNumericInput()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns("abc")
                        .Returns("50");

            double length = _handler.GetLength();

            Assert.Equal(50, length);
            _mockConsole.Verify(c => c.WriteLine("Error: Invalid input. Please enter a numeric value.", ConsoleColorType.Error), Times.Once);
        }

        [Theory]
        [InlineData("0.01", 0.01)]
        [InlineData("10000", 10000)]
        public void GetLength_AcceptsExactBoundaryValues(string input, double expected)
        {
            _mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns(input);

            double result = _handler.GetLength();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLength_RepeatsOnValueBelowMin()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns("0")
                        .Returns("0.01");

            double length = _handler.GetLength();

            Assert.Equal(0.01, length);
            _mockConsole.Verify(c => c.WriteLine(
                "Error: Invalid length. Please enter a value between 0.01 and 10000 cm.",
                ConsoleColorType.Error), Times.Once);
        }

        [Fact]
        public void GetLength_RepeatsOnValueAboveMax()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns("10001")
                        .Returns("10000");

            double length = _handler.GetLength();

            Assert.Equal(10000, length);
            _mockConsole.Verify(c => c.WriteLine(
                "Error: Invalid length. Please enter a value between 0.01 and 10000 cm.",
                ConsoleColorType.Error), Times.Once);
        }

        [Fact]
        public void GetLength_MultipleInvalidInputsBeforeValid()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                        .Returns("abc")
                        .Returns("-1")
                        .Returns("100000")
                        .Returns("123");

            double length = _handler.GetLength();

            Assert.Equal(123, length);

            _mockConsole.Verify(c => c.WriteLine(
                "Error: Invalid input. Please enter a numeric value.", ConsoleColorType.Error), Times.Once);

            _mockConsole.Verify(c => c.WriteLine(
                "Error: Invalid length. Please enter a value between 0.01 and 10000 cm.", ConsoleColorType.Error), Times.Exactly(2));
        }

        [Fact]
        public void GetLength_RepeatsOnWhitespaceOnlyInput()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns("   ")
                .Returns("50");

            double length = _handler.GetLength();

            Assert.Equal(50, length);
            _mockConsole.Verify(c => c.WriteLine("Error: Invalid input. Please enter a numeric value.", ConsoleColorType.Error), Times.Once);
        }

        [Fact]
        public void GetLength_RepeatsOnNullInput()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns(default(string))
                .Returns("25");

            double length = _handler.GetLength();

            Assert.Equal(25, length);

            _mockConsole.Verify(c => c.WriteLine(
                "Error: Invalid input. Please enter a numeric value.",
                ConsoleColorType.Error),
                Times.Once);
        }

        [Fact]
        public void GetLength_AcceptsCommaDecimalSeparator()
        {
            _mockConsole
                .Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns("123,45");

            double result = _handler.GetLength();

            Assert.Equal(123.45, result);
        }

        [Fact]
        public void GetLength_TrimsWhitespaceAroundCommaDecimal()
        {
            _mockConsole
                .Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns("   123,45   ");

            double result = _handler.GetLength();

            Assert.Equal(123.45, result);
        }

        [Fact]
        public void GetLength_InvalidCommaFormat_Repeats()
        {
            _mockConsole.SetupSequence(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                .Returns("123,45,67")
                .Returns("50");

            double result = _handler.GetLength();

            Assert.Equal(50, result);
        }
    }
}