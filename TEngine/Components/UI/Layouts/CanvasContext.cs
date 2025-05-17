using System.Drawing;
using TEngine.Core.AssetManagement.GameObjects.Cameras;
using TEngine.Core.Commands;
using TEngine.Components.UI.Elements;

namespace TEngine.Components.UI.Layouts
{
    public class CanvasContext
    {

        public List<DrawCommand> AllCommands { get; } = new();
        public List<UIElement> AllElements { get; } = new();

        public void Register(UIElement element) => AllElements.Add(element);
        public void Unregister(UIElement element) => AllElements.Remove(element);
        public void Add(DrawCommand cmd) => AllCommands.Add(cmd);

        public void Clear() => AllCommands.Clear();

        public List<DrawCommand> GetCommands()
        {
            return AllCommands;
        }
    }
}
