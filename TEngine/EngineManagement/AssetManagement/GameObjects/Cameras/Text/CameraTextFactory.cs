using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text
{
    public class CameraTextFactory : ICameraFactory
    {
        public ICamera Create(int id, string name, string tag)
        {
            return (ICamera)(object)new CameraText(id, name, tag); // cast safely
        }
    }
}
