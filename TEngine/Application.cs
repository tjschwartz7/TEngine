using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;
using TEngine.Project;

namespace TEngine
{
    internal class Application
    {
        private static Application _applicationInstance;
        private static GraphicsEngine _graphicsEngineInstance;
        private double _framesPerSecond;
        private double _targetFramesPerSecond;
        private double _updateDelay_ms;
        private bool _stopProgram;
        private bool _isRunning;
        internal protected static Application ApplicationInstance { get => _applicationInstance; }
        internal protected static GraphicsEngine GraphicsEngineInstance { get => _graphicsEngineInstance; set => _graphicsEngineInstance = value; }

        public Application() 
        {
            Begin(); 
        }
        public void Begin()
        {
            _isRunning = true;
            _updateDelay_ms = 100; 
            //Call user initialization code
            InitializeSettings();
            OnStart();
            //Create all of our threads
            CreateThreads();
        }
        private async Task AsyncUpdate()
        {
            Stopwatch watch = new Stopwatch();
            double elapsedSeconds = 0;
            int frames = 0;
            watch.Start();
            while (_isRunning) 
            {
                await Task.Run(() => OnUpdate());
                elapsedSeconds = watch.Elapsed.TotalSeconds;
                frames++;

                //One second has passed
                if(elapsedSeconds > 1.0)
                {

                    // Calculate FPS as the number of frames rendered in the last second
                    _framesPerSecond = frames / elapsedSeconds;

                    frames = 0;
                    watch.Restart();

                    //Not quite enough frames
                    if(_framesPerSecond < _targetFramesPerSecond)
                    {
                        //Decrease delay time
                        if(_updateDelay_ms > 0)
                            _updateDelay_ms--;
                    }
                    //Too many frames
                    else if(_framesPerSecond > _targetFramesPerSecond)
                    {
                        //Increase delay time
                        _updateDelay_ms++;
                    }
                }

                await Task.Delay(1000);
            }
            watch.Stop();
        }

        protected virtual void InitializeSettings()
        {

        }

        protected virtual void OnStart()
        {

        }
        protected virtual void OnUpdate()
        {

        }

        private void CreateThreads()
        {
            Task updateHandler = Task.Run(() => AsyncUpdate());
            Task keylogger = Task.Run(() => UserInput.InputHandler.KeyReader());
            Task screenSizeHandler = Task.Run(() => Resolution.ScreenChangeHandler());

        }

        private void Stop() 
        {
            //Perform cleanup here
        }

        public static bool IsRunning() { return Application.ApplicationInstance._isRunning; }
        public static void TerminateApplication()
        { 
            Application.ApplicationInstance._stopProgram = true;
            Application.ApplicationInstance._isRunning = false;
            Application.ApplicationInstance.Stop();
        }

        protected void SetTargetFPS(double fps)
        {
            _targetFramesPerSecond = fps;
        }

        public double GetFPS()
        {
            return _framesPerSecond;
        }

        static async Task Main()
        {
            //Plug in your chosen graphics engine
            Application._applicationInstance = new Primary();



            //Non-busy waiting because this loop doesnt matter
            //Use tasks to keep our thread pool ready and waiting for use
            while (Application.IsRunning()) { await Task.Delay(1 * 1000); }

        }
    }
}
