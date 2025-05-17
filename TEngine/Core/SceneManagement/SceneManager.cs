using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Behavior;
using TEngine.Components.Mesh;
using TEngine.Utils.Logging;

namespace TEngine.Core.Scenes
{
    public class SceneManager
    {
        public static SceneManager Instance { get; private set; } = new SceneManager();
        string activeScene;
        internal Dictionary<string, Scene> scenes;

        private SceneManager()
        {
            Logging.Debug("Loading new scene manager");
            scenes = new Dictionary<string, Scene>();
            string firstSceneName = "FirstScene";
            scenes.Add(firstSceneName, new Scene(firstSceneName));
            activeScene = firstSceneName;
        }

        public void Register(string name)
        {
            Logging.Debug("Registering scene " + name);
            scenes.Add(name, new Scene(name));
        }

        public void RegisterAndLoad(string name)
        {
            Logging.Debug("Adding new active scene " + name);
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
