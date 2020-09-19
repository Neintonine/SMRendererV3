using OpenTK;
using SM.Base.Scene;

namespace SM2D.Types
{
    public class Transformation : GenericTransformation
    {
        public Vector2 Position;
        public Vector2 Size = new Vector2(50);
        public float Rotation;

        public override Matrix4 GetMatrix()
        {
            return Matrix4.CreateScale(Size.X, Size.Y, 1) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) *
                   Matrix4.CreateTranslation(Position.X, Position.Y, 1);
        }
    }
}