using TEngine.Utils.Logging;

namespace TEngine.Core.Drivers.T3D
{
    public class T3DDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            Logging.Critical("T3DDriverFactory is not implemented yet.");
            throw new NotImplementedException("T3DDriverFactory is not implemented yet.");
            if (typeof(T) == typeof(string)) /*!TODO: Change drawable type*/
                return (IDriver<T>)(object)new Driver3D(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for T3DDriverFactory");
        }
    }
}
