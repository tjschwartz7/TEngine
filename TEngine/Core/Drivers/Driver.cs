using TEngine.EngineManagement.Commands;

namespace TEngine.EngineManagement.Drivers
{
    public interface IDriver<T>
    {
        void Draw(T drawable);
    }

    public abstract class DriverBase { }

    public abstract class Driver : DriverBase, IDriver<DrawCommand>
    {
        public abstract void Draw(DrawCommand drawable);
    }
}
