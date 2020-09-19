using System.Drawing;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Textures;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawTexture : DrawingBasis<Transformation>
    {
        public Texture Texture
        {
            get => (Texture) _material.Texture;
            set => _material.Texture = value;
        }

        public Color4 Tint
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        public DrawTexture(Bitmap map) : this(map, Color4.White)
        { }

        public DrawTexture(Bitmap map, Color4 tint)
        {
            _material.Texture = new Texture(map);
            _material.Tint = tint;
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            ApplyContext(ref context);

            context.ModelMatrix = Transform.GetMatrix();

            _material.Shader.Draw(context);
        }
    }
}