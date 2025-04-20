using TEngine.TMath;
using TEngine.EngineManagement;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.EngineManagement.Drivers.T2D;
using TEngine.EngineManagement.Drivers.T3D;

namespace TEngine.EngineManagement.Drivers
{
    public abstract class Driver
    {
        public static Driver Default { get; private set; } = new TerminalDriver();

        public Driver()
        {
            switch (Engine.GraphicSystem)
            {
                case GraphicSystem.Text:
                    Default = new TerminalDriver();
                    break;
                case GraphicSystem.T2D:
                    Default = new Driver2D();
                    break;
                case GraphicSystem.T3D:
                    Default = new Driver3D();
                    break;
            }

        }
    }

    public abstract class Driver<T> : Driver
    {
        public abstract void Draw(T drawable, Vector3 position);

    }
}
