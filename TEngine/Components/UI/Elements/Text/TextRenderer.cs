using TEngine.Components.Graphics;
using TEngine.Components.Transforms;
using TEngine.Components.UI.Layouts;


namespace TEngine.Components.UI.Elements.Text
{
    public class TextRenderer : Graphic
    {
        public string Text { get; set; } = "";
        //public Font Font { get; set; }  // Assuming you have a font type or texture atlas
        public float FontSize { get; set; } = 16f;

        public override void Draw(CanvasContext ctx)
        {
            if (!Visible) return;

            // Render text (this would actually use a font system or texture atlas)
            // Example of drawing text using a simplified approach
            return $"Rendering Text: {Text} at Position: {GetComponent<Transform>().GlobalPosition}, Color: {Color}";
        }
    }


}
