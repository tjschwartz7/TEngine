using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Behavior;
using TEngine.Components.Mesh;

namespace TEngine.EngineManagement.Scenes
{
    public class SceneManager
    {
        public static SceneManager Instance { get; private set; } = new SceneManager();
        string activeScene;
        internal Dictionary<string, Scene> scenes;

        private SceneManager()
        {
            if (Instance != null)
            {
                throw new Exception("SceneManager is a singleton and cannot be instantiated more than once.");
            }
            else
            {
                Instance = this;
            }
            scenes = new Dictionary<string, Scene>();
            string firstSceneName = "FirstScene";
            scenes.Add(firstSceneName, new Scene(firstSceneName));
            activeScene = firstSceneName;
        }

        public void Register(string name)
        {
            scenes.Add(name, new Scene(name));
        }

        public void RegisterAndLoad(string name)
        {
            scenes.Add(name, new Scene(name));
            activeScene = name;
        }


        public HashSet<int> GetAllActiveGameObjectIDs()
        {
            return scenes[activeScene].GetAllObjectIDs();
        }

        public Scene GetActiveScene()
        {
            return scenes[activeScene];
        }

        public string GetActiveSceneName()
        {
            return activeScene;
        }

        public void SetActiveScene(string name)
        {
            activeScene = name;
        }
    }
}
