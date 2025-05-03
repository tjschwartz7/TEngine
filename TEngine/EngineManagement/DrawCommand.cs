using TEngine.Assets;
using TEngine.Components.Graphics;
using TEngine.TMath;
using TEngine.Components.Mesh;

namespace TEngine.EngineManagement
{
    public enum DrawCommandType
    {
        Text,
        Sprite2D,
        Mesh3D,
        UIElement
    }

    public class DrawCommand
    {
        public DrawCommandType Type { get; set; }

        public Vector3 Position { get; set; } = Vector3.zero; // Z = depth/layer

        public Material Material { get; set; } // Nullable; fallback to default if null

        public string? Text { get; set; }             // For UI or Text rendering
        public Sprite? Sprite { get; set; }           // For 2D
        public Mesh? Mesh { get; set; }               // For 3D

        public Matrix4x4? Transform { get; set; }     // For advanced 2D/3D transforms
        public RectangleF? ClippingRect { get; set; } // For UI
        public int SortingOrder { get; set; }         // Optional fine-tune draw order

        // Tag or metadata
        public object? UserData { get; set; }
    }


}
