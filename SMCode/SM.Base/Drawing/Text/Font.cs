using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenTK;
using SharpFont;
using SM.Base.Textures;

namespace SM.Base.Drawing.Text
{
    public class Font : Texture
    {
        private static Library _lib;

        private Face _fontFace;

        public float SpaceWidth { get; private set; } = 0;

        public ICollection<char> CharSet { get; set; } = FontCharStorage.SimpleUTF8;

        public float FontSize { get; set; } = 12;

        public Dictionary<char, CharParameter> Positions = new Dictionary<char, CharParameter>();


        public Font(string path)
        {
            _lib ??= new Library();

            _fontFace = new Face(_lib, path);
            UnpackAlignment = 1;
        }

        public void RegenerateTexture()
        {
            Width = Height = 0;
            Positions = new Dictionary<char, CharParameter>();

            _fontFace.SetCharSize(0, FontSize, 0, 96);

            var pos = new Dictionary<char, float[]>();
            foreach (char c in CharSet)
            {
                _fontFace.LoadChar(c, LoadFlags.Render, LoadTarget.Normal);

                pos.Add(c, new []{(float)_fontFace.Glyph.Bitmap.Width, Width});
                Width += (int)_fontFace.Glyph.Advance.X + 2;
                Height = Math.Max(_fontFace.Glyph.Bitmap.Rows, Height);
            }

            _fontFace.LoadChar('_', LoadFlags.Render, LoadTarget.Normal);
            SpaceWidth = _fontFace.Glyph.Advance.X.ToSingle();

            float bBoxHeight = (Math.Abs(_fontFace.BBox.Bottom) + _fontFace.BBox.Top);
            float bBoxTopScale = _fontFace.BBox.Top / bBoxHeight;
            float baseline = Height * bBoxTopScale + 1;

            Map = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(Map))
            {
                g.Clear(Color.Transparent);
                

                foreach (var keyvalue in pos)
                {
                    _fontFace.LoadChar(keyvalue.Key, LoadFlags.Render, LoadTarget.Normal);

                    int y = ((int)baseline - (int)_fontFace.Glyph.Metrics.HorizontalBearingY);
                    
                    g.DrawImageUnscaled(_fontFace.Glyph.Bitmap.ToGdipBitmap(Color.White), (int)keyvalue.Value[1], y);

                    Vector2 offset = new Vector2(keyvalue.Value[1] / Width, 0);
                    Vector2 scale = new Vector2(keyvalue.Value[0] / Width, 1);
                    Positions.Add(keyvalue.Key, new CharParameter()
                    {
                        Advance = (int)_fontFace.Glyph.LinearHorizontalAdvance,
                        BearingX = _fontFace.Glyph.BitmapLeft,

                        Width = keyvalue.Value[0],

                        TextureMatrix = TextureTransformation.CalculateMatrix(offset,
                            scale, 0),
                    });
                }
            }
        }

        public override void Compile()
        {
            RegenerateTexture();
            base.Compile();
        }
    }
}