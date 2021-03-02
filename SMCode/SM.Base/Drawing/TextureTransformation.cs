using System;
using OpenTK;
using SM.Base.Types;

namespace SM.Base.Drawing
{
    public class TextureTransformation
    {
        public CVector2 Offset = new CVector2(0);
        public CVector2 Scale = new CVector2(1);
        public CVector1 Rotation = new CVector1(0);

        public Matrix3 GetMatrix()
        {
            return CalculateMatrix(Offset, Scale, Rotation);
        }

        public static Matrix3 CalculateMatrix(Vector2 offset, Vector2 scale, float rotation)
        {
            float radians = MathHelper.DegreesToRadians(rotation);
            Matrix3 result = Matrix3.CreateScale(scale.X, scale.Y, 1) * Matrix3.CreateRotationZ(radians);
            result.Row2 = new Vector3(offset.X, offset.Y, 1);
            return result;
        }
    }
}