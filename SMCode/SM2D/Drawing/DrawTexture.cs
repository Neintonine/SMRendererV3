#region usings

using System.Drawing;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Textures;
using SM.Base.Types;

#endregion

namespace SM2D.Drawing
{
    public class DrawTexture : DrawColor
    {
        public static float MasterScale = .25f;
        public bool ManualSize = false;

        public float Scale = 1;

        public DrawTexture()
        {
        }

        protected DrawTexture(Color4 color) : base(color)
        {
        }

        public DrawTexture(Bitmap map) : this(map, Color4.White)
        {
        }

        public DrawTexture(Bitmap map, Color4 color) : this((Texture) map, color)
        {
        }

        public DrawTexture(Texture texture, Color4 color)
        {
            _material.Texture = texture;
            _material.Tint = color;
        }

        public Texture Texture
        {
            get => (Texture) _material.Texture;
            set => _material.Texture = value;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            if (!ManualSize)
                Transform.Size = new CVector2(Texture.Map.Width * MasterScale * Scale,
                    Texture.Map.Height * MasterScale * Scale);
            base.DrawContext(ref context);
        }
    }
}