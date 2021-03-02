#region usings

using System;
using System.Drawing.Drawing2D;
using OpenTK;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Types;
using SM.Utility;

#endregion

namespace SM2D.Types
{
    public class Transformation : GenericTransformation
    {
        public static int ZIndexPercision = 300;

        public CVector2 Position { get; set; } = new CVector2(0);

        public CVector2 Size { get; set; } = new CVector2(50);

        public CVector1 Rotation { get; set; } = new CVector1(0);

        public bool HorizontalFlip { get; set; } = false;

        public bool VerticalFlip { get; set; } = false;

        public int ZIndex { get; set; }

        protected override Matrix4 RequestMatrix()
        {
            return Matrix4.CreateScale(Size.X, Size.Y, 1) *
                   Matrix4.CreateRotationX(MathHelper.DegreesToRadians(HorizontalFlip ? 180 : 0)) *
                   Matrix4.CreateRotationY(MathHelper.DegreesToRadians(VerticalFlip ? 180 : 0)) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) *
                   Matrix4.CreateTranslation(Position.X, Position.Y, -(1 / (float)ZIndexPercision * ZIndex));
        }

        public void TurnTo(Vector2 v)
        {
            Rotation.Set(RotationUtility.TurnTowards(Position, v));
        }

        public Vector2 LookAtVector()
        {
            if (_modelMatrix.Determinant < 0.0001) return new Vector2(0);

            var vec = Vector3.TransformNormal(Vector3.UnitY, _modelMatrix);
            vec.Normalize();
            return vec.Xy;
        }

        public void ApplyTextureSize(Texture texture)
        {
            Size.Set(texture.Width, texture.Height);
        }

        public void ApplyTextureSize(Texture texture, float width)
        {
            Size.Set(width, width / texture.Aspect);
        }
    }
}