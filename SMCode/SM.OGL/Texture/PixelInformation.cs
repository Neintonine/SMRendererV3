using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Texture
{
    public struct PixelInformation
    {
        public static PixelInformation RGB_LDR = new PixelInformation(PixelInternalFormat.Rgb, PixelFormat.Rgb, PixelType.UnsignedByte);
        public static PixelInformation RGB_HDR = new PixelInformation(PixelInternalFormat.Rgb16f, PixelFormat.Rgb, PixelType.Float);
        public static PixelInformation RGBA_LDR = new PixelInformation(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte);
        public static PixelInformation RGBA_HDR = new PixelInformation(PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float);

        public PixelInternalFormat InternalFormat { get; }
        public PixelFormat Format { get; }
        public PixelType DataType { get; }

        public PixelInformation(PixelInternalFormat internalFormat, PixelFormat format, PixelType dataType)
        {
            InternalFormat = internalFormat;
            Format = format;
            DataType = dataType;
        }
    }
}