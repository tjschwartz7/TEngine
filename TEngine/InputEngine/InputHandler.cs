using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.InputEngine
{
    internal class InputHandler
    {
        public async Task KeyReader()
        {
            // Loop to keep the application running
            while (Application.IsRunning())
            {
                // Wait for a key press
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // intercept: true to not show the key in the console
                Task backgroundTask = Task.Run(() => HandleKeyPress(keyInfo));
                await Task.Delay(10);
            }
        }

        //You'll have to edit this to do what you need it to do
        private void HandleKeyPress(ConsoleKeyInfo keyInfo)
        {

            // Check which key was pressed
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    OnUpArrowPressed();
                    break;

                case ConsoleKey.DownArrow:
                    OnDownArrowPressed();
                    break;

                case ConsoleKey.LeftArrow:
                    OnLeftArrowPressed();
                    break;

                case ConsoleKey.RightArrow:
                    OnRightArrowPressed();
                    break;
                case ConsoleKey.W:
                    OnWKeyPressed();
                    break;
                case ConsoleKey.A:
                    OnAKeyPressed();
                    break;
                case ConsoleKey.S:
                    OnSKeyPressed();
                    break;
                case ConsoleKey.D:
                    OnDKeyPressed();
                    break;
                case ConsoleKey.F:
                    OnFKeyPressed();
                    break;
                case ConsoleKey.Enter:
                    OnEnterPressed();
                    break;
                case ConsoleKey.Escape:
                    OnEscapePressed();
                    break;

                default:
                    break;
            }
        }

        protected virtual void OnUpArrowPressed()
        {

        }

        protected virtual void OnDownArrowPressed()
        {

        }

        protected virtual void OnLeftArrowPressed()
        {

        }

        protected virtual void OnRightArrowPressed()
        {

        }

        protected virtual void OnWKeyPressed()
        {

        }

        protected virtual void OnAKeyPressed()
        {

        }

        protected virtual void OnSKeyPressed()
        {

        }

        protected virtual void OnDKeyPressed()
        {

        }

        protected virtual void OnFKeyPressed()
        {

        }


        protected virtual void OnEnterPressed()
        {

        }

        protected virtual void OnEscapePressed()
        {

        }
    }
}
