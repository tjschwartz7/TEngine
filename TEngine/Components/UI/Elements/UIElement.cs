using TEngine.Components.UI.Layouts;

namespace TEngine.Components.UI.Elements
{
    public class UIElement : Component, IUIComponent
    {
        public UIElement()
        {
            // Initialize if needed
            Owner?.AddComponent<RectTransform>();
        }
        public bool Visible { get; set; } = true;
        public bool IsDirty { get; set; } = true;

        // This method will be called to draw the UI element
        public virtual void Draw(CanvasContext context)
        {
            // Implement drawing logic here
        }
    }
}
