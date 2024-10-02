using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;



namespace TEngine.InputEngine
{
    public class InputHandler
    {
        IntPtr _keyboardLayout;
        private Dictionary<ConsoleKey, Tuple<ConsoleKeyInfo, bool>> _currentlyPressedKeys;
        const int KEY_HELD_DOWN = 0x8000;
        const int KEY_NEWLY_PRESSED = 0x0001;

        //Application stuff
        private bool _isRunning;

        public Dictionary<ConsoleKey,Tuple<ConsoleKeyInfo, bool>> CurrentlyPressedKeys { get => _currentlyPressedKeys; }

        public InputHandler() 
        {
            _isRunning = true;
            _currentlyPressedKeys = new Dictionary<ConsoleKey, Tuple<ConsoleKeyInfo, bool>>();
            _keyboardLayout = GetKeyboardLayout(0);
            _ = Task.Run(() => KeyReader());
            _ = Task.Run(() => KeyChecker());
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern short VkKeyScanExA(char ch, IntPtr dwhkl);
        // Import GetKeyboardLayout to retrieve the current keyboard layout
        [DllImport("user32.dll")]

        public static extern IntPtr GetKeyboardLayout(uint idThread);

        public async Task KeyReader()
        {
            
            // Loop to keep the application running
            while (_isRunning)
            {
                // Wait for a key press
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // intercept: true to not show the key in the console((
                if(!_currentlyPressedKeys.ContainsKey(keyInfo.Key))
                    _currentlyPressedKeys.Add(keyInfo.Key, new Tuple<ConsoleKeyInfo,bool>(keyInfo, true));
                
                await Task.Delay(25); //Let the key reader be a fast thread
            }
        }

        public async Task KeyChecker()
        {
            while(_isRunning)
            {
                CheckIfKeysStillPressed();
                await Task.Delay(50);
            }
        }

        private void CheckIfKeysStillPressed()
        { 
            foreach (var key in CurrentlyPressedKeys)
            {
                //Get the result
                short result = VkKeyScanExA(key.Value.Item1.KeyChar, _keyboardLayout);

                // Extract the virtual key code (low byte) and shift state (high byte)
                byte vkCode = (byte)(result & 0xFF);  // Low byte
                byte shiftState = (byte)((result >> 8) & 0xFF);  // High byte

                //Check if key has been released
                if((GetAsyncKeyState(vkCode) & KEY_HELD_DOWN) == 0)
                {
                    //Remove key; its been released
                    _currentlyPressedKeys.Remove(key.Key);
                }
            }
        }

        public bool KeyPressed(ConsoleKey key)
        {
            return _currentlyPressedKeys.ContainsKey(key);
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
