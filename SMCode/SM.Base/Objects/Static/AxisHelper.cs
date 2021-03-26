#region usings

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Objects.Static
{
    /// <summary>
    ///     An AxisHelper-Model
    ///     <para>White: -X, -Y, -Z</para>
    ///     <para>Red: +X </para>
    ///     <para>Green: +Y </para>
    ///     <para>Blue: +Z </para>
    /// </summary>
    public class AxisHelper : Mesh
    {
        /// <summary>
        ///     Object
        /// </summary>
        public static AxisHelper Object = new AxisHelper();

        private AxisHelper() : base(PrimitiveType.Lines)
        {
        }

        /// <inheritdoc />
        public override VBO<Vector3> Vertex { get; protected set; } = new VBO<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(.5f, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, .5f, 0),
            new Vector3(0, 0, -.5f),
            new Vector3(0, 0, .5f)
        };

        /// <inheritdoc />
        public override VBO<Color4> Color { get; protected set; } = new VBO<Color4>
        {
            Color4.White,
            Color4.Red,
            Color4.White,
            Color4.Green,
            Color4.White,
            Color4.DarkBlue
        };
    }
}