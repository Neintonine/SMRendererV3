#region usings

using System;

#endregion

namespace SM.Base.Utility
{
    /// <summary>
    /// Utility-Functions that are too small for a own class.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Activates a <see cref="IInitializable"/>
        /// </summary>
        /// <param name="obj"></param>
        public static void Activate(IInitializable obj)
        {
            if (!obj.IsInitialized)
            {
                obj.Initialization();
                obj.IsInitialized = true;
            }

            obj.Activate();
        }

        /// <summary>
        /// Calls a garbage collector.
        /// </summary>
        public static void CallGarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}