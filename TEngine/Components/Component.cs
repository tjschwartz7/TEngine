using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components
{
    public abstract class Component : IDisposable
    {

        public Component()
        {

        }

        public GameObject? Owner { get; internal set; } = null;
        public bool Enabled { get; set; } = true;
        public bool HasStarted = false;    


        public void Register(GameObject gameObject) { Owner = gameObject; }

        public T? GetComponent<T>() where T : Component
        {
            if (Owner == null)
            {
                throw new InvalidOperationException("Component does not have an owner.");
            }
            return Owner.GetComponent<T>();
        }

        // Adding GetComponents<T> to your GameObject class
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            if (Owner == null)
            {
                throw new InvalidOperationException("Component does not have an owner.");
            }
            return Owner.GetComponents<T>();
        }


        public bool HasComponent<T>() where T : Component
        {
            return Owner != null && Owner.HasComponent<T>();
        }

        // Dispose method for manual cleanup
        public void Dispose()
        {
            OnDestroy();
        }

        public void TryStart()
        {
            if (!HasStarted)
            {
                HasStarted = true;
                Start();
            }
        }

        // Virtual methods for users to override
        public virtual void Awake() { } // Initialization that needs to happen before Start
        public virtual void OnAttach() { }
        public virtual void Start() { } // Called when the game starts
        public virtual void OnEnable() { } // Logic to initialize or subscribe to events
        public virtual void OnDisable() { } // Cleanup resources or unsubscribe from events
        public virtual void Update() { }
        public virtual void FixedUpdate() { } // Called at fixed intervals, useful for physics calculations
        public virtual void LateUpdate() { } // Logic that depends on other updates in the frame
        public virtual void OnApplicationQuit() { } // Called when the application is about to quit
        public virtual void OnGUI() { } // Logic for rendering GUI elements
        public virtual void OnDestroy() { } // Called when the object is destroyed

        public GameObject? GetOwner()
        {
            return Owner;
        }

        public UpdateTag UpdateTag {get; set;} = UpdateTag.Update; // Default to Update, can be changed to FixedUpdate, LateUpdate, or OnGUI
    }

    public enum UpdateTag
    {
        Update,
        FixedUpdate,
        LateUpdate,
        OnGUI
    }
}
