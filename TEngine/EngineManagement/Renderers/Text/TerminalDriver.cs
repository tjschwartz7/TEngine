using TEngine.TMath;


namespace TEngine.EngineManagement.RenderingEngines.Text
{
    internal class TerminalDriver
    {
        public static void DrawText(string text, Vector3 position)
        {

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
