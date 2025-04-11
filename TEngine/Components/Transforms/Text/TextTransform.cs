using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.TMath;

namespace TEngine.Components.Transforms.Text
{
    public class TextTransform : Transform
    {
        /// <summary>
        /// LocalPosition represents the top-left corner of the object in text space.
        /// (0,0) is the top-left of the screen.
        /// Positive X moves right; positive Y moves down.
        /// </summary>
        public Vector2Int LocalPosition { get; private set; }

        // Global position is calculated from local + parent's global
        public Vector2Int GlobalPosition;


        // Constructor
        public TextTransform()
        {
            LocalPosition = Vector2Int.zero;
        }

        /// <summary>
        /// Update the transform position
        /// </summary>
        /// <param name="newPosition">Sets local position to new position.</param>
        public void MoveTo(Vector2Int newPosition)
        {
            LocalPosition = newPosition;
        }

        /// <summary>
        /// Move by a delta (useful for nudging position)
        /// </summary>
        /// <param name="delta">The vector of movement.</param>
        public void Translate(Vector2Int delta)
        {
            LocalPosition += delta;
        }
    }
}
