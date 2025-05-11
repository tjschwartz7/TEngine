using TEngine.TMath;
using TEngine.EngineManagement.Drivers;
using TEngine.EngineManagement.Commands;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;
using TEngine.Core.Services;

namespace TEngine.EngineManagement.Pipelines.Text
{


    public class TextRenderingPipeline : RenderingPipeline
    {
        private readonly IDriver<DrawCommand> _driver;

        public TextRenderingPipeline()
        {
            _driver = DriverService.Get<DrawCommand>() ?? throw new InvalidOperationException("Failed to retrieve the DrawCommand driver."); ;
        }

        // Override the Render method to use DrawCommands instead of GameObjects.
        public override void Render(IEnumerable<DrawCommand> commands, Camera camera)
        {
            if (camera is not CameraText cameraText)
                throw new InvalidOperationException("The provided camera is incompatible with the Text Rendering Pipeline.");

            var drawCommands = CullAndSort(commands, cameraText);
            drawCommands = Preprocess(drawCommands, cameraText);
            Draw(drawCommands, cameraText);
        }

        // This method now collects DrawCommands rather than GameObjects.
        public override IEnumerable<DrawCommand> CullAndSort(IEnumerable<DrawCommand> commands, Camera camera)
        {
            var viewBounds = new RectangleF(camera.Position.X, camera.Position.Y, camera.ViewSize.X, camera.ViewSize.Y);

            return commands.Where(cmd => viewBounds.Contains(cmd.Position.X, cmd.Position.Y));
        }

        // Preprocess draw commands if needed. In this case, we're keeping it simple.
        public override IEnumerable<DrawCommand> Preprocess(IEnumerable<DrawCommand> commands, Camera camera)
        {
            // For now, there's nothing special to preprocess.
            // If animations, shaders, or transformations are applied, it could go here.

            return commands;
        }

        // Draw the collected DrawCommands to the screen.
        public override void Draw(IEnumerable<DrawCommand> commands, Camera camera)
        {
            foreach (var drawCommand in commands)
            {
                _driver.Draw(drawCommand);
            }
        }
    }
}



