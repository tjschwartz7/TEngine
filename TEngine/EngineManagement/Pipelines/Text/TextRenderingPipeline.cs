using TEngine.Components.Rendering;
using TEngine.EngineManagement.Scenes;
using System.Drawing;
using TEngine.EngineManagement.Drivers;
using TEngine.Services;
using TEngine.Components.UI.Layouts;
using TEngine.EngineManagement.Commands;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;

namespace TEngine.EngineManagement.Pipelines.Text
{


    public class TextRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<DrawCommand> _driver;

        public TextRenderingPipeline()
        {
            _driver = DriverService.Get<DrawCommand>();
        }

        // Override the Render method to use DrawCommands instead of GameObjects.
        public override void Render(Scene scene)
        {
            if (scene.MainCamera is not CameraText camera)
                throw new InvalidOperationException("MainCamera is not CameraText");

            var drawCommands = CullAndSort(scene, camera);
            Preprocess(drawCommands, camera);
            Draw(drawCommands, camera);
        }

        // This method now collects DrawCommands rather than GameObjects.
        public override List<DrawCommand> CullAndSort(Scene scene, Camera camera)
        {
            var drawCommands = new List<DrawCommand>();
            var viewBounds = new RectangleF(camera.Position.X, camera.Position.Y, camera.ViewSize.X, camera.ViewSize.Y);

            // Iterate over all renderable game objects to generate draw commands.
            foreach (var go in scene.GetRenderableGameObjects())
            {
                //The TextPipeline is only for UI elements,
                // meaning all of the renderables need to have a RectTransform and a Renderer component.
                if (!go.HasComponent<Renderer>() || !go.HasComponent<RectTransform>())
                    continue;

                var transform = go.GetComponent<RectTransform>();
                var renderer = go.GetComponent<Renderer>();

                // If the GameObject is within the camera view, create a DrawCommand.
                if (viewBounds.Contains(transform.GlobalPosition.X, transform.GlobalPosition.Y))
                {
                    var drawCommand = renderer.GetDrawCommands();
                    drawCommands.AddRange(drawCommand);
                }
            }

            // Sorting can be added if needed, based on layers or other criteria.
            return drawCommands;
        }

        // Preprocess draw commands if needed. In this case, we're keeping it simple.
        public override void Preprocess(List<DrawCommand> drawCommands, Camera camera)
        {
            // For now, there's nothing special to preprocess.
            // If animations, shaders, or transformations are applied, it could go here.
        }

        // Draw the collected DrawCommands to the screen.
        public override void Draw(List<DrawCommand> drawCommands, Camera camera)
        {
            foreach (var drawCommand in drawCommands)
            {
                _driver.Draw(drawCommand);
            }
        }
    }
}



