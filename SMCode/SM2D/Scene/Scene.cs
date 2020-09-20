using OpenTK.Graphics;
using SM.Base.Scene;
using SM2D.Drawing;

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera>
    {
        public DrawBackground Background => (DrawBackground)_background;

        public Scene()
        {
            _background = new DrawBackground(Color4.Black);
        }
    }
}