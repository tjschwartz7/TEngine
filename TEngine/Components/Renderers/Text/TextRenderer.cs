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
            if(HasComponent<TextAnimation>())
            {
                var textAnimation = GetComponent<TextAnimation>();
                if (textAnimation != null)
                {
                    return textAnimation.Apply(Text);
                }
            }
            
            return Text;
        }
    }

}
