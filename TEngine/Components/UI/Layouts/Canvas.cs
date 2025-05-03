using TEngine.Components.Transforms;
using TEngine.Components.UI.Elements;

namespace TEngine.Components.UI.Layouts
{
    // Canvas is a container for UI elements
    public class Canvas : Component, IUIComponent
    {
        // Stores all child Graphic components (like TextRenderer, ImageRenderer)
        private List<UIElement> uiComponents = new List<UIElement>();
        public CanvasContext Context { get; private set; } = new CanvasContext();

        // Constructor
        public Canvas()
        {
            // Initialize if needed
            Owner?.AddComponent<RectTransform>();
        }

        // Call this to add Graphics to the Canvas
        public void AddUIElement(UIElement element)
        {
            uiComponents.Add(element);
        }

        // Optionally you can add layout management features like sorting or anchoring later
        public void SortGraphics()
        {
            uiComponents.Sort((g1, g2) => g1.GetComponent<RectTransform>().GlobalPosition.Y.CompareTo(g2.GetComponent<RectTransform>().GlobalPosition.Y));
        }
    }
}
