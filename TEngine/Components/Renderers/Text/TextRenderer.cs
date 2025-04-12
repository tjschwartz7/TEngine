using TEngine.Components.Animations.Text;

namespace TEngine.Components.Renderers.Text
{
    public class TextRenderer : Component
    {
        public string Text { get; set; } = "";

        public TextRenderer()
        {

      
        }

        public override void Update()
        {
            GetComponent<TextAnimation>()?.Tick();
        }

        public string GetRenderedText()
        {
            return HasComponent<TextAnimation>() ? ((TextAnimation)(GetComponent<TextAnimation>())).Apply(Text) : Text;
        }
    }

}
