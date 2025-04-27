using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.EngineManagement;
using TEngine.EngineManagement.RenderingEngines;

namespace TEngine
{
    static class Application
    {
        private static Engine _gameEngine = Engine.Instance;

        public static void Start()
        {

            _gameEngine.Start();
        }

        public static void Run()
        {
            _gameEngine.Run();
        }


        public static void Quit()
        {
            EventManager.Instance.Trigger("QUIT");
        }

    }
}