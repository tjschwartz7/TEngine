using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Renderers;

namespace TEngine.EngineManagement.Scenes
{
    internal class Scene
    {
        string name;

        private List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> GetRenderableObjects()
        {
            return gameObjects.Where(go => go.HasComponent<Renderer>()).ToList();
        }

        public Scene(string name)
        {
            this.name = name;
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        public void AddGameObjects(List<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.gameObjects.Add(gameObject);
            }
        }
    }
}
