using OpenTK;
using SM.Base.Scene;

namespace SM2D.Scene
{
    public class Camera : GenericCamera
    {
        public override bool Orthographic { get; } = true;

        public Vector2 Position;

        public override Matrix4 ViewCalculation()
        {
            return Matrix4.LookAt(Position.X, Position.Y, -1, Position.X, Position.Y, 0, 0, 1, 0);
        }

        public override void RecalculateWorld(float width, float height)
        {
            OrthographicWorld = Matrix4.CreateOrthographicOffCenter(-width / 2, width / 2, height / 2, -height / 2, 0.1f, 100);
        }
    }
}