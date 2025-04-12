using System;

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
        public static Vector2Int one = new Vector2Int(1, 1);

        // Addition of two vectors
        public static Vector2Int operator +(Vector2Int v1, Vector2Int v2) =>
            new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);

        // Subtraction of two vectors
        public static Vector2Int operator -(Vector2Int v1, Vector2Int v2) =>
            new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);

        // Scalar multiplication (multiplying vector by a scalar)
        public static Vector2Int operator *(Vector2Int v, int scalar) =>
            new Vector2Int(v.X * scalar, v.Y * scalar);

        // Scalar division (dividing vector by a scalar)
        public static Vector2Int operator /(Vector2Int v, int scalar) =>
            new Vector2Int(v.X / scalar, v.Y / scalar);

        // Dot product of two vectors
        public static int Dot(Vector2Int v1, Vector2Int v2) =>
            v1.X * v2.X + v1.Y * v2.Y;

        // Magnitude (length) of the vector
        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        // Normalize the vector (unit vector) (not practical for integer vectors, approximation)
        public Vector2Int Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > 0)
                return new Vector2Int((int)(X / magnitude), (int)(Y / magnitude));
            return zero;
        }

        // Clamping the vector between a min and max vector
        public static Vector2Int Clamp(Vector2Int value, Vector2Int min, Vector2Int max)
        {
            return new Vector2Int(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y)
            );
        }

        // Linear interpolation (Lerp)
        public static Vector2Int Lerp(Vector2Int start, Vector2Int end, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector2Int(
                (int)(start.X + (end.X - start.X) * t),
                (int)(start.Y + (end.Y - start.Y) * t)
            );
        }

        // Spherical Linear interpolation (Slerp) (approximated by Lerp)
        public static Vector2Int Slerp(Vector2Int start, Vector2Int end, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return Lerp(start, end, t); // Approximation with Lerp
        }

        // Convert Vector2 to Vector2Int (rounding)
        public static Vector2Int FromVector2(Vector2 v) =>
            new Vector2Int((int)Math.Round(v.X), (int)Math.Round(v.Y));

        // Convert Vector2 to Vector2Int (flooring)
        public static Vector2Int FromVector2Floor(Vector2 v) =>
            new Vector2Int((int)Math.Floor(v.X), (int)Math.Floor(v.Y));

        // Convert Vector2 to Vector2Int (ceiling)
        public static Vector2Int FromVector2Ceiling(Vector2 v) =>
            new Vector2Int((int)Math.Ceiling(v.X), (int)Math.Ceiling(v.Y));

        // Convert Vector3 to Vector2Int (rounding)
        public static Vector2Int FromVector3(Vector3 v) =>
            new Vector2Int((int)Math.Round(v.X), (int)Math.Round(v.Y));

        // Convert Vector3 to Vector2Int (flooring)
        public static Vector2Int FromVector3Floor(Vector3 v) =>
            new Vector2Int((int)Math.Floor(v.X), (int)Math.Floor(v.Y));

        // Convert Vector3 to Vector2Int (ceiling)
        public static Vector2Int FromVector3Ceiling(Vector3 v) =>
            new Vector2Int((int)Math.Ceiling(v.X), (int)Math.Ceiling(v.Y));

        // Negate the vector
        public static Vector2Int operator -(Vector2Int v) =>
            new Vector2Int(-v.X, -v.Y);

        // ToString for easy debugging
        public override string ToString() =>
            $"({X}, {Y})";
    }
}
