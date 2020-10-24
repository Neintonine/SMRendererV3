using SM.Base.Contexts;
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
            context.Instances[0].ModelMatrix = Transform.GetMatrix();

            _material.CustomShader.Draw(context);
        }
    }
}