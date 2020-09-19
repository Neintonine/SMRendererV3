namespace SM.Base.Types
{
    public class Vector4 : Vector3
    {
        public float W
        {
            get => _W;
            set => _W = value;
        }

        public Vector4(float uniform) : base(uniform)
        {
        }

        public Vector4(float x, float y, float z, float w) : base(x, y, z, w)
        {
        }

        public static implicit operator Vector4(OpenTK.Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.W);
    }
}