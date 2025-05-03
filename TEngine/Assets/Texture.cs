using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Assets
{
    public enum TextureType
    {
        Image,
        Font,
        Tilemap,
        TextBlock
    }

    public class Texture
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public TextureType Type { get; }

        // Raw pixel data — can be a 2D char array for text mode,
        // or a byte[] for raw image data (if extended later)
        private readonly object _data;

        public Texture(string name, int width, int height, object data, TextureType type)
        {
            Name = name;
            Width = width;
            Height = height;
            _data = data;
            Type = type;
        }

        public T GetData<T>() => (T)_data;

        // Optional helper: useful for debugging or previews
        public override string ToString() =>
            $"Texture(Name={Name}, Size={Width}x{Height}, Type={Type})";
    }
}
