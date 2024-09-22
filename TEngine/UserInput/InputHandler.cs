using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.UserInput
{
    internal class InputHandler
    {
        public static async Task KeyReader()
        {
            // Loop to keep the application running
            while (Application.IsRunning())
            {
                // Wait for a key press
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // intercept: true to not show the key in the console

                // Check if the 'Escape' key is pressed to exit
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Exiting...");
                    Application.TerminateApplication();
                    break;
                }

                Task backgroundTask = Task.Run(() => HandleKeyPress(keyInfo));
                await Task.Delay(10);
            }
        }


        public static void HandleKeyPress(ConsoleKeyInfo keyInfo)
        {

            // Check which key was pressed
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("Up Arrow pressed");
                            
                    break;

                case ConsoleKey.DownArrow:
                    Console.WriteLine("Down Arrow pressed");
                    break;

                case ConsoleKey.LeftArrow:
                    Console.WriteLine("Left Arrow pressed");
                    break;

                case ConsoleKey.RightArrow:
                    Console.WriteLine("Right Arrow pressed");
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine("Enter key pressed");
                    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Another key pressed");
                    break;
            }
                
            
        }
    }
}
