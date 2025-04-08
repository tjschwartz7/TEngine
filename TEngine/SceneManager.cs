using TEngine.Components.Mesh;

namespace TEngine
{
    public class SceneManager
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> GetRenderableObjects()
        {
            return gameObjects.Where(go => go.HasComponent<Mesh>()).ToList();
        }

    }
}
