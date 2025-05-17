using TEngine.Utils;
using TEngine.Core.Drivers.T2D;
using TEngine.Core.Drivers.T3D;
using TEngine.Core.RenderingEngines.Text;
using TEngine.Core;
using TEngine.Core.Drivers.Text;
using TEngine.Core.Pipelines;
using TEngine.Core.Pipelines.Text;

namespace TEngine.Core.Drivers
{
    public static class RenderingPipelineFactoryProvider
    {
        private static IRenderingPipelineFactory _instance;

        public static IRenderingPipelineFactory Get()
        {
            if (_instance != null) return _instance;

            _instance = Engine.GraphicSystem switch
            {
                GraphicSystem.Text => new TextRenderingPipelineFactory(),
                GraphicSystem.T2D => new T2DRenderingPipelineFactory(),
                GraphicSystem.T3D => new T3DRenderingPipelineFactory(),
                _ => throw new ArgumentOutOfRangeException()
            };

            return _instance;
        }
    }

    public interface IRenderingPipelineFactory
    {
        IRenderingPipeline Create();
    }

}
