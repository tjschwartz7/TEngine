using System;
using System.Threading;

namespace TEngine
{
    public class InputManager
    {
        private Thread inputThread;
        private bool isRunning = true;

        public void Start()
        {
            EventManager.Instance.Subscribe("QUIT", Stop); // Stop input loop when game ends

            inputThread = new Thread(InputLoop);
            inputThread.IsBackground = true; // Ensure it doesn't prevent the program from closing
            inputThread.Start();
        }

        private void InputLoop()
        {
            while (isRunning)
            {
                if (Console.KeyAvailable) // Non-blocking check for key press
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Read key without displaying it
                    EventManager.Instance.Trigger($"KEY_{keyInfo.Key}"); // Fire event for key press
                }

                Thread.Sleep(10); // Small delay to prevent high CPU usage
            }
        }

        public void Stop()
        {
            isRunning = false;
            inputThread?.Join(); // Wait for thread to exit cleanly
        }
    }
}
