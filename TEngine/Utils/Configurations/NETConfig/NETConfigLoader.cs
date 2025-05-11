using System.Configuration;
using TEngine.Core.Services;
using TEngine.Utils;

namespace TEngine.Utils.Configurations.NETConfig
{
    public class NETConfigLoader
    {
        public static EngineConfig Load(string path)
        {
            var graphicSystem = ConfigurationManager.AppSettings["Rendering:GraphicSystem"];
            var resolution = ConfigurationManager.AppSettings["Rendering:Resolution"];

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

            Resolution res = Resolution.R80x25;
            switch (resolution)
            {
                case "R80x25":
                    res = Resolution.R80x25;
                    break;
                case "R100x30":
                    res = Resolution.R100x30;
                    break;
                case "R120x40":
                    res = Resolution.R120x40;
                    break;
                case "R160x50":
                    res = Resolution.R160x50;
                    break;
                default:
                    throw new ConfigurationErrorsException("Invalid Rendering:Resolution in app.config: " + resolution);
            }



            return new EngineConfig(system, res);
        }
    }
}
