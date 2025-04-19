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

        }

        public override void Update()
        {
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
