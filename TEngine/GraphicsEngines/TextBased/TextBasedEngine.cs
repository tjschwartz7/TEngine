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
        private bool[,] _lineHasChanged;
        private char[,] _screen;


        //This is the Screen that is used to display things to the user.
        //It is not intended to be updated correctly;
        //Whenever a line is edited, those lines must be updated in the bool LineHasChanged array.
        //If they aren't things won't display properly!
        public char[,] Screen { get => _screen; }

        //Again, this is not meant to be updated directly. 
        //One bool in the array represents a line that has changed on the screen.
        public bool[,] LineHasChanged {  get => _lineHasChanged; }   

        public async Task Print()
        {

            Console.Clear();
            OnFrame();

            //One loop is faster than 2
            for(int i = 0; i < ScreenWidth*ScreenHeight; i++)
            {
                //Save some calculations
                int row = i / ScreenHeight;
                int col = i % ScreenWidth;
                if (_lineHasChanged[row, col])
                {
                    Console.Write($"\033[{i};0H"); //Move cursor to line we want to erase
                    Console.Write("\033[K"); //Erase the line
                    Console.WriteLine($"\033[{i};0H{_screen[row, col]}"); //Move cursor to start of line and write new text
                }
            }


            

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

        public TextBasedEngine(int screenWidth, int screenHeight) : base(Style.TextBased, screenWidth, screenHeight)
        {
            _lineHasChanged = new bool[screenHeight, screenWidth];
            _screen = new char[screenHeight, screenWidth];
        }
        public void Initialize()
        {
        }


        

        protected virtual void PrintDebug()
        {

        }

        protected virtual void PrintPrimaryHeader()
        {

        }

        protected virtual void PrintSecondaryHeader()
        {

        }

        protected virtual void PrintBackground()
        {

        }

        protected virtual void PrintForeground()
        {

        }

        protected virtual void PrintOptions()
        {

        }

        protected virtual void PrintFooter()
        {

        }

       

    }
}
