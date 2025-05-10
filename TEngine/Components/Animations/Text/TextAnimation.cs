using System.Reflection.Metadata.Ecma335;
using TEngine.Components.Rendering;
using TEngine.EngineManagement.Commands;

namespace TEngine.Components.Animations.Text
{
    public class TextAnimation : Animation<string>
    {
        public override void Update()
        {
            base.Update();

            if (IsPlaying)
            {
                Tick();
                if (HasComponent<Renderer>())
                {
                    foreach (DrawCommand cmd in GetComponent<Renderer>().GetDrawCommands())
                        Apply(cmd.TextValue); // Replace with actual target
                }
            }
        }

        public virtual string Apply(string target) { return target; }
    }
}
