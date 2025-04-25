using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.Drivers.T3D
{
    public class T3DDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            if (typeof(T) == typeof(string)) /*!TODO: Change drawable type*/
                return (IDriver<T>)(object)new Driver3D(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for T3DDriverFactory");
        }
    }
}
