using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased.ScreenElements;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    internal class TextBasedEngine : GraphicsEngine
    {
        private ScreenRenderer _renderer;
        private Boundaries _bounds;

        public TextBasedEngine(int screenWidth, int screenHeight) : base(Style.TextBased, screenWidth, screenHeight)
        {
            _renderer = new ScreenRenderer(screenWidth, screenHeight);
            _bounds = new Boundaries();

        }

        public async Task Print()
        {
            while(Application.IsRunning())
            {
                Console.Clear();
                OnFrame();
                //$"\033[{i};0H{_screen[row, col]}"

                //This is what controls our framerate
                await Task.Delay(Application.GetTargetLatency());

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
        }

        /// <summary>
        /// Adds a new boundary and returns its index. 
        /// </summary>
        /// <param name="topLeftRow">The top left row of our selection.</param>
        /// <param name="topLeftCol">The top left column of our selection.</param>
        /// <param name="bottomRightRow">The bottom right row of our selection.</param>
        /// <param name="bottomRightCol">The bottom right column of our selection.</param>
        /// <returns>
        /// The index of the newly created boundary. 
        /// It's recommended that you keep track of these indices for your own usage, and perhaps make an enum to map the number to what it is.
        /// </returns>
        public int AddBoundary(int topLeftRow, int topLeftCol, int bottomRightRow, int bottomRightCol)
        {
            return _bounds.AddBoundary(topLeftRow, topLeftCol, bottomRightRow, bottomRightCol);
        }

        public void SetScreenOnBound(int boundaryIndex, string[] screen)
        {
            if(boundaryIndex < 0 || boundaryIndex >= _bounds.Count())
            {
                MessageUtils.TerminateWithError("TextBasedEngine", "SetScreenOnBound","Set boundary does not exist!!");
            }
            Tuple<int,int,int,int> boundary = _bounds.GetBound(boundaryIndex);



            //_renderer.UpdateScreen(, boundary, _bounds.getNumRows(boundaryIndex), _bounds.getNumCols(boundaryIndex));
        }


    }
}
