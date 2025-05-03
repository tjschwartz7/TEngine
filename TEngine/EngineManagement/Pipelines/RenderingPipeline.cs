
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Transforms;
using TEngine.EngineManagement.Drivers;
using TEngine.Services;

namespace TEngine.EngineManagement.Pipelines
{
    public interface IRenderingPipeline
    {
        public void Render(Scene scene);

        protected List<GameObject> CullAndSort(Scene scene, Camera camera);
        protected void Preprocess(List<GameObject> gameObjects, Camera camera);
        protected void Draw(List<GameObject> gameObjects, Camera camera);
    }

    public abstract class RenderingPipeline : IRenderingPipeline
    {
        public abstract void Render(Scene scene);
        public abstract void Preprocess(List<GameObject> gameObjects, Camera camera);
        public abstract void Draw(List<GameObject> gameObjects, Camera camera);
        public abstract List<GameObject> CullAndSort(Scene scene, Camera camera);
        public IDriver<DrawCommand> Driver { get; } = DriverService.Get<DrawCommand>();
        protected RenderingPipeline()
        {

        }
    }




}
