#region usings

using System;
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
        public CVector2 Position { get; set; } = new CVector2(0);

        public CVector2 Size { get; set; } = new CVector2(50);

        public float Rotation { get; set; }

        protected override Matrix4 RequestMatrix()
        {
            return Matrix4.CreateScale(Size.X, Size.Y, 1) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) *
                   Matrix4.CreateTranslation(Position.X, Position.Y, 0);
        }

        public void TurnTo(Vector2 v)
        {
            Rotation = RotationUtility.TurnTowards(Position, v);
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