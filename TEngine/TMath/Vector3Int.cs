using System;

namespace TEngine.TMath
{
    public class Vector3Int
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Static fields
        public static Vector3Int zero = new Vector3Int(0, 0, 0);
        public static Vector3Int one = new Vector3Int(1, 1, 1);

        // Operator overloads
        public static Vector3Int operator +(Vector3Int a, Vector3Int b) =>
            new Vector3Int(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3Int operator -(Vector3Int a, Vector3Int b) =>
            new Vector3Int(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3Int operator -(Vector3Int v) =>
            new Vector3Int(-v.X, -v.Y, -v.Z);

        public static Vector3Int operator *(Vector3Int v, int scalar) =>
            new Vector3Int(v.X * scalar, v.Y * scalar, v.Z * scalar);

        public static Vector3Int operator /(Vector3Int v, int scalar) =>
            new Vector3Int(v.X / scalar, v.Y / scalar, v.Z / scalar);

        // Dot product (returns int)
        public static int Dot(Vector3Int a, Vector3Int b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        // Distance (int-based)
        public static int Distance(Vector3Int a, Vector3Int b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            int dz = a.Z - b.Z;
            return (int)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        // Clamp (clamps between min and max vector)
        public static Vector3Int Clamp(Vector3Int value, Vector3Int min, Vector3Int max)
        {
            return new Vector3Int(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y),
                Math.Clamp(value.Z, min.Z, max.Z)
            );
        }

        // Conversion to Vector3 (returns a Vector3 with float precision)
        public Vector3 ToVector3() =>
            new Vector3(X, Y, Z);

        // Conversion from Vector3 (rounding or flooring)
        public static Vector3Int FromVector3(Vector3 v)
        {
            return new Vector3Int(
                (int)Math.Floor(v.X),
                (int)Math.Floor(v.Y),
                (int)Math.Floor(v.Z)
            );
        }

        // Linear interpolation (Lerp) between Vector3Int and Vector3 (returns Vector3)
        public static Vector3 Lerp(Vector3Int a, Vector3Int b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector3(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t,
                a.Z + (b.Z - a.Z) * t
            );
        }

        // Spherical Linear interpolation (Slerp) between Vector3Int and Vector3 (returns Vector3)
        public static Vector3 Slerp(Vector3Int a, Vector3Int b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return Lerp(a, b, t); // Use Lerp as an approximation for Slerp in int space
        }

        public static Vector2 ToVector2(Vector3Int v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Vector3Int FromVector2(Vector2 v, float z = 0)
        {
            return new Vector3Int((int)v.X, (int)v.Y, z);
        }

        public static Vector2Int ToVector2Int(Vector3Int v)
        {
            return new Vector2Int((int)v.X, (int)v.Y);
        }

        public static Vector3Int FromVector2Int(Vector2Int v, float z = 0)
        {
            return new Vector3Int((int)v.X, (int)v.Y, (int)z);
        }

        public static Vector3Int RoundToInt(Vector3 v)
        {
            return new Vector3Int((int)Math.Round(v.X), (int)Math.Round(v.Y), (int)Math.Round(v.Z));
        }

        // ToString override
        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
