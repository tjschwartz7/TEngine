using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GoneMedieval.Levels.Scenes;
using TEngine.GoneMedieval.Managers;

namespace TEngine.GoneMedieval
{
    internal class GoneMedieval : Game
    {
        public override void Initialize()
        {
            Engine.Instance.Register(new UI.UI());

            SceneManager.Instance.Register(new MainMenu("Main Menu", "The titular title to the GoneMedieval Franchise."));
        }

        public override void OnKeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    EventManager.Instance.Trigger("QUIT");
                    break;
            }

        }

        static void Main()
        {
            GoneMedieval game = new GoneMedieval();
            game.Start();
        }
    }
}
