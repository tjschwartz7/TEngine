using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;

namespace TEngine.Core.Services
{
    public class CameraService
    {
        private static ICameraFactory? _factory;

        /// <summary>
        /// Initializes the driver service by selecting the appropriate driver factory.
        /// Should be called during engine setup.
        /// </summary>
        public static void Initialize()
        {
            _factory = CameraFactoryProvider.Get();
        }

        /// <summary>
        /// Clears the current factory (optional, useful if GraphicSystem can change).
        /// </summary>
        public static void Reset()
        {
            _factory = null;
        }

        /// <summary>
        /// Returns a driver for the specified drawable type.
        /// </summary>
        public static ICamera Get(int id, string name, string tag)
        {
            if (_factory == null)
                throw new InvalidOperationException("DriverService is not initialized. Call Initialize() first.");

            return _factory.Create(id, name, tag);
        }
    }
}
