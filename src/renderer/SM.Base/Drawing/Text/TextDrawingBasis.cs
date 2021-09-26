#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Objects.Static;
using SM.Base.Window;

#endregion

namespace SM.Base.Drawing.Text
{
    /// <summary>
    /// Represents the options for <see cref="TextDrawingBasis{TTransform}.Origin"/>
    /// </summary>
    public enum TextOrigin
    {
        /// <summary>
        /// The position equals (0,0) in the left side of the text.
        /// </summary>
        Left,
        /// <summary>
        /// The position equals (0,0) in the center of the text.
        /// </summary>
        Center,
        /// <summary>
        /// The position equals (0,0) in the right side of the text.
        /// </summary>
        Right
    }

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
        /// Allow to change the origin of the text.
        /// <para>Default: <see cref="TextOrigin.Left"/></para>
        /// </summary>
        public TextOrigin Origin = TextOrigin.Left;

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
            Mesh = Plate.Object;
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

            if (string.IsNullOrEmpty(_text)) return;

            _text = _text.Replace("\r\n", "\n").Replace("\t", "    ");

            _instances = new Instance[_text.Length];
            List<Tuple<Vector2, Instance[]>> lines = new List<Tuple<Vector2, Instance[]>>();
            List<Instance> currentLineInstances = new List<Instance>();

            float x = 0;
            float y = 0;
            for (var i = 0; i < _text.Length; i++)
            {
                if (_text[i] == ' ')
                {
                    x += Font.SpaceWidth * Spacing;
                    continue;
                }

                if (_text[i] == '\n')
                {
                    if (currentLineInstances.Count > 0)
                    {
                        lines.Add(new Tuple<Vector2, Instance[]>(new Vector2(x, y), currentLineInstances.ToArray()));
                        currentLineInstances.Clear();
                    }

                    y += Font.Height;
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
                             Matrix4.CreateTranslation(x + parameter.Width / 2, -y, 0);
                currentLineInstances.Add(_instances[i] = new Instance
                {
                    ModelMatrix = matrix,
                    TextureMatrix = parameter.TextureMatrix
                });

                x += parameter.Advance;
            }
            if (currentLineInstances.Count > 0)
                lines.Add(new Tuple<Vector2, Instance[]>(new Vector2(x, y), currentLineInstances.ToArray()));

            Height = y + Font.Height;
            Width = lines.Max(a => a.Item1.X);

            if (Origin != TextOrigin.Left)
            {
                foreach (Tuple<Vector2, Instance[]> line in lines)
                {
                    
                    foreach (Instance i in line.Item2)
                    {
                        if (i == null) continue;
                        switch (Origin)
                        {
                            case TextOrigin.Center:
                                i.ModelMatrix *= Matrix4.CreateTranslation(-line.Item1.X / 2, 0, 0);
                                break;
                            case TextOrigin.Right:
                                i.ModelMatrix *= Matrix4.CreateTranslation(-line.Item1.X, 0, 0);
                                break;
                        }
                    }
                }

                
            }
        }
    }
}