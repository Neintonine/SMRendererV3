using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Policy;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base.Textures;
using SM.Data.Fonts;
using SM.OGL.Texture;

namespace SM.Base.Text
{
    /// <summary>
    /// Represents a font.
    /// </summary>
    public class Font : Texture
    {
        /// <inheritdoc />
        public override TextureWrapMode WrapMode { get; set; } = TextureWrapMode.ClampToEdge;
        
        /// <summary>
        /// The font family, that is used to find the right font.
        /// </summary>
        public FontFamily FontFamily;

        /// <summary>
        /// The font style.
        /// <para>Default: <see cref="System.Drawing.FontStyle.Regular"/></para>
        /// </summary>
        public FontStyle FontStyle = FontStyle.Regular;
        /// <summary>
        /// The font size.
        /// <para>Default: 12</para>
        /// </summary>
        public float FontSize = 12;
        /// <summary>
        /// The char set for the font.
        /// <para>Default: <see cref="FontCharStorage.SimpleUTF8"/></para>
        /// </summary>
        public ICollection<char> CharSet = FontCharStorage.SimpleUTF8;

        /// <summary>
        /// This contains all information for the different font character.
        /// </summary>
        public Dictionary<char, CharParameter> Positions = new Dictionary<char, CharParameter>();

        /// <summary>
        /// Generates a font from a font family from the specified path.
        /// </summary>
        /// <param name="path">The specified path</param>
        public Font(string path)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            FontFamily = pfc.Families[0];
        }

        /// <summary>
        /// Generates a font from a specified font family.
        /// </summary>
        /// <param name="font">Font-Family</param>
        public Font(FontFamily font)
        {
            FontFamily = font;
        }
        /// <summary>
        /// Regenerates the texture.
        /// </summary>
        public void RegenerateTexture()
        {
            Width = 0;
            Height = 0;
            Positions = new Dictionary<char, CharParameter>();


            Bitmap map = new Bitmap(1000, 20);
            Dictionary<char, float[]> charParams = new Dictionary<char, float[]>();
            using (System.Drawing.Font f = new System.Drawing.Font(FontFamily, FontSize, FontStyle))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.Transparent);

                    foreach (char c in CharSet)
                    {
                        string s = c.ToString();
                        SizeF size = g.MeasureString(s, f);
                        try
                        {
                            charParams.Add(c, new[] {size.Width, Width });
                        }
                        catch
                        {
                            // ignored
                        }

                        if (Height < size.Height) Height = (int)size.Height;
                        Width += (int)size.Width;
                    }
                }

                map = new Bitmap(Width, Height);
                using (Graphics g = Graphics.FromImage(map))
                {
                    foreach (KeyValuePair<char, float[]> keyValuePair in charParams)
                    {
                        float normalizedX = (keyValuePair.Value[1] + 0.00001f) / Width;
                        float normalizedWidth = (keyValuePair.Value[0]) / Width;

                        CharParameter parameter;
                        Positions.Add(keyValuePair.Key, parameter = new CharParameter()
                        {
                            NormalizedWidth = normalizedWidth,
                            NormalizedX = normalizedX,
                            Width = keyValuePair.Value[0],
                            X = (int)keyValuePair.Value[1]
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