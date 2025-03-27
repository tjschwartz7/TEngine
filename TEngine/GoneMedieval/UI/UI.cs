using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.GoneMedieval.Managers;
using TEngine.Rendering;
using TEngine2.Behavior;

namespace TEngine.GoneMedieval.UI
{
    public class UI : Monobehavior
    {

        private string[] ?MenuOptions;
        public override void Start()
        {
            EventManager.Instance.Subscribe("UPDATE_MENU", OnUpdateMenu);

            TextRenderer.Instance.RegisterLayer("DEBUG", 0, 3);
            TextRenderer.Instance.RegisterLayer("HUD", 1, 3);
            TextRenderer.Instance.RegisterLayer("BACKGROUND", 2, 20);
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
            string hudText = String.Format("Name: {0}    Health: {1}    Mana: {2}",GameManager.Instance.Player.Name, GameManager.Instance.Player.Health, GameManager.Instance.Player.Mana);
            TextRenderer.Instance.AddText("HUD", hudText);
            TextRenderer.Instance.AddText("HUD", "");


            TextRenderer.Instance.AddText("BACKGROUND", "");
            

            if(MenuOptions != null)
            {
                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    TextRenderer.Instance.AddText("MENU", MenuOptions[i]);
                }
            }
        }

        public void OnUpdateMenu(object args)
        {
            MenuOptions = (string[])args;
        }
    }

}
