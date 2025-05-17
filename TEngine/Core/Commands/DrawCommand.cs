using TEngine.TMath;
using TEngine.Components.Mesh;
using System.Drawing;
using TEngine.Styles;
using TEngine.Utils;

namespace TEngine.Core.Commands
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
        public Layer Layer { get; set; } = Layers.GetLayer(0); // Default layer for rendering
        public Vector3 Position { get; set; } = Vector3.zero; // Z = depth/layer

        public Material? Material { get; set; } // Nullable; fallback to default if null

        public string? TextValue { get; set; }             // For UI or Text rendering
        public TextAttribute? TextAttr { get; set; }  // For UI or Text rendering
        public Sprite? Sprite { get; set; }           // For 2D
        public Mesh? Mesh { get; set; }               // For 3D

        public Matrix4x4? Transform { get; set; }     // For advanced 2D/3D transforms
        public TMath.RectangleF? ClippingRect { get; set; } // For UI
        public int SortingOrder { get; set; }         // Optional fine-tune draw order

        // Tag or metadata
        public object? UserData { get; set; }
        public int uuid { get; set; } = 0; // Unique identifier for the command
    }
}
