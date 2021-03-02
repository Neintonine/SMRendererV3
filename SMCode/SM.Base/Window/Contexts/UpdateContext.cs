using OpenTK.Input;
using SM.Base.Scene;

namespace SM.Base.Windows
{
    public struct UpdateContext
    {
        public IGenericWindow Window;

        public float Deltatime => SMRenderer.DefaultDeltatime.DeltaTime;

        public GenericScene Scene;
    }
}