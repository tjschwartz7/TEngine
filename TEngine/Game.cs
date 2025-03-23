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

        public void Start()
        {
          
            EventManager.Instance.Subscribe("QUIT", OnQuit);

            Initialize(); // Let the developer register their behaviors

            while (isRunning)
            {
                Update(); // Call the update method
            }
        }

        private void OnQuit()
        {
            isRunning = false;
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Update()
        {

        }
    }
}
