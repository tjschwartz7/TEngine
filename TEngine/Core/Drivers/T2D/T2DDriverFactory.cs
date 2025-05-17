using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Drivers.T3D;
using TEngine.Utils.Logging;

namespace TEngine.Core.Drivers.T2D
{
    public class T2DDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            Logging.Critical("T2DDriverFactory is not implemented yet.");
            throw new NotImplementedException("T2DDriverFactory is not implemented yet.");
            if (typeof(T) == typeof(string)) /*!TODO: Change drawable type*/
                return (IDriver<T>)(object)new Driver2D(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for T2DDriverFactory");
        }
    }
}
