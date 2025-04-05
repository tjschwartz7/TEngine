using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public class GameStateManager
    {
        private bool _isPaused;
        public bool IsPaused => _isPaused;
        public GameStateManager Instance { get; private set; } = new GameStateManager();

        private GameStateManager()
        {
            //Default is not paused
            _isPaused = false;
        }

        // Call this to toggle the pause state
        public void TogglePause()
        {
            _isPaused = !_isPaused;
            if (_isPaused) EventManager.Instance.Trigger("PAUSE");
        }

        // Or you can set it directly
        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
            if(_isPaused) EventManager.Instance.Trigger("PAUSE");
        }
    }


}
