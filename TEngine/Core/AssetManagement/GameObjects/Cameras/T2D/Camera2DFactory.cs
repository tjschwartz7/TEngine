

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.T2D
{
    public class Camera2DFactory : ICameraFactory
    {
        public ICamera Create(int id, string name, string tag)
        {
            return (ICamera)(object)new Camera2D(id, name, tag); // cast safely
        }
    }
}
