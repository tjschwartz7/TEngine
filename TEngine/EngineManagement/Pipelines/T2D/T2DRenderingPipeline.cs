using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
namespace TEngine.EngineManagement.Pipelines.T2D
{
    public class T2DRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<string> _driver;
        public T2DRenderingPipeline(IDriverFactory factory)
        : base(factory)
        {
            _driver = DriverFactory.Create<string>();
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
