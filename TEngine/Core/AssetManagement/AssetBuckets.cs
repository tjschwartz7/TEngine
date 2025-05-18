/*
 * 
 * AssetBuckets.cs
 * Trenton Schwartz
 * 
 * This file contains the AssetBuckets class, which is used to manage the loading and unloading of assets in TEngine.
 * It is intended to have the sole purpose of managing the loading and unloading of assets. 
 * AssetBuckets maintains the state of assets, including their IDs and paths, and provides methods to add, load, and unload assets.
 * It also maintains the strategies used to load them, because it is responsible for managing the buckets of assets.
 */

using System.Linq;
using TEngine.Utils.Logging;

namespace TEngine.Core.AssetManagement
{
    public class AssetBuckets
    {
        private int _currentMaxAssetID = 0; // Used to assign unique IDs to assets
        public Dictionary<int, string> AssetIDs { get; private set; } = new Dictionary<int, string>();
        public Dictionary<int, string> UnloadedAssetIDs { get; private set; } = new Dictionary<int, string>();
        public Dictionary<int, string> LoadedAssetIDs { get; private set; } = new Dictionary<int, string>();

        public IAssetStrategy AssetStrategy { get; private set; }
        public ILoadStrategy LoadStrategy { get; private set; }

        public AssetBuckets(IAssetStrategy assetStrategy, ILoadStrategy loadStrategy) 
        {
            AssetStrategy = assetStrategy;
            LoadStrategy = loadStrategy;
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

        public void Clear()
        {
            AssetIDs.Clear();
            UnloadedAssetIDs.Clear();
            LoadedAssetIDs.Clear();
        }

        public void AddAsset(int id, string assetPath)
        {
            //The structure of this is kind of ugly but it works in all cases at least

            AssetIDs[id] = assetPath;

            //All assets are unloaded until they are loaded
            UnloadAsset(id);
        }

        public void LoadAsset(int id)
        {
            if (AssetIDs.ContainsKey(id))
            {
                UnloadedAssetIDs.Remove(id);

                LoadedAssetIDs[id] = AssetIDs[id];
                // Load the asset using the strategy
                LoadStrategy.LoadAssets(this);
            }
            else
            {
                Logging.Error($"Asset ID {id} not found in AssetIDs.");
            }
        }

        public void UnloadAsset(int id)
        {
            if(AssetIDs.ContainsKey(id))
            {
                LoadedAssetIDs.Remove(id);   
                
                UnloadedAssetIDs[id] = AssetIDs[id];

                LoadStrategy.UnloadAssets(this);
            }
            else
            {
                Logging.Error($"Asset ID {id} not found in AssetIDs.");
            }

        }
    }
}
