using System;
using TEngine.Core.Drivers;

namespace TEngine.Core.Services
{
    public static class DriverService
    {
        private static IDriverFactory? _factory;

        /// <summary>
        /// Initializes the driver service by selecting the appropriate driver factory.
        /// Should be called during engine setup.
        /// </summary>
        public static void Initialize()
        {
            _factory = DriverFactoryProvider.Get();
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
        public static IDriver<T> Get<T>()
        {
            if (_factory == null)
                throw new InvalidOperationException("DriverService is not initialized. Call Initialize() first.");

            return _factory.Create<T>();
        }
    }
}

