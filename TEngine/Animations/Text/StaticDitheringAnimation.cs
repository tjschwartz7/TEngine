using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Animations.Text
{
    public class StaticDitheringAnimation : ITextAnimation
    {
        public string Apply(string input, int frame)
        {
            return TextEffects.ApplyDithering(input, frame);
        }
    }
}
