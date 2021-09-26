#region usings

using System;
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
        /// <inheritdoc />
        protected override bool AutoCompile { get; set; } = true;

        /// <summary>
        ///     Contains the different files for the shader.
        /// </summary>
        public ShaderFileCollection ShaderFiles;

        /// <summary>
        ///     Contains and manage the uniforms from the shader.
        /// </summary>
        protected UniformCollection Uniforms;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combinedData"></param>
        protected GenericShader(string combinedData)
        {
            int firstPos = combinedData.IndexOf("//# region ", StringComparison.Ordinal);
            string header = combinedData.Substring(0, firstPos);

            int regionAmount = combinedData.Split(new string[] {"//# region "}, StringSplitOptions.None).Length - 1;
            int pos = firstPos + 10;

            string vertex = "";
            string geometry = "";
            string fragment = "";
            for (int i = 0; i < regionAmount; i++)
            {
                int posOfBreak = combinedData.IndexOf("\n", pos, StringComparison.Ordinal);
                string name = combinedData.Substring(pos, posOfBreak - pos).Trim();

                int nextPos = i == regionAmount - 1 ? combinedData.Length : combinedData.IndexOf("//# region ", posOfBreak, StringComparison.Ordinal);

                string data = header + combinedData.Substring(posOfBreak, nextPos - posOfBreak);
                pos = nextPos + 10;

                switch (name)
                {
                    case "vertex":
                        vertex = data.Replace("vmain()", "main()");
                        break;
                    case "geometry":
                        geometry = data.Replace("gmain()", "main()");
                        break;
                    case "fragment":
                        fragment = data.Replace("fmain()", "main()");
                        break;
                }
            }

            Console.WriteLine();

            ShaderFiles = new ShaderFileCollection(vertex,fragment, geometry);
        }

        /// <summary>
        /// Creates a shader out of a vertex and an fragment shader.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        protected GenericShader(string vertex, string fragment) : this(new ShaderFileCollection(vertex, fragment)){}

        /// <inheritdoc />
        protected GenericShader(ShaderFileCollection shaderFileFiles)
        {
            ShaderFiles = shaderFileFiles;
        }

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Program;

        /// <summary>
        /// Updates the shader files and recompiles the shader.
        /// </summary>
        /// <param name="newShaderFiles"></param>
        public void Update(ShaderFileCollection newShaderFiles)
        {
            ShaderFiles = newShaderFiles;
            Recompile();
        }

        /// <summary>
        ///     Loads the shader to the GPU.
        /// </summary>
        public void Load()
        {
            _id = GL.CreateProgram();

            ShaderFiles.Append(this);
            GL.LinkProgram(_id);
            ShaderFiles.Detach(this);

            Uniforms = new UniformCollection {ParentShader = this};
            Uniforms.Import(this);

            GLDebugging.CheckGLErrors($"A error occured at shader creation for '{GetType()}': %code%");
        }

        /// <summary>
        /// Activates the shader
        /// </summary>
        public void Activate()
        {
            GL.UseProgram(ID);
        }

        /// <inheritdoc />
        public override void Compile()
        {
            Load();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            GL.DeleteProgram(this);
            base.Dispose();
        }

        /// <summary>
        ///     Draws the mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="amount">The amounts for instancing.</param>
        public static void DrawObject(GenericMesh mesh, int amount = 1)
        {
            if (mesh.Indices != null)
                GL.DrawElementsInstanced(mesh.PrimitiveType, 0, DrawElementsType.UnsignedInt, mesh.Indices, amount);
            else
                GL.DrawArraysInstanced(mesh.PrimitiveType, 0, mesh.Vertex.Count, amount);
        }
        /// <summary>
        ///     Draws the mesh while forcing a primitive type instead of using the mesh type.
        /// </summary>
        /// <param name="modelType">The type, as what the object should be rendered.</param>
        /// <param name="mesh">The mesh.</param>
        /// <param name="amount">The amounts for instancing.</param>
        public static void DrawObject(PrimitiveType modelType, GenericMesh mesh, int amount = 1)
        {
            if (mesh.Indices != null)
                GL.DrawElementsInstanced(modelType, 0, DrawElementsType.UnsignedInt, mesh.Indices, amount);
            else
                GL.DrawArraysInstanced(modelType, 0, mesh.Vertex.Count, amount);
        }

        /// <summary>
        ///     Resets the shader specific settings to ensure proper workings.
        /// </summary>
        protected void CleanUp()
        {
            Uniforms.NextTexture = 0;

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}