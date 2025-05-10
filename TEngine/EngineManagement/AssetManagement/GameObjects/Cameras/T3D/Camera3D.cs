using TEngine.EngineManagement.RenderingEngines;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T2D;
using TEngine.TMath;
using TEngine.Utils;
using TEngine.EngineManagement;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T3D
{
    public class Camera3D : Camera
    {
        public Camera3D(int id, string name, string tag) : base(id, name, tag)
        {
            Projection = new ProjectionSettings(ProjectionType.Perspective, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(GraphicSystem.T3D, true, false, false, true, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, ViewSize.X, ViewSize.Y), false, false, false);
        }
    }
}
