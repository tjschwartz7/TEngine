using TEngine.TMath;
using TEngine.EngineManagement;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.EngineManagement.Drivers.T2D;
using TEngine.EngineManagement.Drivers.T3D;

namespace TEngine.EngineManagement.Drivers
{
    public interface IDriver<T>
    {
        void Draw(T drawable, Vector3 position);
    }

    public abstract class DriverBase { }

    public abstract class Driver<T> : DriverBase, IDriver<T>
    {
        public abstract void Draw(T drawable, Vector3 position);

    }


}
