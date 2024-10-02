using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;
using TEngine.Project;
using System.Xml.Linq;
using TEngine.Project.Graphics;
using TEngine.Helpers;

namespace TEngine
{
    internal class Application
    {
        private static Application _applicationInstance;
        //Engine stuff
        private static GraphicsEngine _graphicsEngineInstance;
        private GraphicsEngine.Style _engine;
        private int _screenWidth;
        private int _screenHeight;
        //Inputs
        private static InputEngine.InputHandler _inputHandlerInstance;

        //UPS stuff
        private double _updatesPerSecond;
        private double _targetUpdatesPerSecond;
        private int _updateDelay_ms;
        private int _targetUpdates_low_ms;
        private int _targetUpdates_high_ms;


        //Program stuff
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
            _isRunning = true;
            _stopProgram = false;

            //Call user initialization code
            InitializeSettings();
            //Set important variables
            _updateDelay_ms =(int)(1000 / (double)_targetUpdatesPerSecond); //Assume starting out that update loop takes 0 seconds
            _targetUpdates_low_ms = (int)((double)_targetUpdatesPerSecond * .9);
            _targetUpdates_high_ms = (int)((double)_targetUpdatesPerSecond * 1.1);
            OnStart();

            //Create all of our threads
            _inputHandlerInstance = new InputEngine.InputHandler();
            _ = Task.Run(() => AsyncUpdate());
            _ = Task.Run(() => Resolution.ScreenChangeHandler());
        }
        private async Task AsyncUpdate()
        {
            Stopwatch updateTimer = new Stopwatch();
            Stopwatch temp = new Stopwatch();
            double elapsedSeconds = 0;
            int updates = 0;
            updateTimer.Start();
            while (_isRunning) 
            {
                Task updateTask;
                updateTask = Task.Run(() => OnUpdate());
                if(_errorFlag) await Task.Run(() => OnNewError()); 
                if (_statusFlag) await Task.Run(() => OnNewStatus());
                await updateTask; //Let the other threads start before we await.
                elapsedSeconds = updateTimer.Elapsed.TotalSeconds;
                updates++;

                //One second has passed
                if(elapsedSeconds > 1.0)
                {
                    // Calculate UPS as the number of updates rendered in the last second
                    _updatesPerSecond = ((double)updates / elapsedSeconds);
                    updates = 0;

                    //PID Loop for frames

                    //Not quite enough frames
                    if(_updatesPerSecond < _targetUpdates_low_ms)
                    {
                        //Decrease delay time
                        if(_updateDelay_ms > 0)
                            _updateDelay_ms--;
                    }
                    //Too many frames
                    else if(_updatesPerSecond > _targetUpdates_high_ms)
                    {
                        //Increase delay time
                        _updateDelay_ms++;
                    }
                    updateTimer.Restart();
                }
                await Task.Delay(_updateDelay_ms);
            }
            updateTimer.Stop();
        }

        private void InitializeSettings()
        {
            var doc = XDocument.Load("../../../../TEngine/Project/config.xml");
            // Get all settings
            var settings = doc.Descendants("setting").ToArray(); // Convert to array for indexing

            int screenWidth = -1;
            int screenHeight = -1;
            int targetFPS = -1;
            int targetUPS = -1;
            _engine = GraphicsEngine.Style.NoneSelected;
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
                    case "TargetUPS":
                        targetUPS = int.Parse(value);
                        break;
                    case "GraphicsEngine":
                        switch (value)
                        {
                            case "TextBased":
                                _engine = GraphicsEngine.Style.TextBased;
                                break;
                            case "G_2D":
                                _engine = GraphicsEngine.Style.G_2D;
                                break;
                            case "G_3D":
                                _engine = GraphicsEngine.Style.G_3D;
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
                else if (targetUPS < 0)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'TargetUPS'!!");
                }
                else if(_engine == GraphicsEngine.Style.NoneSelected)
                {
                    MessageUtils.TerminateWithError("Application", "InitializeSettings", "Config file missing parameter 'GraphicsEngine'!!");
                }
                
            }

            _screenHeight = screenHeight;
            _screenWidth = screenWidth; 

            switch (_engine)
            {
                case GraphicsEngine.Style.TextBased:
                    _graphicsEngineInstance = new TextBased(_screenHeight, _screenWidth, targetFPS);
                    break;
                case GraphicsEngine.Style.G_2D:
                    break;
                case GraphicsEngine.Style.G_3D:
                    break;
            }

            SetTargetUPS(targetUPS);
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

        private void Stop()
        {
            //Perform cleanup here
            GraphicsEngineInstance.Stop();
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

        protected void SetTargetUPS(double fps)
        {
            _targetUpdatesPerSecond = fps;
        }

        public static double GetUPS()
        {
            return _applicationInstance._updatesPerSecond;
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

        protected bool KeyPressed(ConsoleKey key) { return _inputHandlerInstance.KeyPressed(key); }

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
