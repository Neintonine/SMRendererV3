#region usings

using System;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Drawing.Text
{
    /// <summary>
    ///     Defines a basis for text drawing.
    /// </summary>
    /// <typeparam name="TTransform">Transformation type</typeparam>
    public abstract class TextDrawingBasis<TTransform> : DrawingBasis<TTransform>
        where TTransform : GenericTransformation, new()
    {
        /// <summary>
        ///     The different instances for drawing.
        /// </summary>
        protected Instance[] _instances;

        /// <summary>
        ///     The text, that is drawn.
        /// </summary>
        protected string _text;

        /// <summary>
        ///     The spacing between numbers.
        ///     <para>Default: 1</para>
        /// </summary>
        public float Spacing = 1;

        /// <summary>
        ///     Creates a text object with a font.
        /// </summary>
        /// <param name="font">The font.</param>
        protected TextDrawingBasis(Font font)
        {
            _material.Texture = font;
        }

        /// <summary>
        ///     The font.
        /// </summary>
        public Font Font
        {
            get => (Font) _material.Texture;
            set
            {
                _material.Texture = value;
                GenerateMatrixes();
            }
        }

        /// <summary>
        ///     The text, that is drawn.
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                GenerateMatrixes();
            }
        }

        /// <summary>
        ///     The font color.
        /// </summary>
        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }


        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            if (_instances == null) GenerateMatrixes();
        }

        /// <summary>
        ///     This generates the instances.
        /// </summary>
        /// <exception cref="Exception">The font doesn't contain a character that is needed for the text.</exception>
        public void GenerateMatrixes()
        {
            if (!Font.WasCompiled) Font.RegenerateTexture();

            _instances = new Instance[_text.Length];

            float x = 0;
            var _last = new CharParameter();
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

                var matrix = Matrix4.CreateScale(parameter.Width, Font.Height, 1) *
                             Matrix4.CreateTranslation(x, 0, 0);
                _instances[i] = new Instance
                {
                    ModelMatrix = matrix,
                    TexturePosition = new Vector2(parameter.NormalizedX, 0),
                    TextureScale = new Vector2(parameter.NormalizedWidth, 1)
                };

                x += parameter.Width * Spacing;
                _last = parameter;
            }
        }
    }
}