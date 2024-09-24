using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GraphicsEngines.TextBased;

namespace TEngine.Project.UserInput
{
    internal class UserInput : InputEngine.InputHandler
    {
        protected override void OnFKeyPressed()
        {

        }

        protected override void OnEscapePressed()
        {
            Application.TerminateApplication();

        }
    }
}
