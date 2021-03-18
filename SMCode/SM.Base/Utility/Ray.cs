#region usings

using System;
using OpenTK;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Utility
{
    /// <summary>
    /// A utility class to calculate rays.
    /// </summary>
    public struct Ray
    {
        /// <summary>
        /// The position of the ray.
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Constructs a ray.
        /// </summary>
        /// <param name="position">The position of the ray</param>
        /// <param name="direction">The direction of the ray.</param>
        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction.Normalized();
        }

        /// <summary>
        /// Calculates a object intersection with OBB.
        /// </summary>
        /// <param name="box">The bounding box of the object</param>
        /// <param name="modelMatrix">The model matrix of the object</param>
        /// <param name="distance">Distance to the object.</param>
        /// <returns>If true, it intersects with the object.</returns>
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