using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Core.Commands;
using TEngine.Utils.Logging;

namespace TEngine.Core.AssetManagement.Strategy
{
    public class T3DAssetStrategy : IAssetStrategy
    {
        public T3DAssetStrategy() { }
        public IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData)
        {
            Logging.Critical("T3DAssetStrategy is not implemented yet.");
            throw new NotImplementedException("T3DAssetStrategy is not implemented yet.");
        }
    }
}
