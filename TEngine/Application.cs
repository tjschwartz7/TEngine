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
        internal protected static Application ApplicationInstance { get => _applicationInstance; }
        internal protected static GraphicsEngine GraphicsEngineInstance { get => _graphicsEngineInstance; set => _graphicsEngineInstance = value; }

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
                await Task.Run(() => OnUpdate());
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
            int numSections = -1;
            int targetFPS = -1;
            GraphicsEngine.Style engine = GraphicsEngine.Style.NoneSelected;
            foreach(var setting in settings) 
            {

                // Get the name and value attributes
                string? name = setting.Attribute("name")?.Value;
                string? value = setting.Attribute("value")?.Value;
                if (name == null || value == null) 
                {
                    TextWriter errorWriter = Console.Error;
                    errorWriter.WriteLine("InitializeSettings - Config file invalid!!");
                    Application.TerminateApplication();
                }

                switch (name)
                {
                    case "ScreenWidth":
                        screenWidth = int.Parse(value);

                        break;
                    case "ScreenHeight":
                        screenHeight = int.Parse(value);
                        break;
                    case "NumScreenSections":
                        numSections = int.Parse(value);
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

            {
                TextWriter errorWriter = Console.Error;
                if (screenWidth < 0)
                {
                    
                    errorWriter.WriteLine("InitializeSettings - Config file missing parameter 'ScreenWidth'!!");
                    Application.TerminateApplication();
                }
                else if (screenHeight < 0)
                {
                    errorWriter.WriteLine("InitializeSettings - Config file missing parameter 'ScreenHeight'!!");
                    Application.TerminateApplication();
                }
                else if (numSections < 0)
                {
                    errorWriter.WriteLine("InitializeSettings - Config file missing parameter 'NumScreenSections'!!");
                    Application.TerminateApplication();
                }
                else if (targetFPS < 0)
                {
                    errorWriter.WriteLine("InitializeSettings - Config file missing parameter 'TargetFPS'!!");
                    Application.TerminateApplication();
                }
                else if(engine == GraphicsEngine.Style.NoneSelected)
                {
                    errorWriter.WriteLine("InitializeSettings - Config file missing parameter 'GraphicsEngine'!!");
                    Application.TerminateApplication();
                }
            }

            switch (engine)
            {
                case GraphicsEngine.Style.TextBased:
                    GraphicsEngineInstance = new TextBased(screenHeight, screenWidth, numSections);
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
