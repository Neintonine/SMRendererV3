#region usings

using System;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL
{
    /// <summary>
    /// Allows the system to send custom actions.
    /// </summary>
    public class GLCustomActions
    {
        /// <summary>
        ///     A action that is performed, when a OpenGL-error occurs.
        /// </summary>
        public static Action<DebugSource, DebugType, DebugSeverity, string> AtKHRDebug = DefaultDebugAction;

        /// <summary>
        ///     A action, that is performed, when a GLError occurred.
        ///     <para>Doesn't account for "KHR_debugging"</para>
        /// </summary>
        public static Action<string> AtError;

        /// <summary>
        ///     A action, that is performed, when a warning want to be shown.
        /// </summary>
        public static Action<string> AtWarning;

        /// <summary>
        ///     A action, that is performed, when a information needs to be shown.
        /// </summary>
        public static Action<string> AtInfo;

        /// <summary>
        ///     Default action for 'AtKHRDebug'.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <param name="severity"></param>
        /// <param name="msg"></param>
        private static void DefaultDebugAction(DebugSource source, DebugType type, DebugSeverity severity, string msg)
        {
            Console.WriteLine($"{severity}, {type}, {source} -> {msg}");

            if (type == DebugType.DebugTypeError) throw new Exception(msg);
        }
    }
}