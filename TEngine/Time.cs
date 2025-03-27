using System;

namespace TEngine
{
    public static class Time
    {
        // Normal time variables
        public static float time { get; private set; }
        public static float deltaTime { get; private set; }
        public static float unscaledDeltaTime { get; private set; }
        public static float timeScale { get; set; } = 1.0f;  // Default to normal time scale (no scaling)

        // Fixed time variables
        public static float fixedDeltaTime { get; private set; }
        public static float unscaledFixedDeltaTime { get; private set; }
        public static float fixedTime { get; private set; }

        // Time tracking
        private static float lastTime;
        private static float lastFixedTime;

        // Initialize time
        public static void Initialize()
        {
            lastTime = GetTime();
            lastFixedTime = GetTime();
            fixedDeltaTime = 1000f/60f; // Default to 60 updates per second (or 0.01 seconds per update)
        }

        // Update time based on real time
        public static void Update()
        {
            float currentTime = GetTime();
            deltaTime = currentTime - lastTime;
            unscaledDeltaTime = currentTime - lastTime; // Unscaled time is based on real time, not timeScale

            // Apply time scale for normal time
            deltaTime *= timeScale;

            // Update normal time
            time += deltaTime;
            lastTime = currentTime;
        }

        // Update fixed time
        public static void UpdateFixed()
        {
            float currentFixedTime = GetTime();
            fixedDeltaTime = currentFixedTime - lastFixedTime;
            unscaledFixedDeltaTime = currentFixedTime - lastFixedTime; // Unscaled fixed time is based on real time

            // Apply time scale for fixed time
            fixedDeltaTime *= timeScale;

            // Update fixed time
            fixedTime += fixedDeltaTime;
            lastFixedTime = currentFixedTime;
        }

        // Get the current time in seconds (you can use System.Time or any other system to measure time)
        private static float GetTime()
        {
            return (float)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
