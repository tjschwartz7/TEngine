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
            //Run any level code here. Nice.
            Console.WriteLine("Welcome to Gone Medieval!");
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();
        }
    }
}
