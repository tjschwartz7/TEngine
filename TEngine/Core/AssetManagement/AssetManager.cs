
using TEngine.EngineManagement.Commands;
using TEngine.Utils.Logging;

namespace TEngine.Core.AssetManagement
{
    public class AssetManager
    {
        IAssetStrategy AssetStrategy;
        public IEnumerable<int> AssetIDs { get; private set; } = new List<int>();
        public AssetManager(IAssetStrategy strategy) { AssetStrategy = strategy; }


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

        }

        public void UnloadAssets(IEnumerable<int> assetIDs)
        {

        }

    }

    public interface IAssetStrategy
    {
        IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData);
    }

    public interface ILoadStrategy
    {
        void LoadAssets(IEnumerable<int> assetIDs);
    }
}
