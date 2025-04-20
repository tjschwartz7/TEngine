using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Renderers;
using TEngine.GameObjects.Cameras;
using TEngine.GameObjects.Cameras.Text;
using TEngine.GameObjects.Comparers;
using TEngine;

namespace TEngine.EngineManagement.Scenes
{
    public class Scene
    {
        string name;

        private List<GameObject> gameObjects = new List<GameObject>();
        public Camera MainCamera { get; set; } = null;
        private readonly GameObjectZIndexComparer comparer = new();
        public List<GameObject> GetRenderableGameObjects()
        {
            return gameObjects.Where(go => go.HasComponent<Renderer>()).ToList();
        }

        public List<GameObject> GetAllObjects()
        {
            return gameObjects;
        }

        public Scene(string name)
        {
            this.name = name;

            MainCamera = Camera.Default;

        }

        public void AddGameObject(GameObject gameObject)
        {
            int index = gameObjects.BinarySearch(gameObject, comparer);
            if (index < 0) index = ~index; // bitwise complement gives insert index
            gameObjects.Insert(index, gameObject);
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
