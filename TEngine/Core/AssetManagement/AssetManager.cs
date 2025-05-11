
using TEngine.EngineManagement.Commands;

namespace TEngine.Core.AssetManagement
{
    public class AssetManager
    {

    }

    public interface IAssetStrategy<T>
    {
        IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> assetData);
    }
}
