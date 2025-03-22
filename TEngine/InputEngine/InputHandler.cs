using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data;



namespace TEngine.InputEngine
{
    public static class InputHandler
    {
        private static HashSet<ConsoleKey> keysPressed = new HashSet<ConsoleKey>();

        public static void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keysPressed.Add(keyInfo.Key);
            }
        }

        public static bool GetKey(ConsoleKey key)
        {
            return keysPressed.Contains(key);
        }

        public static void ClearKeys()
        {
            keysPressed.Clear(); // Clear key states after processing
        }


    }
}
