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
        // Text-space coordinates: top-left (0, 0), y increases downward
        public new Vector2Int LocalPosition { get; private set; } = Vector2Int.zero;

        public override Vector3 GlobalPosition
        {
            get
            {
                Vector2Int result = LocalPosition;
                var current = Owner?.Parent;
                var currentTransform = current?.Parent?.GetComponent<Transform>();
                while (current != null)
                {
                    if (currentTransform != null && currentTransform is TextTransform parentText)
                        result += parentText.LocalPosition;
                    current = current.Parent;
                }
                return new Vector3(result.X, result.Y, 0);
            }
        }

        public void MoveTo(Vector2Int newPosition)
        {
            LocalPosition = newPosition;
        }

        public void Translate(Vector2Int delta)
        {
            LocalPosition += delta;
        }

        // Optionally override MoveTo(Vector3) for compatibility
        public override void MoveTo(Vector3 newPosition)
        {
            LocalPosition = Vector2Int.FromVector3Floor(newPosition);
        }

        public override void Translate(Vector3 delta)
        {
            LocalPosition += Vector2Int.FromVector3Floor(delta);
        }
    }
}
