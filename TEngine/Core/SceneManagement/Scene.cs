using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Rendering;
using TEngine.Core.AssetManagement.GameObjects.Cameras;
using TEngine.Core.AssetManagement.GameObjects.Comparers;
using TEngine.Core.AssetManagement.GameObjects;

namespace TEngine.Core.Scenes
{
    public class Scene
    {
        public string name;

        private HashSet<int> gameObjectIDs = new HashSet<int>();
        public Camera MainCamera { get; set; }
        private readonly GameObjectZIndexComparer comparer = new();

        public HashSet<int> GetAllObjectIDs()
        {
            return gameObjectIDs;
        }

        public Scene(string name)
        {
            this.name = name;
            MainCamera = GameObjectManager.Instance.CreateCamera("MainCamera", "MainCamera");
            

        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjectIDs.Add(gameObject.ID);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjectIDs.Remove(gameObject.ID);
        }

        public void AddGameObjects(List<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.gameObjectIDs.Add(gameObject.ID);
            }
        }
    }
}
