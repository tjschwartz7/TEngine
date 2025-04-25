using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.Services;

namespace TEngine.EngineManagement.Pipelines.Text
{
    public class TextRenderingPipelineFactory : IRenderingPipelineFactory
    {
        public IRenderingPipeline Create()
        {
            return (IRenderingPipeline)(object)new TextRenderingPipeline(DriverService.Get<string>()); // cast safely
        }
    }
}
