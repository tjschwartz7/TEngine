using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.GameObjects.Cameras.Text
{
    public class CameraTextFactory : ICameraFactory
    {
        public ICamera Create()
        {
            return (ICamera)(object)new CameraText(); // cast safely
        }
    }
}
