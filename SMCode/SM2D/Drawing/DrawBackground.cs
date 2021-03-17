#region usings

using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Window;
using SM.OGL.Texture;
using SM2D.Scene;

#endregion

namespace SM2D.Drawing
{
    public class DrawBackground : DrawingBasis, IBackgroundItem
    {
        public Color4 Color
        {
            get => Material.Tint;
            set => Material.Tint = value;
        }

        public TextureBase Texture
        {
            get => Material.Texture;
            set
            {
                if (Material.Tint == Color4.Black) Material.Tint = Color4.White;
                Material.Texture = value;
            }
        }

        public DrawBackground() : this(Color4.Black) {}

        public DrawBackground(Color4 color)
        {
            Color = color;
        }

        public DrawBackground(Bitmap texture)
        {
            Texture = (Texture) texture;
        }

        public DrawBackground(Bitmap texture, Color4 tint)
        {
            Color = tint;
            Texture = (Texture) texture;
        }


        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.ModelMatrix = Matrix4.CreateScale((context.UseCamera as Camera).WorldScale.X, (context.UseCamera as Camera).WorldScale.Y, 0) * Matrix4.CreateTranslation(0,0, -1.1f);
            context.Shader.Draw(context);
        }
    }
}