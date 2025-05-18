using TEngine.Utils;

namespace TEngine.Core.Commands
{
    /// <summary>
    /// Buffers draw commands grouped by layer.
    /// </summary>
    public class DrawCommandBuffer
    {
        private readonly SortedDictionary<int, List<DrawCommand>> _layeredCommands = new();

        /// <summary>
        /// Adds a draw command to the appropriate layer.
        /// </summary>
        public void Add(DrawCommand command)
        {
            Layer layer = command.Layer;
            int layerIndex = layer.index;
            if (!_layeredCommands.TryGetValue(layerIndex, out var list))
            {
                list = new List<DrawCommand>();
                _layeredCommands[layerIndex] = list;
            }
            list.Add(command);
        }

        /// <summary>
        /// Adds a collection of Draw Commands to their correct layer.
        /// </summary>
        /// <param name="command">The command to add.</param>
        public void Add(IEnumerable<DrawCommand> commands)
        {
            foreach(var cmd in commands)
            {
                Add(cmd);
            }
        }

        /// <summary>
        /// Gets all draw commands sorted by ascending layer.
        /// </summary>
        public IEnumerable<DrawCommand> GetSortedCommands()
        {
            foreach (var layer in _layeredCommands.Keys.OrderBy(k => k))
            {
                foreach (var command in _layeredCommands[layer])
                {
                    yield return command;
                }
            }
        }

        /// <summary>
        /// Clears all buffered draw commands.
        /// </summary>
        public void Clear()
        {
            foreach (var list in _layeredCommands.Values)
            {
                list.Clear();
            }
        }

        /// <summary>
        /// Checks if the buffer is empty.
        /// </summary>
        public bool IsEmpty => _layeredCommands.All(pair => pair.Value.Count == 0);
    }
}
