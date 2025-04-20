using TEngine.TMath;
using TEngine.EngineManagement.Drivers;


namespace TEngine.EngineManagement.RenderingEngines.Text
{
    public class TerminalDriver : Driver<string>
    {
        public TerminalDriver Instance { get; private set; } = new TerminalDriver();
        public override void Draw(string text, Vector3 position)
        {
            // Convert position to screen coordinates (X, Y)
            int x = (int)position.X;
            int y = (int)position.Y;

            // Save the current cursor position to restore later
            var originalCursorPosition = Console.GetCursorPosition();

            // Move the cursor to the specified position
            Console.SetCursorPosition(x, y);

            // Write the text at the specified position
            Console.Write(text);

            // Restore the original cursor position to avoid affecting the terminal's normal behavior
            Console.SetCursorPosition(originalCursorPosition.Left, originalCursorPosition.Top);
        }

        // 🟩 Apply foreground color (text color)
        public static void WriteColored(string text, ConsoleColor color, bool reset = true)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            if (reset)
                Console.ForegroundColor = oldColor;
        }

        // 🟥 Apply background color (block behind text)
        public static void WriteWithBackground(string text, ConsoleColor bgColor, ConsoleColor? fgColor = null, bool reset = true)
        {
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            Console.BackgroundColor = bgColor;
            if (fgColor != null)
                Console.ForegroundColor = fgColor.Value;

            Console.Write(text);

            if (reset)
            {
                Console.ForegroundColor = oldFg;
                Console.BackgroundColor = oldBg;
            }
        }

        // 🌀 Convenience: WriteLine with color
        public static void WriteLineColored(string text, ConsoleColor color, bool reset = true)
        {
            WriteColored(text + Environment.NewLine, color, reset);
        }

        public static void WriteLineWithBackground(string text, ConsoleColor bgColor, ConsoleColor? fgColor = null, bool reset = true)
        {
            WriteWithBackground(text + Environment.NewLine, bgColor, fgColor, reset);
        }
    }
}
