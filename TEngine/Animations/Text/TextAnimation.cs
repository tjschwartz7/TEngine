using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Animations.Text
{
    public class TextAnimation
    {
        public TextAnimationType AnimationType { get; private set; } = TextAnimationType.Default;
        public int FrameCount { get; private set; } = 0;
        private ITextAnimation animation;


        public TextAnimation()
        {
            animation = CreateAnimation(AnimationType);
        }

        public TextAnimation(TextAnimationType animationType)
        {
            AnimationType = animationType;
            animation = CreateAnimation(AnimationType);
        }

        public void Tick()
        {
            FrameCount++;
        }

        public void SetFrame(int frame)
        {
            FrameCount = frame;
        }

        public string Apply(string input)
        {
            return animation.Apply(input, FrameCount);
        }

        public void SetAnimationType(TextAnimationType animationType)
        {
            AnimationType = animationType;
            animation = CreateAnimation(AnimationType);
        }

        private ITextAnimation CreateAnimation(TextAnimationType type)
        {
            return type switch
            {
                TextAnimationType.StaticDithering => new StaticDitheringAnimation(),
                TextAnimationType.RandomDithering => new RandomDitheringAnimation(),
                TextAnimationType.RippleDithering => new RippleDitheringAnimation(),
                _ => new DefaultTextAnimation()
            };
        }
    }

    public enum TextAnimationType
    {
        Default,
        StaticDithering,
        RandomDithering,
        RippleDithering
    }
}
