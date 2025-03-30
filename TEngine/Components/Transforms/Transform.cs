using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components.Transform
{
    public class Transform : Component
    {
        public void TransformX(int deltaX)
        {
            if (deltaX != 0)
            {
                bool canMove = true;
                int newX = X + deltaX;

                if (_hasCollisions)
                {
                    if (deltaX < 0)
                    {
                        for (int i = 0; i > deltaX; i -= 1)
                        {
                            canMove = canMove & CanMove(newX + i, Y);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < deltaX; i += 1)
                        {
                            canMove = canMove & CanMove(newX + i, Y);
                        }
                    }
                }

                if (canMove)
                {
                    X = newX;
                }
            }
        }

        public void TransformY(int deltaY)
        {

            if (deltaY != 0)
            {
                bool canMove = true;
                int newY = Y + deltaY;

                if (_hasCollisions)
                {
                    if (deltaY < 0)
                    {
                        for (int i = 0; i > deltaY; i -= 1)
                        {
                            canMove = canMove & CanMove(X, newY + i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < deltaY; i += 1)
                        {
                            canMove = canMove & CanMove(X, newY + i);
                        }
                    }
                }

                if (canMove)
                {
                    Y = newY;
                }
            }
        }
    }
}
