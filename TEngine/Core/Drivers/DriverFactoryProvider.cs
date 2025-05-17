using TEngine.Utils;
using TEngine.Core.Drivers.T2D;
using TEngine.Core.Drivers.T3D;
using TEngine.Core.RenderingEngines.Text;
using TEngine.Core;
using TEngine.Core.Drivers.Text;

namespace TEngine.Core.Drivers
{
    public static class DriverFactoryProvider
    {
        private static IDriverFactory _instance;

        public static IDriverFactory Get()
        {
            if (_instance != null) return _instance;

            _instance = Engine.GraphicSystem switch
            {
                GraphicSystem.Text => new TextDriverFactory(),
                GraphicSystem.T2D => new T2DDriverFactory(),
                GraphicSystem.T3D => new T3DDriverFactory(),
                _ => throw new ArgumentOutOfRangeException()
            };

            return _instance;
        }
    }

    public interface IDriverFactory
    {
        IDriver<T> Create<T>();
    }

}
