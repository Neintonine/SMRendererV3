#region usings

using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    ///     Collects all files that are needed for a shader.
    /// </summary>
    public struct ShaderFileCollection
    {
        /// <summary>
        ///     Contains the vertex file.
        /// </summary>
        public ShaderFile Vertex;

        /// <summary>
        ///     Contains the geometry file.
        /// </summary>
        public ShaderFile Geometry;

        /// <summary>
        ///     Contains the fragment file.
        /// </summary>
        public ShaderFile Fragment;

        /// <summary>
        ///     Creating the collection with vertex and fragment files.
        /// </summary>
        /// <param name="vertex">The vertex source file.</param>
        /// <param name="fragment">The fragment source file.</param>
        public ShaderFileCollection(string vertex, string fragment) : this(new ShaderFile(vertex),
            new ShaderFile(fragment))
        {
        }

        /// <summary>
        ///     Creating the collection with shader files.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        /// <param name="geometry"></param>
        public ShaderFileCollection(ShaderFile vertex, ShaderFile fragment, ShaderFile geometry = default)
        {
            Vertex = vertex;
            Geometry = geometry;
            Fragment = fragment;
        }

        /// <summary>
        ///     Appends the files to the shader.
        /// </summary>
        /// <param name="shader"></param>
        internal void Append(GenericShader shader)
        {
            Vertex.Compile(shader, ShaderType.VertexShader);
            Geometry?.Compile(shader, ShaderType.GeometryShader);
            Fragment.Compile(shader, ShaderType.FragmentShader);
        }

        /// <summary>
        ///     Removes the files form the shader.
        /// </summary>
        /// <param name="shader"></param>
        internal void Detach(GenericShader shader)
        {
            GL.DetachShader(shader, Vertex);
            if (Geometry != null) GL.DetachShader(shader, Geometry);
            GL.DetachShader(shader, Fragment);

            GLDebugging.CheckGLErrors($"Error at detaching '{shader.GetType()}'");
        }
    }
}