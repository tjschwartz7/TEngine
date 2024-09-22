using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Project
{
    internal class Primary : Application
    {
        //You shouldn't mess with this
        public Primary(GraphicsEngine.Style graphicsEngine) : base(graphicsEngine) { }

        //You can plug code into these

        /// <summary>
        /// Called once when the game starts
        /// </summary>
        protected override void OnStart()
        {

        }

        /// <summary>
        /// Called occasionally to match a specific framerate
        /// </summary>
        protected override void OnUpdate()
        {

        }
    }
}
