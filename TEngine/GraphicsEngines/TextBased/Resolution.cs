using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased
{
    internal class Resolution
    {
        private static int _screenWidth;
        private static int _screenHeight;
        private static string _terminalWidthLine;

        public static string TerminalWidthLine { get => _terminalWidthLine; }
        public static int ScreenWidth { get => _screenWidth; }
        public static int ScreenHeight { get => _screenHeight; }

        private static void CalculateLineAcrossScreen()
        {
            _terminalWidthLine = "";
            for (int i = 1; i < _screenWidth; i++)
            {
                _terminalWidthLine += '#';
            }
            _terminalWidthLine += "\n";
        }

        public static async Task ScreenChangeHandler()
        {
            while (Application.IsRunning())
            {
                // Create a background task
                Task backgroundTask = Task.Run(() => HandleScreenSizeChanges());
                await Task.Delay(1000);
            }
        }

        private static void HandleScreenSizeChanges()
        {
            bool heightChanged = _screenHeight != Console.WindowHeight;
            bool widthChanged = _screenWidth != Console.WindowWidth;
            if (heightChanged || widthChanged)
            {
                _screenHeight = Console.WindowHeight;
                _screenWidth = Console.WindowWidth;
                CalculateLineAcrossScreen();
            }
        }


    }
}
