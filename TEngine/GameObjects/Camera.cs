using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Rendering;
using TEngine.TMath;
using TEngine.Utils;

namespace TEngine.GameObjects
{
    public class Camera : GameObject
    {

        public RenderType RenderType { get; set; } = RenderType.Base;

        public ProjectionSettings Projection = new();
        public RenderingSettings Rendering = new();
        public StackSettings Stack = new();
        public EnvironmentSettings Environment = new();
        public VolumeSettings Volumes = new();
        public OutputSettings Output = new();

        public Vector2Int Position = Vector2Int.zero; // Top-left world coordinate in view
        public Vector2Int ViewSize = new(Console.WindowWidth, Console.WindowHeight);

        public Layer Layer { get; set; } = new(0, "Default");

        // Constructor
        public Camera() 
        {
            ObjectTag = new Tag("MainCamera");
        }
    }

    public enum RenderType
    {
        Base,           // The main world renderer (typically the gameplay scene)
        Overlay,        // UI or HUD layers (drawn over Base)
        Background,     // Used for static or parallax backgrounds
        Minimap,        // Specialized camera for a small map overlay
        Debug,          // For rendering debug overlays or logs
        Screenshot,     // Off-screen rendering to a texture (if supported)
        DepthOnly,      // Doesn't draw characters, just stores visibility/priority (experimental)
        LightingOnly    // Could be used to simulate a lighting pass (fake brightness/shadow dither)
    }
}
