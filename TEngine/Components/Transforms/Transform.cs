using System;
using TEngine.TMath;

namespace TEngine.Components.Transforms
{
    public class Transform  : Component
    {

        // Position in world space (default behavior)
        public virtual Vector3 GlobalPosition => ComputeGlobalPosition();

        // Position relative to parent
        public virtual Vector3 LocalPosition { get; protected set; } = Vector3.zero;

        // Translation helpers
        public virtual void MoveTo(Vector3 newPosition)
        {
            LocalPosition = newPosition;
        }

        public virtual void Translate(Vector3 delta)
        {
            LocalPosition += delta;
        }

        protected virtual Vector3 ComputeGlobalPosition()
        {
            Vector3 result = LocalPosition;
            var current = Owner?.Parent;
            var currentTransform = current?.GetComponent<Transform>();
            while (current != null)
            {
                if (currentTransform != null)
                    result += currentTransform.LocalPosition;
                current = current.Parent;
            }
            return result;
        }
    }
}
