using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public class Blink : TextAnimation
    {
        public uint Speed = 5;
        public uint BlinkRate = 30;

        public override string Apply(string target)
        {
            return TextEffects.ApplyBlink(target, FrameCount, BlinkRate);
        }

    }
}
