#region usings

using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    ///     Abstract class, that is used to create graphic shader.
    /// </summary>
    public abstract class GenericShader : GLObject
    {
        protected override bool AutoCompile { get; } = true;

        /// <summary>
        ///     Contains the different files for the shader.
        /// </summary>
        protected ShaderFileCollection ShaderFileFiles;

        /// <summary>
        ///     Contains and manage the uniforms from the shader.
        /// </summary>
        protected UniformCollection Uniforms;

        protected GenericShader(string vertex, string fragment) : this(new ShaderFileCollection(vertex, fragment)){}

        /// <inheritdoc />
        protected GenericShader(ShaderFileCollection shaderFileFiles)
        {
            ShaderFileFiles = shaderFileFiles;
        }

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Program;

        /// <summary>
        ///     Loads the shader to the GPU.
        /// </summary>
        public void Load()
        {
            _id = GL.CreateProgram();

            ShaderFileFiles.Append(this);
            GL.LinkProgram(_id);
            Name(GetType().Name);
            ShaderFileFiles.Detach(this);

            Uniforms = new UniformCollection();
            Uniforms.ParentShader = this;
            Uniforms.Import(this);

            GLDebugging.CheckGLErrors($"A error occured at shader creation for '{GetType()}': %code%");
        }

        /// <inheritdoc />
        public override void Compile()
        {
            Load();
        }

        /// <summary>
        ///     Draws the mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="amount">The amounts for instancing.</param>
        /// <param name="bindVAO">Binds the vertex array for the mesh.</param>
        protected void DrawObject(GenericMesh mesh, int amount = 1)
        {
            if (mesh.Indices != null)
                GL.DrawElementsInstanced(mesh.PrimitiveType, 0, DrawElementsType.UnsignedInt, mesh.Indices, amount);
            else
                GL.DrawArraysInstanced(mesh.PrimitiveType, 0, mesh.Vertex.Count, amount);
        }

        /// <summary>
        ///     Resets the shader specific settings to ensure proper workings.
        /// </summary>
        protected void CleanUp()
        {
            Uniforms.NextTexture = 0;

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
        }
    }
}