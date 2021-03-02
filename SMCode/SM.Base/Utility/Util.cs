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
    }
}