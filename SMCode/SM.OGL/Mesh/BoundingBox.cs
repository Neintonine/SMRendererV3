using System;
using System.Runtime.CompilerServices;
using OpenTK;

namespace SM.OGL.Mesh
{
    /// <summary>
    /// Contains information about bounding boxes of meshes
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// The minimum corner.
        /// </summary>
        public Vector3 Min = Vector3.Zero;
        /// <summary>
        /// The maximum corner.
        /// </summary>
        public Vector3 Max = Vector3.Zero;

        /// <summary>
        /// Returns specific configurations of corners
        /// </summary>
        /// <param name="x">If true, it takes the X-value of maximum, otherwise the minimum.</param>
        /// <param name="y">If true, it takes the Y-value of maximum, otherwise the minimum.</param>
        /// <param name="z">If true, it takes the Z-value of maximum, otherwise the minimum.</param>
        /// <returns></returns>
        public Vector3 this[bool x, bool y, bool z] => new Vector3(x ? Max.X : Min.X, y ? Max.Y : Min.Y, z ? Max.Z : Min.Z);

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BoundingBox() {}

        /// <summary>
        /// Creates the bounding box with predefined min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Updates the bounding box.
        /// </summary>
        /// <param name="vector"></param>
        public void Update(Vector2 vector)
        {
            for (int i = 0; i < 2; i++)
            {
                Min[i] = Math.Min(Min[i], vector[i]);
                Max[i] = Math.Max(Max[i], vector[i]);
            }
        }

        /// <summary>
        /// Updates the bounding box.
        /// </summary>
        /// <param name="vector"></param>
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