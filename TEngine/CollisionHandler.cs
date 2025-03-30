using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine
{
    public class CollisionHandler
    {
        private HashSet<char> boundaryCharacters = new HashSet<char> { '#' };
        public static CollisionHandler Instance { get; private set; } = new CollisionHandler();

        public bool IsColliding(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            char characterAtPos = (char)Console.Read();
            return boundaryCharacters.Contains(characterAtPos);
        }

        public void addBoundaryCharacter(char boundChar)
        {
            if (!boundaryCharacters.Contains(boundChar))
            {
                boundaryCharacters.Add(boundChar);
            }
        }

        public void addBoundaryCharacters(string boundChars)
        {
            foreach(char boundChar in boundChars)
            {
                if (!boundaryCharacters.Contains(boundChar))
                {
                    boundaryCharacters.Add(boundChar);
                }
            } 
        }
    }
}
