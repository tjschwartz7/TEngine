using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
using TEngine.Services;

namespace TEngine.EngineManagement.Pipelines.T3D
{
    public class T3DRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<string> _driver;
        public T3DRenderingPipeline()
        {
            _driver = DriverService.Get<string>();
        }
        public override void Render(Scene scene)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override List<GameObject> CullAndSort(Scene scene, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override void Preprocess(List<GameObject> gameObjects, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override void Draw(List<GameObject> gameObjects, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
    }
}
