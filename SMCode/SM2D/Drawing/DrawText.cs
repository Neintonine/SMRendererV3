#region usings

using SM.Base.Contexts;
using SM.Base.Text;
using SM.Base.Types;
using SM2D.Scene;
using SM2D.Types;

#endregion

namespace SM2D.Drawing
{
    public class DrawText : TextDrawingBasis<Transformation>, I2DShowItem
    {
        public DrawText(Font font, string text) : base(font)
        {
            _text = text;
            Transform.Size = new CVector2(1);
        }

        public int ZIndex { get; set; }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.Instances = _instances;

            context.View = Transform.GetMatrix() * context.View;

            context.Shader.Draw(context);
        }
    }
}