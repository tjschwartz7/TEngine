
using TEngine.Core.Commands;
using TEngine.Utils.Logging;
using TEngine.Core.AssetManagement.Strategy;
using TEngine.Utils;

namespace TEngine.Core.AssetManagement
{
    public class AssetManager
    {
        IAssetStrategy AssetStrategy;
        ILoadStrategy LoadStrategy;
        public IEnumerable<int> AssetIDs { get; private set; } = new List<int>();
        public AssetManager(GraphicSystem graphicSystem) 
        {
            switch(graphicSystem)
            {
                case GraphicSystem.Text:
                    AssetStrategy = new TextAssetStrategy();

                    break;
                case GraphicSystem.T2D:
                    AssetStrategy = new T2DAssetStrategy();
                    break;
                case GraphicSystem.T3D:
                    AssetStrategy = new T3DAssetStrategy();
                    break;
                default:
                    Logging.Critical($"Graphic system {graphicSystem} is not implemented.");
                    throw new NotImplementedException($"Graphic system {graphicSystem} is not implemented.");
            }
        }


        /// <summary>
        /// Register assets from a given path.
        /// </summary>
        /// <param name="path">The path to the top assets directory.</param>
        /// <param name="includeSubdirs">Choose whether to include subdirectories when registering assets.</param>
        public void RegisterAssets(string path, bool includeSubdirs = false)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                Logging.Error($"Invalid path: {path}");
                return;
            }

            SearchOption option = includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            try
            {

                //TODO: Update this so it looks for specific file types
                foreach (string file in Directory.GetFiles(path, "*.*", option))
                {
                    // Placeholder for asset registration logic
                    Logging.Info($"Registering asset: {file}");
                }
            }
            catch (Exception ex)
            {
                Logging.Error($"Error while registering assets: {ex.Message}");
            }
        }

        public void LoadAssets(IEnumerable<int> assetIDs)
        {
            LoadStrategy.LoadAssets(assetIDs);
        }

        public void UnloadAssets(IEnumerable<int> assetIDs)
        {
            LoadStrategy.UnloadAssets(assetIDs);
        }

    }

    public interface IAssetStrategy
    {
        IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData);
    }

    public interface ILoadStrategy
    {
        void LoadAssets(IEnumerable<int> assetIDs);
        void UnloadAssets(IEnumerable<int> assetIDs);
    }


}
