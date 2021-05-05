#region usings

using OpenTK;
using OpenTK.Graphics.ES10;
using SM.Base.Textures;
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
        /// Sets the offset relative to the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture it should use.</param>
        /// <param name="pixelLocation">The offset in pixel.</param>
        public void SetOffsetRelative(Texture texture, Vector2 pixelLocation)
        {
            Vector2 textureSize = new Vector2(texture.Width, texture.Height);
            Offset.Set( Vector2.Divide(pixelLocation, textureSize) );
        }
        /// <summary>
        /// Sets the scale relative to the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="rectangleSize">The scale in pixel.</param>
        public void SetScaleRelative(Texture texture, Vector2 rectangleSize)
        {
            Vector2 textureSize = new Vector2(texture.Width, texture.Height);
            Scale.Set( Vector2.Divide(rectangleSize, textureSize) );
        }

        /// <summary>
        /// Sets the offset and scale relative to the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="location">Offset in pixel</param>
        /// <param name="rectangleSize">Scale in pixel.</param>
        public void SetRectangleRelative(Texture texture, Vector2 location, Vector2 rectangleSize)
        {
            Vector2 textureSize = new Vector2(texture.Width, texture.Height);

            Offset.Set(Vector2.Divide(location, textureSize));
            Scale.Set(Vector2.Divide(rectangleSize, textureSize));
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