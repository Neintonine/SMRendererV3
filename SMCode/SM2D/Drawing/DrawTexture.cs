using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Types;
using SM2D.Types;
using Vector2 = SM.Base.Types.Vector2;

namespace SM2D.Drawing
{
    public class DrawTexture : DrawingBasis<Transformation>
    {
        public static float MasterScale = .25f;

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

        public float Scale = 1;


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

            Transform.Size = new Vector2(Texture.Map.Width * MasterScale * Scale, Texture.Map.Height  * MasterScale * Scale);
            context.Instances[0].ModelMatrix = Transform.GetMatrix();

            _material.Shader.Draw(context);
        }
    }
}