using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine2.Behavior;

namespace TEngine
{
    public static class Engine
    {
        private static List<Monobehavior> behaviors = new();
        private static bool isRunning = true;

        public static void Register(Monobehavior behavior)
        {
            behaviors.Add(behavior);
        }

        public static void Awake()
        {
            foreach(var behavior in behaviors)
            {
                behavior.Awake();
            }
        }

        public static void Start()
        {
            Console.WriteLine("Engine Starting...");

            // Call Start() on all registered behaviors
            foreach (var behavior in behaviors)
            {
                behavior.Start();
            }

            // Run Update loop
            while (isRunning)
            {
                foreach (var behavior in behaviors)
                {
                    behavior.Update();
                }

                Thread.Sleep(16); // Simulate frame time (about 60 FPS)
            }
        }

        public static void Stop()
        {
            isRunning = false;
            Console.WriteLine("Engine Stopping...");
        }
    }

}
