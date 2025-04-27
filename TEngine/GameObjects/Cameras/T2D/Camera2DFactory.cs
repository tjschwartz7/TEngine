using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GameObjects.Cameras.T3D;

namespace TEngine.GameObjects.Cameras.T2D
{
    public class Camera2DFactory : ICameraFactory
    {
        public ICamera Create()
        {
            return (ICamera)(object)new Camera2D(); // cast safely
        }
    }
}
