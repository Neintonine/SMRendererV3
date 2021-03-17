#region usings

using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Audio;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL
{
    /// <summary>
    ///     Specifies default object behaviour.
    /// </summary>
    public abstract class GLObject
    {
        private static List<GLObject> _disposableObjects = new List<GLObject>();
        private string _name = "";

        protected bool ReportAsNotCompiled;

        /// <summary>
        ///     Contains the OpenGL ID
        /// </summary>
        protected int _id = -1;

        protected bool CanCompile = true;
        
        /// <summary>
        ///     If true, the system will call "Compile()", when "ID" is tried to get, but the id is still -1.
        /// </summary>
        protected virtual bool AutoCompile { get; set; } = false;

        /// <summary>
        ///     Checks if the object was compiled.
        /// </summary>
        public bool WasCompiled => _id > 0 && !ReportAsNotCompiled;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                if (GLSystem.Debugging && WasCompiled) GL.ObjectLabel(TypeIdentifier, _id, _name.Length, _name);
            }
        }

        /// <summary>
        ///     Returns the id for this object.
        ///     <para>It will auto compile, if needed and allowed.</para>
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
        ///     Identifies the object.
        /// </summary>
        public abstract ObjectLabelIdentifier TypeIdentifier { get; }

        [DebuggerStepThrough]
        private void PerformCompile()
        {
            if (!CanCompile) return;

            Compile();

            if (GLSystem.Debugging && string.IsNullOrEmpty(_name))
            {
                try
                {
                    GL.ObjectLabel(TypeIdentifier, _id, _name.Length, _name);
                }
                catch
                {
                    // ignore
                }
            }
        }

        /// <summary>
        ///     The action, that is called, when "ID" tries to compile something.
        /// </summary>
        [DebuggerStepThrough]
        public virtual void Compile()
        {
        }

        /// <summary>
        ///     Is triggered, when something want to dispose this object.
        /// </summary>
        public virtual void Dispose()
        {
            _id = -1;
        }

        /// <summary>
        ///     Re-compiles the object.
        /// </summary>
        public void Recompile()
        {
            if (!WasCompiled) return;

            Dispose();
            Compile();
        }

        public override string ToString()
        {
            return $"{GetType().Name} {(string.IsNullOrEmpty(_name) ? "" : $"\"{_name}\" ")}[{_id}]";
        }

        public static void DisposeMarkedObjects()
        {
            foreach (GLObject o in _disposableObjects)
            {
                o.Dispose();
            }
            _disposableObjects.Clear();
        }

        /// <summary>
        ///     Returns the ID for the object.
        /// </summary>
        /// <param name="glo"></param>
        public static implicit operator int(GLObject glo)
        {
            return glo.ID;
        }

        ~GLObject()
        {
            if (WasCompiled) _disposableObjects.Add(this);
        }
    }
}