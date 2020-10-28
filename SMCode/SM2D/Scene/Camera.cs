#region usings

using OpenTK;
using SM.Base.Scene;
using SM.Base.Types;

#endregion

namespace SM2D.Scene
{
    public class Camera : GenericCamera
    {
        public CVector2 Position = new CVector2(0);
        public override bool Orthographic { get; } = true;

        protected override Matrix4 ViewCalculation()
        {
            return Matrix4.LookAt(Position.X, Position.Y, 2, Position.X, Position.Y, 0, 0, 1, 0);
        }

        public override void RecalculateWorld(Vector2 world, float aspect)
        {
            OrthographicWorld =
                Matrix4.CreateOrthographic(world.X, world.Y, 0.1f, 100f);
        }
    }
}