
using TEngine.Utils;
using TEngine.Core;
using TEngine.Core.RenderingEngines;
using TEngine.Core.AssetManagement.GameObjects.Cameras;

namespace TEngine.Core.AssetManagement.GameObjects.Cameras.Text
{
    public class CameraText : Camera
    {
        public CameraText(int id, string name, string tag="Camera") : base(id, name, tag)
        {
            Projection = new ProjectionSettings(ProjectionType.Orthographic, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(GraphicSystem.Text, true, false, false, false, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, ViewSize.X, ViewSize.Y), false, false, false);
        }
    }
}
