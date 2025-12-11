using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;
using ShapeCalculations.Domain;
using System.Globalization;

namespace ShapeCalculations.App.Input
{
    /// <summary>
    /// Handles user input for selecting shapes and entering numeric values.
    /// Uses an <see cref="IConsole"/> abstraction to support testing.
    /// </summary>
    public class ShapeInputHandler
    {
        private readonly IConsole _console;
        private const double MinLength = 0.01;
        private const double MaxLength = 10000;

        /// <summary>
        /// Creates a new input handler using the provided console implementation.
        /// </summary>
        public ShapeInputHandler(IConsole console) => _console = console;

        /// <summary>
        /// Reads a shape selection (C, S, T) from the console.
        /// Returns <c>null</c> if the input is unrecognized.
        /// </summary>
        public ShapeType? GetShapeType()
        {
            string input = (_console.ReadInput(ConsoleColorType.Input) ?? "")
                .Trim()
                .ToLowerInvariant();

            return input switch
            {
                "c" => ShapeType.Circle,
                "s" => ShapeType.Square,
                "t" => ShapeType.Triangle,
                _ => null
            };
        }

        /// <summary>
        /// Repeatedly prompts the user for a valid numeric length within the allowed range.
        /// Returns the parsed value when valid.
        /// </summary>
        public double GetLength()
        {
            while (true)
            {
                _console.Write("Enter the length: ", ConsoleColorType.Input);

                string input = (_console.ReadInput(ConsoleColorType.Input) ?? "")
                    .Trim()
                    .Replace(",", ".");

                if (!double.TryParse(
                        input,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double value))
                {
                    ShowError("Invalid input. Please enter a numeric value.");
                    continue;
                }

                if (value >= MinLength && value <= MaxLength)
                    return value;

                ShowError($"Invalid length. Please enter a value between {MinLength} and {MaxLength} cm.");
            }
        }

        /// <summary>
        /// Displays a formatted error message to the console.
        /// </summary>
        private void ShowError(string message) =>
            _console.WriteLine($"Error: {message}", ConsoleColorType.Error);
    }
}