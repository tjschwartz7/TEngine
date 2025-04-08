using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Mesh.Text
{
    public class TextMesh : Mesh
    {
        public string Text { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        // Constructor
        public TextMesh()
        {
            Text = string.Empty;
            FontSize = 12;
            FontFamily = "Arial";
            Bold = false;
            Italic = false;
            Underline = false;
        }
        // Method to update the text mesh properties
        public void UpdateTextMesh(string newText, int newFontSize, string newFontFamily)
        {
            Text = newText;
            FontSize = newFontSize;
            FontFamily = newFontFamily;
        }
    }
}
