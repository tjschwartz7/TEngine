
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;
using TEngine.Components.Transforms;

namespace TEngine.EngineManagement.Pipelines
{
    public abstract class RenderingPipeline
    {
        // The Camera to use for rendering
        protected Camera _camera;

        // Set the camera for the pipeline
        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        // Abstract method to render the objects, will be overridden by child pipelines
        public abstract void Render(List<GameObject> gameObjects);

        // Add other shared logic like culling, sorting, etc.
        protected List<GameObject> CullAndSort(List<GameObject> gameObjects)
        {
            // Cull objects outside the camera view, sort based on layer, etc.
            return gameObjects
                .Where(gameObject => _camera.IsVisible(gameObject.GetComponent<Transform>().GlobalPosition))
                .OrderBy(gameObject => gameObject.ObjectLayer.number)
                .ToList();
        }
    }


}
