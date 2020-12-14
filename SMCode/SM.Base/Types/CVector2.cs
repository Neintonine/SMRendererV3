using OpenTK;

namespace SM.Base.Types
{
    public class CVector2 : CVector1
    {
        public float Y { get; set; }

        public CVector2(float uniform) : base(uniform)
        {
            Y = uniform;
        }

        public CVector2(float x, float y) : base(x)
        {
            Y = y;
        }

        public virtual void Set(float uniform)
        {
            base.Set(uniform);
            Y = uniform;
        }

        public void Set(Vector2 vector)
        {
            Set(vector.X, vector.Y);
        }

        public void Set(float x, float y)
        {
            base.Set(x);
            Y = y;
        }

        public static implicit operator Vector2(CVector2 vector2) => new Vector2(vector2.X, vector2.Y);
        public static implicit operator CVector2(Vector2 vector2) => new CVector2(vector2.X, vector2.Y);
    }
}