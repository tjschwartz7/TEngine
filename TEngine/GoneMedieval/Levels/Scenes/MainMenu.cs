using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GoneMedieval.Levels.Scenes
{
    public class MainMenu : Scene
    {
        public MainMenu(string name, string description) : base(name, description)
        {

        }

        public override void Run()
        {
            EventManager.Instance.Trigger("UPDATE_MENU", new string[] { "New Game", "Load Game", "Options", "Quit" });
        }
    }
}
