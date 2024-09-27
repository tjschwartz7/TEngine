using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    /// <summary>
    /// This class handles everything related to the screen pixels.
    /// It is a static class, declared once in memory, because it should only ever exist once in any Application,
    /// and because we don't need to hold instances of it for anything.
    /// </summary>
    internal class ScreenRenderer
    {
        public const int MAX_THREADS = 4;
        private char[]? _screen;
        private bool[]? _screenHasChanged;
        private int _latency;
        private int _width;
        private int _height;
        private bool _stop;


        public ScreenRenderer(int width, int height)
        {
            _width = width;
            _height = height;

            _stop = false;
            //We use 1D arrays for a quick speed boost
            //Screen declaration
            _screen = new char[height * width];
            //Screen change array declaration
            _screenHasChanged = new bool[height * width];

            //Create our screen rendering threads
            CreateScreenRenderer();
        }

        /// <summary>
        /// Starts our screen render threads.
        /// </summary>
        private void CreateScreenRenderer()
        {
            int slice = _height / MAX_THREADS;

            int numThreads = MAX_THREADS;
            //Assuming one thread can easily handle 400 rows
            int maxNeededThreads = (_height / 400); //I.E. 5 threads for 2000 rows, 4 for 1600
            if (numThreads > maxNeededThreads) numThreads = maxNeededThreads;

            if (numThreads == 1) //In this case we should really only need one thread
            {
               _ = Task.Run(() => RenderScreen(0, _height-1));
            }
            else
            {
                //Create multiple threads for screen rendering
                for (int i = _height; i > 0; i -= slice)
                {
                    int rowStart = i;
                    int rowStop = _height;

                    //If this is the last iteration of the loop
                    if (i - slice <= 0)
                    {
                        //The final bound is just 0
                        rowStop = 0;
                    }
                    else
                    {
                        //It's one above i - slice
                        rowStop = i - slice + 1;
                    }

                    // Create a thread and pass a function and its arguments
                    _ = Task.Run(() => RenderScreen(rowStart, rowStop));
                }
            }

        }



        /// <summary>
        /// Print out the screen.
        /// </summary>
        /// <param name="startIndex">The starting index of where to handle screen printing.</param>
        /// <param name="stopIndex">The final (included) index of where to handle screen printing.</param>
        private async void RenderScreen(int startIndex, int stopIndex)
        {

            //If these were null here it would be really bad...
            //TERMINATE!
            if (_screen == null) { Application.TerminateApplication(); }
            if (_screenHasChanged == null) { Application.TerminateApplication(); }
            //This thread will end when Stop() is called
            while (!_stop)
            {
                //We assume the arrays won't become null during operation. That's why they're private.
                //We really don't wanna keep checking them in here...

                for (int i = startIndex; i < stopIndex; i++)
                {

                    int row = i / _height;
                    int col = i % _width;
                    //$"\033[{i};0H{_screen[row, col]}"
                    //Replace character at this index
                    //But only if its changed
                    if (_screenHasChanged[i])
                        Console.Write($"\033[{row};{col}H{_screen[i]}");
                }
                    await Task.Delay(Application.GetTargetLatency()); //Clear up a thread in the pool
            }
        }


        /// <summary>
        /// Updates the screen in a rectangular fashion from the top left coordinate to the bottom right coordinate.
        /// </summary>
        /// <param name="charArray">The char array to write over the local screen bound.</param>
        /// <param name="bound">The actual boundary of our screen to be written over.</param>
        public void UpdateScreen(char[] charArray, ScreenElements.ScreenBounds.Bound bound)
        {
            int topLeftRow = bound.GetTopLeftRow();
            int topLeftCol = bound.GetTopLeftCol();
            int bottomRightRow = bound.GetBottomRightRow();
            int bottomRightCol = bound.GetBottomRightCol();
            int numCols = bound.GetNumCols();
            int numRows = bound.GetNumRows();
            int totalNumPixels = bound.GetNumPixels();

            //Just in case
            if (_screen == null) { return; }
            if (_screenHasChanged == null) { return; }


            int startColumn = topLeftCol;
            int stopColumn = bottomRightCol;
            int startRow = topLeftRow;
            int stopRow = bottomRightRow;

            //Invalid selection
            if(numCols < 0 || numRows < 0)
            {
                MessageUtils.TerminateWithError("ScreenRenderer", "UpdateScreen", "Invalid selection coordinates!!");
            }
            //Invalid
            else if (charArray.Length != totalNumPixels) 
            {
                MessageUtils.TerminateWithError("ScreenRenderer", "UpdateScreen", "Array size does not match selection size!!");
            }

            //Iterate over each character in the selection
            for(int row = startRow; row <= stopRow; row++)
            {
                int rowIndex = row * _width;
                for (int col = startColumn; col <= stopColumn; col++)
                {
                    int currentIndex = rowIndex + col;

                    //Note: We COULD call UpdateSinglePixel here, for clarity, but we won't.
                    //The reason is, the function call would make this loop take longer than it needs to. No reason when the code is already so short.

                    //Check if the pixels are different.
                    //We only need to write to _screen if they are.
                    if (_screen[currentIndex] != charArray[currentIndex]) { _screenHasChanged[currentIndex] = true; _screen[currentIndex] = charArray[currentIndex]; }
                }
            }
        }

        /// <summary>
        /// Updates a specific pixel on the screen.
        /// </summary>
        /// <param name="pixel">The new pixel to overwrite the previous one.</param>
        /// <param name="row">The row of the new pixel.</param>
        /// <param name="col">The column of the new pixel. </param>
        public void UpdatePixel(char pixel, int row, int col)
        {
            //Just in case
            if (_screen == null) { return; }
            if(_screenHasChanged == null) { return; }

            int index = row * _height + col;
            //If new pixel is different than old one, the screen needs updated
            if (_screen[index] != pixel) { _screenHasChanged[index] = true; _screen[index] = pixel; }
        }
    }
}
