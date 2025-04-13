using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Behavior;
using TEngine.EngineManagement;
using TEngine.EngineManagement.RenderingEngines.Text;
using TEngine.GoneMedieval.Managers;

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

            TextRenderSystem.Instance.RegisterLayer("DEBUG", 0, 3);
            TextRenderSystem.Instance.RegisterLayer("HUD", 1, 3);
            TextRenderSystem.Instance.RegisterLayer("BACKGROUND", 2, 20);
            TextRenderSystem.Instance.RegisterLayer("DIALOGUE", 3, 10);
            TextRenderSystem.Instance.RegisterLayer("MENU", 4, 10);
        }

        public override void Update()
        {
            TextRenderSystem.Instance.ClearLayer("DEBUG");
            TextRenderSystem.Instance.ClearLayer("HUD");
            TextRenderSystem.Instance.ClearLayer("BACKGROUND");
            TextRenderSystem.Instance.ClearLayer("MENU");

            TextRenderSystem.Instance.AddText("DEBUG", "");
            TextRenderSystem.Instance.AddText("DEBUG", "FPS: " + Engine.Instance.FPS);
            TextRenderSystem.Instance.AddText("DEBUG", "");

            string hudText = String.Format("Name: {0}    Health: {1}    Mana: {2}", GameManager.Instance.Player.Name, GameManager.Instance.Player.Health, GameManager.Instance.Player.Mana);
            TextRenderSystem.Instance.AddText("HUD", "");
            TextRenderSystem.Instance.AddText("HUD", hudText);
            TextRenderSystem.Instance.AddText("HUD", "");

            TextRenderSystem.Instance.AddText("BACKGROUND", "");

            if(displayText)
            {
                TextRenderSystem.Instance.AddText("DIALOGUE", dialogue);
            }
            else if (MenuOptions != null)
            {
                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (i == menuCursorIndex)
                    {
                        TextRenderSystem.Instance.AddText("MENU", "> " + MenuOptions[i]);
                    }
                    else
                    {
                        TextRenderSystem.Instance.AddText("MENU", MenuOptions[i]);
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
