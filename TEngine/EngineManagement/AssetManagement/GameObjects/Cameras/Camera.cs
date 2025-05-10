using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.RenderingEngines;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T2D;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T3D;
using TEngine.TMath;
using TEngine.Utils;
using TEngine.EngineManagement;

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Cameras
{
    public abstract class Camera : GameObject, ICamera
    {
        public RenderType RenderType { get; set; } = RenderType.Base;

        public ProjectionSettings Projection;
        public RenderingSettings Rendering;
        public StackSettings Stack;
        public EnvironmentSettings Environment;
        public VolumeSettings Volumes;
        public OutputSettings Output;

        public Vector3 Position = Vector3.zero; // Top-left world coordinate in view
        public Vector2 ViewSize = new(80, 25);

        public float Zoom { get; set; } = 1f; // 1 = normal, >1 = zoom in, <1 = zoom out

        // Method to get the scaled view size based on zoom
        public Vector2 GetScaledViewSize()
        {
            return ViewSize / Zoom;
        }

        // Constructor
        public Camera(int id, string name, string tag = "Camera") : base(id, name, tag)
        {

            Projection = new ProjectionSettings(ProjectionType.Perspective, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(GraphicSystem.Text, true, false, false, true, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, Console.WindowWidth, Console.WindowHeight), false, false, false);
            switch (Engine.Resolution)
            {
                case Resolution.R80x25:
                    ViewSize = new Vector2(80, 25);
                    break;
                case Resolution.R160x50:
                    ViewSize = new Vector2(160, 50);
                    break;
                case Resolution.R120x40:
                    ViewSize = new Vector2(120, 40);
                    break;
                case Resolution.R100x30:
                    ViewSize = new Vector2(100, 30);
                    break;
                default:
                    ViewSize = new Vector2(80, 25);
                    break;
            }
        }

        public bool IsVisible(Vector3 position)
        {
            // Get the scaled view size based on zoom
            var scaledViewSize = GetScaledViewSize();

            return position.X >= Position.X && position.X <= Position.X + scaledViewSize.X &&
                   position.Y >= Position.Y && position.Y <= Position.Y + scaledViewSize.Y;
        }

        public record ProjectionSettings(ProjectionType ProjectionType, FieldOfViewAxis FOVAxis, float FieldOfView, float NearClipPlane, bool PhysicalCamera);
        public record EnvironmentSettings(BackgroundType BackgroundType, ConsoleColor BackgroundColor);

        public record OutputSettings(int TargetDisplay, TargetEye TargetEye, Rect ViewportRect, bool HDRRendering, bool MSAA, bool URPDynamicResolution);

        public record RenderingSettings(GraphicSystem Renderer, bool PostProcessing, bool Glow, bool Dithering, bool RenderShadows, int Priority, bool OpaqueTexture);
        public record StackSettings(List<Camera> Cameras);
        public record VolumeSettings(VolumeUpdateMode UpdateMode, int VolumeMask, object VolumeTrigger);
    }

    public interface ICamera
    {
    }

    public enum RenderType
    {
        Base,           // The main world renderer (typically the gameplay scene)
        Overlay,        // UI or HUD layers (drawn over Base)
    }

    public enum ProjectionType { Orthographic, Perspective }
    public enum FieldOfViewAxis { Vertical, Horizontal }

    public enum BackgroundType { SolidColor, Skybox, Gradient, Custom }

    public enum TargetEye { Left, Right, Both }

    public struct Rect
    {
        public float X, Y, W, H;
        public Rect(float x, float y, float w, float h) =>
            (X, Y, W, H) = (x, y, w, h);
    }

    public enum VolumeUpdateMode { EveryFrame, OnTriggerEnter, Manual }

}
