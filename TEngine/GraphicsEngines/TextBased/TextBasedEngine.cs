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
    internal class TextBasedEngine : GraphicsEngine
    {
        private ScreenRenderer _renderer;
        private List<Tuple<int, int, int, int>> _boundaries;

        public TextBasedEngine(int screenWidth, int screenHeight) : base(Style.TextBased, screenWidth, screenHeight)
        {
            _renderer = new ScreenRenderer(screenWidth, screenHeight);
            _boundaries = new List<Tuple<int, int, int, int>>();
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
            _boundaries.Add(new Tuple<int, int, int, int>(topLeftRow, topLeftCol, bottomRightRow, bottomRightCol));

            //This will always be the index of the most recently inserted boundary
            return _boundaries.Count() - 1;
        }

        public void SetScreenOnBound(int boundaryIndex, string[] screen)
        {
            if(boundaryIndex < 0 || boundaryIndex >= _boundaries.Count)
            {
                MessageUtils.TerminateWithError("TextBasedEngine", "SetScreenOnBound","Set boundary does not exist!!");
            }
            Tuple<int,int,int,int> boundary = _boundaries.ElementAt(boundaryIndex);



            _renderer.UpdateScreen(, boundary.Item1, boundary.Item2, boundary.Item3, boundary.Item4);
        }


    }
}
