using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased.ScreenElements.ScreenBounds;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    internal class TextBasedEngine : GraphicsEngine
    {
        private ScreenRenderer _renderer;
        private Boundaries _bounds;

        public TextBasedEngine(int screenWidth, int screenHeight, int targetFPS) : base(Style.TextBased, screenWidth, screenHeight, targetFPS)
        {
            _renderer = new ScreenRenderer(screenWidth, screenHeight);
            _bounds = new Boundaries();
            OnStart();
            _ = AsyncFrame();
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

        public void SetScreenOnBound(int boundaryIndex, string[] partition)
        {
            if (boundaryIndex < 0 || boundaryIndex >= _bounds.Count())
            {
                MessageUtils.TerminateWithError("TextBasedEngine", "SetScreenOnBound","Set boundary does not exist!!");
            }
            Bound boundary = _bounds.GetBound(boundaryIndex);
            if(partition.Length != boundary.GetNumRows())
            {
                MessageUtils.TerminateWithError("TextBasedEngine", "SetScreenOnBound", $"Incorrect number of rows provided ({partition.Length} != {boundary.GetNumRows()}) !!");
            }


            int row = boundary.GetTopLeftRow();
            int startCol = boundary.GetTopLeftCol();
            int stopCol = boundary.GetBottomRightCol();
            int width = boundary.GetNumCols();
            for(int i = 0; i < partition.Length; i++)
            {
                Console.WriteLine($"{i}");
                try
                {
                    if (partition[i] == null) partition[i] = "";
                    _renderer.UpdateScreenRow(partition[i].PadRight(width).ToCharArray(), row, startCol, stopCol);
                }
                catch(Exception e) { MessageUtils.TerminateWithError("TextBasedEngine", "SetScreenOnBound", $"Error occurred: {e.Message}"); }
                
            }
        }

        public void UpdatePixel(char pixel, int row, int col)
        {
            _renderer.UpdatePixel(pixel, row, col);
        }

        protected async void BlockThreadFor(int numMillis)
        {
            await Task.Delay(numMillis);
        }

        protected override void OnFrame()
        {
            base.OnFrame();
            _renderer.Latency = UpdateDelay_ms;
            if (PauseUI)
            {
                ResumeUI = false;
                PauseUI = false;
                IsUIPaused = true;
                //Busy sleep
                while (!ResumeUI)
                {
                    //Clear up this thread for a second while we're waiting
                    BlockThreadFor(1000);
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            
            _renderer.Stop();
        }
    }
}
