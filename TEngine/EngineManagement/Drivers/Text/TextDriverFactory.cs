using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.Drivers.Text
{
    public class TextDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            if (typeof(T) == typeof(string))
                return (IDriver<T>)(object)new TerminalDriver(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for TextDriverFactory");
        }
    }
}
