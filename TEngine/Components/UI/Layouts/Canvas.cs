using TEngine.Core.Commands;
using TEngine.Components.UI.Elements;

namespace TEngine.Components.UI.Layouts
{
    public class Canvas : RenderableComponent, IUIComponent
    {
        public CanvasContext Context { get; private set; } = new CanvasContext();

        public Canvas()
        {
            Owner?.AddComponent<RectTransform>();
        }

        public override List<DrawCommand> GetDrawCommands()
        {
            var allUIElements = new List<UIElement>();
            CollectUIElements(allUIElements);

            allUIElements.Sort((a, b) =>
            {
                var az = a.GetComponent<RectTransform>()?.GlobalPosition.Z ?? 0;
                var bz = b.GetComponent<RectTransform>()?.GlobalPosition.Z ?? 0;
                return az.CompareTo(bz);
            });

            var commands = new List<DrawCommand>();
            foreach (var ui in allUIElements)
            {
                commands.AddRange(ui.GetDrawCommands());
            }

            return commands;
        }

        private void CollectUIElements(List<UIElement> list)
        {
            IEnumerable<GameObject>? children = Owner?.GetHierarchy() ?? null;
            if (children == null)
                return;
            foreach (var child in children)
            {
                if (child.HasComponent<UIElement>())
                {
                    list.Add(child.GetComponent<UIElement>());
                }
            }
        }
    }
}
