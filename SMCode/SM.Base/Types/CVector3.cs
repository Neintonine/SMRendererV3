using OpenTK;

namespace SM.Base.Types
{
    public class CVector3 : CVector2
    {
        public float Z { get; set; }

        public CVector3(float uniform) : base(uniform)
        {
            Z = uniform;
        }

        public CVector3(float x, float y, float z) : base(x, y)
        {
            Z = z;
        }

        public override void Set(float uniform)
        {
            base.Set(uniform);
            Z = uniform;
        }

        public void Set(float x, float y, float z)
        {
            base.Set(x,y);
            Z = z;
        }

        public void Set(Vector3 vector)
        {
            Set(vector.X, vector.Y, vector.Z);
        }

        public static implicit operator Vector3(CVector3 vector) => new Vector3(vector.X, vector.Y, vector.Z);
        public static implicit operator CVector3(Vector3 vector) => new CVector3(vector.X, vector.Y, vector.Z);
    }
}