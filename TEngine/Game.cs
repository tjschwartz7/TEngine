using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public abstract class Game
    {
        public abstract void Initialize();

        public void Start()
        {
            Initialize(); // Let the developer register their behaviors
        }
    }
}
