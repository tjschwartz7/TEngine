using TEngine.TMath;
using TEngine.Components.Mesh;
using System.Drawing;
using TEngine.Styles;
using TEngine.Utils;
using TEngine.Core.AssetManagement;

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

        public Asset Asset { get; set; } = new Asset(); // Asset to be rendered
        public int SortingOrder { get; set; }         // Optional fine-tune draw order

        // Tag or metadata
        public object? UserData { get; set; }
        public int uuid { get; set; } = 0; // Unique identifier for the command
    }
}
