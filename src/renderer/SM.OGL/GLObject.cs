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
        private static readonly List<GLObject> _disposableObjects = new();
        private string _name = "";


        /// <summary>
        ///     Contains the OpenGL ID
        /// </summary>
        protected int _id = -1;

        /// <summary>
        /// This can mark the object to never report as compiled, even when it was.
        /// <para>You can still figure out, if it was compiled by checking <see cref="_id"/>. If not -1, its compiled.</para>
        /// <para>Default: false</para>
        /// </summary>
        protected bool ReportAsNotCompiled;
        /// <summary>
        /// This can prevent the object to compile.
        /// <para>Default: true</para>
        /// </summary>
        protected bool CanCompile = true;
        
        /// <summary>
        ///     If true, the system will call "Compile()", when "ID" is tried to get, but the id is still -1.
        /// <para>Default: false</para>
        /// </summary>
        protected virtual bool AutoCompile { get; set; } = false;

        /// <summary>
        ///     Checks if the object was compiled.
        /// </summary>
        public bool WasCompiled => _id > 0 && !ReportAsNotCompiled;

        /// <summary>
        /// Names the object
        /// <para>If <see cref="GLSystem.Debugging"/> is true, then it will also name the object in the system.</para>
        /// </summary>
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

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{GetType().Name} {(string.IsNullOrEmpty(_name) ? "" : $"\"{_name}\" ")}[{_id}]";
        }

        /// <summary>
        /// This disposes the current objects, that where marked by the garbage collector.
        /// </summary>
        public static void DisposeMarkedObjects()
        {
            foreach (GLObject o in _disposableObjects.ToArray())
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

        /// <summary>
        /// If the garbage collector is trying to remove this object, it will add the object to a list, what get removed when <see cref="DisposeMarkedObjects"/> is called.
        /// </summary>
        ~GLObject()
        {
            if (WasCompiled) _disposableObjects.Add(this);
        }
    }
}