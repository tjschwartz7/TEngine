using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    internal class GraphicsEngine
    {
        public GraphicsEngine(Style style) { _gameStyle = style; }
        public enum Style
        {
            TextBased = 0,
            G_2D,
            G_3D,
        }

        private Style _gameStyle;
        public Style GameStyle
        {
            get => _gameStyle;
        }

    }
}
