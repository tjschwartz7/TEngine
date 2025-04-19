using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEngine.Utils;

using TEngine.Components;
using TEngine.Components.Renderers;
using TEngine.EngineManagement.Scenes;
using TEngine.EngineManagement.RenderingEngines;

namespace TEngine.EngineManagement
{
    public class Engine
    {
        
         
        public static Engine Instance { get; private set; } = new Engine();
        public static RenderType RenderType { get; private set; } = RenderType.Text;
        public static RenderSystem Renderer { get; private set; } = RenderSystem.Default;
        public static SceneManager SceneManager { get; private set; } = SceneManager.Instance;


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

        // Set the rendering type (Text, T2D, or T3D)
        public void SetRenderer(RenderType type)
        {
            RenderType = type;
            switch (type)
            {
                case RenderType.Text:
                    Renderer = RenderSystem.Default; // Set the default renderer
                    break;
                case RenderType.T2D:
                    // Add logic for 2D renderer
                    break;
                case RenderType.T3D:
                    // Add logic for 3D renderer
                    break;
            }
        }

        public void Start()
        {
            // Loop over each GameObject in the scene and start their lifecycle
            foreach (var gameObject in SceneManager.GetAllGameObjects())
            {
                gameObject.StartLifecycle(); // Initialize each GameObject's lifecycle
            }
        }

        public void Update()
        {
            foreach (var gameObject in SceneManager.GetAllGameObjects())
            {
                gameObject.UpdateLifecycle(); // Initialize each GameObject's lifecycle
            }
        }

        public void FixedUpdate()
        {
            foreach (var gameObject in SceneManager.GetAllGameObjects())
            {
                gameObject.FixedUpdateLifecycle(); // Initialize each GameObject's lifecycle
            }
        }

        public void LateUpdate()
        {
            foreach (var gameObject in SceneManager.GetAllGameObjects())
            {
                gameObject.LateUpdateLifecycle(); // Initialize each GameObject's lifecycle
            }
        }

        public void Render()
        {
            // Handle the rendering logic directly here
            if (Renderer != null)
            {
                // Call the renderer form to display the current game state
                Renderer.Render(SceneManager.GetRenderableGameObjects());  // Pass renderable GameObjects to the renderer
            }
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

                InputManager.Instance.UpdateInput(); // Update input system

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
    public enum RenderType
    {
        Text,
        T2D,
        T3D
    }
}
