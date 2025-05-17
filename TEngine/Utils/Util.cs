using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Pipelines;

namespace TEngine.Utils
{
    public record EngineConfig(GraphicSystem system, Resolution resolution);
    public enum GraphicSystem
    {
        Text,
        T2D,
        T3D
    }

    public enum Resolution
    {
        R80x25,
        R100x30,
        R120x40,
        R160x50
    }

}
