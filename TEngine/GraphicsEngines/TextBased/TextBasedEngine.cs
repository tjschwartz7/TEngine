using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Helpers;

namespace TEngine.GraphicsEngines.TextBased
{
    public class TextBasedEngine : GraphicsEngine
    {
        private static TextBasedEngine _engine;
        private ScreenRenderer _renderer;

        //User input
        private static bool _waitingForUserInput;


        public TextBasedEngine(int targetFPS) : base(Style.TextBased, Console.WindowWidth, Console.WindowHeight, targetFPS)
        {
            _renderer = new ScreenRenderer();
            
            OnStart();
            _ = Task.Run(() => Resolution.ScreenChangeHandler());
            _ = Task.Run(() => AsyncFrame());
        }

        public static void Begin(TextBasedEngine instance)
        {
            _engine = instance;
        }

        public string GetTerminalWidthLine()
        {
            return Resolution.TerminalWidthLine;
        }

        public void PauseUIFor(int millis)
        {
            _renderer.SetTimeDelay(millis);
        }

        protected override void OnFrame()
        {
            base.OnFrame();
            _renderer.Latency = UpdateDelay_ms;
        }

        public override void Stop()
        {
            base.Stop();
            
            _renderer.Stop();
        }

        public static void TerminateWindow()
        {
            if(_engine != null)
                _engine.Stop();
        }

        public int SetupWaitForInput(List<string> options)
        {
            ScreenRenderer.Options = options;
            _waitingForUserInput = true;
            WaitForUserInput();
            int ret = ScreenRenderer.CurrentSelection;
            ScreenRenderer.Options = null;

            return ret;
        }

        private async void WaitForUserInput()
        {
            while(_waitingForUserInput)
            {
                await Task.Delay(200);
            }
        }

        public static void UpKeyPressed()
        {
            if(ScreenRenderer.CurrentSelection > 0) ScreenRenderer.CurrentSelection--;
        }

        public static void DownKeyPressed()
        {
            if(ScreenRenderer.CurrentSelection < (ScreenRenderer.Options.Count - 1)) ScreenRenderer.CurrentSelection++;
        }

        public static void 
    }
}
