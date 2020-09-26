using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform.Egl;

namespace SM.OGL
{
    public static class GLDebugging
    {
        private static DebugProc _debugProc = DebugCallback;
        private static GCHandle _debugGcHandle;

        public static Action<DebugSource, DebugType, DebugSeverity, string> DebugAction = DefaultDebugAction;

        [DebuggerStepThrough]
        private static void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity,
            int length, IntPtr message, IntPtr userparam)
        {
            string msg = Marshal.PtrToStringAnsi(message, length);
            DebugAction?.Invoke(source, type, severity, msg);
        }

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

        public static void DefaultDebugAction(DebugSource source, DebugType type, DebugSeverity severity, string msg)
        {
            Console.WriteLine($"{severity}, {type}, {source} -> {msg}");

            if (type == DebugType.DebugTypeError) throw new Exception(msg);
        }
    }
}