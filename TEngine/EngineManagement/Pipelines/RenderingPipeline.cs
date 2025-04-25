
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

    public abstract class RenderingPipeline
    {
        protected IDriverFactory DriverFactory { get; }

        protected RenderingPipeline(IDriverFactory driverFactory)
        {
            DriverFactory = driverFactory;
        }
    }




}
