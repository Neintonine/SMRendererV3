using OpenTK;
using SM.Base.Scene;
using SM.Base.Types;

namespace SM2D.Types
{
    public class Transformation : GenericTransformation
    {
        public CVector2 Position = new CVector2(0);
        public CVector2 Size = new CVector2(50);
        public float Rotation;

        public override Matrix4 GetMatrix()
        {
            return Matrix4.CreateScale(Size.X, Size.Y, 1) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) *
                   Matrix4.CreateTranslation(Position.X, Position.Y, 0);
        }
    }
}