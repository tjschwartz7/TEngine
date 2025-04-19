using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement.Scenes;
using TEngine.GameObjects.Cameras;

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
        protected void CullAndSort(List<GameObject> gameObjects)
        {
            // Cull objects outside the camera view, sort based on layer, etc.
        }
    }


}
