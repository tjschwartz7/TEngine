using Microsoft.VisualBasic;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing.Drawing2D;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    /// <summary>
    /// This class handles everything related to the screen pixels.
    /// It is a static class, declared once in memory, because it should only ever exist once in any Application,
    /// and because we don't need to hold instances of it for anything.
    /// </summary>
    internal class ScreenRenderer
    {
        //For waiting
        private int _latency;
        //Are we still running?
        private bool _isRunning;

        //Our display strings
        private static string _standardHeader;
        private static string _battleHeader;
        private static string _foreground;
        private static string _background;
        private static string _footer;
        private static List<string> _options;
        private static List<string> _battleOptionsMenu = ["Attack", "Magic", "Skills", "Item", "Observe", "Escape"];

        //For options (user input)
        private static int _currentSelection;

        //Other
        private bool _pauseUI;
        private bool _resumeUI;
        private bool _isUIPaused;
        private static bool _needsRefresh;

        public static string StandardHeader 
        { 
            get => _standardHeader; 
            set
            {
                _standardHeader = value;
                _needsRefresh = true;
            } 
        }
        public static string BattleHeader 
        { 
            get => _battleHeader;
            set
            {
                _battleHeader = value;
                _needsRefresh = true;
            }
        }
        public static string Foreground 
        { 
            get => _foreground;
            set
            {
                _foreground = value;
                _needsRefresh = true;
            }
        }
        public static string Background 
        { 
            get => _background;
            set
            {
                _background = value;
                _needsRefresh = true;
            }
        }
        public static string Footer 
        { 
            get => _footer;
            set 
            { 
                _footer = value; 
                _needsRefresh = true;
            }
        }
        public static List<string> Options 
        { 
            get => _options;
            set
            { 
                _options = value; 
                _needsRefresh = true;
            }
        }

        public static List<string> BattleOptionsMenu
        {
            get => _battleOptionsMenu;
            set
            {
                _battleOptionsMenu = value;
                _needsRefresh = true;
            }
        }

        public static int CurrentSelection { get => _currentSelection;  set { _currentSelection = value; _needsRefresh = true; } }

        public int Latency { get => _latency; set => _latency = value; }

        public bool PauseUI { get => _pauseUI; set => _pauseUI = value; }
        public bool ResumeUI { get => _resumeUI; set => _resumeUI = value; }
        public bool IsUIPaused { get => _isUIPaused; set => _isUIPaused = value; }

        public ScreenRenderer()
        {

            _isRunning = true;
            _latency = 100;
            //We use 1D arrays for a quick speed boost

            //Create our screen rendering threads
            _ = Task.Run(() => RenderScreen());
        }

        /// <summary>
        /// Print out the screen.
        /// </summary>
        /// <param name="startIndex">The starting index of where to handle screen printing.</param>
        /// <param name="stopIndex">The final (included) index of where to handle screen printing.</param>
        private async void RenderScreen()
        {
            //This thread will end when Stop() is called
            while (_isRunning)
            {
                if(!_needsRefresh)
                {
                    Console.Clear();

                    Console.Write(Resolution.TerminalWidthLine);
                    Console.WriteLine($"{StandardHeader}".PadRight(Console.WindowWidth - 2) + "#");
                    Console.WriteLine($"{BattleHeader}".PadRight(Console.WindowWidth - 2) + "#");
                    Console.WriteLine($"{Background}".PadRight(Console.WindowWidth - 2) + "#");
                    Console.WriteLine($"{Foreground}".PadRight(Console.WindowWidth - 2) + "#");
                    Console.Write(Resolution.TerminalWidthLine);
                    if (Options != null)
                    {
                        for (int i = 0; i < Options.Count; i++)
                        {
                            string option = Options[i];
                            if (i == CurrentSelection)
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine($"#{option}".PadRight(Console.WindowWidth - 2) + "#");
                            Console.ResetColor();
                        }
                        Console.Write(Resolution.TerminalWidthLine);
                    }
                    Console.WriteLine($"{Footer}".PadRight(Console.WindowWidth - 2) + "#");
                    Console.Write(Resolution.TerminalWidthLine);
                }

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
                await Task.Delay(_latency); //Clear up a thread in the pool
            }
        }

        public void Stop()
        {
            _isRunning = false;
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
        public void SetTimeDelay(int numMillis)
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
        public void SetPauseUI()
        {
            _pauseUI = true;
        }

        /// <summary>
        /// Resume the UI after it's been paused.
        /// Only resumes if the UI is currently paused.
        /// </summary>
        public void SetResumeUI()
        {
            if (_isUIPaused)
                _resumeUI = true;
        }
    }
}
