using ShapeCalculations.App.Interfaces;
using ShapeCalculations.App.Utilities;
using ShapeCalculations.Domain;

namespace ShapeCalculations.App.UI
{
    /// <summary>
    /// Console UI responsible for rendering menus, results, and prompts.
    /// Uses an injected <see cref="IConsole"/> to avoid static global state.
    /// </summary>
    public class ConsoleUI
    {
        private readonly IConsole _console;
        private const char BorderChar = '=';
        private const string HeaderText = "Shape Calculator App";
        private const string FooterText = "Thank you for using the Shape Calculator App!";

        private static int SectionWidth => FooterText.Length + 8;

        /// <summary>
        /// Create a ConsoleUI with the given console implementation.
        /// </summary>
        public ConsoleUI(IConsole console) => _console = console;

        /// <summary>
        /// Displays the formatted header banner.
        /// </summary>
        public void ShowHeader() =>
            WriteSection(ConsoleFormatter.Center(HeaderText, SectionWidth), ConsoleColorType.Header);

        /// <summary>
        /// Displays the formatted footer banner.
        /// </summary>
        public void ShowFooter() =>
            WriteSection(ConsoleFormatter.Center(FooterText, SectionWidth), ConsoleColorType.Footer);

        /// <summary>
        /// Displays the shape selection menu.
        /// </summary>
        public void ShowShapeMenu()
        {
            _console.WriteLine("\n" + ConsoleFormatter.Center("Shape Menu", SectionWidth), ConsoleColorType.Header);
            _console.WriteLine(new string('-', SectionWidth), ConsoleColorType.Header);

            _console.WriteLine("  Key    |   Shape", ConsoleColorType.Prompt);
            _console.WriteLine("---------+-------------------------", ConsoleColorType.Prompt);
            _console.WriteLine("   c     |   Circle", ConsoleColorType.Prompt);
            _console.WriteLine("   s     |   Square", ConsoleColorType.Prompt);
            _console.WriteLine("   t     |   Triangle (equilateral)\n", ConsoleColorType.Prompt);

            _console.Write("Enter your choice: ", ConsoleColorType.Input);
        }

        /// <summary>
        /// Displays a standard error message for invalid shape selections.
        /// </summary>
        public void ShowInvalidSelectionMessage() =>
            _console.WriteLine("Invalid selection. Try again.", ConsoleColorType.Error);

        /// <summary>
        /// Displays formatted length, area, and boundary length for a shape.
        /// </summary>
        public void ShowShapeResults(Shape shape)
        {
            _console.WriteLine($"\nLength: {ConsoleFormatter.FormatLength(shape.Length)}", ConsoleColorType.Result);
            _console.WriteLine($"Area: {ConsoleFormatter.FormatArea(shape.CalculateArea())}", ConsoleColorType.Result);
            _console.WriteLine(
                $"Boundary Length: {ConsoleFormatter.FormatLength(shape.CalculateBoundaryLength())}",
                ConsoleColorType.Result);
        }

        /// <summary>
        /// Asks the user whether to continue and validates input ("y" for yes, "n" for no).
        /// </summary>
        /// <returns><c>true</c> if user chooses to continue; otherwise <c>false</c>.</returns>
        public bool AskToContinue()
        {
            while (true)
            {
                _console.Write("\nCalculate another shape? (y/n): ", ConsoleColorType.Input);
                string input = (_console.ReadInput(ConsoleColorType.Input) ?? "").Trim().ToLowerInvariant();

                if (input == "y") return true;
                if (input == "n") return false;

                _console.WriteLine("Please enter 'y' or 'n'.", ConsoleColorType.Error);
            }
        }

        private void WriteSection(string text, ConsoleColorType color)
        {
            string border = new(BorderChar, SectionWidth);
            _console.WriteLine(border, color);
            _console.WriteLine(text, color);
            _console.WriteLine(border, color);
        }
    }
}