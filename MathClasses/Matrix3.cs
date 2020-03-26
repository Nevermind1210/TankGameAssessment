using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }
        public Matrix3(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9)
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }
        public void Set(float _m1, float _m2, float _m3, float _m4, float _m5, float _m6, float _m7, float _m8, float _m9)
        {
            m1 = _m1; m2 = _m2;  m3 = _m3;
            m4 = _m4; m5 = _m5;  m6 = _m6;
            m7 = _m7; m8 = _m8;  m9 = _m9;
        }
        public void SetRotateX(double radians)
        {
            Set(1, 0, 0, 0, (float)Math.Cos(radians),(float)Math.Sin(radians), 0, (float)Math.Sin(radians),(float)(float)Math.Cos(radians));
        }
        //public void SetRotateY(double radians)
        //{
        //    Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0, (float)Math.Sin(radians), (float)(float)Math.Cos(radians));
        //}
        //public void SetRotateZ(double radians)
        //{
        //    Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0, (float)Math.Sin(radians), (float)(float)Math.Cos(radians));
        }
    }