#region usings

using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM2D.Drawing;

#endregion

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera, ItemCollection, I2DShowItem>
    {
        private static DrawObject2D _axisHelper;

        public float AxisHelperSize = 100;
        static Scene()
        {
            _axisHelper = new DrawObject2D();
            _axisHelper.ApplyMesh(AxisHelper.Object);
        }

        public Scene()
        {
            _Background = new DrawBackground(Color4.Black);
        }


        public DrawBackground Background => (DrawBackground) _Background;

        public override void DrawDebug(DrawContext context)
        {
            if (ShowAxisHelper)
            {
                _axisHelper.Transform.Size.Set(AxisHelperSize, AxisHelperSize);
                _axisHelper.Draw(context);
            }
        }
    }
}