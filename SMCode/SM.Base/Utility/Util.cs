using System;

namespace SM.Utility
{
    public class Util
    {
        public static void Activate(IInitializable obj)
        {
            if (!obj.IsInitialized)
            {
                obj.Initialization();
                obj.IsInitialized = true;
            }
            obj.Activate();
        }

        public static void CallGarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}