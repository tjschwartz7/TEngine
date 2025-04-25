using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.Pipelines.T2D;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.Pipelines.Text
{
    public class T2DRenderingPipelineFactory : IRenderingPipelineFactory
    {
        public IRenderingPipeline Create()
        {
            return (IRenderingPipeline)(object)new T2DRenderingPipeline(); // cast safely
        }
    }
}
