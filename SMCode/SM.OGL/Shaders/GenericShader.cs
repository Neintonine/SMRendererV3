using System;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    /// <summary>
    /// Abstract class, that is used to create graphic shader.
    /// </summary>
    public abstract class GenericShader : GLObject
    {
        /// <summary>
        /// Contains the different files for the shader.
        /// </summary>
        protected ShaderFileCollection ShaderFileFiles;
        /// <summary>
        /// Contains and manage the uniforms from the shader.
        /// </summary>
        protected UniformCollection Uniforms;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Program;


        /// <inheritdoc />
        protected GenericShader(ShaderFileCollection shaderFileFiles)
        {
            ShaderFileFiles = shaderFileFiles;
        }

        /// <summary>
        /// Loads the shader to the GPU.
        /// </summary>
        public void Load()
        {
            
            _id = GL.CreateProgram();

            ShaderFileFiles.Append(this);
            GL.LinkProgram(_id);
            this.Name(GetType().Name);
            ShaderFileFiles.Detach(this);

            GL.GetProgram(_id, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            if (uniformCount < 1)
                throw new Exception("[Critical] No uniforms has been found.");

            Uniforms = new UniformCollection();
            Uniforms._parentShader = this;
            for (int i = 0; i < uniformCount; i++)
            {
                string key = GL.GetActiveUniform(_id, i, out _, out _);
                int loc = GL.GetUniformLocation(_id, key);

                if (key.EndsWith("]")) 
                    key = key.Split('[', ']')[0];
                Uniforms.Add(key, loc);
            }

        }

        /// <inheritdoc />
        protected override void Compile()
        {
            Load();
        }

        /// <summary>
        /// Draws the mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="amount">The amounts for instancing.</param>
        /// <param name="bindVAO">Binds the vertex array for the mesh.</param>
        protected void DrawObject(Mesh.GenericMesh mesh, int amount, bool bindVAO = false)
        {
            if (bindVAO) GL.BindVertexArray(mesh);

            if (mesh.Indices != null)
                GL.DrawElementsInstanced(mesh.PrimitiveType, 0, DrawElementsType.UnsignedInt, mesh.Indices, amount);
            else 
                GL.DrawArraysInstanced(mesh.PrimitiveType, 0, mesh.Vertex.Count, amount);
        }
        /// <summary>
        /// Resets the shader specific settings to ensure proper workings.
        /// </summary>
        protected void CleanUp()
        {
            Uniforms.NextTexture = 0;

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
        }
    }
}