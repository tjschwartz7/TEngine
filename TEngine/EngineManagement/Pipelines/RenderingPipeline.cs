
using TEngine.EngineManagement.Commands;
using TEngine.EngineManagement.Drivers;
using TEngine.Services;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;

namespace TEngine.EngineManagement.Pipelines
{
    public interface IRenderingPipeline
    {
        public void Render(IEnumerable<DrawCommand> commands, Camera camera);

        protected IEnumerable<DrawCommand> CullAndSort(IEnumerable<DrawCommand> commands, Camera camera);
        protected IEnumerable<DrawCommand> Preprocess(IEnumerable<DrawCommand> commands, Camera camera);
        protected void Draw(IEnumerable<DrawCommand> commands, Camera camera);
    }

    public abstract class RenderingPipeline : IRenderingPipeline
    {
        public abstract void Render(IEnumerable<DrawCommand> commands, Camera camera);
        public abstract IEnumerable<DrawCommand> CullAndSort(IEnumerable<DrawCommand> commands, Camera camera);
        public abstract IEnumerable<DrawCommand> Preprocess(IEnumerable<DrawCommand> commands, Camera camera);
        public abstract void Draw(IEnumerable<DrawCommand> commands, Camera camera);
        public IDriver<DrawCommand> Driver { get; } = DriverService.Get<DrawCommand>() ?? throw new InvalidOperationException("Driver for DrawCommand could not be retrieved.");
        protected RenderingPipeline()
        {

        }
    }




}
