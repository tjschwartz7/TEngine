using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Assets.UI
{
    public class Font
    {
        public static Font Default = new Font("Consolas", 16, FontStyle.Regular);
        public string Name { get; set; } = "Consolas";
        public int Size { get; set; } = 16;
        public FontStyle Style { get; set; } = FontStyle.Regular;

        public Font(Font @default)
        {
            Default = @default;
        }
        public Font(string name, int size, FontStyle style)
        {
            Name = name;
            Size = size;
            Style = style;
        }
    }

    public enum FontStyle
    {
        Regular,
        Bold,
        Italic,
        Underline,
        Strikeout
    }
}
