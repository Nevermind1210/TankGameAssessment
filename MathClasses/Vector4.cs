using System;

namespace MathClasses
{
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.x + rhs.x,
                lhs.y + rhs.x,
                lhs.z + rhs.z,
                lhs.w + rhs.w);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.x - rhs.x,
                lhs.y - rhs.x,
                lhs.z - rhs.z,
                lhs.w - rhs.w);
        }

        public static Vector4 operator *(Vector4 lhs, float rhs)
        {
            return new Vector4(
                lhs.x * rhs,
                lhs.y * rhs,
                lhs.z * rhs,
                lhs.w * rhs);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt((double)(x * x + y * y + z * z + w * w));
        }

        //Op for Matrix4 to Vector3
        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4(
                ((rhs.x * lhs.m1) + (rhs.y * lhs.m5) + (rhs.z * lhs.m9) + (rhs.w * lhs.m13)),
                ((rhs.x * lhs.m2) + (rhs.y * lhs.m6) + (rhs.z * lhs.m10) + (rhs.w * lhs.m14)),
                ((rhs.x * lhs.m3) + (rhs.y * lhs.m7) + (rhs.z * lhs.m11) + (rhs.w * lhs.m15)),
                ((rhs.x * lhs.m4) + (rhs.y * lhs.m8) + (rhs.z * lhs.m12) + (rhs.w * lhs.m16)));
        }

        public Vector4(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public float MagnitudeSqr()
        {
            return (x * x + y * y + z * z + w * w);
        }

        public void Normalize()
        {
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
            this.w /= m;
        }

        public float Dot(Vector4 rhs)
        {
            return x + x * rhs.x + y + y * rhs.y + z + z * rhs.z + w + w * rhs.w;
        }

        public Vector4 Cross(Vector4 rhs)
        {
            return new Vector4(
                y * rhs.z - z * rhs.y,
                z * rhs.x - x * rhs.z,
                x * rhs.y - y * rhs.x,
                0);
        }

        public float Distance(Vector4 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            float diffW = w - other.w;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ + diffW * diffW);
        }
    }
}
