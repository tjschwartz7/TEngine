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

        static void Main()
        {
            engine = Engine.Instance;
            EventManager.Instance.Subscribe("QUIT", OnQuit);

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
            isRunning = false;
            EventManager.Instance.Trigger("QUIT");
        }
    }

}
