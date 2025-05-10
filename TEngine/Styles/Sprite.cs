using TEngine.TMath;

namespace TEngine.Styles
{
    public class Sprite
    {
        public Texture Texture { get; }
        public RectangleF SourceRect { get; }
        public Vector2 Pivot { get; }  // Normalized (0-1) or pixel-based, your choice
        public Vector2 Size { get; }   // World or screen units

        public Sprite(Texture texture, RectangleF sourceRect, Vector2 pivot, Vector2 size)
        {
            Texture = texture;
            SourceRect = sourceRect;
            Pivot = pivot;
            Size = size;
        }

        public static Sprite FromFullTexture(Texture texture)
        {
            return new Sprite(texture,
                new RectangleF(0, 0, texture.Width, texture.Height),
                new Vector2(0.5f, 0.5f), // center
                new Vector2(texture.Width, texture.Height));
        }
    }
}
