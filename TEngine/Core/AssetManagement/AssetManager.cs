
using TEngine.Core.Commands;
using TEngine.Utils.Logging;
using TEngine.Core.AssetManagement.Strategy;
using TEngine.Utils;
using TEngine.Core.AssetManagement.Loading;

namespace TEngine.Core.AssetManagement
{
    public class AssetManager
    {
        IAssetStrategy AssetStrategy;
        ILoadStrategy LoadStrategy;
        public AssetLoader AssetLoader { get; private set; }
        int _currentMaxAssetID = 0; // Used to assign unique IDs to assets
        public Dictionary<int, string> AssetIDs { get; private set; } = new Dictionary<int, string>();
        public Dictionary<int, string> UnloadedAssetIDs { get; private set; } = new Dictionary<int, string>();
        public Dictionary<int, string> LoadedAssetIDs { get; private set; } = new Dictionary<int, string>();

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

            AssetLoader = new AssetLoader(AssetStrategy);
            LoadStrategy = LoadTypes.Caching; // Default load strategy
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
                    AssetIDs.Add(_currentMaxAssetID, file);
                    UnloadedAssetIDs.Add(_currentMaxAssetID, file);

                    _currentMaxAssetID++;
                }
            }
            catch (Exception ex)
            {
                Logging.Error($"Error while registering assets: {ex.Message}");
            }
        }

        public void LoadAssets()
        {
            LoadStrategy.LoadAssets(UnloadedAssetIDs, LoadedAssetIDs);
        }

        public void UnloadAssets()
        {
            LoadStrategy.UnloadAssets(UnloadedAssetIDs, LoadedAssetIDs);
        }

    }

    public interface IAssetStrategy
    {
        IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData);
    }

    public interface ILoadStrategy
    {
        void LoadAssets(Dictionary<int, string> unloadedAssets, Dictionary<int, string> loadedAssets);
        void UnloadAssets(Dictionary<int, string> unloadedAssets, Dictionary<int, string> loadedAssets);
    }

    public enum LoadTypes
    {
        Preloading,
        LazyLoad,
        Caching,

    }

}
