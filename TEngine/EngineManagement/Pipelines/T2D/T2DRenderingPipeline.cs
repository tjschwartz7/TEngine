using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
namespace TEngine.EngineManagement.Pipelines.T2D
{
    public class T2DRenderingPipeline : RenderingPipeline
    {
        public void Render(Scene scene)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        protected List<GameObject> CullAndSort(Scene scene, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        protected void Preprocess(List<GameObject> gameObjects, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        protected void Draw(List<GameObject> gameObjects, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
    }
}
