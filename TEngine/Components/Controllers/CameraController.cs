using TEngine.TMath;
using TEngine.GameObjects.Cameras;

namespace TEngine.Components.Controllers
{
    public class CameraController : Component
    {
        public Rect? Bounds;

        public override void OnAttach()
        {
            if (Owner is not Camera)
                throw new InvalidOperationException("CameraController can only be attached to a Camera GameObject.");
        }

        public override void Update()
        {
            if (Bounds is Rect bounds)
            {
                var camera = Owner as Camera;
                if (camera != null)
                {
                    camera.Position = ClampPosition(camera.Position, bounds);
                }
            }
        }

        private Vector3 ClampPosition(Vector3 position, Rect bounds)
        {
            // Assume center-aligned camera for clamping math
            float halfWidth = Console.WindowWidth / 2f;
            float halfHeight = Console.WindowHeight / 2f;

            float minX = bounds.X + halfWidth;
            float maxX = bounds.X + bounds.W - halfWidth;
            float minY = bounds.Y + halfHeight;
            float maxY = bounds.Y + bounds.H - halfHeight;

            return new Vector3(
                Math.Clamp(position.X, minX, maxX),
                Math.Clamp(position.Y, minY, maxY),
                position.Z
            );
        }
    }

}
