using TEngine.Core.Drivers;
using TEngine.Core.RenderingEngines.Text;
using TEngine.Core.Services;

namespace TEngine.Core.Pipelines.Text
{
    public class TextRenderingPipelineFactory : IRenderingPipelineFactory
    {
        public IRenderingPipeline Create()
        {
            return (IRenderingPipeline)(object)new TextRenderingPipeline(); // cast safely
        }
    }
}
