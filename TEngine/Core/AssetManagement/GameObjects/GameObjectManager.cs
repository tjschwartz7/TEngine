using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras.Text;
using TEngine.EngineManagement.AssetManagement.GameObjects.Cameras;
using TEngine.Core.Services;


namespace TEngine.Core.AssetManagement.GameObjects
{
    public class GameObjectManager
    {
        public static GameObjectManager Instance { get; private set; } = new GameObjectManager();

        private Dictionary<int, GameObject> _gameObjectsByID = new Dictionary<int, GameObject>();
        private Dictionary<string, List<GameObject>> _gameObjectsByName = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, List<GameObject>> _gameObjectsByTag = new Dictionary<string, List<GameObject>>();

        private int _nextID = 1;

        public GameObjectManager() 
        {
            if (Instance != null)
            {
                throw new Exception("SceneManager is a singleton and cannot be instantiated more than once.");
            }
            else
            {
                Instance = this;
            }
        }

        

        // Create a new GameObject and automatically assign it a unique ID
        public GameObject CreateGameObject(string name, string tag = "")
        {
            int id = _nextID++;
            GameObject newObject = new GameObject(id, name, tag);

            // Register the new object in the appropriate dictionaries
            _gameObjectsByID[id] = newObject;

            if (!string.IsNullOrEmpty(name))
            {
                if (!_gameObjectsByName.ContainsKey(name))
                    _gameObjectsByName[name] = new List<GameObject>();
                _gameObjectsByName[name].Add(newObject);
            }

            if (!string.IsNullOrEmpty(tag))
            {
                if (!_gameObjectsByTag.ContainsKey(tag))
                    _gameObjectsByTag[tag] = new List<GameObject>();
                _gameObjectsByTag[tag].Add(newObject);
            }

            return newObject;
        }

        // Retrieve a GameObject by its ID
        public GameObject? GetGameObjectByID(int id)
        {
            if (_gameObjectsByID.ContainsKey(id))
            {
                return _gameObjectsByID[id];
            }
            else
            {
                Console.WriteLine($"No GameObject found with ID {id}");
                return null;
            }
        }

        // Retrieve a GameObject by its name (returns a list of objects if there are duplicates)
        public IEnumerable<GameObject> GetGameObjectsByName(string name)
        {
            if (_gameObjectsByName.ContainsKey(name))
            {
                return _gameObjectsByName[name];
            }
            else
            {
                Console.WriteLine($"No GameObjects found with name '{name}'");
                return new List<GameObject>();
            }
        }

        // Retrieve a GameObject by its tag (returns a list of objects if there are multiple with the same tag)
        public IEnumerable<GameObject> GetGameObjectsByTag(string tag)
        {
            if (_gameObjectsByTag.ContainsKey(tag))
            {
                return _gameObjectsByTag[tag];
            }
            else
            {
                Console.WriteLine($"No GameObjects found with tag '{tag}'");
                return new List<GameObject>();
            }
        }

        // Optionally: Destroy a GameObject by ID (or name if needed)
        public void DestroyGameObjectByID(int id)
        {
            if (_gameObjectsByID.ContainsKey(id))
            {
                GameObject obj = _gameObjectsByID[id];

                // Remove from all dictionaries
                _gameObjectsByID.Remove(id);
                _gameObjectsByName[obj.Name].Remove(obj);
                if (_gameObjectsByName[obj.Name].Count == 0)
                    _gameObjectsByName.Remove(obj.Name);

                _gameObjectsByTag[obj.Tag].Remove(obj);
                if (_gameObjectsByTag[obj.Tag].Count == 0)
                    _gameObjectsByTag.Remove(obj.Tag);

                Console.WriteLine($"Destroyed GameObject {obj.Name} (ID {id})");
            }
            else
            {
                Console.WriteLine($"No GameObject found with ID {id} to destroy.");
            }
        }

        public void SetGameObjectName(int id, string newName)
        {
            // Check if the object exists
            if (!_gameObjectsByID.ContainsKey(id))
            {
                Console.WriteLine($"GameObject with ID {id} not found.");
                return;
            }

            GameObject obj = _gameObjectsByID[id];
            string oldName = obj.Name;

            // Remove from the old name map
            if (_gameObjectsByName.ContainsKey(oldName))
            {
                _gameObjectsByName[oldName].Remove(obj);
                // Clean up the list if it becomes empty
                if (_gameObjectsByName[oldName].Count == 0)
                    _gameObjectsByName.Remove(oldName);
            }

            // Update the name in the object itself
            obj.SetName(newName);

            // Add to the new name map, creating the list if it doesn't exist
            if (!_gameObjectsByName.TryGetValue(newName, out var nameList))
            {
                nameList = new List<GameObject>();
                _gameObjectsByName[newName] = nameList;
            }
            nameList.Add(obj);
        }

        public void SetGameObjectTag(int id, string newTag)
        {
            // Check if the object exists
            if (!_gameObjectsByID.ContainsKey(id))
            {
                Console.WriteLine($"GameObject with ID {id} not found.");
                return;
            }

            GameObject obj = _gameObjectsByID[id];
            string oldTag = obj.Tag;

            // Remove from the old tag map
            if (_gameObjectsByTag.ContainsKey(oldTag))
            {
                _gameObjectsByTag[oldTag].Remove(obj);
                // Clean up the list if it becomes empty
                if (_gameObjectsByTag[oldTag].Count == 0)
                    _gameObjectsByTag.Remove(oldTag);
            }

            // Update the tag in the object itself
            obj.SetTag(newTag);

            // Add to the new tag map, creating the list if it doesn't exist
            if (!_gameObjectsByTag.TryGetValue(newTag, out var tagList))
            {
                tagList = new List<GameObject>();
                _gameObjectsByTag[newTag] = tagList;
            }
            tagList.Add(obj);
        }

        public Camera CreateCamera(string name, string tag="Camera")
        {
            int id = _nextID++;
            Camera? camera = CameraService.Get(id, name, tag) as Camera;
            if (camera == null)
            {
                throw new Exception("CameraService has not yet been initialized!");
            }
            _gameObjectsByID[id] = camera;
            return camera;
        }

        public IEnumerable<GameObject> GetGameObjectsByIDs(IEnumerable<int> ids)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            foreach (int id in ids)
            {
                if (_gameObjectsByID.TryGetValue(id, out GameObject obj))
                {
                    gameObjects.Add(obj);
                }
                else
                {
                    Console.WriteLine($"GameObject with ID {id} not found.");
                }
            }

            return gameObjects;
        }

    }
}
