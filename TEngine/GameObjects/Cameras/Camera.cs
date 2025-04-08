using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Rendering;
using TEngine.Rendering.Text;
using TEngine.TMath;
using TEngine.Utils;

namespace TEngine.GameObjects.Cameras
{
    public abstract class Camera : GameObject
    {
        public RenderType RenderType { get; set; } = RenderType.Base;

        public ProjectionSettings Projection;
        public RenderingSettings Rendering;
        public StackSettings Stack;
        public EnvironmentSettings Environment;
        public VolumeSettings Volumes;
        public OutputSettings Output;

        public Vector2Int Position = Vector2Int.zero; // Top-left world coordinate in view
        public Vector2Int ViewSize = new(Console.WindowWidth, Console.WindowHeight);

        public Layer Layer { get; set; } = new(0, "Default");

        // Constructor
        public Camera() 
        {
            ObjectTag = new Tag("MainCamera");

            Projection = new ProjectionSettings(ProjectionType.Perspective, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(RenderingEngine.Default, true, false, false, true, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, Console.WindowWidth, Console.WindowHeight), false, false, false);

        }

        public record ProjectionSettings(ProjectionType ProjectionType, FieldOfViewAxis FOVAxis, float FieldOfView, float NearClipPlane, bool PhysicalCamera);
        public record EnvironmentSettings(BackgroundType BackgroundType, ConsoleColor BackgroundColor);

        public record OutputSettings(int TargetDisplay, TargetEye TargetEye, Rect ViewportRect, bool HDRRendering, bool MSAA, bool URPDynamicResolution);

        public record RenderingSettings(RenderingEngine Renderer, bool PostProcessing, bool Glow, bool Dithering, bool RenderShadows, int Priority, bool OpaqueTexture);
        public record StackSettings(List<Camera> Cameras);
        public record VolumeSettings(VolumeUpdateMode UpdateMode, int VolumeMask, object VolumeTrigger);
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
