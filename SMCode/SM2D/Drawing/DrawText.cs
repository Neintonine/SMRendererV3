using SM.Base.Contexts;
using SM.Base.Text;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawText : TextDrawingBasis<Transformation>
    {
        public DrawText(Font font, string text) : base(font)
        {
            _text = text;
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Instances = _modelMatrixs;
            ApplyContext(ref context);

            context.View = Transform.GetMatrix() * context.View;

            _material.Shader.Draw(context);
        }
    }
}