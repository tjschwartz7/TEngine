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
        private bool displayText = false;
        private string dialogue = "";
        private int menuCursorIndex = 0;
        
        public override void Start()
        {
            EventManager.Instance.Subscribe("UPDATE_MENU", OnUpdateMenu);
            EventManager.Instance.Subscribe("TOGGLE_DISPLAY_DIALOGUE", OnToggleDisplayText);
            EventManager.Instance.Subscribe("SET_DIALOGUE", onSetText);
            EventManager.Instance.Subscribe("CURSOR_UP", OnCursorUp);
            EventManager.Instance.Subscribe("CURSOR_DOWN", OnCursorDown);

            TextRenderer.Instance.RegisterLayer("DEBUG", 0, 3);
            TextRenderer.Instance.RegisterLayer("HUD", 1, 3);
            TextRenderer.Instance.RegisterLayer("BACKGROUND", 2, 20);
            TextRenderer.Instance.RegisterLayer("DIALOGUE", 3, 10);
            TextRenderer.Instance.RegisterLayer("MENU", 4, 10);
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

            string hudText = String.Format("Name: {0}    Health: {1}    Mana: {2}", GameManager.Instance.Player.Name, GameManager.Instance.Player.Health, GameManager.Instance.Player.Mana);
            TextRenderer.Instance.AddText("HUD", "");
            TextRenderer.Instance.AddText("HUD", hudText);
            TextRenderer.Instance.AddText("HUD", "");

            TextRenderer.Instance.AddText("BACKGROUND", "");

            if(displayText)
            {
                TextRenderer.Instance.AddText("DIALOGUE", dialogue);
            }
            else if (MenuOptions != null)
            {
                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (i == menuCursorIndex)
                    {
                        TextRenderer.Instance.AddText("MENU", "> " + MenuOptions[i]);
                    }
                    else
                    {
                        TextRenderer.Instance.AddText("MENU", MenuOptions[i]);
                    }
                }
            }
        }

        public void OnUpdateMenu(object args)
        {
            MenuOptions = (string[])args;
        }

        public void OnToggleDisplayText()
        {
            displayText = !displayText;
        }

        public void onSetText(object args)
        {
            dialogue = (string)args;
        }

        public void OnCursorUp()
        {
            if (menuCursorIndex > 0)
            {
                menuCursorIndex--;
            }
        }

        public void OnCursorDown()
        {
            if (MenuOptions != null && menuCursorIndex < MenuOptions.Length - 1)
            {
                menuCursorIndex++;
            }
        }
    }
}
