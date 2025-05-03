using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public struct RectangleInt
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public int Left => X;
        public int Right => X + Width;
        public int Top => Y;
        public int Bottom => Y + Height;

        public Vector2 Center => new Vector2(X + Width / 2f, Y + Height / 2f);

        public RectangleInt(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Contains(int px, int py)
        {
            return px >= X && px < X + Width &&
                   py >= Y && py < Y + Height;
        }

        public bool Contains(Vector2 point)
        {
            return Contains((int)point.X, (int)point.Y);
        }

        public bool Intersects(RectangleInt other)
        {
            return !(other.Left >= Right ||
                     other.Right <= Left ||
                     other.Top >= Bottom ||
                     other.Bottom <= Top);
        }

        public override string ToString()
        {
            return $"RectInt [X={X}, Y={Y}, W={Width}, H={Height}]";
        }
    }
}
