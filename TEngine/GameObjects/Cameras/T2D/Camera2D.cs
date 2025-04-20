using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.RenderingEngines;
using TEngine.GameObjects.Cameras;
using TEngine.EngineManagement;
using TEngine.Utils;

namespace TEngine.GameObjects.Cameras.T2D
{
    public class Camera2D : Camera
    {
        public Camera2D()
        {
            ObjectTag = new Tag("Camera2D");
            Projection = new ProjectionSettings(ProjectionType.Orthographic, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(GraphicSystem.T2D, true, false, false, true, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, ViewSize.X, ViewSize.Y), false, false, false);
        }
    }
}
