using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.TMath
{
    public struct Matrix4x4
    {
        private float[,] _m;

        public static Matrix4x4 Identity =>
            new Matrix4x4(new float[,]
            {
            {1,0,0,0},
            {0,1,0,0},
            {0,0,1,0},
            {0,0,0,1}
            });

        public Matrix4x4(float[,] values)
        {
            if (values.GetLength(0) != 4 || values.GetLength(1) != 4)
            {
                throw new ArgumentException("Matrix must be 4x4.", nameof(values));
            }
            _m = values;
        }

        public static Matrix4x4 CreateTranslation(float x, float y, float z = 0)
        {
            var m = Identity;
            m._m[0, 3] = x;
            m._m[1, 3] = y;
            m._m[2, 3] = z;
            return m;
        }

        public static Matrix4x4 CreateScale(float x, float y, float z = 1)
        {
            var m = Identity;
            m._m[0, 0] = x;
            m._m[1, 1] = y;
            m._m[2, 2] = z;
            return m;
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            var result = new float[4, 4];
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                {
                    result[row, col] = 0;
                    for (int k = 0; k < 4; k++)
                        result[row, col] += a._m[row, k] * b._m[k, col];
                }

            return new Matrix4x4(result);
        }

        public Vector3 Transform(Vector3 v)
        {
            float x = v.X * _m[0, 0] + v.Y * _m[0, 1] + v.Z * _m[0, 2] + _m[0, 3];
            float y = v.X * _m[1, 0] + v.Y * _m[1, 1] + v.Z * _m[1, 2] + _m[1, 3];
            float z = v.X * _m[2, 0] + v.Y * _m[2, 1] + v.Z * _m[2, 2] + _m[2, 3];
            return new Vector3(x, y, z);
        }
    }

}
