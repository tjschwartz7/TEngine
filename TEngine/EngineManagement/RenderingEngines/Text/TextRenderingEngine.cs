using TEngine.TMath;
using TEngine.GameObjects.Cameras;

namespace TEngine.EngineManagement.RenderingEngines.Text
{
    public class TextRenderingEngine : RenderingEngine
    {
        private Camera? _camera;

        //!TODO: Renderer deals in GameObjects instead of raw text. 
        //Idea: Add render function to GameObject and have it return something the renderer can use?

        public TextRenderingEngine()
        {
            
        }


        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public override void Render(List<GameObject> renderableGameObjects)
        {
            Console.Clear();

            int cameraPosX = _camera?.Position.X ?? 0;
            int cameraPosY = _camera?.Position.Y ?? 0;
            int cameraWidth = _camera?.ViewSize.X ?? Console.WindowWidth;
            int cameraHeight = _camera?.ViewSize.Y ?? Console.WindowHeight;
            bool ditheringEnabled = _camera?.Rendering.Dithering ?? false;
            bool glowEnabled = _camera?.Rendering.Glow ?? false;

            //Add render logic

        }
    }
}
