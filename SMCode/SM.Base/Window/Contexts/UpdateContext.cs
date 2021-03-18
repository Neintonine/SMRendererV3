#region usings

using SM.Base.Scene;

#endregion

namespace SM.Base.Window
{
    public struct UpdateContext
    {
        public IGenericWindow Window;

        public float Deltatime => SMRenderer.DefaultDeltatime.DeltaTime;

        public GenericScene Scene;
    }
}