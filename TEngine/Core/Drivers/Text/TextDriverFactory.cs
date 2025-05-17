using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Commands;
using TEngine.Core.RenderingEngines.Text;

namespace TEngine.Core.Drivers.Text
{
    public class TextDriverFactory : IDriverFactory
    {
        public IDriver<T> Create<T>()
        {
            if (typeof(T) == typeof(DrawCommand))
                return (IDriver<T>)(object)new TerminalDriver(); // cast safely
            throw new NotSupportedException($"Unsupported drawable type {typeof(T)} for TextDriverFactory");
        }
    }
}
