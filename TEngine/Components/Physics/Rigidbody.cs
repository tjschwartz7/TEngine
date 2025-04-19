using TEngine.Utils;
using TEngine.TMath;
using TEngine.Components;

namespace TEngine.Components.Physics
{

    public class Rigidbody : Component
    {
        // Basic properties for 2D Rigidbody
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Vector2 force;
        public float mass;
        public bool useGravity;
        public bool isKinematic;
        public float drag;

        // Constructor to initialize the Rigidbody
        public Rigidbody()
        {
            position = Vector2.zero;
            velocity = Vector2.zero;
            acceleration = Vector2.zero;
            force = Vector2.zero;
            mass = 1.0f;
            useGravity = true;
            isKinematic = false;
            drag = 0.1f;
            UpdateTag = UpdateTag.FixedUpdate;
        }

        // Applies force to the Rigidbody, influencing velocity/acceleration
        public void AddForce(Vector2 appliedForce)
        {
            if (isKinematic) return;  // If the Rigidbody is kinematic, forces won't affect it
            force += appliedForce;
        }

        // Calculates the resulting velocity from the applied forces
        public void ApplyForces()
        {
            if (isKinematic) return;

            // Gravity simulation (if enabled)
            if (useGravity)
            {
                force += new Vector2(0, -9.81f * mass); // Gravity in the Y direction
            }

            // Newton's Second Law: F = ma (Force = mass * acceleration)
            acceleration = force / mass;

            // Update velocity based on acceleration
            velocity += acceleration * Time.deltaTime;

            // Apply drag (dampening effect)
            velocity *= (1 - drag * Time.deltaTime);

            // Reset force after each update
            force = Vector2.zero;
        }

        // Moves the Rigidbody based on velocity
        public void Move()
        {
            if (isKinematic) return;

            position += velocity * Time.deltaTime;
        }

        // For simplicity, collision detection isn't handled here, but you can extend this
        public bool CheckCollision(Rigidbody other)
        {
            // Simple collision check: compare positions (in a real engine, this would involve complex logic)
            return position == other.position;
        }

        // Debugging output to visualize Rigidbody state
        public void DebugInfo()
        {
            //Debug.Log($"Position: {position}, Velocity: {velocity}, Force: {force}, Acceleration: {acceleration}");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }

}
