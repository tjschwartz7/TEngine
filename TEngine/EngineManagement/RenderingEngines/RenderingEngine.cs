using System;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.RenderingEngines
{
    public abstract class RenderingEngine
    {
        //Until there's multiple renderers, this is the default
        public static RenderingEngine Default { get; } = new TextRenderingEngine();
        public static RenderingEngine Text { get; } = new TextRenderingEngine();
        public abstract void Render(List<GameObject> renderableGameObjects);
    }
}
