using TEngine.Styles.UI;


namespace TEngine.Styles
{
    public class TextAttribute
    {
        // Default values for text properties
        public ConsoleColor WinBackgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor WinForegroundColor { get; set; } = ConsoleColor.White;
        public Font TextFont { get; set; } = new Font("Consolas", 16, FontStyle.Regular);
        public TextAlignment Alignment { get; set; } = TextAlignment.Left;

        // Constructor for easier initialization
        public TextAttribute(ConsoleColor backgroundColor = default, ConsoleColor foregroundColor = default,
                    Font font = null, TextAlignment alignment = TextAlignment.Left)
        {
            // Default to existing values if no parameters are provided
            WinBackgroundColor = backgroundColor == default ? ConsoleColor.Black : backgroundColor;
            WinForegroundColor = foregroundColor == default ? ConsoleColor.White : foregroundColor;
            TextFont = font ?? new Font("Consolas", 16, FontStyle.Regular);
            Alignment = alignment;
        }

        // Optional: Method to change the font style (e.g., Bold, Italic)
        public void SetFontStyle(FontStyle style)
        {
            TextFont.Style = style;
        }

        // Method to apply text changes easily (e.g., alignment or colors)
        public void ApplyTextChanges(ConsoleColor? backgroundColor = null, ConsoleColor? foregroundColor = null,
                                     Font? font = null, TextAlignment? alignment = null)
        {
            if (backgroundColor.HasValue)
                WinBackgroundColor = backgroundColor.Value;
            if (foregroundColor.HasValue)
                WinForegroundColor = foregroundColor.Value;
            if (font != null)
                TextFont = font;
            if (alignment.HasValue)
                Alignment = alignment.Value;
        }
    }
}
