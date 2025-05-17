using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Commands;

namespace TEngine.Components.Rendering
{
    public interface IRenderer
    {
        public IEnumerable<DrawCommand> GetDrawCommands();
        public void CollectDrawCommands();
    }
}
