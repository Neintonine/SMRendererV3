#region usings

using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics;
using SM.Base;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.Base.Windows;
using SM2D.Drawing;

#endregion

namespace SM2D.Scene
{
    public class Scene : GenericScene<Camera, ItemCollection>
    {
        private static DrawObject2D _axisHelper;

        public float AxisHelperSize = 100;
        static Scene()
        {
            _axisHelper = new DrawObject2D();
            _axisHelper.Mesh = AxisHelper.Object;
        }

        public Scene()
        {
            _Background = new DrawBackground(Color4.Black);
            Objects = new ItemCollection();

            BackgroundCamera = new Camera();
            HUDCamera = new Camera();
        }


        public DrawBackground Background
        {
            get => (DrawBackground) _Background;
            set => _Background = value;
        }

        public override void DrawHUD(DrawContext context)
        {
            context.ModelMatrix *= Matrix4.CreateTranslation(0,0,1);

            base.DrawHUD(context);
        }

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