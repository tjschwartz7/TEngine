using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public class RippleGlow : TextAnimation
    {
        public override string Apply(string input)
        {
            return TextEffects.ApplyRippleGlow(input, FrameCount);
        }
    }
}
