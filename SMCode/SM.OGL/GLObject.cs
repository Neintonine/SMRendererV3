using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL
{
    /// <summary>
    /// Specifies default object behaviour.
    /// </summary>
    public abstract class GLObject
    {
        /// <summary>
        /// Contains the OpenGL ID
        /// </summary>
        protected int _id = -1;
        /// <summary>
        /// If true, the system will call "Compile()", when "ID" is tried to get, but the id is still -1.
        /// </summary>
        protected virtual bool AutoCompile { get; } = false;

        /// <summary>
        /// Checks if the object was compiled.
        /// </summary>
        public bool WasCompiled => _id > 0;

        /// <summary>
        /// Returns the id for this object.
        /// <para>It will auto compile, if needed and allowed.</para>
        /// </summary>
        public virtual int ID
        {
            get
            {
                if (AutoCompile && !WasCompiled) PerformCompile();
                return _id;
            }
        }

        /// <summary>
        /// Identifies the object.
        /// </summary>
        public abstract ObjectLabelIdentifier TypeIdentifier { get; }

        [DebuggerStepThrough]
        private void PerformCompile()
        {
            Compile();
        }

        /// <summary>
        /// The action, that is called, when "ID" tries to compile something.
        /// </summary>

        public virtual void Compile()
        {

        }

        /// <summary>
        /// Is triggered, when something want to dispose this object.
        /// </summary>
        public virtual void Dispose() {}

        /// <summary>
        /// Re-compiles the object.
        /// </summary>
        public void Recompile()
        {
            if (!WasCompiled) return;

            Dispose();
            Compile();
        }

        /// <summary>
        /// Names the object for debugging.
        /// </summary>
        /// <param name="name"></param>
        public void Name(string name)
        {
            if (GLSystem.Debugging) GL.ObjectLabel(TypeIdentifier, _id, name.Length, name);
        }

        /// <summary>
        /// Returns the ID for the object.
        /// </summary>
        /// <param name="glo"></param>
        public static implicit operator int(GLObject glo) => glo.ID;
    }
}