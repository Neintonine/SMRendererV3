#region usings

using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.OGL.Texture;

#endregion

namespace SM2D.Drawing
{
    public class DrawBackground : IBackgroundItem
    {
        private Material _material = new Material();

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

        public object Parent { get; set; }
        public string Name { get; set; } = "Background";
        public ICollection<string> Flags { get; set; } = new string[0];

        public void Update(UpdateContext context)
        {
        }

        public void Draw(DrawContext context)
        {
            context.Material = _material;
            context.Mesh = Plate.Object;

            context.Instances[0].ModelMatrix = Matrix4.CreateScale(context.WorldScale.X, context.WorldScale.Y, 1);
            context.Shader.Draw(context);
        }

        public void OnAdded(object sender)
        {
        }

        public void OnRemoved(object sender)
        {
        }
    }
}