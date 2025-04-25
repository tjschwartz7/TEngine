using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Drivers.T2D;
using TEngine.EngineManagement.Drivers.T3D;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.EngineManagement;
using TEngine.EngineManagement.Drivers.Text;
using TEngine.EngineManagement.Pipelines;
using TEngine.EngineManagement.Pipelines.Text;

namespace TEngine.EngineManagement.Drivers
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
