using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine
;

namespace TEngine2.Behavior
{
    public abstract class Monobehavior : IDisposable
    {
        protected Monobehavior()
        {
            Engine.Instance.Register(this);
            Awake();
        }

        // Dispose method for manual cleanup
        public void Dispose()
        {
            OnDestroy();
        }

        // Virtual methods for users to override
        public virtual void Awake() { } // Initialization that needs to happen before Start
        public virtual void Start() { } // Called when the game starts
        public virtual void Update() { } // Called every frame
        public virtual void OnEnable() { } // Logic to initialize or subscribe to events
        public virtual void OnDisable() { } // Cleanup resources or unsubscribe from events

        public virtual void FixedUpdate() { } // Called at fixed intervals, useful for physics calculations
        public virtual void LateUpdate() { } // Logic that depends on other updates in the frame
        public virtual void OnApplicationQuit() { } // Called when the application is about to quit
        public virtual void OnGUI() { } // Logic for rendering GUI elements
        public virtual void OnDestroy() { } // Called when the object is destroyed

        //Physics
        //!TODO: Add collision and trigger methods
        //public virtual void OnCollisionEnter(Collision collision) { } // Handle collision events
        //public virtual void OnTriggerEnter(Collider other) { } // Handle trigger events


    }
}
