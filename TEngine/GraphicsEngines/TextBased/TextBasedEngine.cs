using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    public class TextBasedEngine : GraphicsEngine
    {
        private static TextBasedEngine _engine;

        //Other
        private bool _pauseUI;
        private bool _resumeUI;
        private bool _isUIPaused;

        public bool PauseUI { get => _pauseUI; set => _pauseUI = value; }
        public bool ResumeUI { get => _resumeUI; set => _resumeUI = value; }
        public bool IsUIPaused { get => _isUIPaused; set => _isUIPaused = value; }


        public TextBasedEngine() : base(Style.TextBased, Console.WindowWidth, Console.WindowHeight, 60)
        {
            _pauseUI = false;
            _resumeUI = false;
            _isUIPaused = false;
            Console.WriteLine("TBE Constructor");
            OnStart();
            _ = Task.Run(() => Resolution.ScreenChangeHandler());
            _ = Task.Run(() => AsyncFrame());
        }

        public static void Begin(TextBasedEngine instance)
        {
            _engine = instance;
        }

        public string GetTerminalWidthLine()
        {
            return Resolution.TerminalWidthLine;
        }

        protected override void OnFrame()
        {
            base.OnFrame();

            if (PauseUI)
            {
                ResumeUI = false;
                PauseUI = false;
                IsUIPaused = true;
                //Busy sleep
                while (!ResumeUI)
                {
                    //Clear up this thread for a while while we're waiting
                    BlockThreadFor(500);
                }
            }
            BlockThreadFor(FrameDelay_ms);
            
        }

        public override void Stop()
        {
            base.Stop();
            
        }

        public static void TerminateWindow()
        {
            if(_engine != null)
                _engine.Stop();
        }

  

        protected async void BlockThreadFor(int numMillis)
        {
            await Task.Delay(numMillis);
        }


        /// <summary>
        /// Pauses the UI thread for some given number of milliseconds.
        /// CAUTION: If you use a millisecond timer that is around the time of the UI delay period,
        /// there is a risk that a race condition will leave the UI paused without resuming it.
        /// This occurs when you set both the Pause condition and Resume condition before the UI gets a chance to run
        /// once, which prevents it from entering its waiting state. In this situation, the UI will resume waiting
        /// until the Resume condition is set again.
        /// </summary>
        /// <param name="numMillis">The number of milliseconds to pause the UI for.</param>
        protected void PauseUIFor(int numMillis)
        {
            //Begin the function call then trash the result
            _ = HandleTimeDelay(numMillis);
        }

        /// <summary>
        /// The actual async handler for pausing the UI. System level only.
        /// </summary>
        /// <param name="numMillis">The number of milliseconds to pause the UI for.</param>
        /// <returns>A task for if you chose to await this for some reason.</returns>
        private async Task HandleTimeDelay(int numMillis)
        {
            _pauseUI = true;
            await Task.Delay(numMillis);
            _resumeUI = true;
        }

        /// <summary>
        /// Pause the UI. Must be paired with a SetResumeUI to resume the UI.
        /// </summary>
        protected void SetPauseUI()
        {
            _pauseUI = true;
        }

        /// <summary>
        /// Resume the UI after it's been paused.
        /// Only resumes if the UI is currently paused.
        /// </summary>
        protected void SetResumeUI()
        {
            if (_isUIPaused)
                _resumeUI = true;
        }
    }
}
