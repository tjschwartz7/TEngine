using TEngine.Assets.UI;
using TEngine.Components.Graphics;
using TEngine.Components.UI.Layouts;

namespace TEngine.Components.UI.Elements.Text
{
    public class TextGraphic : Graphic
    {
        public string Text { get; set; } = string.Empty;
        public Font Font { get; set; } = Font.Default;
        public TextAlignment Alignment { get; set; } = TextAlignment.Left;

        protected override void Draw(CanvasContext ctx)
        {
            ctx.DrawText(Text, Font, RectTransform.GetWorldRect(), Alignment);
        }
    }

}
