using TEngine.EngineManagement.Commands;

namespace TEngine.Components.UI.Elements.Text
{
    public class Rectangle : UIElement, IUIComponent
    {
        public Rectangle()
        {
            // Constructor logic if needed
        }
        public override List<DrawCommand> GetDrawCommands()
        {
            // Implement the logic to generate draw commands for the rectangle
            List<DrawCommand> cmds = new List<DrawCommand>();
            
            return cmds;
        }
    }
}
