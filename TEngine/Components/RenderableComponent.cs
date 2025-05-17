using TEngine.Components;
using TEngine.Components.Rendering;
using TEngine.Core.Commands;

namespace TEngine.Components
{
    public interface IRenderable
    {
        List<DrawCommand> GetDrawCommands();
    }

    public abstract class RenderableComponent : Component, IRenderable
    {
        public abstract List<DrawCommand> GetDrawCommands();

        public void UpdateDrawCommand()
        {
            // Notify the Renderer that it needs to update its draw commands
            if(HasComponent<Renderer>())
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.CollectDrawCommands();
            }
        }
    }
}
