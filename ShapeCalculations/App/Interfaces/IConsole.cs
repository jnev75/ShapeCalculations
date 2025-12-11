using ShapeCalculations.App.Utilities;

namespace ShapeCalculations.App.Interfaces
{
    /// <summary>
    /// Provides an abstraction over console-like input/output operations.
    /// Allows UI logic to be isolated from the system console
    /// for testing or alternative front-end environments.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Gets or sets the active foreground color used for text rendering.
        /// </summary>
        ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Writes text without a newline using the current console color.
        /// </summary>
        void Write(string message);

        /// <summary>
        /// Writes text followed by a newline using the current console color.
        /// </summary>
        void WriteLine(string message);

        /// <summary>
        /// Writes text using a specific <see cref="ConsoleColorType"/>,
        /// then restores the original color.
        /// </summary>
        void Write(string message, ConsoleColorType color);

        /// <summary>
        /// Writes text and a newline using a specific <see cref="ConsoleColorType"/>,
        /// then restores the original color.
        /// </summary>
        void WriteLine(string message, ConsoleColorType color);

        /// <summary>
        /// Reads a line of text from the input source.
        /// May return <c>null</c> if input ends.
        /// </summary>
        string? ReadLine();

        /// <summary>
        /// Reads input while temporarily applying a specific console color,
        /// then restores the previous color.
        /// </summary>
        string? ReadInput(ConsoleColorType color);

        /// <summary>
        /// Restores the console’s color state to its default.
        /// </summary>
        void ResetColor();
    }
}