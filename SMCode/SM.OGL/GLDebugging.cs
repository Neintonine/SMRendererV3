#region usings

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL
{
    /// <summary>
    ///     Contains everything that is needed to debug OpenGL
    /// </summary>
    public static class GLDebugging
    {
        private static DebugProc _debugProc = DebugCallback;
        private static GCHandle _debugGcHandle;


        [DebuggerStepThrough]
        private static void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity,
            int length, IntPtr message, IntPtr userparam)
        {
            var msg = Marshal.PtrToStringAnsi(message, length);
            GLCustomActions.AtKHRDebug?.Invoke(source, type, severity, msg);
        }

        /// <summary>
        ///     Enables the debugging.
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
        ///     Checks for OpenGL errors.
        /// </summary>
        public static bool CheckGLErrors()
        {
            var hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                GLCustomActions.AtError?.Invoke("A GLError occurred: " + c);
            }

            return hasError;
        }

        /// <summary>
        ///     Checks for OpenGL errors, while allowing to put stuff before and behind it.
        /// </summary>
        /// <param name="formating"></param>
        /// <returns></returns>
        public static bool CheckGLErrors(string formating)
        {
            var hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                GLCustomActions.AtError?.Invoke(formating.Replace("%code%", c.ToString()));
            }

            return hasError;
        }
    }
}