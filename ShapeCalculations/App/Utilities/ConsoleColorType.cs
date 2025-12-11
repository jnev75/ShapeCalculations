namespace ShapeCalculations.App.Utilities
{
    /// <summary>
    /// Logical color categories used by the UI.
    /// Mapped to real console colors by the <see cref="IConsole"/> implementation.
    /// </summary>
    public enum ConsoleColorType
    {
        /// <summary>Use default console foreground color.</summary>
        Default,

        /// <summary>Banner/header color.</summary>
        Header,

        /// <summary>Footer color.</summary>
        Footer,

        /// <summary>Menu/prompts color.</summary>
        Prompt,

        /// <summary>Input read color.</summary>
        Input,

        /// <summary>Result display color.</summary>
        Result,

        /// <summary>Error/warning color.</summary>
        Error
    }
}