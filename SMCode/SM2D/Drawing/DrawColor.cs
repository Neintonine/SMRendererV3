using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawColor : DrawingBasis<Transformation>, I2DShowItem
    {
        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        public int ZIndex { get; set; }

        public DrawColor() {}

        public DrawColor(Color4 color)
        {
            _material.Tint = color;
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            ApplyContext(ref context);
            context.Instances[0].ModelMatrix = Transform.GetMatrix();

            _material.Shader.Draw(context);
        }
    }
}