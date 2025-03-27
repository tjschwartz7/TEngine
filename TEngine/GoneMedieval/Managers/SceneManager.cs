using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GoneMedieval.Scenes;
using TEngine2.Behavior;

namespace TEngine.GoneMedieval.Managers
{
    public class SceneManager : Monobehavior
    {
        public static SceneManager Instance { get; private set; } = new SceneManager();
        int scene = 1;
        private List<Scene> scenes;
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
            scenes = new List<Scene>();
        }

        public override void Start()
        {

        }

        public void Register(Scene scene)
        {
            scenes.Add(scene);
        }

    }
}
