using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawShader : DrawingBasis<Transformation>, I2DShowItem
    {
        public int ZIndex { get; set; }

        public DrawShader(MaterialShader shader)
        {
            _material.CustomShader = shader;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            _material.CustomShader.Draw(context);
        }
    }
}