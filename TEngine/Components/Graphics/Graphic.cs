using System.Drawing;
using TEngine.TMath;

namespace TEngine.Components.Graphics
{
    public abstract class Graphic : Component
    {
        // Common properties for anything drawable
        public Color Color { get; set; } = Color.White;
        public bool RaycastTarget { get; set; } = false; // For UI interaction later

        // If you want layout-type stuff later:
        public Vector2 Pivot { get; set; } = new Vector2(0.5f, 0.5f); // Centered by default
        public Vector2 Size { get; set; } = new Vector2(100, 100);     // Default size

        // State flags (optional)
        public bool Visible { get; set; } = true;

        // Constructor
        protected Graphic()
        {
        }

        // Abstract method for all Graphics to implement
        public abstract void Render();
    }
}
