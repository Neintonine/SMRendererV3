#region usings

using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

#endregion

namespace SM.Base.Textures
{
    /// <summary>
    ///     Texture that can be drawn to an object.
    /// </summary>
    public class Texture : TextureBase
    {
        static HintMode _hintMode;
        /// <summary>
        /// Defines the texture quailty.
        /// <para>It won't change already compiled textures!</para>
        /// </summary>
        public static HintMode TextureQuality
        {
            get => _hintMode;
            set
            {
                _hintMode = value;
                GL.Hint(HintTarget.TextureCompressionHint, value);
            }
        }

        private int? _height;
        private int? _width;

        /// <summary>
        ///     Decides if the bitmap will automatically dispose itself.
        /// </summary>
        public bool AutoDispose = false;

        /// <summary>
        ///     The texture as bitmap.
        /// </summary>
        public Bitmap Map;

        /// <summary>
        ///     Empty constructor
        /// </summary>
        protected Texture()
        {
        }

        /// <summary>
        ///     Creates a texture with only the map.
        ///     <para>Sets the filter to Linear and WrapMode to Repeat.</para>
        /// </summary>
        /// <param name="map">The map</param>
        public Texture(Bitmap map) : this(map, TextureMinFilter.Linear, TextureWrapMode.Repeat)
        {
        }

        /// <summary>
        ///     Creates the texture.
        /// </summary>
        /// <param name="map">The texture map</param>
        /// <param name="filter">The filter</param>
        /// <param name="wrapMode">The wrap mode</param>
        public Texture(Bitmap map, TextureMinFilter filter, TextureWrapMode wrapMode)
        {
            Map = map;

            Aspect = (float) map.Width / map.Height;

            Filter = filter;
            WrapMode = wrapMode;
        }

        /// <inheritdoc />
        public override int Width
        {
            get => _width ?? Map.Width;
            protected set => _width = value;
        }

        /// <inheritdoc />
        public override int Height
        {
            get => _height ?? Map.Height;
            protected set => _height = value;
        }

        /// <summary>
        ///     Aspect ratio of Width and Height of the texture
        /// </summary>
        public float Aspect { get; }


        /// <inheritdoc />
        public override void Compile()
        {
            base.Compile();

            _id = GenerateTexture(Map, Filter, WrapMode, AutoDispose);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            GL.DeleteTexture(this);
        }

        /// <summary>
        ///     Generates a OpenGL-texture.
        /// </summary>
        /// <param name="map">The texture as bitmap</param>
        /// <param name="filter">The filter</param>
        /// <param name="wrapMode">The wrap mode</param>
        /// <param name="dispose">Auto dispose of the bitmap? Default: false</param>
        /// <returns></returns>
        public static int GenerateTexture(Bitmap map, TextureMinFilter filter, TextureWrapMode wrapMode,
            bool dispose = false)
        {
            var id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            var data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadOnly,
                map.PixelFormat);

            var transparenz = map.PixelFormat == PixelFormat.Format32bppArgb;

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                transparenz ? PixelInternalFormat.CompressedRgba : PixelInternalFormat.CompressedRgb,
                data.Width, data.Height, 0,
                transparenz ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgra : OpenTK.Graphics.OpenGL4.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) wrapMode);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            map.UnlockBits(data);

            if (dispose) map.Dispose();

            return id;
        }

        /// <summary>
        ///     Converts a bitmap to a texture.
        /// </summary>
        public static implicit operator Texture(Bitmap map)
        {
            return new Texture(map);
        }
    }
}