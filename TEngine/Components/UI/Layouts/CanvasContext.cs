using System.Drawing;
using TEngine.EngineManagement;
using TEngine.GameObjects.Cameras;
using TEngine.Assets;
using TEngine.Assets.UI;

namespace TEngine.Components.UI.Layouts
{
    public class CanvasContext
    {
        public List<DrawCommand> AllCommands { get; } = new();

        public void Add(DrawCommand cmd) => AllCommands.Add(cmd);

        public void Clear() => AllCommands.Clear();
    }

}
