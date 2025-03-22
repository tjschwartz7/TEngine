using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine2.Behavior;

namespace TEngine
{
    public class Engine
    {
        private List<Monobehavior> behaviors = new();

        public static Engine Instance { get; private set; } = new Engine();
        private static EventManager eventManager = EventManager.Instance;

        

        private float targetFps = 60f; // Target FPS    
        private float targetUps = 60f; // Target UPS

        private float fpsStepTarget;
        private float upsStepTarget;
        private float fixedUpdateTimeStep;

        private float fixedUpdateAccumulator;
        private float fpsAccumulator;
        private float upsAccumulator;

        private int fps;
        private int ups;

        private float lastFpsUpdateTime;
        private float lastUpsUpdateTime;

        private bool isRunning = true;

        Engine()
        {
            eventManager.Subscribe("QUIT", OnQuit);

            // Initialize time tracking variables
            fixedUpdateAccumulator = 0f;
            fpsAccumulator = 0f;
            upsAccumulator = 0f;
            fps = 0;
            ups = 0;
            lastFpsUpdateTime = 0f;
            lastUpsUpdateTime = 0f;

            fixedUpdateTimeStep = 1/targetFps; // Calculate the fixed update time step based on target UPS
            fpsStepTarget = 1f / targetFps; // Calculate the time step for FPS tracking
            upsStepTarget = 1f / targetUps; // Calculate the time step for UPS tracking

            if (Instance != null)
            {
                throw new Exception("Engine instance already exists.");
            }
            else 
            {
                Instance = this;
            }

            // Initialize the time system
            Time.Initialize();
        }

        public void Register(Monobehavior behavior)
        {
            behaviors.Add(behavior);
        }

        public void Start()
        {
            Console.WriteLine("Engine Starting...");

            // Call Start() on all registered behaviors
            foreach (var behavior in behaviors)
            {
                behavior.Start();
            }

        }

        public void Update()
        {
            foreach (var behavior in behaviors)
            {
                behavior.Update();
            }
        }

        public void FixedUpdate()
        {
            foreach (var behavior in behaviors)
            {
                behavior.FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            foreach (var behavior in behaviors)
            {
                behavior.LateUpdate();
            }
        }

        public void Render()
        {
            foreach (var behavior in behaviors)
            {
                behavior.OnGUI();
            }
        }

        public void Run()
        {
            // Start the main loop
            while (isRunning)
            {
                Time.Update(); // Update the time system
                Time.UpdateFixed(); // Update the fixed time system

                // Call Update, FixedUpdate, and LateUpdate
                Update();

                // Handle fixed updates with the fixed time step
                fixedUpdateAccumulator += Time.fixedDeltaTime;
                while (fixedUpdateAccumulator >= fixedUpdateTimeStep)
                {
                    FixedUpdate();
                    fixedUpdateAccumulator -= fixedUpdateTimeStep;
                }


                // Handle FPS and UPS tracking
                CalculatePerformanceMetrics();

                LateUpdate();

                Render();
            }
        }

        // Calculate FPS, UPS, and update the values
        private void CalculatePerformanceMetrics()
        {
            // FPS Calculation
            fpsAccumulator += Time.deltaTime;
            if (Time.time - lastFpsUpdateTime >= fpsStepTarget) // Update FPS every second
            {
                fps = (int)(fpsAccumulator / (Time.time - lastFpsUpdateTime));
                fpsAccumulator = 0f;
                lastFpsUpdateTime = Time.time;
                Console.WriteLine($"FPS: {fps}");
            }

            // UPS Calculation
            upsAccumulator += Time.deltaTime;          
            if (Time.time - lastUpsUpdateTime >= upsStepTarget) // Update UPS based on fixed time step
            {
                ups = (int)(upsAccumulator / upsStepTarget);
                upsAccumulator = 0f;
                lastUpsUpdateTime = Time.time;
                Console.WriteLine($"UPS: {ups}");
            }
        }


        private void OnQuit()
        {
            isRunning = false;
        }

        public int FPS => fps;
        public int UPS => ups;
        public float TargetFps => targetFps;
        public float TargetUps => targetUps;    
        public void SetTargetUps(float targetUps)
        {
            this.targetUps = targetUps; 
            fpsStepTarget = 1f / targetFps; 
            fixedUpdateTimeStep = 1 / targetFps; 
        }
        public void SetTargetFps(float targetFps) 
        { 
            this.targetFps = targetFps; 
            upsStepTarget = 1f / targetUps; 
        }

    }

}
