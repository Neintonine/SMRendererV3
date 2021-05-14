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

        /// <summary>
        /// Sets the height of the text.
        /// </summary>
        /// <param name="desiredHeight">The height it should be.</param>
        public void SetHeight(float desiredHeight)
        {
            if (!Font.WasCompiled) Font.Compile();
            
            float factor = desiredHeight / Font.Height;
            Transform.Size.Set(factor);
        }

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.Instances = _instances;
            
            Material.Draw(context);
        }
    }
}