using System;
using System.Runtime.CompilerServices;
using OpenTK;

namespace SM.OGL.Mesh
{
    public class BoundingBox
    {
        public Vector3 Max = Vector3.Zero;
        public Vector3 Min = Vector3.Zero;

        public Vector3 this[bool x, bool y, bool z] => new Vector3(x ? Max.X : Min.X, y ? Max.Y : Min.Y, z ? Max.Z : Min.Z);

        public BoundingBox() {}

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public void Update(Vector2 vector)
        {
            for (int i = 0; i < 2; i++)
            {
                Min[i] = Math.Min(Min[i], vector[i]);
                Max[i] = Math.Max(Min[i], vector[i]);
            }
        }

        public void Update(Vector3 vector)
        {
            for (int i = 0; i < 3; i++)
            {
                Min[i] = Math.Min(Min[i], vector[i]);
                Max[i] = Math.Max(Min[i], vector[i]);
            }
        }
    }
}