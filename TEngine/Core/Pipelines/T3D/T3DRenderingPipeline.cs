using TEngine.Core.Services;
using TEngine.Core.AssetManagement.GameObjects.Cameras;
using TEngine.Core.Commands;
using TEngine.Core.Drivers;
using TEngine.Core.Scenes;

namespace TEngine.Core.Pipelines.T3D
{
    public class T3DRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<DrawCommand> _driver;
        public T3DRenderingPipeline()
        {
            _driver = DriverService.Get<DrawCommand>();
        }
        public override void Render(IEnumerable<DrawCommand> commands, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override IEnumerable<DrawCommand> CullAndSort(IEnumerable<DrawCommand> commands, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override IEnumerable<DrawCommand> Preprocess(IEnumerable<DrawCommand> commands, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
        public override void Draw(IEnumerable<DrawCommand> commands, Camera camera)
        {
            throw new NotImplementedException("Render method not implemented for T2DRenderingPipeline");
        }
    }
}
