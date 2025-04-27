using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GameObjects.Cameras.Text;

namespace TEngine.GameObjects.Cameras.T3D
{
    public class Camera3DFactory : ICameraFactory
    {
        public ICamera Create()
        {
            return (ICamera)(object)new Camera3D(); // cast safely
        }
    }
}
