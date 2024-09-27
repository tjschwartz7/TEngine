using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;
using TEngine.Project.Graphics;
using TEngine.SaveEngine;

namespace TEngine.Project
{
    internal class Primary : Application
    {

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
            if(KeyPressed(ConsoleKey.Escape)) TerminateApplication();
        }
    }
}
