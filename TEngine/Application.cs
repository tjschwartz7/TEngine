using System;
using System.Collections.Generic;
using System.Linq;
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
            //Call user initialization code
            InitializeSettings();
            OnStart();
            //Create all of our threads
            CreateThreads();
        }
        private async Task AsyncUpdate()
        {
            while(_isRunning) 
            {
                Task updater = Task.Run(() => OnUpdate());
                await Task.Delay(1000);
            }
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
