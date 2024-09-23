﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEngine.SaveEngine;

namespace TEngine.Project
{
    internal class Primary : Application
    {
        /// <summary>
        /// Set initial values to certain engine settings. 
        /// Do NOT delete any of these, just change the values to how you want them.
        /// </summary>
        protected override void InitializeSettings()
        {
            GraphicsEngineInstance = new GraphicsEngine(GraphicsEngine.Style.TextBased); //Change this to whatever you want your engine to be
        }

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
