using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Behavior;
using TEngine.GoneMedieval.Levels.Scenes;

namespace TEngine.Scenes
{
    public class SceneManager : Monobehavior
    {
        public static SceneManager Instance { get; private set; } = new SceneManager();
        int scene = 0;
        private Dictionary<string, Scene> scenes;
        public override void Awake()
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
        }

        public void Register(string name, Scene scene)
        {
            scenes.Add(name, scene);
        }

        public void LoadScene(string name)
        {
            scenes[name].Run();
        }

    }
}
