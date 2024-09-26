using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;
using TEngine.Project;
using TEngine.Project.UserInput;
using System.Xml.Linq;
using TEngine.Project.Graphics;
using TEngine.Helpers;

namespace TEngine
{
    internal class Application
    {
        private static Application _applicationInstance;
        private static GraphicsEngine _graphicsEngineInstance;
        private static InputEngine.InputHandler _inputHandlerInstance;
        private double _framesPerSecond;
        private double _targetFramesPerSecond;
        private int _updateDelay_ms;
        private int _targetFrames_low_ms;
        private int _targetFrames_high_ms;
        private bool _stopProgram;
        private bool _isRunning;

        //Result handler
        private string _errorMessage;
        private bool _errorFlag;
        private string _statusMessage;
        private bool _statusFlag;
        internal protected static Application ApplicationInstance { get => _applicationInstance; }
        internal protected static GraphicsEngine GraphicsEngineInstance { get => _graphicsEngineInstance; }
        internal protected static InputEngine.InputHandler InputHandlerInstance { get => _inputHandlerInstance; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public string StatusMessage { get => _statusMessage; set => _statusMessage = value; }

        public Application() 
        {
            Begin(); 
        }
        public void Begin()
        {
            _inputHandlerInstance = new UserInput();
            _isRunning = true;
            _stopProgram = false;
            //Call user initialization code
            InitializeSettings();
            OnStart();
            //Set important variables
            _updateDelay_ms =(int)(1000 / (double)_targetFramesPerSecond); //Assume starting out that update loop takes 0 seconds
            _targetFrames_low_ms = (int)((double)_targetFramesPerSecond * .9);
            _targetFrames_high_ms = (int)((double)_targetFramesPerSecond * 1.1);
            //Create all of our threads
            CreateThreads();
        }
        private async Task AsyncUpdate()
        {
            Stopwatch frameTimer = new Stopwatch();
            Stopwatch temp = new Stopwatch();
            double elapsedSeconds = 0;
            int frames = 0;
            frameTimer.Start();
            while (_isRunning) 
            {
                Task updateTask;
                updateTask = Task.Run(() => OnUpdate());
                if(_errorFlag) await Task.Run(() => OnNewError()); 
                if (_statusFlag) await Task.Run(() => OnNewStatus());
                await updateTask;
                elapsedSeconds = frameTimer.Elapsed.TotalSeconds;
                frames++;

                //One second has passed
                if(elapsedSeconds > 1.0)
                {
                    // Calculate FPS as the number of frames rendered in the last second
                    _framesPerSecond = ((double)frames / elapsedSeconds);
                    frames = 0;

                    //PID Loop for frames

                    //Not quite enough frames
                    if(_framesPerSecond < _targetFrames_low_ms)
                    {
                        //Decrease delay time
                        if(_updateDelay_ms > 0)
                            _updateDelay_ms--;
                    }
                    //Too many frames
                    else if(_framesPerSecond > _targetFrames_high_ms)
                    {
                        //Increase delay time
                        _updateDelay_ms++;
                    }
                    frameTimer.Restart();
                }
                await Task.Delay(_updateDelay_ms);
            }
            frameTimer.Stop();
        }

        private void InitializeSettings()
        {
            var doc = XDocument.Load("../../../../TEngine/Project/config.xml");
            // Get all settings
            var settings = doc.Descendants("setting").ToArray(); // Convert to array for indexing

            int screenWidth = -1;
            int screenHeight = -1;
            int targetFPS = -1;
            GraphicsEngine.Style engine = GraphicsEngine.Style.NoneSelected;
            foreach(var setting in settings) 
            {

                // Get the name and value attributes
                string? name = setting.Attribute("name")?.Value;
                string? value = setting.Attribute("value")?.Value;
                if (name == null || value == null) 
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file invalid!!");
                }

                switch (name)
                {
                    case "ScreenWidth":
                        screenWidth = int.Parse(value);

                        break;
                    case "ScreenHeight":
                        screenHeight = int.Parse(value);
                        break;
                    case "TargetFPS":
                        targetFPS = int.Parse(value);
                        break;
                    case "GraphicsEngine":
                        switch (value)
                        {
                            case "TextBased":
                                engine = GraphicsEngine.Style.TextBased;
                                break;
                            case "G_2D":
                                engine = GraphicsEngine.Style.G_2D;
                                break;
                            case "G_3D":
                                engine = GraphicsEngine.Style.G_3D;
                                break;
                        }

                        break;

                }
            }

            //Handle errors in the config file
            //The conditions here express configs that MUST be contained in the config file for everything to work
            {
                if (screenWidth < 0)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'ScreenWidth'!!");
                }
                else if (screenHeight < 0)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'ScreenHeight'!!");
                }
                else if (targetFPS < 0)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'TargetFPS'!!");
                }
                else if(engine == GraphicsEngine.Style.NoneSelected)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'GraphicsEngine'!!");
                }
            }

            switch (engine)
            {
                case GraphicsEngine.Style.TextBased:
                    _graphicsEngineInstance = new TextBased(screenHeight, screenWidth);
                    break;
                case GraphicsEngine.Style.G_2D:
                    break;
                case GraphicsEngine.Style.G_3D:
                    break;
            }

            SetTargetFPS(targetFPS); //Set your target FPS here (can be changed dynamically later)
        }

        protected virtual void OnStart()
        {

        }
        protected virtual void OnUpdate()
        {

        }

        protected virtual void OnNewError()
        {
            _errorFlag = false;
        }
        protected virtual void OnNewStatus()
        {
            _statusFlag = false;
        }

        private void CreateThreads()
        {
            Task updateHandler = Task.Run(() => AsyncUpdate());
            Task keylogger = Task.Run(() => _inputHandlerInstance.KeyReader());
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

        public static int GetTargetLatency()
        {
            return Application._applicationInstance._updateDelay_ms;
        }

        protected void SetTargetFPS(double fps)
        {
            _targetFramesPerSecond = fps;
        }

        public double GetFPS()
        {
            return _framesPerSecond;
        }

        /// <summary>
        /// Notify that an error has occurred.
        /// </summary>
        public void SetErrorFlag()
        {
            _errorFlag = true;
        }

        /// <summary>
        /// Notify that a new status message is available.
        /// </summary>
        public void SetStatusFlag()
        {
            _statusFlag = true;
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
