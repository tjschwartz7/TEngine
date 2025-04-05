using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Rendering
{
    public class OutputSettings
    {
        public object OutputTexture { get; set; } = null;
        public int TargetDisplay { get; set; } = 0;
        public TargetEye TargetEye { get; set; } = TargetEye.Both;
        public Rect ViewportRect { get; set; } = new(0, 0, 1, 1);
        public bool HDRRendering { get; set; } = false;
        public bool MSAA { get; set; } = false;
        public bool URPDynamicResolution { get; set; } = false;
    }

    public enum TargetEye { Left, Right, Both }

    public struct Rect
    {
        public float X, Y, W, H;
        public Rect(float x, float y, float w, float h) =>
            (X, Y, W, H) = (x, y, w, h);
    }
}
