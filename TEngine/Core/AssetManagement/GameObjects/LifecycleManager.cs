
using TEngine.Components;

namespace TEngine.Core.AssetManagement.GameObjects
{
    public class LifecycleManager
    {
        public static LifecycleManager Instance { get; private set; } = new();

        private LifecycleManager() { }


        // Call Start for all behaviors and components
        public void StartGameObjectLifecycle(GameObject root)
        {
            if(root.Paused || !root.IsActive) return;

            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            // BFS to start all gameObjects
            while (gameObjectQueue.Count > 0) 
            {
                var go = gameObjectQueue.Dequeue();
                // Start behaviors and components for the current gameObject
                foreach (var behavior in go.MonoBehaviors.Where(b => b.IsActiveAndEnabled))
                {
                    behavior.Start();
                }
                foreach (var component in go.Components.Values.Where(c => c.Enabled))
                {
                    component.Start();
                }
                // Enqueue children for processing
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }

            }

        }

        public void UpdateGameObjectLifecycle(GameObject root)
        {
            if (root.Paused || !root.IsActive) return;

            //Begin BFS to update all gameObjects
            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();

                // Update behaviors and components for the current gameObject
                foreach (var behavior in go.MonoBehaviors.Where(b => b.IsActiveAndEnabled))
                {
                    behavior.Update();
                }

                foreach (var component in go.Components.Values.Where(c => c.Enabled && c.UpdateTag == UpdateTag.Update))
                {
                    component.Update();
                }

                // Enqueue children for processing
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }
            }
        }

        // Separate function for physics update
        public void FixedUpdateLifecycle(GameObject root)
        {
            if (root.Paused || !root.IsActive) return;

            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();

                // Update physics components for the current gameObject
                foreach (var component in go.Components.Values.Where(c => c.Enabled && c.UpdateTag == UpdateTag.FixedUpdate))
                {
                    component.FixedUpdate();
                }

                // Enqueue children for physics update
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }
            }
        }

        // Separate function for physics update
        public void LateUpdateLifecycle(GameObject root)
        {
            if (root.Paused || !root.IsActive) return;

            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();

                // Update physics components for the current gameObject
                foreach (var component in go.Components.Values.Where(c => c.Enabled && c.UpdateTag == UpdateTag.LateUpdate))
                {
                    component.LateUpdate();
                }

                // Enqueue children for physics update
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }
            }
        }

        // Separate function for physics update
        public void GUILifecycle(GameObject root)
        {
            if (root.Paused || !root.IsActive) return;

            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();

                // Update physics components for the current gameObject
                foreach (var component in go.Components.Values.Where(c => c.Enabled && c.UpdateTag == UpdateTag.OnGUI))
                {
                    component.OnGUI();
                }

                // Enqueue children for physics update
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }
            }
        }

        // Propagate the IsActive status to all children
        public void PropagateActiveStatusToChildren(GameObject root, bool active)
        {
            Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
            gameObjectQueue.Enqueue(root);

            // BFS to propagate IsActive status
            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();
                go.IsActive = active;  // Propagate IsActive to children

                // Enqueue children for processing
                foreach (var child in go.Children)
                {
                    gameObjectQueue.Enqueue(child);
                }
            }
        }
    }

}
