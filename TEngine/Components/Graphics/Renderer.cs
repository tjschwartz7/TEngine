using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Animations;

namespace TEngine.Components.Graphics
{
    public abstract class Renderer : Component
    {

    }

    public abstract class Renderer<T> : Renderer
    {
        public T RenderData { get; set; } // Placeholder for actual render data type

        public T GetRenderedData()
        {
            T modifiedRenderData = RenderData;

            // Apply each animation to the render data, in the order they were added
            foreach (var animation in GetComponents<Animation<T>>())
            {
                modifiedRenderData = animation.Apply(modifiedRenderData);
            }

            return modifiedRenderData;
        }
    }


}
