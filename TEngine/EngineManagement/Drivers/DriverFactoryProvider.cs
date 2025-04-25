using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers.T2D;
using TEngine.EngineManagement.Drivers.T3D;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.EngineManagement;
using TEngine.EngineManagement.Drivers.Text;

namespace TEngine.EngineManagement.Drivers
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
