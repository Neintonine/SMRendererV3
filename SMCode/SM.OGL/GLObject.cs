using OpenTK.Graphics.OpenGL4;

namespace SM.OGL
{
    public abstract class GLObject
    {
        protected int _id = -1;
        protected virtual bool AutoCompile { get; } = false;

        public virtual int ID
        {
            get
            {
                if (AutoCompile && _id < 0) Compile();
                return _id;
            }
        }

        public abstract ObjectLabelIdentifier TypeIdentifier { get; }

        protected virtual void Compile()
        {

        }

        public void Name(string name)
        {
            GL.ObjectLabel(TypeIdentifier, _id, name.Length, name);
        }

        public static implicit operator int(GLObject glo) => glo.ID;
    }
}