using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Animations.Text
{
    internal static class ANSI
    {
        public static string Brightness(uint level) => level switch
        {
            3 => "\u001b[97m", // Bright white
            2 => "\u001b[37m", // Gray
            1 => "\u001b[90m", // Dark gray
            _ => "\u001b[30m", // Black or dark background
        };

        public static string Reset() => "\u001b[0m";

        public static string Blink() => "\u001b[5m";
    }
}
