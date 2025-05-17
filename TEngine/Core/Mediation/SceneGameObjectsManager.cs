using TEngine.Core.Scenes;
using TEngine.Components.Rendering;
using TEngine.Core.AssetManagement.GameObjects;

namespace TEngine.Core.Mediation
{
    public class SceneGameObjectsManager
    {
        public static SceneGameObjectsManager Instance { get; private set; } = new SceneGameObjectsManager();

        private SceneGameObjectsManager() { }

        public IEnumerable<GameObject> GetRenderableGameObjects()
        {
            // This method should return a list of renderable game objects.
            var sceneGameObjectIDs = SceneManager.Instance.GetAllActiveGameObjectIDs();

            if (sceneGameObjectIDs == null || !sceneGameObjectIDs.Any())
            {
                return new List<GameObject>(); // Return an empty list if no game objects
            }

            return GameObjectManager.Instance.GetGameObjectsByIDs(sceneGameObjectIDs)
                .Where(go => go.IsActive && go.Layer.isVisible && go.HasComponent<Renderer>())
                .ToList();
        }

        public IEnumerable<GameObject> GetActiveGameObjects()
        {
            // This method should return a list of renderable game objects.
            var sceneGameObjectIDs = SceneManager.Instance.GetAllActiveGameObjectIDs();

            if (sceneGameObjectIDs == null || !sceneGameObjectIDs.Any())
            {
                return new List<GameObject>(); // Return an empty list if no game objects
            }

            return GameObjectManager.Instance.GetGameObjectsByIDs(sceneGameObjectIDs)
                .Where(go => go.IsActive)
                .ToList();
        }

        public IEnumerable<GameObject> GetAllGameObjects()
        {
            // This method should return a list of renderable game objects.
            var sceneGameObjectIDs = SceneManager.Instance.GetAllActiveGameObjectIDs();

            if (sceneGameObjectIDs == null || !sceneGameObjectIDs.Any())
            {
                return new List<GameObject>(); // Return an empty list if no game objects
            }

            return GameObjectManager.Instance.GetGameObjectsByIDs(sceneGameObjectIDs).ToList();
        }
    }
}
