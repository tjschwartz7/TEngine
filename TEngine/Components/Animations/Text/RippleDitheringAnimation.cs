using System;

namespace TEngine.Components.Animations.Text
{
    public class RippleDitheringAnimation : ITextAnimation
    {
        public string Apply(string input, int frame)
        {
            return TextEffects.ApplyRippleDithering(input, frame);
        }
    }

}
