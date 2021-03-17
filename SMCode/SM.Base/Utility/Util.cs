﻿#region usings

using System;

#endregion

namespace SM.Base.Utility
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