using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM2D.Drawing;

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera, I2DShowItem>
    {
        public DrawBackground Background => (DrawBackground)_background;

        public Scene()
        {
            _background = new DrawBackground(Color4.Black);
        }

        public override void Draw(DrawContext context)
        {
            Objects.Sort((x,y) => x.ZIndex - y.ZIndex);
            base.Draw(context);
        }
    }
}