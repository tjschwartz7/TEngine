using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.GoneMedieval
{
    internal class GoneMedieval : Game
    {
        public override void Initialize()
        {
           
        }

        static void Main()
        {
            GoneMedieval game = new GoneMedieval();
            game.Start();
            Application.Start();
        }
    }
}
