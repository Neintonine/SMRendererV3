using SM.Base.Contexts;
using SM.Base.Scene;
using SM2D.Scene;

namespace SM2D.Drawing
{
    public class DrawBackgroundShader : DrawShader, IBackgroundItem
    {
        public DrawBackgroundShader(MaterialShader shader) : base(shader)
        { }

        protected override void DrawContext(ref DrawContext context)
        {
            Transform.Size.Set(context.WorldScale);

            base.DrawContext(ref context);
        }
    }
}