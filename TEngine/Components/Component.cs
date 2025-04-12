using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components
{
    public abstract class Component
    {

        public GameObject? Owner { get; internal set; } = null;
        public bool enabled { get; set; } = true;
        public bool isActiveAndEnabled { get; private set; } = true;


        public void Register(GameObject gameObject) { Owner = gameObject; }

        public Component GetComponent<T>()
        {
            return this;
        }

        public bool HasComponent<T>() where T : Component
        {
            return Owner != null && Owner.HasComponent<T>();
        }

        public virtual void Update()
        {
            // Default update logic, can be overridden by derived classes
        }

        public virtual void FixedUpdate()
        {
            // Default fixed update logic, can be overridden by derived classes
        }

        public virtual void LateUpdate()
        {
            // Default late update logic, can be overridden by derived classes
        }

        public GameObject? GetOwner()
        {
            return Owner;
        }
    }
}
