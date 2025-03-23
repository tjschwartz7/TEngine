using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TEngine.Rendering
{
    public class TextRenderer
    {
        private class Layer
        {
            public int Order { get; }
            public int Height { get; }
            public List<string> Content { get; }

            public Layer(int order, int height)
            {
                Order = order;
                Height = height;
                Content = new List<string>();
            }

            public void AddLine(string line)
            {
                if (Content.Count < Height)
                {
                    Content.Add(line);
                }
            }

            public void Clear()
            {
                Content.Clear();
            }

            public List<string> GetFormattedContent()
            {
                List<string> formattedContent = new List<string>(Content);

                // Ensure layer matches its fixed height
                while (formattedContent.Count < Height) formattedContent.Add("");  // Pad with empty lines
                if (formattedContent.Count > Height) formattedContent = formattedContent.Take(Height).ToList(); // Trim excess

                return formattedContent;
            }
        }


        public static TextRenderer Instance { get; private set; } = new TextRenderer();
        private static Dictionary<string, Layer> layers = new();
        private static string lastScreenHash = "";

        private TextRenderer()
        {
            if(Instance != null)
            {
                throw new Exception("TextRenderer is a singleton and should not be instantiated multiple times.");
            }
            else
            {
                Instance = this;
            }
        }

        public int WindowWidth { get; set; } = Console.WindowWidth;
        public int WindowHeight { get; set; } = Console.WindowHeight;
        public bool DisplayBorders { get; set; } = true;
        public char BorderCharacter { get; set; } = '#';
        public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        

        public void RegisterLayer(string layer, int order, int height)
        {
            if (!layers.ContainsKey(layer))
            {
                layers[layer] = new Layer(order, height);
            }
        }

        public void AddText(string layer, string text)
        {
            if (layers.ContainsKey(layer))
            {
                layers[layer].AddLine(text);
            }
            else
            {
                throw new Exception($"Layer '{layer}' does not exist. Register it first.");
            }
        }

        public void ClearLayer(string layer)
        {
            if (layers.ContainsKey(layer))
            {
                layers[layer].Clear();
            }
        }

        public void Render()
        {
            // Check for console resize
            if (Console.WindowWidth != WindowWidth || Console.WindowHeight != WindowHeight)
            {
                WindowWidth = Console.WindowWidth;
                WindowHeight = Console.WindowHeight;
                Console.Clear(); // Redraw everything when resized
            }

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;

            // Sort layers by order
            var sortedLayers = layers.Values.OrderBy(layer => layer.Order);

            StringBuilder output = new StringBuilder();

            if (DisplayBorders)
            {
                // Draw top border
                Console.ForegroundColor = BorderColor;
                output.AppendLine(new string(BorderCharacter, WindowWidth));
            }

            foreach (var layer in sortedLayers)
            {
                for (int i = 0; i < layer.Height; i++)
                {                                         
                    if (DisplayBorders)
                    {
                        output.Append(BorderCharacter);
                        if (i <= layer.GetFormattedContent().Count)
                        {
                            string line = layer.GetFormattedContent()[i];
                            output.Append(line.PadRight(WindowWidth - 2)); // Ensure text fits inside the border
                        }
                        else
                        {
                            output.Append(new string(' ', WindowWidth - 2)); // Empty line if no content
                        }
                            output.AppendLine(BorderCharacter.ToString());
                    }
                    else
                    {
                        if (i <= layer.GetFormattedContent().Count)
                        {
                            string line = layer.GetFormattedContent()[i];
                            output.Append(line.PadRight(WindowWidth - 2)); // Ensure text fits inside the border
                        }
                        else
                        {
                            output.Append(new string(' ', WindowWidth - 2)); // Empty line if no content
                        }
                    }
                }
            }

            if (DisplayBorders)
            {
                // Draw bottom border
                output.AppendLine(new string(BorderCharacter, WindowWidth));
            }

            // Convert output to string
            string screenContent = output.ToString();
            // Compute hash
            string newHash = ComputeHash(screenContent);

            // Only update the screen if content has changed
            if (newHash != lastScreenHash)
            {
                Console.Clear();
                Console.Write(screenContent);
                lastScreenHash = newHash; // Store the new hash
            }
        }

        // Compute SHA256 hash of the screen content
        private static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
