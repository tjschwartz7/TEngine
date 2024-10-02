using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;


namespace TEngine.Project.Graphics
{
    internal class TextBased : TextBasedEngine
    {
        public TextBased(int screenHeight, int screenWidth, int targetFPS) : base(screenHeight, screenWidth, targetFPS) { }

        protected override void OnStart()
        {
            base.OnStart();
            int bound1 = AddBoundary(0, 0, 2, 99);
            int bound2 = AddBoundary(3, 0, 99, 99);

            
        }

        /// <summary>
        /// This code runs in the TextEngine and is designed to be used for calculating what goes on the screen. It runs once per frame.
        /// </summary>
        protected override void OnFrame()
        {
            base.OnFrame(); //The parent OnFrame handles UI blocking when the UI is paused

            SetScreenOnBound(0, [Resolution.TerminalWidthLine, $"FPS: {FramesPerSecond}", Resolution.TerminalWidthLine]);

            Random rand = new Random();
            int row = rand.Next(3, 99);
            int col = rand.Next(0, 99);
            char pixel = (char)rand.Next(((int)'a'), ((int)'z'));
            UpdatePixel(pixel, row, col);

        }
    }
}
