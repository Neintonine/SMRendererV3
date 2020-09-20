using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Policy;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Data.Fonts;
using SM.OGL.Texture;

namespace SM.Base.Text
{
    public class Font : TextureBase
    {
        public  Dictionary<char, CharParameter> Positions = new Dictionary<char, CharParameter>();

        public override TextureMinFilter Filter { get; set; } = TextureMinFilter.Linear;
        public override TextureWrapMode WrapMode { get; set; } = TextureWrapMode.ClampToEdge;

        public FontFamily FontFamily;

        public FontStyle FontStyle = FontStyle.Regular;
        public float FontSize = 12;
        public ICollection<char> CharSet = FontCharStorage.SimpleUTF8;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Bitmap Texture { get; private set; }

        public Font(string path)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            FontFamily = pfc.Families[0];
        }

        public Font(FontFamily font)
        {
            FontFamily = font;
        }

        public void RegenerateTexture()
        {
            Bitmap map = new Bitmap(1000,20);
            Width = 0;
            Height = 0;
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
                            Positions.Add(c, new CharParameter() { Width = size.Width, X = Width });
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
                    foreach (KeyValuePair<char, CharParameter> keyValuePair in Positions)
                    {
                        keyValuePair.Value.RelativeX = (keyValuePair.Value.X + 0.00001f) / Width;
                        keyValuePair.Value.RelativeWidth = (keyValuePair.Value.Width) / Width;

                        g.DrawString(keyValuePair.Key.ToString(), f, Brushes.White, keyValuePair.Value.X, 0);
                    }
                }
            }

            Texture = map;

            _id = SM.Base.Textures.Texture.GenerateTexture(map, Filter, WrapMode);
        }

        protected override void Compile()
        {
            base.Compile();
            RegenerateTexture();
        }
    }
}