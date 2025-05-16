using static TEngine.Utils.Logging.Logging;

namespace TEngine.Utils
{
    public record Layer(int index, string name, bool isVisible=true);
    public class Layers
    {
        private static List<string> AllLayers { get; } = new List<string>
        {
            "Default",
            "UI",
            "Background",
            "Foreground",
            "Overlay"
        };

        public static void AddLayer(string layerName)
        {
            if (!AllLayers.Contains(layerName))
            {
                AllLayers.Add(layerName);
            }
            else
            {
                Error($"Layer '{layerName}' already exists.");
            }
        }

        public static Layer GetLayer(int index) { return new Layer(index, AllLayers[index]); }
        public static Layer GetLayer(string name)
        {
            int index = AllLayers.IndexOf(name);
            if (index == -1)
            {
                Error($"Layer '{name}' not found.");
                return new Layer(0, AllLayers[index]);
            }
            return new Layer(index, name);
        }
    }


}
