using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.Services;

namespace TEngine.EngineManagement.Pipelines.Text
{
    public class TextRenderingPipelineFactory : IRenderingPipelineFactory
    {
        public IRenderingPipeline Create()
        {
            return (IRenderingPipeline)(object)new TextRenderingPipeline(); // cast safely
        }
    }
}
