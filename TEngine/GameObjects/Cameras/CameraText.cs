using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Utils;
using TEngine.Rendering;

namespace TEngine.GameObjects.Cameras
{
    public class CameraText : Camera
    {
        public CameraText()
        {
            ObjectTag = new Tag("CameraText");
            Projection = new ProjectionSettings(ProjectionType.Orthographic, FieldOfViewAxis.Vertical, 60f, 0.1f, false);
            Rendering = new RenderingSettings(RenderingEngine.Text, true, false, false, true, 0, false);
            Stack = new StackSettings(new List<Camera>());
            Environment = new EnvironmentSettings(BackgroundType.SolidColor, ConsoleColor.Black);
            Volumes = new VolumeSettings(VolumeUpdateMode.EveryFrame, 0, null);
            Output = new OutputSettings(0, TargetEye.Both, new Rect(0, 0, Console.WindowWidth, Console.WindowHeight), false, false, false);
        }
    }
}
