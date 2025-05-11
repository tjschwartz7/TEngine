using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Animations;
using static System.Net.Mime.MediaTypeNames;
using TEngine.EngineManagement.Commands;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;
using TEngine.Components.UI.Layouts;

namespace TEngine.Components.Rendering
{
    public class Renderer : Component, IRenderer
    {
        private List<DrawCommand> _commandBuffer = new List<DrawCommand>();
        public IReadOnlyList<DrawCommand> CommandBuffer => _commandBuffer;

        public void CollectDrawCommands()
        {
            _commandBuffer.Clear();

            foreach (var renderable in GetComponents<RenderableComponent>())
            {
                _commandBuffer.AddRange(renderable.GetDrawCommands());
            }

            // Sort the draw commands based on sorting order
            _commandBuffer.Sort((a, b) => a.SortingOrder.CompareTo(b.SortingOrder));
        }

        public IEnumerable<DrawCommand> GetDrawCommands()
        {
            return _commandBuffer;
        }
    }
}
