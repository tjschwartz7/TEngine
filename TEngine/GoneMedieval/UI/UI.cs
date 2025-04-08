using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Behavior;
using TEngine.GoneMedieval.Managers;
using TEngine.Rendering.Text;

namespace TEngine.GoneMedieval.UI
{
    public class UI : MonoBehavior
    {

        private string[] ?MenuOptions;
        private bool displayText = false;
        private string dialogue = "";
        private int menuCursorIndex = 0;
        private int playerLocationIndex;
        
        public override void Start()
        {
            EventManager.Instance.Subscribe("UPDATE_MENU", OnUpdateMenu);
            EventManager.Instance.Subscribe("TOGGLE_DISPLAY_DIALOGUE", OnToggleDisplayText);
            EventManager.Instance.Subscribe("SET_DIALOGUE", onSetText);
            EventManager.Instance.Subscribe("CURSOR_UP", OnCursorUp);
            EventManager.Instance.Subscribe("CURSOR_DOWN", OnCursorDown);

            TextRenderingEngine.Instance.RegisterLayer("DEBUG", 0, 3);
            TextRenderingEngine.Instance.RegisterLayer("HUD", 1, 3);
            TextRenderingEngine.Instance.RegisterLayer("BACKGROUND", 2, 20);
            TextRenderingEngine.Instance.RegisterLayer("DIALOGUE", 3, 10);
            TextRenderingEngine.Instance.RegisterLayer("MENU", 4, 10);
        }

        public override void Update()
        {
            TextRenderingEngine.Instance.ClearLayer("DEBUG");
            TextRenderingEngine.Instance.ClearLayer("HUD");
            TextRenderingEngine.Instance.ClearLayer("BACKGROUND");
            TextRenderingEngine.Instance.ClearLayer("MENU");

            TextRenderingEngine.Instance.AddText("DEBUG", "");
            TextRenderingEngine.Instance.AddText("DEBUG", "FPS: " + Engine.Instance.FPS);
            TextRenderingEngine.Instance.AddText("DEBUG", "");

            string hudText = String.Format("Name: {0}    Health: {1}    Mana: {2}", GameManager.Instance.Player.Name, GameManager.Instance.Player.Health, GameManager.Instance.Player.Mana);
            TextRenderingEngine.Instance.AddText("HUD", "");
            TextRenderingEngine.Instance.AddText("HUD", hudText);
            TextRenderingEngine.Instance.AddText("HUD", "");

            TextRenderingEngine.Instance.AddText("BACKGROUND", "");

            if(displayText)
            {
                TextRenderingEngine.Instance.AddText("DIALOGUE", dialogue);
            }
            else if (MenuOptions != null)
            {
                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (i == menuCursorIndex)
                    {
                        TextRenderingEngine.Instance.AddText("MENU", "> " + MenuOptions[i]);
                    }
                    else
                    {
                        TextRenderingEngine.Instance.AddText("MENU", MenuOptions[i]);
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
