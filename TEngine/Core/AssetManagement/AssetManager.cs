
using TEngine.Core.Commands;
using TEngine.Utils.Logging;
using TEngine.Core.AssetManagement.LoadStrategy;
using TEngine.Core.AssetManagement.AssetStrategy;
using TEngine.Utils;
using TEngine.Core.AssetManagement.Loading;

/*
 * AssetManager.cs
 * Trenton Schwartz
 * 
 * The AssetManager has the responsibility of, well,
 * managing assets. This mostly includes managing the AssetBuckets class,
 * who is actually in charge of loading and unloading assets, and keeping track of the different 
 * asset buckets.
 * 
 */

namespace TEngine.Core.AssetManagement
{
    public class AssetManager
    {
        public AssetBuckets AssetBuckets { get; private set; } 

        public AssetManager(GraphicSystem graphicSystem, LoadTypes LoadType) 
        {
            IAssetStrategy assetStrategy;
            ILoadStrategy loadStrategy;
            switch (graphicSystem)
            {
                case GraphicSystem.Text:
                    assetStrategy = new TextAssetStrategy();
                    break;
                case GraphicSystem.T2D:
                    assetStrategy = new T2DAssetStrategy();
                    break;
                case GraphicSystem.T3D:
                    assetStrategy = new T3DAssetStrategy();
                    break;
                default:
                    Logging.Critical($"Graphic system {graphicSystem} is not implemented.");
                    throw new NotImplementedException($"Graphic system {graphicSystem} is not implemented.");
            }

            switch (LoadType)
            {
                case LoadTypes.Preloading:
                    loadStrategy = new PreloadStrategy();
                    break;
                case LoadTypes.LazyLoad:
                    loadStrategy = new LazyLoadStrategy();
                    break;
                case LoadTypes.Caching:
                    loadStrategy = new CachingStrategy();
                    break;
                default:
                    Logging.Critical($"Load type {LoadType} is not implemented.");
                    throw new NotImplementedException($"Load type {LoadType} is not implemented.");
            }

            AssetBuckets = new AssetBuckets(assetStrategy, loadStrategy);
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
        void LoadAssets(AssetBuckets assetBuckets);
        void UnloadAssets(AssetBuckets assetBuckets);
    }

    public enum LoadTypes
    {
        Preloading,
        LazyLoad,
        Caching,

    }

}
