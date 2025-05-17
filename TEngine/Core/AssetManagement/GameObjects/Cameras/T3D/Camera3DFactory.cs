
namespace TEngine.Core.AssetManagement.GameObjects.Cameras.T3D
{
    public class Camera3DFactory : ICameraFactory
    {
        public ICamera Create(int id, string name, string tag)
        {
            return (ICamera)(object)new Camera3D(id, name, tag); // cast safely
        }
    }
}
