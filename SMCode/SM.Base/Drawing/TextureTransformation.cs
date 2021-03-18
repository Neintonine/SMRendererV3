#region usings

using OpenTK;
using SM.Base.Types;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    /// Stores transformations for the textures.
    /// </summary>
    public class TextureTransformation
    {
        /// <summary>
        /// The offset from the origin.
        /// </summary>
        public CVector2 Offset = new CVector2(0);
        /// <summary>
        /// The rotation of the texture.
        /// </summary>
        public CVector1 Rotation = new CVector1(0);
        /// <summary>
        /// The scale of the texture.
        /// </summary>
        public CVector2 Scale = new CVector2(1);

        /// <summary>
        /// Get the texture matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix3 GetMatrix()
        {
            return CalculateMatrix(Offset, Scale, Rotation);
        }

        /// <summary>
        /// Calculates a texture matrix.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Matrix3 CalculateMatrix(Vector2 offset, Vector2 scale, float rotation)
        {
            float radians = MathHelper.DegreesToRadians(rotation);
            Matrix3 result = Matrix3.CreateScale(scale.X, scale.Y, 1) * Matrix3.CreateRotationZ(radians);
            result.Row2 = new Vector3(offset.X, offset.Y, 1);
            return result;
        }
    }
}