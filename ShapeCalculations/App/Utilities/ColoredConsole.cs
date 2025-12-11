using ShapeCalculations.App.Interfaces;

namespace ShapeCalculations.App.Utilities
{
    /// <summary>
    /// Provides a static façade for console-like input/output operations.
    /// Delegates all behavior to an <see cref="IConsole"/> implementation,
    /// enabling isolated UI testing and customizable console behavior.
    /// </summary>
    public static class ColoredConsole
    {
        /// <summary>
        /// The underlying console implementation.
        /// Typically <see cref="RealConsole"/> in production,
        /// and a test double (fake/stub) during unit testing.
        /// </summary>
        public static IConsole ConsoleWrapper { get; set; } = new RealConsole();

        /// <summary>
        /// Writes a line of text using an optional color category.
        /// </summary>
        public static void WriteLine(string text, ConsoleColorType type = ConsoleColorType.Default) =>
            ConsoleWrapper.WriteLine(text, type);

        /// <summary>
        /// Writes text without a newline using an optional color category.
        /// </summary>
        public static void Write(string text, ConsoleColorType type = ConsoleColorType.Default) =>
            ConsoleWrapper.Write(text, type);

        /// <summary>
        /// Reads user input while applying the specified color category.
        /// Delegates entirely to the underlying console implementation.
        /// </summary>
        public static string ReadInput(ConsoleColorType type = ConsoleColorType.Input) =>
            ConsoleWrapper.ReadInput(type) ?? string.Empty;

        /// <summary>
        /// Resets the console wrapper back to the default implementation.
        /// Useful in unit tests to prevent wrapper leakage.
        /// </summary>
        public static void ResetToDefault() =>
            ConsoleWrapper = new RealConsole();
    }
}