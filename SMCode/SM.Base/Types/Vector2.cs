namespace SM.Base.Types
{
    public class Vector2 : Vector
    {
        public float X
        {
            get => _X;
            set => _X = value;
        }
        public float Y
        {
            get => _Y;
            set => _Y = value;
        }

        public Vector2() : this(0)
        {}

        public Vector2(float uniform) : base(uniform)
        {

        }
        public Vector2(float x, float y) : base(x,y, default, default) {}
        protected Vector2(float x, float y, float z, float w) : base(x, y, z, w) {}

        public new void Set(float x, float y) => base.Set(x, y);
        public new void Set(OpenTK.Vector2 vector) => base.Set(vector);

        public new void Add(OpenTK.Vector2 vector) => base.Add(vector);

        public static implicit operator Vector2(OpenTK.Vector2 v) => new Vector2(v.X, v.Y);
    }
}