using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.OGL.Texture;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawBackground : DrawingBasis
    {
        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        public TextureBase Texture
        {
            get => _material.Texture;
            set => _material.Texture = value;
        }
        public DrawBackground(Color4 color)
        {
            Color = color;
        }

        public DrawBackground(Bitmap texture)
        {
            Texture = (Texture)texture;
        }

        public DrawBackground(Bitmap texture, Color4 tint)
        {
            Color = tint;
            Texture = (Texture) texture;
        }

        public override void Draw(DrawContext context)
        {
            ApplyContext(ref context);

            context.ModelMatrix = Matrix4.CreateScale(context.WorldScale.X, context.WorldScale.Y, 1);
            _material.Shader.Draw(context);
        }
    }
}