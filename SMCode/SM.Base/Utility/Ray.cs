#region usings

using System;
using OpenTK;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Utility
{
    public struct Ray
    {
        public Vector3 Position;
        public Vector3 Direction;

        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction.Normalized();
        }

        public bool ObjectIntersection(BoundingBox box, Matrix4 modelMatrix, out float distance)
        {
            distance = 0.0f;
            float tMin = 0.0f;
            float tMax = 100000.0f;

            Vector3 delta = modelMatrix.Row3.Xyz - Position;

            for (int i = 0; i < 3; i++)
            {
                Vector3 axis = new Vector3(modelMatrix[i, 0], modelMatrix[i, 1], modelMatrix[i, 2]);

                float e = Vector3.Dot(axis, delta);
                float f = Vector3.Dot(Direction, axis);

                if (Math.Abs(f) > 0.001f)
                {
                    float t1 = (e + box.Min[i]) / f;
                    float t2 = (e + box.Max[i]) / f;

                    if (t1 > t2)
                    {
                        float w = t1;
                        t1 = t2;
                        t2 = w;
                    }

                    if (t2 < tMax) tMax = t2;
                    if (t1 > tMin) tMin = t1;

                    if (tMax < tMin)
                        return false;
                }
                else
                {
                    if (-e + box.Min[i] > 0.0f || -e + box.Max[i] < 0.0f)
                        return false;
                }
            }

            distance = tMin;
            return true;
        }
    }
}