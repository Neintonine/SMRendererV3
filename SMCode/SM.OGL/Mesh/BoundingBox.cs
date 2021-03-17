#region usings

using System;
using OpenTK;

#endregion

namespace SM.OGL.Mesh
{
    /// <summary>
    ///     Contains information about bounding boxes of meshes
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        ///     The maximum corner.
        /// </summary>
        public Vector3 Max = Vector3.Zero;

        /// <summary>
        ///     The minimum corner.
        /// </summary>
        public Vector3 Min = Vector3.Zero;

        /// <summary>
        ///     Empty constructor
        /// </summary>
        public BoundingBox()
        {
        }

        /// <summary>
        ///     Creates the bounding box with predefined min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        ///     Returns specific configurations of corners
        /// </summary>
        /// <param name="x">If true, it takes the X-value of maximum, otherwise the minimum.</param>
        /// <param name="y">If true, it takes the Y-value of maximum, otherwise the minimum.</param>
        /// <param name="z">If true, it takes the Z-value of maximum, otherwise the minimum.</param>
        /// <returns></returns>
        public Vector3 this[bool x, bool y, bool z] => Get(x,y,z);

        public Vector3 Get(bool x, bool y, bool z)
        {
            return new Vector3(x ? Max.X : Min.X, y ? Max.Y : Min.Y, z ? Max.Z : Min.Z);
        }

        public Vector3 Get(bool xyz) => Get(xyz, xyz, xyz);

        public Vector3 Get(Matrix4 transformation, bool x, bool y, bool z)
        {
            Vector3 get = Get(x, y, z);
            return (new Vector4(get, 1) * transformation).Xyz;
        }

        public Vector3 Get(Matrix4 transformation, bool xyz) => Get(transformation, xyz, xyz, xyz);

        public void GetBounds(Matrix4 transformation, out Vector3 min, out Vector3 max)
        {
            min = Get(transformation, false);
            max = Get(transformation, true);

            for (int i = 0; i < 3; i++)
            {
                float newmin = Math.Min(min[i], max[i]);
                float newmax = Math.Max(min[i], max[i]);

                min[i] = newmin;
                max[i] = newmax;
            }
        }

        public void Update(GenericMesh mesh)
        {
            int pos = 0;
            foreach (float f in mesh.Vertex)
            {
                Min[pos] = Math.Min(Min[pos], f);
                Max[pos] = Math.Max(Max[pos], f);

                pos++;
                pos %= mesh.Vertex.PointerSize;
            }
        }
        /// <summary>
        ///     Updates the bounding box.
        /// </summary>
        /// <param name="vector"></param>
        public void Update(Vector2 vector)
        {
            for (var i = 0; i < 2; i++)
            {
                Min[i] = Math.Min(Min[i], vector[i]);
                Max[i] = Math.Max(Max[i], vector[i]);
            }
        }

        /// <summary>
        ///     Updates the bounding box.
        /// </summary>
        /// <param name="vector"></param>
        public void Update(Vector3 vector)
        {
            for (var i = 0; i < 3; i++)
            {
                Min[i] = Math.Min(Min[i], vector[i]);
                Max[i] = Math.Max(Min[i], vector[i]);
            }
        }
    }
}