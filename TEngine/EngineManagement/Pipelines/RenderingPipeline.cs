
using TEngine.EngineManagement.Scenes;
using TEngine.EngineManagement.Commands;
using TEngine.EngineManagement.Drivers;
using TEngine.Services;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;

namespace TEngine.EngineManagement.Pipelines
{
    public interface IRenderingPipeline
    {
        public void Render(List<DrawCommand> scene);

        protected List<DrawCommand> CullAndSort(List<DrawCommand> scene, Camera camera);
        protected void Preprocess(List<DrawCommand> commands, Camera camera);
        protected void Draw(List<DrawCommand> commands, Camera camera);
    }

    public abstract class RenderingPipeline : IRenderingPipeline
    {
        public abstract void Render(List<DrawCommand> scene);
        public abstract void Preprocess(List<DrawCommand> drawCommands, Camera camera);
        public abstract void Draw(List<DrawCommand> drawCommands, Camera camera);
        public abstract List<DrawCommand> CullAndSort(List<DrawCommand> scene, Camera camera);
        public IDriver<DrawCommand> Driver { get; } = DriverService.Get<DrawCommand>();
        protected RenderingPipeline()
        {

        }
    }




}
