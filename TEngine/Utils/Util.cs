using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Pipelines;

namespace TEngine.Utils
{

    public record Layer(int number, string name);
    public record Tag(string name);
    public record EngineConfig(GraphicSystem system);
    public enum GraphicSystem
    {
        Text,
        T2D,
        T3D
    }
}
