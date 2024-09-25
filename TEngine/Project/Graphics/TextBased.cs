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
        public TextBased(int screenHeight, int screenWidth, int numGrids) : base(screenHeight, screenWidth, numGrids) { }
        protected override void OnFrame()
        {
            base.OnFrame(); 

        }
    }
}
