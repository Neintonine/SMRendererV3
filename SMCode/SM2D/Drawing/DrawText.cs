using SM.Base.Contexts;
using SM.Base.Text;
using SM.Base.Types;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawText : TextDrawingBasis<Transformation>, I2DShowItem
    {
        public DrawText(Font font, string text) : base(font)
        {
            _text = text;
            Transform.Size = new Vector2(1);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Instances = _modelMatrixs;
            ApplyContext(ref context);

            context.View = Transform.GetMatrix() * context.View;

            _material.Shader.Draw(context);
        }

        public int ZIndex { get; set; }
    }
}