using TEngine.TMath;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Renderers.Text;
using TEngine.Components.Transforms;
using System.Drawing;
using TEngine.Components.Animations.Text;

namespace TEngine.EngineManagement.RenderingEngines.Text
{
    public class TextRenderSystem : RenderSystem
    {
        private Camera? _camera;

        //!TODO: Renderer deals in GameObjects instead of raw text. 
        //Idea: Add render function to GameObject and have it return something the renderer can use?

        public TextRenderSystem()
        {
            
        }


        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public override void Render(List<GameObject> renderableGameObjects)
        {
            float cameraPosX = _camera?.Position.X ?? 0;
            float cameraPosY = _camera?.Position.Y ?? 0;
            float cameraWidth = _camera?.ViewSize.X ?? Console.WindowWidth;
            float cameraHeight = _camera?.ViewSize.Y ?? Console.WindowHeight;
            bool ditheringEnabled = _camera?.Rendering.Dithering ?? false;
            bool glowEnabled = _camera?.Rendering.Glow ?? false;

            var viewBounds = new RectangleF(cameraPosX, cameraPosY, cameraWidth, cameraHeight);

            foreach (var gameObject in renderableGameObjects)
            {
                if (!gameObject.HasComponent<TextRenderer>() || !gameObject.HasComponent<Transform>())
                    continue;

                var renderer = gameObject.GetComponent<TextRenderer>();
                var transform = gameObject.GetComponent<Transform>();

                Vector3 screenPos = transform.GlobalPosition;

                // Cull objects outside the camera view
                if (!viewBounds.Contains(screenPos.X, screenPos.Y))
                    continue;

                string text = renderer.GetRenderedText();

                // Apply effects
                if (ditheringEnabled)
                    text = TextEffects.ApplyDithering(text);

                if (glowEnabled)
                    text = TextEffects.ApplyGlow(text);

                TerminalDriver.DrawText(text, screenPos);
            }
        }

    }
}
}
