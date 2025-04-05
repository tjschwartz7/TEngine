using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Rendering
{
    public class EnvironmentSettings
    {
        public BackgroundType BackgroundType { get; set; } = BackgroundType.SolidColor;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    }

    public enum BackgroundType { SolidColor, Skybox, Gradient, Custom }
}
