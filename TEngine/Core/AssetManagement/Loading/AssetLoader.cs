using TEngine.Core.Commands;
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
