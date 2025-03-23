using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Rendering;
using TEngine2.Behavior;

namespace TEngine.GoneMedieval.UI
{
    public class UI : Monobehavior
    {
        public override void Start()
        {
            TextRenderer.Instance.RegisterLayer("DEBUG", 0, 3);
            TextRenderer.Instance.RegisterLayer("HUD", 1, 3);
            TextRenderer.Instance.RegisterLayer("BACKGROUND", 2, 30);
            TextRenderer.Instance.RegisterLayer("MENU", 3, 10);
        }

        public override void Update()
        {
            TextRenderer.Instance.ClearLayer("DEBUG");
            TextRenderer.Instance.ClearLayer("HUD");
            TextRenderer.Instance.ClearLayer("BACKGROUND");
            TextRenderer.Instance.ClearLayer("MENU");

            TextRenderer.Instance.AddText("DEBUG", "");
            TextRenderer.Instance.AddText("DEBUG", "FPS: " + Engine.Instance.FPS);
            TextRenderer.Instance.AddText("DEBUG", "");

            TextRenderer.Instance.AddText("HUD", "");
            TextRenderer.Instance.AddText("HUD", "");
            TextRenderer.Instance.AddText("HUD", "");



            TextRenderer.Instance.AddText("BACKGROUND", "");
            

            TextRenderer.Instance.AddText("MENU", "");


        }
    }

}
