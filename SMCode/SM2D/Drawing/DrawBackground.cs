#region usings

using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Drawing;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Window;
using SM.OGL.Texture;
using SM2D.Scene;

#endregion

namespace SM2D.Drawing
{
    /// <summary>
    /// Allows easy access to draw something on the background.
    /// </summary>
    public class DrawBackground : DrawingBasis, IBackgroundItem
    {
        /// <summary>
        /// Sets the color or tint (in case a texture is set).
        /// </summary>
        public Color4 Color
        {
            get => Material.Tint;
            set => Material.Tint = value;
        }

        /// <summary>
        /// Sets the texture of the background.
        /// </summary>
        public TextureBase Texture
        {
            get => Material.Texture;
            set
            {
                if (Material.Tint == Color4.Black) Material.Tint = Color4.White;
                Material.Texture = value;
            }
        }

        /// <summary>
        /// Creates a black background.
        /// </summary>
        public DrawBackground() : this(Color4.Black) {}

        /// <summary>
        /// Creates a background with a color.
        /// </summary>
        /// <param name="color"></param>
        public DrawBackground(Color4 color)
        {
            Color = color;
            Mesh = Plate.Object;
        }

        /// <summary>
        /// Creates a background with a texture.
        /// </summary>
        /// <param name="texture"></param>
        public DrawBackground(Bitmap texture)
        {
            Texture = (Texture) texture;
            Mesh = Plate.Object;
        }

        /// <summary>
        /// Creates a background with a texture and a tint.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="tint"></param>
        public DrawBackground(Bitmap texture, Color4 tint)
        {
            Color = tint;
            Texture = (Texture) texture;
            Mesh = Plate.Object;
        }


        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.ModelMatrix = Matrix4.CreateScale((context.UseCamera as Camera).WorldScale.X, (context.UseCamera as Camera).WorldScale.Y, 0) * Matrix4.CreateTranslation(0,0, -1.1f);
            context.Shader.Draw(context);
        }
    }
}