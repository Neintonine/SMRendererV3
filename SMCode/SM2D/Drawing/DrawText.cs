#region usings

using SM.Base.Drawing.Text;
using SM.Base.Types;
using SM.Base.Window;
using SM2D.Types;

#endregion

namespace SM2D.Drawing
{
    /// <summary>
    /// Draws a text to the world.
    /// </summary>
    public class DrawText : TextDrawingBasis<Transformation>
    {
        /// <summary>
        /// Creates a text object.
        /// </summary>
        public DrawText(Font font, string text) : base(font)
        {
            _text = text;
            Transform.Size = new CVector2(1);
        }

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.Instances = _instances;
            
            context.Shader.Draw(context);
        }
    }
}