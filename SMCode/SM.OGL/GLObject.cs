#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        protected bool ReportAsNotCompiled;

        /// <summary>
        ///     Contains the OpenGL ID
        /// </summary>
        protected int _id = -1;
        
        /// <summary>
        ///     If true, the system will call "Compile()", when "ID" is tried to get, but the id is still -1.
        /// </summary>
        protected virtual bool AutoCompile { get; } = false;

        /// <summary>
        ///     Checks if the object was compiled.
        /// </summary>
        public bool WasCompiled => _id > 0 && !ReportAsNotCompiled;

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
            Compile();
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

        /// <summary>
        ///     Names the object for debugging.
        /// </summary>
        /// <param name="name"></param>
        public void Name(string name)
        {
            if (GLSystem.Debugging) GL.ObjectLabel(TypeIdentifier, _id, name.Length, name);
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