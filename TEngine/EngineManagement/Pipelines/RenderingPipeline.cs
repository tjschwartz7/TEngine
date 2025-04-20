
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Transforms;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement;
using TEngine.EngineManagement.Pipelines.Text;
using TEngine.EngineManagement.Pipelines.T2D;
using TEngine.EngineManagement.Pipelines.T3D;


namespace TEngine.EngineManagement.Pipelines
{

    public abstract class RenderingPipeline
    {
        public Driver Driver { get; set; } = null;
        public static RenderingPipeline Default { get; private set; } = null;
        public RenderingPipeline()
        {
            switch (Engine.GraphicSystem)
            {
                case GraphicSystem.Text:
                    Default = new TextRenderingPipeline();
                    break;
                case GraphicSystem.T2D:
                    Default = new T2DRenderingPipeline();
                    break;
                case GraphicSystem.T3D:
                    Default = new T3DRenderingPipeline();
                    break;
                default:
                    throw new NotImplementedException("Graphic system not implemented");
            }

        }
        public abstract void Render(Scene scene);

        protected abstract List<GameObject> CullAndSort(Scene scene, Camera camera);
        protected abstract void Preprocess(List<GameObject> gameObjects, Camera camera);
        protected abstract void Draw(List<GameObject> gameObjects, Camera camera);
    }



}
