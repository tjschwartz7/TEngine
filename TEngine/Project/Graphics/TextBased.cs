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
        public TextBased(int screenHeight, int screenWidth) : base(screenHeight, screenWidth) { }

        protected override void OnStart()
        {
            base.OnStart();
            AddBoundary(0, 0, 3, 99);
            AddBoundary(4, 0, 99, 99);
        }

        /// <summary>
        /// This code runs in the TextEngine and is designed to be used for calculating what goes on the screen. It runs once per frame.
        /// </summary>
        protected override void OnFrame()
        {
            base.OnFrame();
            SetScreenOnBound(0, [Resolution.TerminalWidthLine, $"FPS: {Application.GetFPS()}", Resolution.TerminalWidthLine]);
            

        }
    }
}
