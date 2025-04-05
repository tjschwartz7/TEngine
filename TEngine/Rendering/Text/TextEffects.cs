using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Rendering.Text
{
    public static class TextEffects
    {
        public static string ApplyDithering(string input, int frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new System.Text.StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char original = input[i];

                if (char.IsWhiteSpace(original))
                {
                    output.Append(original);
                    continue;
                }

                int index = (i + frame) % chars.Length;
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyGlow(string input)
        {
            // Just a simulated glow wrapper for now
            return $"*{input}*";
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
