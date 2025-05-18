using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Core.AssetManagement.LoadStrategy
{
    public class CachingStrategy : ILoadStrategy
    {
        public void LoadAssets(AssetBuckets assetBuckets)
        {
            throw new NotImplementedException("Caching strategy is not implemented yet.");
        }
        public void UnloadAssets(AssetBuckets assetBuckets)
        {
            throw new NotImplementedException("Caching strategy is not implemented yet.");
        }
    }
}
