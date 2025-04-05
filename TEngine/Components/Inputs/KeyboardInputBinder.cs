using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Inputs
{
    public class KeyboardInputBinder : Component
    {
        private Dictionary<ConsoleKey, Action> _bindings = new Dictionary<ConsoleKey, Action>();
        private EventManager instance = EventManager.Instance;

        /// <summary>
        /// This function allows you to bind a key to an action. This is useful for creating custom key bindings for your game.
        /// </summary>
        /// <param name="key">The key which is binded to the action.</param>
        /// <param name="action">The function intended to be performed when a key is pressed.</param>
        public void Bind(ConsoleKey key, Action action)
        {
            if (!_bindings.ContainsKey(key))
            {
                _bindings.Add(key, action);
                instance.Subscribe($"KEY_{key}", action);
            }
        }

        /// <summary>
        /// This function allows you to unbind a key from its action. This is useful for removing key bindings when they are no longer needed.
        /// </summary>
        /// <param name="key"></param>
        public void Unbind(ConsoleKey key)
        {
            if (_bindings.ContainsKey(key))
            {
                _bindings.Remove(key);
                instance.Unsubscribe($"KEY_{key}", _bindings[key]);
            }
        }

        /// <summary>
        /// This function allows you to trigger the action associated with a key binding manually.
        /// </summary>
        /// <param name="key">The key which has been pressed.</param>
        public void Trigger(ConsoleKey key)
        {
            if (_bindings.ContainsKey(key))
            {
                _bindings[key].Invoke();
            }
        }
    }
}
