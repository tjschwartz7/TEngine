using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public class Glow : TextAnimation
    {
        public override string Apply(string target)
        {
            return TextEffects.ApplyGlow(target, FrameCount);
        }
    }
}
