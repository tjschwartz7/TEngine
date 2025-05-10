using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using TEngine.Components.UI.Layouts;
using TEngine.EngineManagement.Commands;

namespace TEngine.Components.UI.Elements
{
    public abstract class UIElement : RenderableComponent
    {
        private CanvasContext? context; 
        public bool IsActive { get; private set; } = true; // Add a flag to control visibility

        public override void OnEnable()
        {
            base.OnEnable();
            RegisterToNearestCanvas();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            UnregisterFromCanvas();
        }

        private void RegisterToNearestCanvas()
        {
            var current = Owner;
            while (current != null)
            {
                if (current.HasComponent<Canvas>())
                {
                    context = current.GetComponent<Canvas>().Context;
                    context.Register(this);
                    IsActive = true; // Set active when registered
                    break;
                }

                current = current.Parent;
            }
        }

        private void UnregisterFromCanvas()
        {
            context?.Unregister(this);
            IsActive = false; // Set to inactive when unregistered
            context = null;
        }

        // Override Render method to respect IsActive flag
        public override List<DrawCommand> GetDrawCommands()
        {
            if (!IsActive)
                return new List<DrawCommand>(); // Don't return any commands if inactive

            // If active, proceed to generate the draw commands
            return new List<DrawCommand>(); // Replace with actual draw command generation logic
        }
    }

}
