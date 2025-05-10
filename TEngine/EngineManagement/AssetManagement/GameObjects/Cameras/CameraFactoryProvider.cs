
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement;
using TEngine.Utils;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T2D;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T3D;

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Cameras
{
    public class CameraFactoryProvider
    {
        private static ICameraFactory _instance;

        public static ICameraFactory Get()
        {
            if (_instance != null) return _instance;

            _instance = Engine.GraphicSystem switch
            {
                GraphicSystem.Text => new CameraTextFactory(),
                GraphicSystem.T2D => new Camera2DFactory(),
                GraphicSystem.T3D => new Camera3DFactory(),
                _ => throw new ArgumentOutOfRangeException()
            };

            return _instance;
        }
    }

    public interface ICameraFactory
    {
        ICamera Create(int id, string name, string tag);
    }
}

