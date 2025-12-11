using ShapeCalculations.App.Interfaces;

namespace ShapeCalculations.App.Utilities
{
    /// <summary>
    /// Real system console implementation of <see cref="IConsole"/>.
    /// Delegates operations to <see cref="System.Console"/>.
    /// </summary>
    public class RealConsole : IConsole
    {
        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public void Write(string text) => Console.Write(text);

        public void WriteLine(string text) => Console.WriteLine(text);

        public string? ReadLine() => Console.ReadLine();

        public void ResetColor() => Console.ResetColor();

        public void Write(string text, ConsoleColorType color)
        {
            var original = ForegroundColor;
            ForegroundColor = MapColor(color);
            try
            {
                Write(text);
            }
            finally
            {
                ForegroundColor = original;
            }
        }

        public void WriteLine(string text, ConsoleColorType color)
        {
            var original = ForegroundColor;
            ForegroundColor = MapColor(color);
            try
            {
                WriteLine(text);
            }
            finally
            {
                ForegroundColor = original;
            }
        }

        public string ReadInput(ConsoleColorType color)
        {
            ForegroundColor = MapColor(color);
            try
            {
                return ReadLine() ?? string.Empty;
            }
            finally
            {
                ResetColor();
            }
        }

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