using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEngine.Rendering;
using TEngine.Utils;

using TEngine.Components;

namespace TEngine
{
    public class Engine
    {
        private List<GameObject> gameObjects = new List<GameObject>();
         
        public static Engine Instance { get; private set; } = new Engine();

        

        private float targetFps = 5f; // Target FPS    
        private float targetUps = 5f; // Target UPS

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
            EventManager.Instance.Subscribe("QUIT", OnQuit);

            // Initialize time tracking variables
            fixedUpdateAccumulator = 0f;
            fpsAccumulator = 0f;
            upsAccumulator = 0f;
            fps = 0;
            ups = 0;
            lastFpsUpdateTime = 0f;
            lastUpsUpdateTime = 0f;

            fixedUpdateTimeStep = 1000/targetFps; // Calculate the fixed update time step based on target UPS
            fpsStepTarget = 1000f / targetFps; // Calculate the time step for FPS tracking
            upsStepTarget = 1000f / targetUps; // Calculate the time step for UPS tracking

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

        public void Register(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {
            
        }

        public void LateUpdate()
        {

        }

        public void Render()
        {

        }

        public void Run()
        {
            // Start the main loop
            while (isRunning)
            {
                Time.Update(); // Update the time system
                Time.UpdateFixed(); // Update the fixed time system

                float time = Time.time;

                if (Time.deltaTime < fpsStepTarget)
                {
                    // Sleep for the remaining time
                    int sleepTime = (int)(fpsStepTarget - Time.time);
                    if (sleepTime > 0)
                        Thread.Sleep(sleepTime);
                }

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
                TextRenderer.Instance.Render(); // Render text using the TextRenderer
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
                ups = (int)(upsAccumulator / (upsStepTarget));
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
