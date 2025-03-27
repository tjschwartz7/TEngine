using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GoneMedieval.Levels
{
    public class Scene
    {
        string name;
        string description;


        public Scene(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public virtual void Run() { }
    }
}
