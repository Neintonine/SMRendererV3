using OpenTK;

namespace SM.Base.Scene
{
    public abstract class GenericCamera
    {
        public static Matrix4 OrthographicWorld { get; protected set; }
        public static Matrix4 PerspectiveWorld { get; protected set; }
        public static Vector3 UpVector { get; set; } = Vector3.UnitY;
        
        public Matrix4 ViewMatrix { get; protected set; }

        public Matrix4 World => Orthographic ? OrthographicWorld : PerspectiveWorld;

        internal Matrix4 CalculateViewMatrix()
        {
            ViewMatrix = ViewCalculation();
            return ViewMatrix;
        }

        public abstract Matrix4 ViewCalculation();

        public abstract bool Orthographic { get; }
        public abstract void RecalculateWorld(float width, float height);
    }
}