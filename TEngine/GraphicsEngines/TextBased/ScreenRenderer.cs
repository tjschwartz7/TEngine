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
        private bool _initialized = false;


        /// <summary>
        /// Initialize all of our variables and start the screen renderer threads.
        /// </summary>
        /// <param name="width">The width of our screen.</param>
        /// <param name="height">The height of our screen.</param>
        public void Initialize(int width, int height)
        {
            _width = width;
            _height = height;
            _stop = false;
            _initialized = true;
            //We use 1D arrays for a quick speed boost
            //Screen declaration
            _screen = new char[height * width];
            //Screen change array declaration
            _screenHasChanged = new bool[height * width];

            //Create our screen rendering threads
            CreateScreenRenderer();
        }

        public ScreenRenderer(int width, int height)
        {
            _width = width;
            _height = height;

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
        /// <param name="charArray">The character array to write over the screen. Must be the same size as your rectangular selection.</param>
        /// <param name="topLeftRow">The row of the top left coordinate.</param>
        /// <param name="topLeftCol">The col of the top left coordinate.</param>
        /// <param name="bottomRightRow">The row of the bottom right coordinate.</param>
        /// <param name="bottomRightCol">The col of the bottom right coordinate.</param>
        public void UpdateScreen(char[] charArray, Tuple<int,int,int,int> bound, int numRows, int numCols)
        {
            int topLeftRow = bound.Item1;
            int topLeftCol = bound.Item2;
            int bottomRightRow = bound.Item3;
            int bottomRightCol = bound.Item4;

            //Initializer call hasn't been made yet; don't do anything.
            if (!_initialized) return;

            //Just in case
            if (_screen == null) { return; }
            if (_screenHasChanged == null) { return; }


            int startIndex = topLeftRow * _height + topLeftCol;
            int stopIndex = bottomRightRow * _height + bottomRightCol;

            //Invalid selection
            if(numCols < 0 || numRows < 0)
            {
                MessageUtils.TerminateWithError("ScreenRenderer", "UpdateScreen", "Invalid selection coordinates!!");
            }
            //Calculate how many pixels our charArray SHOULD be
            //Subtract actual indices (top from bottom) to get the result
            int totalNumPixels = numRows*numCols; //This is effectively the area of the surface
            //Invalid
            if (charArray.Length != totalNumPixels) 
            {
                MessageUtils.TerminateWithError("ScreenRenderer", "UpdateScreen", "Array size does not match selection size!!");
            }
            else if(stopIndex > charArray.Length)
            {
                MessageUtils.TerminateWithError("ScreenRenderer", "UpdateScreen", "Selection size larger than array size!!");
            }

            int row = 0;
            //Iterate over each character in the selection
            for (int i = startIndex; i <= stopIndex; i++)
            {

                //Note: We COULD call UpdateSinglePixel here, for clarity, but we won't.
                //The reason is, the function call would make this loop take longer than it needs to. No reason when the code is already so short.

                //Check if the pixels are different.
                //We only need to write to _screen if they are.
                if (_screen[i] != charArray[i]) { _screenHasChanged[i] = true; _screen[i] = charArray[i]; }

                //Quick note: putting the conditional below the actual screen change calculation saves one iteration in the loop. Yay.
                //Another condition in here to avoid needing another for loop
                //If i is out of range of our selection, jump to the next row
                int col = i % numRows;
                if(col >= numCols)
                {
                    //Increment our row number
                    row++;
                    //Set i to the col 0 of the next row, plus the topLeftCol
                    //This jumps to our next starting position
                    i = row * numCols + topLeftCol;
                    continue;
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
            //Initializer call hasn't been made yet; don't do anything.
            if (!_initialized) return;
            //Just in case
            if (_screen == null) { return; }
            if(_screenHasChanged == null) { return; }

            int index = row * _height + col;
            //If new pixel is different than old one, the screen needs updated
            if (_screen[index] != pixel) { _screenHasChanged[index] = true; _screen[index] = pixel; }
        }
    }
}
