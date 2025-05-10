using TEngine.Components.UI.Layouts;

using TEngine.Components.Behavior;
using TEngine.GoneMedieval.Player;

namespace TEngine.GoneMedieval.Managers
{
    public class GameManager : MonoBehavior
    {
        public PlayerStats Player { get; private set; } = new PlayerStats();
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
