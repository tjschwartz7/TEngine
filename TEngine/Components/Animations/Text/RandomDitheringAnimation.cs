using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public class RandomDitheringAnimation : ITextAnimation
    {
        private Random random = new Random();

        public string Apply(string input, int frame)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new System.Text.StringBuilder();

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
    }

}
