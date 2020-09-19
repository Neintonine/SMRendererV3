using System.Runtime.InteropServices;

namespace SM.Base.Types
{
    public class Vector3 : Vector2
    {
        public float Z
        {
            get => _Z;
            set => _Z = value;
        }

        public Vector3(float uniform) : base(uniform)
        { }

        public Vector3(float x, float y, float z) : base(x, y, z, default)
        { }

        protected Vector3(float x, float y, float z, float w) : base(x, y, z, w) { }

        public static implicit operator Vector3(OpenTK.Vector3 v) => new Vector3(v.X, v.Y, v.Z);
    }
}