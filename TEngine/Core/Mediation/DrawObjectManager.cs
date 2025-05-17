using TEngine.Components.Rendering;
using TEngine.Core.Commands;

namespace TEngine.Core.Mediation
{
    public class DrawObjectManager
    {
        public static DrawObjectManager Instance { get; } = new DrawObjectManager();
        public DrawObjectManager() { }

        public IEnumerable<DrawCommand> GetDrawCommands(IEnumerable<GameObject> gameObjects)
        {
            //Null list means nothing to return
            if (gameObjects == null) yield break;

            foreach(var gameObject in gameObjects)
            {
                if (gameObject != null && gameObject.HasComponent<Renderer>())
                {
                    foreach (var command in gameObject.GetComponent<Renderer>().GetDrawCommands())
                    {
                        yield return command;
                    }
                }
            }
        }
    }
}
