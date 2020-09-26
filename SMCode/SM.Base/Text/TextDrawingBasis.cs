using System;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Data.Fonts;

namespace SM.Base.Text
{
    public abstract class TextDrawingBasis<TTransform> : DrawingBasis<TTransform>
        where TTransform : GenericTransformation, new()
    {
        protected Instance[] _modelMatrixs;
        protected string _text;

        public Font Font
        {
            get => (Font) _material.Texture;
            set
            {
                _material.Texture = value;
                GenerateMatrixes();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                GenerateMatrixes();
            }
        }

        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        public float Spacing = 1;

        protected TextDrawingBasis(Font font)
        {
            _material.Texture = font;
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            if (_modelMatrixs == null) GenerateMatrixes();
        }

        public void GenerateMatrixes()
        {
            if (!Font.WasCompiled) Font.RegenerateTexture();

            _modelMatrixs = new Instance[_text.Length];

            float x = 0;
            CharParameter _last = new CharParameter();
            for (var i = 0; i < _text.Length; i++)
            {
                if (_text[i] == 32)
                {
                    x += _last.Width * Spacing;
                    continue;
                }
                
                CharParameter parameter;
                try
                {
                    parameter = Font.Positions[_text[i]];
                }
                catch
                {
                    throw new Exception("Font doesn't contain '" + _text[i] + "'");
                }

                Matrix4 matrix = Matrix4.CreateScale(parameter.Width, Font.Height, 1) *
                                 Matrix4.CreateTranslation(x, 0, 0);
                _modelMatrixs[i] = new Instance
                {
                    ModelMatrix = matrix,
                    TexturePosition = new Vector2(parameter.RelativeX, 0),
                    TextureScale = new Vector2(parameter.RelativeWidth, 1)
                };
                
                x += parameter.Width * Spacing;
                _last = parameter;
            }
        }
    }
}