using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Scenes
{
    public class Scene
    {
        string name;
        string description;

        public List<GameObject> gameObjects = new List<GameObject>();

        public Scene(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public virtual void OnLoad() { }
        public virtual void Run() { }
    }
}
