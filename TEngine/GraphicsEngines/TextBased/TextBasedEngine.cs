using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GraphicsEngines.TextBased
{
    internal class TextBasedEngine : GraphicsEngine
    {

        private Tuple<int, int, int, int>[] boundaries;


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

        public TextBasedEngine(int screenWidth, int screenHeight, int numGrids) : base(Style.TextBased, screenWidth, screenHeight)
        {
            ScreenRenderer.Initialize(screenWidth, screenHeight);
            boundaries = new Tuple<int, int, int, int>[numGrids];
        }

    }

}
