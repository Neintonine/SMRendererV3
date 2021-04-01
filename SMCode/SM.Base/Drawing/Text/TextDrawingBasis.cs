#region usings

using System;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Window;

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
        /// The width of the text object.
        /// </summary>
        public float Width;

        /// <summary>
        /// The height of the text object.
        /// </summary>
        public float Height;

        /// <summary>
        ///     The spacing between numbers.
        ///     <para>Default: 1</para>
        /// </summary>
        public float Spacing = 1f;

        /// <summary>
        ///     The font.
        /// </summary>
        public Font Font
        {
            get => (Font)Material.Texture;
            set
            {
                Material.Texture = value;
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
            get => Material.Tint;
            set => Material.Tint = value;
        }

        /// <summary>
        ///     Creates a text object with a font.
        /// </summary>
        /// <param name="font">The font.</param>
        protected TextDrawingBasis(Font font)
        {
            Material.Texture = font;
            Material.Blending = true;
        }


        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            if (_instances == null) GenerateMatrixes();
        }

        /// <summary>
        ///     This generates the instances.
        /// </summary>
        /// <exception cref="Exception">The font doesn't contain a character that is needed for the text.</exception>
        public void GenerateMatrixes()
        {
            if (!Font.WasCompiled) Font.RegenerateTexture();

            _text = _text.Replace("\r\n", "\n").Replace("\t", "    ");

            _instances = new Instance[_text.Length];

            float x = 0;
            float y = 0;
            var _last = new CharParameter();
            for (var i = 0; i < _text.Length; i++)
            {

                if (_text[i] == ' ')
                {
                    x += Font.SpaceWidth * Spacing;
                    continue;
                }

                if (_text[i] == '\n')
                {
                    y += Font.Height;
                    Width = Math.Max(Width, x);
                    x = 0;
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


                if (i == 0)
                {
                    x += parameter.Width / 2;
                }

                var matrix = Matrix4.CreateScale(parameter.Width, Font.Height, 1) *
                             Matrix4.CreateTranslation(x, -y, 0);
                _instances[i] = new Instance
                {
                    ModelMatrix = matrix,
                    TextureMatrix = parameter.TextureMatrix
                };

                x += Math.Max(parameter.Advance, 6);
                _last = parameter;
            }

            Width = Math.Max(Width, x);
            Height = y + Font.Height;
        }
    }
}