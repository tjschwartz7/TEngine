using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using TEngine.GameObjects;

namespace TEngine.Rendering.Text
{
    public class TextRenderer : Renderer
    {
        private Camera _camera;
        private List<RenderCommand> _drawCommands = new();

        public TextRenderer(Camera camera)
        {
            _camera = camera;
        }

        public void DrawText(string text, int worldX, int worldY, int layer = 0)
        {
            _drawCommands.Add(new RenderCommand(text, worldX, worldY, layer));
        }

        public override void Render()
        {
            Console.Clear();

            foreach (var cmd in _drawCommands.OrderBy(c => c.Layer))
            {
                // Convert world coords → screen coords
                int screenX = cmd.WorldX - _camera.Position.X;
                int screenY = cmd.WorldY - _camera.Position.Y;

                // Check if inside the screen bounds
                if (screenX >= 0 && screenX < _camera.ViewSize.X &&
                    screenY >= 0 && screenY < _camera.ViewSize.Y)
                {
                    Console.SetCursorPosition(screenX, screenY);
                    Console.Write(cmd.Text);
                }
            }

            _drawCommands.Clear(); // clear after rendering
        }

        private record RenderCommand(string Text, int WorldX, int WorldY, int Layer);
    }
}
