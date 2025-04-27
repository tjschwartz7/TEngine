using TEngine.Components.Transforms;

namespace TEngine.Components.UI
{
    public class Canvas : Component
    {
        // Stores all child Graphic components (like TextRenderer, ImageRenderer)
        private List<Graphic> graphics = new List<Graphic>();

        // Constructor
        public Canvas()
        {
            // Initialize if needed
        }

        // Call this to add Graphics to the Canvas
        public void AddGraphic(Graphic graphic)
        {
            graphics.Add(graphic);
        }

        // Call this in your engine's main loop to render everything
        public void Render()
        {
            foreach (var graphic in graphics)
            {
                if (graphic.Visible)
                {
                    graphic.Render(); // Each Graphic knows how to render itself
                }
            }
        }

        // Optionally you can add layout management features like sorting or anchoring later
        public void SortGraphics()
        {
            graphics.Sort((g1, g2) => g1.GetComponent<Transform>().GlobalPosition.Y.CompareTo(g2.GetComponent<Transform>().GlobalPosition.Y));
        }
    }
}
