using TEngine.Core.Commands;

using TEngine.Utils.Logging;

/*
 * AssetLoader.cs
 * Trenton Schwartz
 * 
 * The Asset Loader class is responsible for converting the raw data from the asset files into
 * a set of draw commands. It uses the AssetStrategy to do this, which is a strategy pattern
 * that knows how to parse the raw data into draw commands.
 */

namespace TEngine.Core.AssetManagement.Loading
{
    public class AssetLoader
    {
        public IAssetStrategy AssetStrategy { get; private set; } = null!;
        public AssetLoader(IAssetStrategy assetStrategy) { AssetStrategy = assetStrategy; }

        public IEnumerable<DrawCommand> LoadAsset(string path)
        {
            IEnumerable<string> lines = AssetParser.LoadRawData(path);
            IEnumerable<DrawCommand> cmds = AssetStrategy.GetDrawCommands(lines);
            return cmds;
        }
    }
}
