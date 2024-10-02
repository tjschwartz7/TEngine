using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;
using TEngine.Helpers;

namespace TEngine
{
    public class Application
    {
        //Inputs
        private static InputEngine.InputHandler _inputHandlerInstance;
        private static Application _applicationInstance;

        //UPS stuff
        private static double _updatesPerSecond;
        private static double _targetUpdatesPerSecond;
        private static int _updateDelay_ms;
        private static int _targetUpdates_low_ms;
        private static int _targetUpdates_high_ms;


        //Program stuff
        private static bool _stopProgram;
        private static bool _isRunning;
        

        //Result handler
        private static string _errorMessage;
        private static bool _errorFlag;
        private static string _statusMessage;
        private static bool _statusFlag;

        //Getters and setters
        private static InputEngine.InputHandler InputHandlerInstance { get => _inputHandlerInstance; }
        private static Application ApplicationInstance { get => _applicationInstance; }
        public static string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public static string StatusMessage { get => _statusMessage; set => _statusMessage = value; }

        public static void Begin(Application instance)
        {
            //Basic program stuff
            _isRunning = true;
            _stopProgram = false;

            //Initialize some variables
            _errorMessage = "";
            _statusMessage = "";

            _errorFlag = false;
            _statusFlag = false;
            _applicationInstance = instance;

            _targetUpdatesPerSecond = 60;

            _updateDelay_ms =(int)(1000 / (double)_targetUpdatesPerSecond); //Assume starting out that update loop takes 0 seconds
            _targetUpdates_low_ms = (int)((double)_targetUpdatesPerSecond * .9);
            _targetUpdates_high_ms = (int)((double)_targetUpdatesPerSecond * 1.1);
            _applicationInstance.OnStart();

            //Create all of our threads
            _inputHandlerInstance = new InputEngine.InputHandler();
            _ = Task.Run(() => _applicationInstance.AsyncUpdate());
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

        protected virtual void Stop()
        {
            //Perform cleanup here
        }

        public static void TerminateApplication()
        {
            _stopProgram = true;
            _isRunning = false;
            if(_applicationInstance != null)
                _applicationInstance.Stop();
        }

        public static bool IsRunning() { return _isRunning; }


        public static int GetTargetLatency()
        {
            return _updateDelay_ms;
        }

        protected void SetTargetUPS(double fps)
        {
            _targetUpdatesPerSecond = fps;
        }

        public static double GetUPS()
        {
            return _updatesPerSecond;
        }

        /// <summary>
        /// Notify that an error has occurred.
        /// </summary>
        public static void SetErrorFlag()
        {
            _errorFlag = true;
        }

        /// <summary>
        /// Notify that a new status message is available.
        /// </summary>
        public static void SetStatusFlag()
        {
            _statusFlag = true;
        }

        protected bool KeyPressed(ConsoleKey key) { return _inputHandlerInstance.KeyPressed(key); }
    }
}
