using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased
{
    internal class TextBasedEngine
    {
        private static string _primaryHeader;
        private static string _secondaryHeader;
        private static string _background;
        private static string _foreground;
        private static string _footer;
        //Input related
        private static bool _awaitingUserInput;
        private static string[] _selectOptions;
        private static int _selectedIndex;
        //Other
        private static bool _pauseUI;
        private static bool _resumeUI;
        private static bool _isUIPaused;

        public static string PrimaryHeader { get => _primaryHeader; set => _primaryHeader = value; }
        public static string SecondaryHeader { get => _secondaryHeader; set => _secondaryHeader = value; }
        public static string Background { get => _background; set => _background = value; }
        public static string Foreground { get => _foreground; set => _foreground = value; }
        public static bool AwaitingUserInput { get => _awaitingUserInput; set => _awaitingUserInput = value; }
        public static string[] SelectOptions { get => _selectOptions; set => _selectOptions = value; }
        public static int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }
        public static bool PauseUI { get => _pauseUI; set => _pauseUI = value; }
        public static bool ResumeUI { get => _resumeUI; set => _resumeUI = value; }
        public static bool IsUIPaused { get => _isUIPaused; set => _isUIPaused = value; }
        public static string Footer { get => _footer; set => _footer = value; }

        public static async Task Print()
        {
            int displayWidth = Resolution.ScreenWidth - 5; //Don't question the almighty 5
            Console.Clear();

            Console.Write(Resolution.TerminalWidthLine);
            Console.WriteLine($"# {PrimaryHeader.PadRight(displayWidth)} #");
            Console.WriteLine($"# {SecondaryHeader.PadRight(displayWidth)} #");
            Console.WriteLine($"# {Background.PadRight(displayWidth)} #");
            Console.WriteLine($"# {Foreground.PadRight(displayWidth)} #");
            Console.Write(Resolution.TerminalWidthLine);
            if (AwaitingUserInput)
            {
                if (SelectOptions != null)
                {
                    for (int i = 0; i < SelectOptions.Length; i++)
                    {
                        string option = SelectOptions[i];
                        if (i == SelectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine($"# {option.PadRight(displayWidth)} #");
                        Console.ResetColor();
                    }
                    Console.Write(Resolution.TerminalWidthLine);
                }
            }

            Console.WriteLine($"# {Footer.PadRight(displayWidth)} #");
            Console.Write(Resolution.TerminalWidthLine);
            if (PauseUI)
            {
                ResumeUI = false;
                PauseUI = false;
                IsUIPaused = true;
                //Busy sleep
                while (!ResumeUI)
                {
                    //Clear up this thread for a second while we're waiting
                    await Task.Delay(1000);
                }
            }

        }

        public static void Initialize()
        {
            _primaryHeader = "";
            _secondaryHeader = "";
            _selectOptions = [""];
            _background = "";
            _foreground = "";
            _footer = "";
            _awaitingUserInput = false;
            _isUIPaused = false;
            _pauseUI = false;
            _resumeUI = false;
            _selectedIndex = -1;
        }
    }
}
