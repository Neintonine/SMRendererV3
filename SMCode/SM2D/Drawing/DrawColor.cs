#region usings

using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM2D.Scene;
using SM2D.Types;

#endregion

namespace SM2D.Drawing
{
    public class DrawColor : DrawingBasis<Transformation>, I2DShowItem
    {
        public DrawColor()
        {
        }

        public DrawColor(Color4 color)
        {
            _material.Tint = color;
        }

        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        public int ZIndex { get; set; }

        protected override void DrawContext(ref DrawContext context)
        {
            context.Shader.Draw(context);
        }
    }
}