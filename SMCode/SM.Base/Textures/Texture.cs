using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace SM.Base.Textures
{
    public class Texture : TextureBase
    {
        public Bitmap Map;

        public Texture(Bitmap map) : this(map, TextureMinFilter.Linear, TextureWrapMode.Repeat) {}

        public Texture(Bitmap map, TextureMinFilter filter, TextureWrapMode wrapMode)
        {
            Map = map;
            Filter = filter;
            WrapMode = wrapMode;
        }

        protected override void Compile()
        {
            base.Compile();

            _id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _id);

            BitmapData data = Map.LockBits(new Rectangle(0, 0, Map.Width, Map.Height), ImageLockMode.ReadOnly,
                Map.PixelFormat);

            bool transparenz = Map.PixelFormat == PixelFormat.Format32bppArgb;

            GL.TexImage2D(TextureTarget.Texture2D, 0, 
                transparenz ? PixelInternalFormat.Rgba : PixelInternalFormat.Rgb,
                data.Width, data.Height, 0,
                transparenz ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgra : OpenTK.Graphics.OpenGL4.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)Filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)Filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) WrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) WrapMode);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            Map.UnlockBits(data);
        }

        public static implicit operator Texture(Bitmap map) => new Texture(map);
        public override TextureMinFilter Filter { get; set; }
        public override TextureWrapMode WrapMode { get; set; }
    }
}