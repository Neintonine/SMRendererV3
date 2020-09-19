using OpenTK;
using SM.Base.Scene;
using Vector2 = SM.Base.Types.Vector2;

namespace SM2D.Scene
{
    public class Camera : GenericCamera
    {
        public override bool Orthographic { get; } = true;

        public Vector2 Position = new Vector2(0);

        protected override Matrix4 ViewCalculation()
        {
            return Matrix4.LookAt(Position.X, Position.Y, -1, Position.X, Position.Y, 0, 0, 1, 0);
        }

        public override void RecalculateWorld(OpenTK.Vector2 world, float aspect)
        {
            OrthographicWorld = Matrix4.CreateOrthographicOffCenter(world.X / 2, -world.X / 2, world.Y / 2, -world.Y / 2, 0.1f, 100);
        }
    }
}