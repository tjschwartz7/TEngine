using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    static class Application
    {
        private static Engine engine;

        static void Start()
        {
            engine = Engine.Instance;

            // Start the main loop
            Run();

        }

        private static void Run()
        {
            engine.Start();
            engine.Run();
        }


        public static void Quit()
        {
            EventManager.Instance.Trigger("QUIT");
        }
    }
}
