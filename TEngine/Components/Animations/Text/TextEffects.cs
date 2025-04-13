using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public static class TextEffects
    {
        public static string ApplyDithering(string input, int frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();

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

        public static string ApplyRippleDithering(string input, int frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    output.Append(input[i]);
                    continue;
                }

                int index = (i + frame + (int)(Math.Sin(i * 0.5 + frame * 0.2) * 2)) % chars.Length;
                if (index < 0) index += chars.Length; // wrap negative
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyRandomDithering(string input, int frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();
            Random random = new Random(frame); // Seed with frame for reproducibility

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    output.Append(input[i]);
                    continue;
                }

                int index = random.Next(chars.Length);
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyGlow(string input)
        {
            // Just a simulated glow wrapper for now
            return $"*{input}*";
        }

       
    }
}
