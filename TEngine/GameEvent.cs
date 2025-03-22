using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public class GameEvent
    {
        private event Action listeners;

        public void Raise()
        {
            listeners?.Invoke(); // Notify all listeners
        }

        public void Subscribe(Action listener)
        {
            listeners += listener;
        }

        public void Unsubscribe(Action listener)
        {
            listeners -= listener;
        }
    }
}
