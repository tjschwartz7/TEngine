using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Commands;
using TEngine.Utils.Logging;

namespace TEngine.Core.AssetManagement.AssetStrategy
{
    public class T2DAssetStrategy : IAssetStrategy
    {
        public T2DAssetStrategy() { }
        public IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData)
        {
            Logging.Critical("T2DAssetStrategy is not implemented yet.");
            throw new NotImplementedException("T2DAssetStrategy is not implemented yet.");
        }
    }
}
