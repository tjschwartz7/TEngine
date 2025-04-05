using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GameObjects;

namespace TEngine.Rendering
{
    public class CameraManager
    {
        public static CameraManager Instance { get; set; } = new CameraManager();

        private readonly List<Camera> _cameras = new();

        public void AddCamera(Camera cam) => _cameras.Add(cam);
        public void RemoveCamera(Camera cam) => _cameras.Remove(cam);

        public void RenderAll()
        {
            var sorted = _cameras
                .Where(c => c.Enabled)
                .OrderBy(c => c.Output.Priority)
                .ThenBy(c => c.RenderType)
                .ToList();

            foreach (var camera in sorted)
            {
                var renderer = RendererFactory.GetRenderer(camera.Rendering.Renderer);
                renderer.Render(camera);
            }
        }

        public void ClearCameras() => _cameras.Clear();
    }
}
