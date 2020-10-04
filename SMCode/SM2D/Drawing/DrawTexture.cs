using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Types;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawTexture : DrawColor
    {
        public static float MasterScale = .25f;

        public float Scale = 1;
        public bool ManualSize = false;
        public Texture Texture
        {
            get => (Texture) _material.Texture;
            set => _material.Texture = value;
        }

        public DrawTexture() {}

        protected DrawTexture(Color4 color) : base(color) { }

        public DrawTexture(Bitmap map) : this(map, Color4.White)
        { }

        public DrawTexture(Bitmap map, Color4 color) : this((Texture)map, color)
        { }

        public DrawTexture(Texture texture, Color4 color)
        {
            _material.Texture = texture;
            _material.Tint = color;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            if (!ManualSize) Transform.Size = new CVector2(Texture.Map.Width * MasterScale * Scale, Texture.Map.Height * MasterScale * Scale);
            base.DrawContext(ref context);
        }
    }
}