using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform.Egl;
using ErrorCode = OpenTK.Graphics.OpenGL4.ErrorCode;

namespace SM.OGL
{
    /// <summary>
    /// Contains everything that is needed to debug OpenGL
    /// </summary>
    public static class GLDebugging
    {
        private static DebugProc _debugProc = DebugCallback;
        private static GCHandle _debugGcHandle;

        /// <summary>
        /// A action that is performed, when a OpenGL-error occurs.
        /// </summary>
        public static Action<DebugSource, DebugType, DebugSeverity, string> DebugAction = DefaultDebugAction;

        [DebuggerStepThrough]
        private static void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity,
            int length, IntPtr message, IntPtr userparam)
        {
            string msg = Marshal.PtrToStringAnsi(message, length);
            DebugAction?.Invoke(source, type, severity, msg);
        }

        /// <summary>
        /// Enables the debugging.
        /// </summary>
        public static void EnableDebugging()
        {
            try
            {
                _debugGcHandle = GCHandle.Alloc(_debugProc);

                GL.DebugMessageCallback(_debugProc, IntPtr.Zero);
                GL.Enable(EnableCap.DebugOutput);
                GL.Enable(EnableCap.DebugOutputSynchronous);
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("Enableing proper GLDebugging failed. \n" +
                                  "Often it fails, because your hardware doesn't provide proper OpenGL 4 \n" +
                                  "    or KHR_debug extension support.");

            }
        }

        /// <summary>
        /// Default action for 'DebugAction'.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <param name="severity"></param>
        /// <param name="msg"></param>
        public static void DefaultDebugAction(DebugSource source, DebugType type, DebugSeverity severity, string msg)
        {
            Console.WriteLine($"{severity}, {type}, {source} -> {msg}");

            if (type == DebugType.DebugTypeError) throw new Exception(msg);
        }

        /// <summary>
        /// A action, that is performed, when <see cref="GLDebugging.CheckGLErrors"/> find an error.
        /// </summary>
        public static Action<ErrorCode> GlErrorAction;

        /// <summary>
        /// Checks for OpenGL errors.
        /// </summary>
        public static bool CheckGLErrors()
        {
            bool hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                GlErrorAction?.Invoke(c);
            }

            return hasError;
        }
    }
}