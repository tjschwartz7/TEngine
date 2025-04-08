using System;
using TEngine.Rendering.Text;

namespace TEngine.Rendering
{
    public abstract class RenderingEngine
    {
        //Until there's multiple renderers, this is the default
        public static RenderingEngine Default { get; } = new TextRenderingEngine();
        public static RenderingEngine Text { get; } = new TextRenderingEngine();
        public abstract void Render();
    }
}
