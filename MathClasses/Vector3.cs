using System;

namespace MathClasses
{
    public class Vector3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.x + rhs.x, 
                lhs.y + rhs.y,  
                lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.x - rhs.x, 
                lhs.y - rhs.y, 
                lhs.z - rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3(
                lhs.x * rhs,
                lhs.y * rhs,
                lhs.z * rhs);
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt((double)(x * x + y * y + z * z));
        }

        //Op for Matrix3 to Vector3
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(
                ((rhs.x  * lhs.m1) + (rhs.y * lhs.m4) + (rhs.z * lhs.m7)),
                ((rhs.x * lhs.m2) + (rhs.y * lhs.m5) + (rhs.z * lhs.m8)),
                ((rhs.x * lhs.m3) + (rhs.y * lhs.m6) + (rhs.z * lhs.m9)));
        }

        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public float MagnitudeSqr()
        {
            return (x * x + y * y + z * z);
        }

        public void Normalize()
        {
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
        }

        public Vector3 GetNormalised()
        {
            return (this / Magnitude());
        }

        public float Dot(Vector3 rhs)
        {
            return x + x * rhs.x + y + y * rhs.y + z + z * rhs.z;
        }

        public Vector3 Cross(Vector3 rhs)
        {
            return new Vector3(
                y * rhs.z - z * rhs.y,
                z * rhs.x - x * rhs.z,
                x * rhs.y - y * rhs.x);
        }

        Vector3 a, b;

        float angle = (float)Math.Acos(a.Dot(b));

        float AngleBetween(Vector3 other)
        {
            // Normalise the Vectors
            Vector3 a = GetNormalised();
            Vector3 b = other.GetNormalised();

            //calculate the dot product
            float d = a.x * b.x + a.y * b.y + a.z * b.z;

            //return the angle between them
            return (float)Math.Acos(d);
        }
        public float Distance(Vector4 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
        }

        public void Set
            (
            float _m1 = 0, float _m2 = 0, float _m3 = 0,
            float _m4 = 0, float _m5 = 0, float _m6 = 0,
            float _m7 = 0, float _m8 = 0, float _m9 = 0
            )
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }

        public void Set(Vector3 m)
        {
            m1 = m.m2; m2 = m.m2; m3 = m.m3;
            m4 = m.m4; m5 = m.m5; m6 = m.m6;
            m7 = m.m7; m8 = m.m8; m9 = m.m9;
        }

        public float Distance(Vector3 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
        }


        public void SetScaled(Vector3 v)
        {
            m1 = v.x; m2 = 0; m3 = 0;
            m4 = 0; m5 = v.y; m6 = 0;
            m7 = 0; m8 = 0; m9 = v.z;
        }

        void Scale(Vector3 v)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(v.x, v.y, v.z);

            Set(this * m);
        }

        public void SetRotateX(double radians)
        {
            Set(1, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians));
        }

        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);

            Set(this * m);
        }

        public void SetRotateY(double radians)
        {
            Set(

                (float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
                0, 1, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians)

                );
        }

        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);

            Set(this * m);
        }

        public void SetRotateZ(double radians)
        {
            Set
                (
                    (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                    (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
                    0, 0, 1
                );
        }

        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);

            //Set(this * m);
        }
        void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix3 x = new Matrix3();
            Matrix3 y = new Matrix3();
            Matrix3 z = new Matrix3();
            x.SetRotateX(pitch);
            y.SetRotateY(yaw);
            z.SetRotateZ(roll);

            //combine rotations  in a specific order
            Set(z * y * x);
        }
    }
}
