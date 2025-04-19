using TEngine.TMath;
using TEngine.GameObjects.Cameras.Text;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Renderers.Text;
using TEngine.Components.Transforms;
using System.Drawing;
using TEngine.Components.Animations.Text;

namespace TEngine.EngineManagement.RenderingEngines.Text
{
    public class TextRenderSystem : RenderSystem
    {
        private CameraText? _camera;

        //!TODO: Renderer deals in GameObjects instead of raw text. 
        //Idea: Add render function to GameObject and have it return something the renderer can use?

        public TextRenderSystem()
        {
            
        }

        public override void SetCamera(Camera camera)
        {
            if (camera is CameraText cameraText)
            {
                _camera = cameraText;
            }
            else
            {
                throw new ArgumentException("Camera must be of type CameraText.");
            }
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

                string text = renderer.GetRenderedData();

                TerminalDriver.DrawText(text, screenPos);
            }
        }
    }
}
