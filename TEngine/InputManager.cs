using System;
using System.Threading;

namespace TEngine
{
    public class InputManager
    {
        public static InputManager Instance { get; private set; } = new InputManager();

        public void UpdateInput()
        {

            if (Console.KeyAvailable) // Non-blocking check for key press
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Read key without displaying it
                EventManager.Instance.Trigger($"KEY_{keyInfo.Key}"); // Fire event for key press
            }
        }
    }
}
