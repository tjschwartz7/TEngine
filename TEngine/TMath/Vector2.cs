using System;

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

        // Static properties for the zero vector (0,0) and the one vector (1,1)
        public static Vector2 zero = new Vector2(0, 0);
        public static Vector2 one = new Vector2(1, 1);

        // Operator overloads
        public static Vector2 operator +(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2 operator -(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X - v2.X, v1.Y - v2.Y);

        public static Vector2 operator -(Vector2 v) =>
            new Vector2(-v.X, -v.Y);

        public static Vector2 operator *(Vector2 v, float scalar) =>
            new Vector2(v.X * scalar, v.Y * scalar);

        public static Vector2 operator /(Vector2 v, float scalar) =>
            new Vector2(v.X / scalar, v.Y / scalar);

        // Dot product of two vectors
        public static float Dot(Vector2 v1, Vector2 v2) =>
            v1.X * v2.X + v1.Y * v2.Y;

        // Magnitude (length) of the vector
        public float Magnitude() =>
            (float)Math.Sqrt(X * X + Y * Y);

        // Normalize the vector (unit vector)
        public Vector2 Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > 0)
                return new Vector2(X / magnitude, Y / magnitude);
            return zero; // Return zero vector if magnitude is 0 to avoid division by zero
        }

        // Get the perpendicular vector (90 degrees counter-clockwise)
        public Vector2 Perpendicular() =>
            new Vector2(-Y, X); // Counter-clockwise 90 degrees

        // Angle between two vectors in radians
        public static float AngleBetween(Vector2 v1, Vector2 v2)
        {
            float dot = Dot(v1, v2);
            float magnitudeProduct = v1.Magnitude() * v2.Magnitude();
            if (magnitudeProduct == 0) return 0;
            return (float)Math.Acos(dot / magnitudeProduct);
        }

        // Linear interpolation (Lerp)
        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector2(
                start.X + (end.X - start.X) * t,
                start.Y + (end.Y - start.Y) * t
            );
        }

        // Spherical Linear interpolation (Slerp) (approximated by Lerp)
        public static Vector2 Slerp(Vector2 start, Vector2 end, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return Lerp(start, end, t); // Approximation with Lerp
        }

        // Clamping the vector between a min and max vector
        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y)
            );
        }

        // Convert Vector2 to Vector2Int (rounding)
        public Vector2Int ToVector2Int() =>
            new Vector2Int((int)Math.Round(X), (int)Math.Round(Y));

        // Convert Vector2 to Vector2Int (flooring)
        public Vector2Int ToVector2IntFloor() =>
            new Vector2Int((int)Math.Floor(X), (int)Math.Floor(Y));

        // Convert Vector2 to Vector2Int (ceiling)
        public Vector2Int ToVector2IntCeiling() =>
            new Vector2Int((int)Math.Ceiling(X), (int)Math.Ceiling(Y));

        // ToString for easy debugging
        public override string ToString() =>
            $"({X}, {Y})";
    }
}
