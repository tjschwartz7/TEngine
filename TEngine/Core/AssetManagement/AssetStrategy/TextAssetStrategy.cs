using TEngine.Core.AssetManagement;
using TEngine.TMath;
using TEngine.Core.Commands;
using TEngine.Utils.Logging;

/*
 * TextAssetStrategy.cs
 * Trenton Schwartz
 * 
 * This class is responsible for converting Text assets into draw commands.
 * 
 */


namespace TEngine.Core.AssetManagement.AssetStrategy
{
    public class TextAssetStrategy : IAssetStrategy
    {
        public IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<string> textData)
        {
            foreach (var line in textData)
            {
                // Create a DrawCommand to render the text
                var command = new DrawCommand
                {
                    TextValue = line,
                    Type = DrawCommandType.Text,
                    Position = new Vector3(0, 0, 0), // Default position
                };

                yield return command;
            }
        }
    }
}
