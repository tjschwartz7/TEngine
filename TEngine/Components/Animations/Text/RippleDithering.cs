using System;

namespace TEngine.Components.Animations.Text
{
    public class RippleDithering : Animation<string>
    {
        public override string Apply(string input)
        {
            return TextEffects.ApplyRippleDithering(input, FrameCount);
        }
    }

}
