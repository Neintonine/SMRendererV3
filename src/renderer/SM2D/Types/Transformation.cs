#region usings

using OpenTK;
using SM.Base.Drawing;
using SM.Base.Textures;
using SM.Base.Types;
using SM.Base.Utility;

#endregion

namespace SM2D.Types
{
    /// <inheritdoc />
    public class Transformation : GenericTransformation
    {
        /// <summary>
        /// The precision of the Z-Index.
        /// <para>High values can result into "z-fighting" and cliping.</para>
        /// </summary>
        public static int ZIndexPercision = 100;

        /// <summary>
        /// The transformations translation.
        /// </summary>
        public CVector2 Position { get; set; } = new CVector2(0);

        /// <summary>
        /// The scaling.
        /// </summary>
        public CVector2 Size { get; set; } = new CVector2(50);

        /// <summary>
        /// The rotation.
        /// </summary>
        public CVector1 Rotation { get; set; } = new CVector1(0);

        /// <summary>
        /// If true, the object get rotated on the X-Axis by 180°.
        /// </summary>
        public bool HorizontalFlip { get; set; } = false;

        /// <summary>
        /// If true, the object get rotated on the Y-Axis by 180°.
        /// </summary>
        public bool VerticalFlip { get; set; } = false;

        /// <summary>
        /// The ZIndex.
        /// </summary>
        public CVector1 ZIndex { get; set; } = new CVector1(0);

        /// <inheritdoc />
        protected override Matrix4 RequestMatrix()
        {
            float z = 1 / (float) ZIndexPercision * ZIndex;

            return Matrix4.CreateScale(Size.X * (VerticalFlip ? -1 : 1), Size.Y * (HorizontalFlip ? -1 : 1), 1) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) *
                   Matrix4.CreateTranslation(Position.X, Position.Y, z);
        }

        /// <summary>
        /// Rotates the object, so it SHOULD turn toward the position.
        /// </summary>
        public void TurnTo(Vector2 turnposition)
        {
            Rotation.Set(RotationUtility.TurnTowards(Position, turnposition));
        }

        /// <summary>
        /// Returns the vector the object is looking.
        /// </summary>
        /// <returns></returns>
        public Vector2 LookAtVector()
        {
            if (_modelMatrix.Determinant < 0.0001) return new Vector2(0);

            var vec = Vector3.TransformNormal(Vector3.UnitY, _modelMatrix);
            vec.Normalize();
            return vec.Xy;
        }

        /// <summary>
        /// The object scale gets set to the same as the resolution of the texture.
        /// </summary>
        /// <param name="texture"></param>
        public void ApplyTextureSize(Texture texture)
        {
            Size.Set(texture.Width, texture.Height);
        }

        /// <summary>
        /// The object scale gets set to the same aspect ratio as the texture and then it will set the width..
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="width"></param>
        public void ApplyTextureSize(Texture texture, float width)
        {
            Size.Set(width, width / texture.Aspect);
        }

        /// <summary>
        /// Adjusts <see cref="Size"/> for the texture transform.
        /// <para>In this way you can make sure, the texture is not stretched.</para>
        /// </summary>
        /// <param name="transform"></param>
        public void AdjustSizeToTextureTransform(TextureTransformation transform)
        {
            Size.Set(transform.Scale * Size);
        }
    }
}