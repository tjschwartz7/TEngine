using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public static class RectExtensions
    {
        public static Vector2 ClampPoint(this RectangleF rect, Vector2 point)
        {
            float x = Math.Clamp(point.X, rect.Left, rect.Right);
            float y = Math.Clamp(point.Y, rect.Top, rect.Bottom);
            return new Vector2(x, y);
        }

        public static Vector2 ClampPoint(this RectangleInt rect, Vector2 point)
        {
            int x = Math.Clamp((int)point.X, rect.Left, rect.Right - 1);
            int y = Math.Clamp((int)point.Y, rect.Top, rect.Bottom - 1);
            return new Vector2(x, y);
        }
    }

}
