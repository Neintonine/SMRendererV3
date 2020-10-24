#region usings

using OpenTK.Graphics;
using SM.Base.Scene;
using SM2D.Drawing;

#endregion

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera, ItemCollection, I2DShowItem>
    {
        public Scene()
        {
            _Background = new DrawBackground(Color4.Black);
        }

        public DrawBackground Background => (DrawBackground) _Background;
    }
}