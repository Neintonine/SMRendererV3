#region usings

using OpenTK;
using OpenTK.Input;
using SM.Base.Controls;
using SM2D.Scene;
using SM2D.Types;

#endregion

namespace SM2D.Controls
{
    public class Mouse2D : Mouse<IGLWindow2D>
    {
        protected internal Mouse2D(IGLWindow2D window) : base(window)
        {
        }

        internal new void MouseMoveEvent(MouseMoveEventArgs mmea)
        {
            base.MouseMoveEvent(mmea);
        }

        public Vector2 InWorld()
        {
            var res = _window.WorldScale;
            res.Y *= -1;
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