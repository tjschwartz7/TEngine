using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public Vector2 Position => new Vector2(X, Y);
        public Vector2 Size => new Vector2(Width, Height);
        public Vector2 Center => new Vector2(X + Width / 2f, Y + Height / 2f);

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public bool Contains(float px, float py)
        {
            return px >= X && px <= X + Width &&
                   py >= Y && py <= Y + Height;
        }

        public bool Contains(Vector2 point)
        {
            return Contains(point.X, point.Y);
        }

        public bool Intersects(RectangleF other)
        {
            return !(other.Left > Right ||
                     other.Right < Left ||
                     other.Top > Bottom ||
                     other.Bottom < Top);
        }

        public override string ToString()
        {
            return $"RectF [X={X}, Y={Y}, W={Width}, H={Height}]";
        }
    }

}
