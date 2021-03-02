#region usings

using SM.Base;
using SM.Base.Drawing.Text;
using SM.Base.Types;
using SM.Base.Windows;
using SM2D.Scene;
using SM2D.Types;

#endregion

namespace SM2D.Drawing
{
    public class DrawText : TextDrawingBasis<Transformation>
    {
        public DrawText(Font font, string text) : base(font)
        {
            _text = text;
            Transform.Size = new CVector2(1);
        }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.Instances = _instances;
            
            context.Shader.Draw(context);
        }
    }
}