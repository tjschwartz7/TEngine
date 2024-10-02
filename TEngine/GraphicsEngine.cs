using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public class GraphicsEngine
    {

        private string _debugInfo;
        private bool _showDebug;
        //Input related
        private bool _awaitingUserInput;
        private string[]? _selectOptions;
        private int _selectedIndex;


        //Application stuff
        private bool _isRunning;

        //Screen information
        private int _screenWidth;
        private int _screenHeight;

        //FPS stuff
        private int _targetFPS;
        private double _framesPerSecond;
        private double _targetFramesPerSecond;
        private int _updateDelay_ms;
        private int _targetFrames_low_ms;
        private int _targetFrames_high_ms;

        public string DebugInfo { get => _debugInfo; set { _debugInfo = value; } }
        public bool AwaitingUserInput { get => _awaitingUserInput; set => _awaitingUserInput = value; }
        public string[]? SelectOptions { get => _selectOptions; set => _selectOptions = value; }
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }
        public bool ShowDebug { get => _showDebug; set { _showDebug = value; } }
        public int ScreenWidth { get => _screenWidth; }
        public int ScreenHeight { get => _screenHeight;  }
        public int TargetFPS { get => _targetFPS; set => _targetFPS = value; }
        public double FramesPerSecond { get => _framesPerSecond; set => _framesPerSecond = value; }
        public double TargetFramesPerSecond { get => _targetFramesPerSecond; set => _targetFramesPerSecond = value; }
        public int UpdateDelay_ms { get => _updateDelay_ms; set => _updateDelay_ms = value; }
        public bool IsRunning { get => _isRunning; set => _isRunning = value; }


        public GraphicsEngine(Style gameStyle, int screenWidth, int screenHeight, int targetFPS) 
        {
            _isRunning = true;
            _gameStyle = gameStyle;
            _awaitingUserInput = false;
            _showDebug = false;
            _selectedIndex = -1;
            _selectOptions = null;
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _debugInfo = "";

        }
        public enum Style
        {
            NoneSelected = 0,
            TextBased,
            G_2D,
            G_3D,
        }

        private Style _gameStyle;
        public Style GameStyle
        {
            get => _gameStyle;
        }

        public void ToggleDebug() { _showDebug = !_showDebug; }

        protected async Task AsyncFrame()
        {
            Stopwatch frameTimer = new Stopwatch();
            Stopwatch temp = new Stopwatch();
            double elapsedSeconds = 0;
            int frames = 0;
            frameTimer.Start();
            while (IsRunning)
            {
                await Task.Run(() => OnFrame());
                elapsedSeconds = frameTimer.Elapsed.TotalSeconds;
                frames++;

                //One second has passed
                if (elapsedSeconds > 1.0)
                {
                    // Calculate FPS as the number of frames rendered in the last second
                    _framesPerSecond = ((double)frames / elapsedSeconds);
                    frames = 0;

                    //PID Loop for frames

                    //Not quite enough frames
                    if (_framesPerSecond < _targetFrames_low_ms)
                    {
                        //Decrease delay time
                        if (_updateDelay_ms > 0)
                            _updateDelay_ms--;
                    }
                    //Too many frames
                    else if (_framesPerSecond > _targetFrames_high_ms)
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

        public virtual void Stop()
        {
            _isRunning = false;
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnFrame()
        {

        }

    }
}
