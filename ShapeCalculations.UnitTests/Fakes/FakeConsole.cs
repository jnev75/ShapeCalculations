using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;
using System.Text;

namespace ShapeCalculations.UnitTests.Fakes
{
    public class FakeConsole : IConsole
    {
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;

        public StringBuilder Output { get; } = new();

        public Queue<string> InputQueue { get; } = new();

        public void Write(string message) => Output.Append(message);

        public void WriteLine(string message) => Output.AppendLine(message);

        public string? ReadLine() =>
            InputQueue.Count > 0 ? InputQueue.Dequeue() : string.Empty;

        public string ReadInput(ConsoleColorType color)
        {
            ForegroundColor = MapColor(color);
            return ReadLine() ?? "";
        }

        public void ResetColor() => ForegroundColor = ConsoleColor.Gray;

        public void Write(string message, ConsoleColorType color) =>
            Write(message);

        public void WriteLine(string message, ConsoleColorType color) =>
            WriteLine(message);

        private static ConsoleColor MapColor(ConsoleColorType type) =>
            type switch
            {
                ConsoleColorType.Header or ConsoleColorType.Footer => ConsoleColor.Green,
                ConsoleColorType.Prompt => ConsoleColor.Cyan,
                ConsoleColorType.Input => ConsoleColor.White,
                ConsoleColorType.Result => ConsoleColor.Yellow,
                ConsoleColorType.Error => ConsoleColor.Red,
                _ => ConsoleColor.Gray
            };
    }
}