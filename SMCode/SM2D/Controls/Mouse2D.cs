using OpenTK;
using OpenTK.Input;
using SM.Base;
using SM.Base.Controls;
using SM2D.Scene;

namespace SM2D.Controls
{
    public class Mouse2D : Mouse<GLWindow2D>
    {
        protected internal Mouse2D(GLWindow2D window) : base(window)
        { }

        internal new void MouseMoveEvent(MouseMoveEventArgs mmea) => base.MouseMoveEvent(mmea);

        public Vector2 InWorld()
        {
            Vector2 res = _window.WorldScale;
            return InScreenNormalized * res - res / 2;
        }

        public Vector2 InWorld(Camera cam)
        {
            return InWorld() + cam.Position;
        }

        public Vector2 InWorld(Vector2 position)
        {
            return InWorld() + position;
        }
    }
}