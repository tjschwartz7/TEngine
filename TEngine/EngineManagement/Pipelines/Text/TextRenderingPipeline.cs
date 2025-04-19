using System;
using TEngine.Components.Transforms.Text;
using TEngine.Components.Animations.Text;
using TEngine.Components.Renderers.Text;
using TEngine.EngineManagement.RenderingEngines.Text;

namespace TEngine.EngineManagement.Pipelines.Text
{
    public class TextRenderingPipeline : RenderingPipeline
    {
        // Implement the render function specific to text rendering
        public override void Render(List<GameObject> gameObjects)
        {
            CullAndSort(gameObjects);

            // Iterate through the game objects and render text
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.HasComponent<TextRenderer>())
                {
                    var textRenderer = gameObject.GetComponent<TextRenderer>();
                    var transform = gameObject.GetComponent<TextTransform>();

                    // Apply effects like dithering, glow, etc.
                    string renderedText = TextEffects.CompositeEffect(textRenderer.GetRenderedData(), frame: 0/*TODO*/, new EffectConfig());

                    // Draw text on the screen
                    TerminalDriver.DrawText(renderedText, transform.GlobalPosition);
                }
            }
        }
    }

}
