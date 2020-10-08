using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM2D.Drawing;

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera, ItemCollection, I2DShowItem>
    {
        public DrawBackground Background => (DrawBackground)_Background;

        public Scene()
        {
            _Background = new DrawBackground(Color4.Black);
        }
    }
}