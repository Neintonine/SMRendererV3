#region usings

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Textures;

#endregion

namespace SM.Base.Drawing.Text
{
    /// <summary>
    ///     Represents a font.
    /// </summary>
    public class Font : Texture
    {
        /// <summary>
        ///     The char set for the font.
        ///     <para>Default: <see cref="FontCharStorage.SimpleUTF8" /></para>
        /// </summary>
        public ICollection<char> CharSet = FontCharStorage.SimpleUTF8;

        /// <summary>
        ///     The font family, that is used to find the right font.
        /// </summary>
        public FontFamily FontFamily;

        /// <summary>
        ///     The font size.
        ///     <para>Default: 12</para>
        /// </summary>
        public float FontSize = 12;

        public float Spacing = 1;

        /// <summary>
        ///     The font style.
        ///     <para>Default: <see cref="System.Drawing.FontStyle.Regular" /></para>
        /// </summary>
        public FontStyle FontStyle = FontStyle.Regular;

        /// <summary>
        ///     This contains all information for the different font character.
        /// </summary>
        public Dictionary<char, CharParameter> Positions = new Dictionary<char, CharParameter>();

        /// <summary>
        ///     Generates a font from a font family from the specified path.
        /// </summary>
        /// <param name="path">The specified path</param>
        public Font(string path)
        {
            var pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            FontFamily = pfc.Families[0];
        }

        /// <summary>
        ///     Generates a font from a specified font family.
        /// </summary>
        /// <param name="font">Font-Family</param>
        public Font(FontFamily font)
        {
            FontFamily = font;
        }

        /// <inheritdoc />
        public override TextureWrapMode WrapMode { get; set; } = TextureWrapMode.ClampToEdge;

        /// <summary>
        ///     Regenerates the texture.
        /// </summary>
        public void RegenerateTexture()
        {
            Width = 0;
            Height = 0;
            Positions = new Dictionary<char, CharParameter>();


            var map = new Bitmap(1000, 20);
            var charParams = new Dictionary<char, float[]>();
            using (var f = new System.Drawing.Font(FontFamily, FontSize, FontStyle))
            {
                using (var g = Graphics.FromImage(map))
                {
                    g.Clear(Color.Transparent);

                    foreach (var c in CharSet)
                    {
                        var s = c.ToString();
                        var size = g.MeasureString(s, f);
                        try
                        {
                            charParams.Add(c, new[] {size.Width, Width});
                        }
                        catch
                        {
                            // ignored
                        }

                        if (Height < size.Height) Height = (int) size.Height;
                        Width += (int) size.Width;
                    }
                }

                map = new Bitmap(Width, Height);
                using (var g = Graphics.FromImage(map))
                {
                    foreach (var keyValuePair in charParams)
                    {
                        var normalizedX = (keyValuePair.Value[1] + 0.00001f) / Width;
                        var normalizedWidth = keyValuePair.Value[0] / Width;

                        CharParameter parameter;
                        Positions.Add(keyValuePair.Key, parameter = new CharParameter
                        {
                            NormalizedWidth = normalizedWidth,
                            NormalizedX = normalizedX,
                            Width = keyValuePair.Value[0],
                            X = (int) keyValuePair.Value[1]
                        });

                        g.DrawString(keyValuePair.Key.ToString(), f, Brushes.White, parameter.X, 0);
                    }
                }
            }

            Map = map;
            Recompile();
        }

        /// <inheritdoc />
        public override void Compile()
        {
            RegenerateTexture();
            base.Compile();
        }
    }
}