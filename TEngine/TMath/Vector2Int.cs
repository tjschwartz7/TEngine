using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public class Vector2Int
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Constructor to initialize the vector
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Static property for the zero vector (0,0)
        public static Vector2Int zero = new Vector2Int(0, 0);

        // Addition of two vectors
        public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
        {
            return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);
        }

        // Subtraction of two vectors
        public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
        {
            return new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);
        }

        // Scalar multiplication (multiplying vector by a scalar)
        public static Vector2Int operator *(Vector2Int v, int scalar)
        {
            return new Vector2Int(v.X * scalar, v.Y * scalar);
        }

        // Scalar division (dividing vector by a scalar)
        public static Vector2Int operator /(Vector2Int v, int scalar)
        {
            return new Vector2Int(v.X / scalar, v.Y / scalar);
        }

        // Dot product of two vectors
        public static int Dot(Vector2Int v1, Vector2Int v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        // Magnitude (length) of the vector
        public int Magnitude()
        {
            return (int)Math.Sqrt(X * X + Y * Y);
        }

        // Normalize the vector (unit vector)
        public Vector2Int Normalize()
        {
            int magnitude = Magnitude();
            if (magnitude > 0)
                return new Vector2Int(X / magnitude, Y / magnitude);
            return zero; // Return zero vector if magnitude is 0 to avoid division by zero
        }

        // Get the perpendicular vector (90 degrees rotation)
        public Vector2Int Perpendicular()
        {
            return new Vector2Int(-Y, X); // Counter-clockwise 90 degrees
        }

        // Angle between two vectors in radians
        public static int AngleBetween(Vector2Int v1, Vector2Int v2)
        {
            int dot = Dot(v1, v2);
            int magnitudeProduct = v1.Magnitude() * v2.Magnitude();
            if (magnitudeProduct == 0) return 0;
            return (int)Math.Acos(dot / magnitudeProduct);
        }

        // Negate the vector
        public static Vector2Int operator -(Vector2Int v)
        {
            return new Vector2Int(-v.X, -v.Y);
        }

        // ToString for easy debugging
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
