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

            SceneManager.Instance.Register("Main Menu", new MainMenu("Main Menu", "The titular title to the GoneMedieval Franchise."));
            SceneManager.Instance.LoadScene("Main Menu");
        }

        public override void OnKeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    EventManager.Instance.Trigger("QUIT");
                    break;
                case ConsoleKey.W:
                    EventManager.Instance.Trigger("KEY_W");
                    break;
                case ConsoleKey.S:
                    EventManager.Instance.Trigger("KEY_S");
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
