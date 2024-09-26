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

        /// <summary>
        /// This code runs in the TextEngine and is designed to be used for calculating what goes on the screen. It runs once per frame.
        /// </summary>
        protected override void OnFrame()
        {
            base.OnFrame(); 

        }
    }
}
