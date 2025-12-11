using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;

namespace ShapeCalculations.UnitTests.Fakes
{
    public class NullReadConsole : IConsole
    {
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;

        public void Write(string message) { }

        public void WriteLine(string message) { }

        public void Write(string message, ConsoleColorType color) { }

        public void WriteLine(string message, ConsoleColorType color) { }

        public string? ReadLine() => null;

        public string? ReadInput(ConsoleColorType color) => null;

        public void ResetColor() => ForegroundColor = ConsoleColor.Gray;
    }
}