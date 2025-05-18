using TEngine.TMath;
using TEngine.Components.Mesh;
using TEngine.Styles;

namespace TEngine.Core.AssetManagement
{
    public class Asset
    {
        public Material? Material { get; set; } // Nullable; fallback to default if null

        public string? TextValue { get; set; }             // For UI or Text rendering
        public TextAttribute? TextAttr { get; set; }  // For UI or Text rendering
        public Sprite? Sprite { get; set; }           // For 2D
        public Mesh? Mesh { get; set; }               // For 3D

        public Matrix4x4? Transform { get; set; }     // For advanced 2D/3D transforms
        public TMath.RectangleF? ClippingRect { get; set; } // For UI
    }
}
