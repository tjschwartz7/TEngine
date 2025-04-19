using System;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.GameObjects.Cameras;

namespace TEngine.EngineManagement.RenderingEngines
{
    public abstract class RenderSystem
    {
        //Until there's multiple renderers, this is the default
        Camera _camera;
        public static RenderSystem Default { get; } = new TextRenderSystem();
        public static RenderSystem Text { get; } = new TextRenderSystem();
        public abstract void Render(List<GameObject> renderableGameObjects);
        public virtual void SetCamera(Camera camera) { _camera = camera; }
    }
}
