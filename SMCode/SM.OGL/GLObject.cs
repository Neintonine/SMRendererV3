using OpenTK.Graphics.OpenGL4;

namespace SM.OGL
{
    public abstract class GLObject
    {
        protected int _id = -1;
        protected virtual bool AutoCompile { get; } = false;

        public bool WasCompiled => _id > 0;

        public virtual int ID
        {
            get
            {
                if (AutoCompile && !WasCompiled) Compile();
                return _id;
            }
        }

        public abstract ObjectLabelIdentifier TypeIdentifier { get; }

        protected virtual void Compile()
        {

        }

        public void Name(string name)
        {
            if (GLSystem.Debugging) GL.ObjectLabel(TypeIdentifier, _id, name.Length, name);
        }

        public static implicit operator int(GLObject glo) => glo.ID;
    }
}