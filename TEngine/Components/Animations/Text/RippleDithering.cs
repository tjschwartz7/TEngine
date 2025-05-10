using System;

namespace TEngine.Components.Animations.Text
{
    public class RippleDithering : TextAnimation
    {
        public override string Apply(string input)
        {
            return TextEffects.ApplyRippleDithering(input, FrameCount);
        }
    }

}
