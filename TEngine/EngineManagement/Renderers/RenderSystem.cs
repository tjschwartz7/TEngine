using System;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.RenderingEngines
{
    public abstract class RenderSystem
    {
        //Until there's multiple renderers, this is the default
        public static RenderSystem Default { get; } = new TextRenderSystem();
        public static RenderSystem Text { get; } = new TextRenderSystem();
        public abstract void Render(List<GameObject> renderableGameObjects);
    }
}
