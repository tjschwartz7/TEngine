using System;

namespace TEngine.TMath
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Static fields
        public static Vector3 zero = new Vector3(0, 0, 0);
        public static Vector3 one = new Vector3(1, 1, 1);

        // Operator overloads
        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3 operator -(Vector3 v) =>
            new Vector3(-v.X, -v.Y, -v.Z);

        public static Vector3 operator *(Vector3 v, float scalar) =>
            new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);

        public static Vector3 operator /(Vector3 v, float scalar) =>
            new Vector3(v.X / scalar, v.Y / scalar, v.Z / scalar);

        // Dot product
        public static float Dot(Vector3 a, Vector3 b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        // Magnitude
        public float Magnitude() =>
            (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        // Normalize
        public Vector3 Normalize()
        {
            float mag = Magnitude();
            if (mag == 0) return zero;
            return this / mag;
        }

        // Distance
        public static float Distance(Vector3 a, Vector3 b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            float dz = a.Z - b.Z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        // Clamp
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y),
                Math.Clamp(value.Z, min.Z, max.Z)
            );
        }

        // Project a onto b
        public static Vector3 Project(Vector3 a, Vector3 b)
        {
            float bMagSquared = Dot(b, b);
            if (bMagSquared == 0) return zero;
            float scalar = Dot(a, b) / bMagSquared;
            return b * scalar;
        }

        // Conversion to Vector3Int
        public Vector3Int FloorToInt()
        {
            return new Vector3Int((int)Math.Floor(X), (int)Math.Floor(Y), (int)Math.Floor(Z));
        }

        public Vector3Int CeilToInt()
        {
            return new Vector3Int((int)Math.Ceiling(X), (int)Math.Ceiling(Y), (int)Math.Ceiling(Z));
        }

        public Vector3Int RoundToInt()
        {
            return new Vector3Int((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Z));
        }

        // Linear interpolation (Lerp)
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Math.Clamp(t, 0f, 1f); // Ensures t is between 0 and 1
            return new Vector3(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t,
                a.Z + (b.Z - a.Z) * t
            );
        }

        // Spherical Linear interpolation (Slerp)
        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        {
            t = Math.Clamp(t, 0f, 1f); // Ensures t is between 0 and 1

            float dot = Dot(a, b);
            const float THRESHOLD = 0.9995f;

            // If the dot product is very close to 1, use linear interpolation
            if (dot > THRESHOLD)
            {
                return Lerp(a, b, t);
            }

            dot = Math.Clamp(dot, -1f, 1f);
            float theta_0 = (float)Math.Acos(dot);
            float theta = theta_0 * t;

            Vector3 bTemp = b - a * dot;
            bTemp = bTemp.Normalize();

            return a * (float)Math.Cos(theta) + bTemp * (float)Math.Sin(theta);
        }

        public static Vector2 ToVector2(Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Vector3 FromVector2(Vector2 v, float z = 0)
        {
            return new Vector3(v.X, v.Y, z);
        }

        public static Vector2Int ToVector2Int(Vector3 v)
        {
            return new Vector2Int((int)v.X, (int)v.Y);
        }

        public static Vector3 FromVector2Int(Vector2Int v, float z = 0)
        {
            return new Vector3(v.X, v.Y, z);
        }

        // ToString override
        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
