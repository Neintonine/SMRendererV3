#region usings

using OpenTK;
using OpenTK.Graphics;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.Base.Window;
using SM2D.Drawing;

#endregion

namespace SM2D.Scene
{
    /// <summary>
    /// The scene allows connecting different objects to render together.
    /// </summary>
    public class Scene : GenericScene<Camera, ItemCollection>
    {
        private static readonly DrawObject2D _axisHelper;

        /// <summary>
        /// This determent how large the axishelper should be.
        /// </summary>
        public float AxisHelperSize = 100;
        static Scene()
        {
            _axisHelper = new DrawObject2D {Mesh = AxisHelper.Object};
        }

        /// <summary>
        /// This creates a new scene.
        /// </summary>
        public Scene()
        {
            _Background = new DrawBackground(Color4.Black);
            Objects = new ItemCollection();

            BackgroundCamera = new Camera();
            HUDCamera = new Camera();
        }


        /// <summary>
        /// Gets/Sets the background.
        /// </summary>
        public DrawBackground Background
        {
            get => (DrawBackground) _Background;
            set => _Background = value;
        }

        /// <inheritdoc />
        public override void DrawHUD(DrawContext context)
        {
            context.ModelMatrix *= Matrix4.CreateTranslation(0,0,1);

            base.DrawHUD(context);
        }

        /// <inheritdoc />
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