using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Rendering.Text;

namespace TEngine.Rendering
{
    public class RenderingSettings
    {
        public Renderer Renderer { get; set; } = TextRenderer();
        public bool PostProcessing { get; set; } = false;
        public bool AntiAliasing { get; set; } = false;
        public bool StopNaNs { get; set; } = false;
        public bool Dithering { get; set; } = false;
        public bool RenderShadows { get; set; } = false;
        public int Priority { get; set; } = 0;
        public bool OpaqueTexture { get; set; } = false;
    }
}
