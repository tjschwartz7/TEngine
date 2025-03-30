using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        // Constructor to initialize the vector
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        // Static property for the zero vector (0,0)
        public static Vector2 zero = new Vector2(0, 0);

        // Addition of two vectors
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        // Subtraction of two vectors
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        // Scalar multiplication (multiplying vector by a scalar)
        public static Vector2 operator *(Vector2 v, float scalar)
        {
            return new Vector2(v.X * scalar, v.Y * scalar);
        }

        // Scalar division (dividing vector by a scalar)
        public static Vector2 operator /(Vector2 v, float scalar)
        {
            return new Vector2(v.X / scalar, v.Y / scalar);
        }

        // Dot product of two vectors
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        // Magnitude (length) of the vector
        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        // Normalize the vector (unit vector)
        public Vector2 Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > 0)
                return new Vector2(X / magnitude, Y / magnitude);
            return zero; // Return zero vector if magnitude is 0 to avoid division by zero
        }

        // Get the perpendicular vector (90 degrees rotation)
        public Vector2 Perpendicular()
        {
            return new Vector2(-Y, X); // Counter-clockwise 90 degrees
        }

        // Angle between two vectors in radians
        public static float AngleBetween(Vector2 v1, Vector2 v2)
        {
            float dot = Dot(v1, v2);
            float magnitudeProduct = v1.Magnitude() * v2.Magnitude();
            if (magnitudeProduct == 0) return 0;
            return (float)Math.Acos(dot / magnitudeProduct);
        }

        // Negate the vector
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        // ToString for easy debugging
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
