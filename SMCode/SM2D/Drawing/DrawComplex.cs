#region usings

using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.OGL.Mesh;
using SM2D.Scene;
using SM2D.Types;

#endregion

namespace SM2D.Drawing
{
    public class DrawComplex : DrawingBasis<Transformation>, I2DShowItem
    {
        public Material Material
        {
            get => _material;
            set => _material = value;
        }

        public GenericMesh Mesh
        {
            get => _mesh;
            set => _mesh = value;
        }

        public int ZIndex { get; set; }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);

            context.Shader.Draw(context);
        }
    }
}