using System.Configuration;
using TEngine.Services;
using TEngine.Utils;

namespace TEngine.Utils.Configurations.NETConfig
{
    public class NETConfigLoader
    {
        public static EngineConfig Load(string path)
        {
            var graphicSystem = ConfigurationManager.AppSettings["Rendering:GraphicSystem"];

            if (graphicSystem == null)
                throw new ConfigurationErrorsException("Missing Rendering:GraphicSystem in app.config");

            GraphicSystem system = GraphicSystem.Text;
            switch (graphicSystem)
            {
                case "Text":
                    system = GraphicSystem.Text;
                    break;
                case "T2D":
                    system = GraphicSystem.T2D;
                    break;
                case "T3D":
                    system = GraphicSystem.T3D;
                    break;
            }


            return new EngineConfig(system);
        }
    }
}
