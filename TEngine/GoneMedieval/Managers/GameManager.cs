using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine2.Behavior;

namespace TEngine.GoneMedieval.Managers
{
    public class GameManager : Monobehavior
    {
        public Player Player { get; private set; } = new Player();
        public static GameManager Instance { get; private set; } = new GameManager();

        public GameManager() 
        {
            if (Instance != null)
            {
                throw new Exception("GameManager is a singleton and cannot be instantiated more than once.");
            }
            else
            {
                Instance = this;
            }
        }

        public override void Awake()
        {

        }
    }
}
