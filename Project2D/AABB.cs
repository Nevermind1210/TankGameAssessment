using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

namespace Project2D
{
    class AABB
    {
        Vector3 min = new Vector3(float.NegativeInfinity,
                                  float.NegativeInfinity,
                                  float.NegativeInfinity);
        Vector3 max = new Vector3(float.PositiveInfinity,
                                  float.PositiveInfinity,
                                  float.PositiveInfinity);

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }
        //This is to resize the Vector3 values for the min and max vals.
        public void Resize(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }


        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x = min.x) * 0.5f,
                    Math.Abs(max.y - min.y) * 0.5f,
                    Math.Abs(max.z - min.z) * 0.5f);
        }

        public List<Vector3> Corners()
        {
            //ignore Z axis for 2D
            List<Vector3> corners = new List<Vector3>(4);
            corners[0] = min;
            corners[1] = new Vector3(min.x, max.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, min.y, min.z);
            return corners;
        }

        public void Fit(List<Vector3> points)
        {
            // invalidates the extents
            Vector3 min = new Vector3(float.PositiveInfinity,
                                  float.PositiveInfinity,
                                  float.PositiveInfinity);
            Vector3 max = new Vector3(float.NegativeInfinity,
                                  float.NegativeInfinity,
                                  float.NegativeInfinity);

            //find min an dmax of the points
            foreach(Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        public void Fit(Vector3[] points)
        {
            // invalidates the extents
            Vector3 min = new Vector3(float.PositiveInfinity,
                                  float.PositiveInfinity,
                                  float.PositiveInfinity);
            Vector3 max = new Vector3(float.NegativeInfinity,
                                  float.NegativeInfinity,
                                  float.NegativeInfinity);

            //find min an dmax of the points
            foreach (Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        public bool Overlaps(Vector3 p)
        {
            //testing for not overlapped as it exits faster
            return !(p.x < min.x || p.y < min.y ||
                p.x > max.x || p.y > max.y);
        }

        public bool Overlaps(AABB other)
        {
            //test for not overlapped as it exits faster
            return !(max.x < other.min.x ||max.y < other.min.y ||
                min.x > other.max.x || min.y > other.max.y);
        }

        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }
            public bool IsEmpty()
        {
            if (float.IsNegativeInfinity(min.x) &&
                float.IsNegativeInfinity(min.z) &&
                float.IsInfinity(max.x) &&
                float.IsInfinity(max.y) &&
                float.IsInfinity(max.z))
                return true;
            return false;
        }
        public void Empty()
        {
            Vector3 min = new Vector3(float.NegativeInfinity,
                                   float.NegativeInfinity,
                                   float.NegativeInfinity);
            Vector3 max = new Vector3(float.PositiveInfinity,
                                      float.PositiveInfinity,
                                      float.PositiveInfinity);
        }
        
        void SetToTransformedBox(AABB box, Matrix3 m)
        {
            //If Empty, then exit (an empty box defined as having the min/max
            // set to Infinity)
            if (box.IsEmpty())
            {
                Empty();
                return;
            }

            //Examine each of the nine martix elements
            //and compute the new AABB
            if (m.m1 > 0.0f) //m1 = m11 in the formula above
            {
                min.x += m.m1 * box.min.x; max.x += m.m1 * box.max.x;
            }
            else
            {
                min.x += m.m1 * box.max.x; max.x += m.m1 * box.min.x;
            }
            if (m.m2 > 0.0f) //m2 = m12 in the formula abooooove
            {
                min.y += m.m2 * box.min.x; max.y += m.m2 * box.max.x;
            }
            else
            {
                min.y += m.m2 * box.max.x; max.y += m.m2 * box.min.x;
            }
            if (m.m3 > 0.0f)   // m3 = m13 in the formula above
            {
                min.z += m.m3 * box.min.x; max.z += m.m3 * box.max.x;
            }
            else
            {
                min.z += m.m3 * box.max.x; max.z += m.m3 * box.min.x;
            }
            if (m.m4 > 0.0f) // m4 = m14 in the formula above
            {
                min.x += m.m4 * box.min.x; max.x += m.m4 * box.max.x;
            }
            else
            {
                min.x += m.m4 * box.min.x; max.x += m.m4 * box.max.x;
            }
            if (m.m5 > 0.0f) // m5 = m15 in the furrrmullaaa abrroovveee
            {
                min.y += m.m5 * box.min.x; max.y += m.m5 * box.max.x;
            }
            else
            {
                min.y += m.m5 * box.min.x; max.y += m.m5 * box.max.x;
            }
            if (m.m6 > 0.0f) // m6 = m16 in th... you know...maaaaaaaan
            {
                min.z += m.m6 * box.min.x; max.z += m.m6 * box.max.x;
            }
            else
            {
                min.z += m.m6 * box.min.x; max.z += m.m6 * box.max.x;
            }
        }
    }
}
