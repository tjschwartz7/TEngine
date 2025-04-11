using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public abstract class Game
    {
        private bool isRunning = true;
        private InputManager inputManager = new InputManager();

        public void Start()
        {
            EventManager.Instance.Subscribe("QUIT", OnQuit);
            SubscribeToAllKeys();


            Application.Start();
            Initialize(); // Let the developer register their behaviors
            Application.Run();
        }

        private void OnQuit()
        {
            isRunning = false;
        }

        public virtual void Initialize(){}

        // Handle generic key press event for all alphabetic keys
        public virtual void OnKeyPressed(ConsoleKey key){}

        // Automatically subscribe to events for all alphabetic keys (A-Z)
        private void SubscribeToAllKeys()
        {
            foreach (var key in Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>())
            {
                string keyEvent = $"KEY_{key}";
                EventManager.Instance.Subscribe(keyEvent, () => OnKeyPressed(key)); // Dynamically create the handler   
            }
        }

    }

    
}