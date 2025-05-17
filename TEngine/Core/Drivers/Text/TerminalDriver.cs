using TEngine.TMath;
using TEngine.Core.Drivers;
using TEngine.Core.Commands;


namespace TEngine.Core.RenderingEngines.Text
{
    public class TerminalDriver : Driver
    {
        public override void Draw(DrawCommand cmd)
        {
            // Convert position to screen coordinates (X, Y)
            int x = (int)cmd.Position.X;
            int y = (int)cmd.Position.Y;

            // Save the current cursor position to restore later
            var originalCursorPosition = Console.GetCursorPosition();

            // Move the cursor to the specified position
            Console.SetCursorPosition(x, y);

            // Write the text at the specified position
            if(cmd.TextAttr != null)
            {
                // Apply background color if specified
                if (cmd.TextAttr.WinBackgroundColor != ConsoleColor.Black)
                {
                    Console.BackgroundColor = cmd.TextAttr.WinBackgroundColor;
                }
                // Apply foreground color if specified
                if (cmd.TextAttr.WinForegroundColor != ConsoleColor.White)
                {
                    Console.ForegroundColor = cmd.TextAttr.WinForegroundColor;
                }
            }

            Console.Write(cmd.TextValue);
            // Reset colors to default
            Console.ResetColor();

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
