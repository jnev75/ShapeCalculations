using Moq;
using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;

namespace ShapeCalculations.UnitTests.App.Utilities
{
    public class ColoredConsoleTests
    {
        [Fact]
        public void WriteLine_WritesTextToConsole_WithDefaultColorType()
        {
            var mockConsole = new Mock<IConsole>();
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            ColoredConsole.WriteLine("Hello World");

            mockConsole.Verify(c => c.WriteLine("Hello World", ConsoleColorType.Default), Times.Once);
        }

        [Fact]
        public void Write_WritesTextToConsole_WithDefaultColorType()
        {
            var mockConsole = new Mock<IConsole>();
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            ColoredConsole.Write("Hello");

            mockConsole.Verify(c => c.Write("Hello", ConsoleColorType.Default), Times.Once);
        }

        [Fact]
        public void ReadInput_ReturnsUserInput_FromWrapper()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                       .Returns("TestInput");
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            string result = ColoredConsole.ReadInput();

            Assert.Equal("TestInput", result);
        }

        [Fact]
        public void ReadInput_ReturnsEmptyStringIfWrapperReturnsNull()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(c => c.ReadInput(It.IsAny<ConsoleColorType>()))
                       .Returns((string?)null);
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            string result = ColoredConsole.ReadInput();

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData(ConsoleColorType.Header)]
        [InlineData(ConsoleColorType.Footer)]
        [InlineData(ConsoleColorType.Prompt)]
        [InlineData(ConsoleColorType.Result)]
        [InlineData(ConsoleColorType.Error)]
        [InlineData(ConsoleColorType.Default)]
        public void WriteLine_DelegatesWithCorrectConsoleColorType(ConsoleColorType type)
        {
            var mockConsole = new Mock<IConsole>();
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            ColoredConsole.WriteLine("Test", type);

            mockConsole.Verify(c => c.WriteLine("Test", type), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleColorType.Header)]
        [InlineData(ConsoleColorType.Footer)]
        [InlineData(ConsoleColorType.Prompt)]
        [InlineData(ConsoleColorType.Result)]
        [InlineData(ConsoleColorType.Error)]
        [InlineData(ConsoleColorType.Default)]
        public void Write_DelegatesWithCorrectConsoleColorType(ConsoleColorType type)
        {
            var mockConsole = new Mock<IConsole>();
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            ColoredConsole.Write("Test", type);

            mockConsole.Verify(c => c.Write("Test", type), Times.Once);
        }

        [Fact]
        public void WriteLine_WhenWrapperThrows_ExceptionPropagates()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(c => c.WriteLine(It.IsAny<string>(), It.IsAny<ConsoleColorType>()))
                       .Throws(new Exception("Simulated failure"));
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            Assert.Throws<Exception>(() => ColoredConsole.WriteLine("Test", ConsoleColorType.Error));
        }

        [Fact]
        public void ResetToDefault_SetsWrapperBackToRealConsole()
        {
            var mockConsole = new Mock<IConsole>();
            ColoredConsole.ConsoleWrapper = mockConsole.Object;

            ColoredConsole.ResetToDefault();

            Assert.IsType<RealConsole>(ColoredConsole.ConsoleWrapper);
        }
    }
}