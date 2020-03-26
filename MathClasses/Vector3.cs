using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public float MagnitudeSqr()
        {
            return (x * x + y * y + z * z);
        }
        public float Distance(Vector3 other)
        {
            float diffX = x - other.x;
            float diffY = y - other.y;
            float diffZ = z - other.z;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
        }
        public void Normalize()
        {
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
        }

        public float Magnitude()
        {
            return 0;
        }


        //Vector3 myVec = new Vector3(1, 1, 1);
        //float mag = myVec.Magnitude();
        //myVec /= mag; 

        //float radius = 10;
        //Vector3 toTarget;

        //if (toTarget.MagnitudeSqr() <= (radius* radius))
        //    {

        //    }
    }
}
