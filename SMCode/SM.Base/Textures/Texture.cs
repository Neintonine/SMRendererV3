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
        public bool AutoDispose = false;

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

            _id = GenerateTexture(Map, Filter, WrapMode, AutoDispose);
        }

        public static int GenerateTexture(Bitmap map, TextureMinFilter filter, TextureWrapMode wrapMode, bool dispose = false)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadOnly,
                map.PixelFormat);

            bool transparenz = map.PixelFormat == PixelFormat.Format32bppArgb;

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                transparenz ? PixelInternalFormat.Rgba : PixelInternalFormat.Rgb,
                data.Width, data.Height, 0,
                transparenz ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgra : OpenTK.Graphics.OpenGL4.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapMode);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            map.UnlockBits(data);

            if (dispose) map.Dispose();

            return id;
        }

        public static implicit operator Texture(Bitmap map) => new Texture(map);
        public override TextureMinFilter Filter { get; set; }
        public override TextureWrapMode WrapMode { get; set; }
    }
}