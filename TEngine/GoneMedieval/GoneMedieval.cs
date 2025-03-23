using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GoneMedieval
{
    internal class GoneMedieval : Game
    {
        protected override void Initialize()
        {
            EventManager.Instance.Subscribe("QUIT", () => { Environment.Exit(0); });
            Engine.Instance.Register(new UI.UI());
        }

        protected override void Update()
        {

        }

        static void Main()
        {
            
            Application.Start();
            GoneMedieval game = new GoneMedieval();
            game.Start();

            while(true)
            {
                wait_for(2000);
            }
        }

        private static async void wait_for(int millis)
        {
            await Task.Delay(millis);
        }
    }
}
