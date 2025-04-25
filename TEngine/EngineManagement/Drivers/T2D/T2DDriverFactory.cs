using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers.T3D;

namespace TEngine.EngineManagement.Drivers.T2D
{
    public class T2DDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            if (typeof(T) == typeof(string)) /*!TODO: Change drawable type*/
                return (IDriver<T>)(object)new Driver2D(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for T2DDriverFactory");
        }
    }
}
