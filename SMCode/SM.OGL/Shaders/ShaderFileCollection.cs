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
        public ShaderFile[] Vertex;

        /// <summary>
        ///     Contains the geometry file.
        /// </summary>
        public ShaderFile[] Geometry;

        /// <summary>
        ///     Contains the fragment file.
        /// </summary>
        public ShaderFile[] Fragment;

        /// <summary>
        ///     Creating the collection with vertex and fragment files.
        /// </summary>
        /// <param name="vertex">The vertex source file.</param>
        /// <param name="fragment">The fragment source file.</param>
        /// <param name="geometry">The geometry source file.</param>
        public ShaderFileCollection(string vertex, string fragment, string geometry = "") : this(new ShaderFile(vertex),
            new ShaderFile(fragment), geometry != "" ? new ShaderFile(geometry) : null)
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
            Vertex = new []{vertex};
            if (geometry != null) Geometry = new[] {geometry};
            else Geometry = default;
            Fragment = new []{fragment};
        }

        /// <summary>
        ///     Creates a collection with arrays of shader files.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        /// <param name="geometry"></param>
        public ShaderFileCollection(ShaderFile[] vertex, ShaderFile[] fragment, ShaderFile[] geometry = default)
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
            foreach (ShaderFile file in Vertex) 
                file.Compile(shader, ShaderType.VertexShader);

            if (Geometry != null) 
                foreach (ShaderFile file in Geometry)
                    file.Compile(shader, ShaderType.GeometryShader);

            foreach (ShaderFile file in Fragment) 
                file.Compile(shader, ShaderType.FragmentShader);
        }

        /// <summary>
        ///     Removes the files form the shader.
        /// </summary>
        /// <param name="shader"></param>
        internal void Detach(GenericShader shader)
        {
            foreach (ShaderFile file in Vertex) 
                GL.DetachShader(shader, file);

            if (Geometry != null) 
                foreach (ShaderFile file in Geometry)
                    GL.DetachShader(shader, file);

            foreach (ShaderFile file in Fragment)
                GL.DetachShader(shader, file);

            GLDebugging.CheckGLErrors($"Error at detaching '{shader.GetType()}'");
        }
    }
}