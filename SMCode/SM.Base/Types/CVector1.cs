namespace SM.Base.Types
{
    public class CVector1
    { 
        public float X { get; set; }

        public CVector1(float x)
        {
            X = x;
        }

        public virtual void Set(float x)
        {
            X = x;
        }

        public static implicit operator float(CVector1 vector1) => vector1.X;
        public static implicit operator CVector1(float f) => new CVector1(f);
    }
}