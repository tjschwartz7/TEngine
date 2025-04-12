using System;
using TEngine.Components;
using TEngine.Components.Animations;

namespace TEngine.Components.Animations.Text
{
    public class TextAnimation : Animation<string>
    {
        public TextAnimationType AnimationType { get; private set; } = TextAnimationType.Default;
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

        public override string Apply(string target)
        {
            if (target is string input)
            {
                return animation.Apply(input, FrameCount);
 
            }
            else
            {
                throw new ArgumentException("Invalid target for TextAnimation");
            }
            
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
