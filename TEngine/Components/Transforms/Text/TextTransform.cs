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
        public Vector2Int Position { get; set; }

        // Constructor
        public TextTransform()
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
