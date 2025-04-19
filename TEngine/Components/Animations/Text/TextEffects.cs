using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    public static class TextEffects
    {
        public static string ApplyDithering(string input, uint frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char original = input[i];

                if (char.IsWhiteSpace(original))
                {
                    output.Append(original);
                    continue;
                }

                uint index = (uint)((i + frame) % chars.Length);
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyRippleDithering(string input, uint frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    output.Append(input[i]);
                    continue;
                }

                uint index = (uint)((i + frame + (uint)(Math.Sin(i * 0.5 + frame * 0.2) * 2)) % chars.Length);
                if (index < 0) index += (uint)chars.Length; // wrap negative
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyRandomDithering(string input, uint frame = 0)
        {
            var chars = new[] { '█', '▓', '▒', '░' };
            var output = new StringBuilder();
            Random random = new Random((int)frame); // Seed with frame for reproducibility

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    output.Append(input[i]);
                    continue;
                }

                uint index = (uint)random.Next(chars.Length);
                output.Append(chars[index]);
            }

            return output.ToString();
        }

        public static string ApplyGlow(string input, uint frame = 0)
        {
            uint brightness = 2 + (uint)(Math.Sin(frame * 0.1f) + 1); // Oscillates between 1–3
            return ANSI.Brightness(brightness) + input + ANSI.Reset();
        }

        public static string ApplyRippleGlow(string input, uint frame = 0, float rippleSpeed = 0.1f)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                    continue;
                }

                // Apply ripple effect with dynamic speed
                double wave = Math.Sin(i * 0.3 + frame * rippleSpeed); // Use rippleSpeed for dynamic effect
                uint brightness = (uint)Math.Round((wave + 1) * 1.5); // Maps to 0–3

                sb.Append(ANSI.Brightness(brightness)).Append(c);
            }

            sb.Append(ANSI.Reset());
            return sb.ToString();
        }


        public static string ApplyBlink(string input, uint frame = 0, uint blinkRate = 30)
        {
            if ((frame / blinkRate) % 2 == 0)
            {
                return ANSI.Blink() + input + ANSI.Reset();
            }
            else
            {
                return input;
            }
        }

        public static string CompositeEffect(string input, uint frame, EffectConfig effectConfig)
        {
            string result = input;

            // Apply dithering effects
            if (effectConfig.Dithering)
                result = ApplyDithering(result, frame);

            if (effectConfig.RippleDithering)
                result = ApplyRippleDithering(result, frame);

            if (effectConfig.RandomDithering)
                result = ApplyRandomDithering(result, frame);

            // Apply blink effect
            if (effectConfig.Blink)
                result = ApplyBlink(result, frame, effectConfig.BlinkRate);

            // Apply glow effect (with intensity)
            if (effectConfig.Glow)
                result = ApplyGlow(result, effectConfig.GlowIntensity);

            // Apply ripple glow effect
            if (effectConfig.RippleGlow)
                result = ApplyRippleGlow(result, frame, effectConfig.RippleGlowSpeed);

            return result;
        }






    }

    public record EffectConfig(
        bool Dithering = false,           // Standard dithering
        bool RippleDithering = false,     // Ripple dithering
        bool RandomDithering = false,     // Random dithering
        bool Blink = false,               // Blink effect
        bool Glow = false,                // Glow effect
        uint BlinkRate = 30,               // Rate at which blinking occurs (frames)
        uint GlowIntensity = 1,            // Intensity of glow
        bool RippleGlow = false,          // Enable ripple glow effect
        float RippleGlowSpeed = 0.1f      // Speed of the ripple glow (optional)
    )
    {
        // Default settings (optional)
        public static EffectConfig Default => new();
    }
}
