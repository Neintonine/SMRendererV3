using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Texture
{
    /// <summary>
    /// Stores information how pixels are stored in textures.
    /// </summary>
    public struct PixelInformation
    {
        /// <summary>
        /// RGB without Alpha channel, Low Dynamic Range (0 - 1)
        /// </summary>
        public static PixelInformation RGB_LDR = new PixelInformation(PixelInternalFormat.Rgb, PixelFormat.Rgb, PixelType.UnsignedByte);
        /// <summary>
        /// RGB without Alpha channel, High Dynamic Range (0 - n)
        /// </summary>
        public static PixelInformation RGB_HDR = new PixelInformation(PixelInternalFormat.Rgb16f, PixelFormat.Rgb, PixelType.Float);
        /// <summary>
        /// RGB with Alpha channel, Low Dynamic Range (0 - 1)
        /// </summary>
        public static PixelInformation RGBA_LDR = new PixelInformation(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte);
        /// <summary>
        /// RGB with Alpha channel, High Dynamic Range (0 - n)
        /// </summary>
        public static PixelInformation RGBA_HDR = new PixelInformation(PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);

        /// <summary>
        /// The internal format of the pixels.
        /// </summary>
        public PixelInternalFormat InternalFormat { get; }
        /// <summary>
        /// The format of the pixels.
        /// </summary>
        public PixelFormat Format { get; }
        /// <summary>
        /// The data type of the pixels,
        /// </summary>
        public PixelType DataType { get; }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="internalFormat"></param>
        /// <param name="format"></param>
        /// <param name="dataType"></param>
        public PixelInformation(PixelInternalFormat internalFormat, PixelFormat format, PixelType dataType)
        {
            InternalFormat = internalFormat;
            Format = format;
            DataType = dataType;
        }
    }
}