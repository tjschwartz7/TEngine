
namespace TEngine.GoneMedieval
{
    internal class GoneMedieval : Game
    {
        public override void Initialize()
        {

        }

        public override void OnKeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    EventManager.Instance.Trigger("QUIT");
                    break;
            }
        }

        static void Main()
        {
            GoneMedieval game = new GoneMedieval();
            game.Start();
        }
    }
}
