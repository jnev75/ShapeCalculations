using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;

namespace ShapeCalculations.UnitTests.Fakes
{
    public class ThrowingConsole : IConsole
    {
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;

        public void Write(string message) =>
            throw new InvalidOperationException("Write failed");

        public void WriteLine(string message) =>
            throw new InvalidOperationException("WriteLine failed");

        public void Write(string message, ConsoleColorType color) =>
            throw new InvalidOperationException("Write failed (colored)");

        public void WriteLine(string message, ConsoleColorType color) =>
            throw new InvalidOperationException("WriteLine failed (colored)");

        public string? ReadLine() => string.Empty;

        public string? ReadInput(ConsoleColorType color) => string.Empty;

        public void ResetColor() => ForegroundColor = ConsoleColor.Gray;
    }
}