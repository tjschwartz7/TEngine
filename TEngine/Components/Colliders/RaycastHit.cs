using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.TMath;
using TEngine.Components.Physics;

namespace TEngine.Components.Colliders
{
    public class RaycastHit
    {
        public Collider collider { get; set; }
        public float distance { get; set; }
        public Vector2 point { get; set; }
        public Vector2 normal { get; set; }
        public Transform Transform { get; set; }
        public Rigidbody rigidBody { get; set; }

    }
}
