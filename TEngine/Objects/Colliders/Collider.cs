using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.TMath;

namespace TEngine.Components.Colliders
{
    public class Collider : Component
    {
        public bool isTrigger { get; set; } = false;    
        public List<int> bounds { get; set; } = new List<int>();
        public bool enabled { get; set; } = true;

        //Returns the closest point on the collider’s surface to the given position.
        public Vector2 ClosestPoint(Vector2 position)
        {
            return new Vector2(0, 0);
        }

        public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
        {
            return false;
        }

        //Returns the closest point inside the collider’s bounds.
        public Vector2 ClosestPointOnBounds(Vector2 position)
        {
            return new Vector2(0, 0);
        }

    }
}
