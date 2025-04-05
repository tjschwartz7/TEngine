using System;
using TEngine.TMath;

namespace TEngine.Components
{
    public class Transform : Component
    {
        public Vector2Int Position { get; set; }

        // Constructor
        public Transform()
        {
            Position = Vector2Int.zero;
        }

        // Update the transform position
        public void MoveTo(Vector2Int newPosition)
        {
            Position = newPosition;
        }
    }
}
