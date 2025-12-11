namespace ShapeCalculations.App.UI
{
    /// <summary>
    /// Internal helper methods for formatting text and numeric values
    /// for display in the console UI. Not part of the public API.
    /// </summary>
    internal static class ConsoleFormatter
    {
        /// <summary>
        /// Centers a text string within a specified total width.
        /// If the text is longer than the width, it is returned unchanged.
        /// </summary>
        internal static string Center(string text, int totalWidth)
        {
            if (text.Length >= totalWidth)
                return text;

            int padding = (totalWidth - text.Length) / 2;
            return new string(' ', padding) + text;
        }

        /// <summary>
        /// Formats a length value into centimeters or meters depending on size.
        /// </summary>
        internal static string FormatLength(double length) =>
            length <= 100
                ? $"{length:F2} cm"
                : $"{length / 100.0:F2} m";

        /// <summary>
        /// Formats an area value into square centimeters or square meters depending on magnitude.
        /// </summary>
        internal static string FormatArea(double area) =>
            area <= 10_000
                ? $"{area:F2} cm²"
                : $"{area / 10_000.0:F2} m²";
    }
}