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

        public static void Start()
        {
            engine = Engine.Instance;
        }

        public static void Run()
        {
            engine.Run();
        }


        public static void Quit()
        {
            EventManager.Instance.Trigger("QUIT");
        }
    }
}
