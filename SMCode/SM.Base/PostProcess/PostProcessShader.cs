using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects.Static;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM.Base.PostProcess
{
    /// <summary>
    ///     Specific shader for post processing.
    /// </summary>
    public class PostProcessShader : GenericShader
    {
        private static readonly ShaderFile _fragExtensions = new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.extensions.frag"));
        private static readonly ShaderFile _normalVertex = new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexFile.vert"));
        private static readonly string _normalVertexWithExt =
            AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexWithExt.vert");

        /// <summary>
        ///     Creates the shader with the default vertex shader and custom fragment.
        /// </summary>
        public PostProcessShader(string fragment) : this(_normalVertex,
            new ShaderFile(fragment)) 
        { }

        /// <summary>
        ///     Creates the shader with an vertex extension and custom fragment.
        /// </summary>
        /// <param name="vertexExt"></param>
        /// <param name="fragment"></param>
        public PostProcessShader(string vertexExt, string fragment) : this(new ShaderFile(_normalVertexWithExt)
        {
            GLSLExtensions = new List<ShaderFile>() { new ShaderFile(vertexExt) }
        }, new ShaderFile(fragment)) 
        { }

        private PostProcessShader(ShaderFile vertex, ShaderFile fragment) : base(
            new ShaderFileCollection(vertex, fragment))
        {
            fragment.GLSLExtensions.Add(_fragExtensions);
        }

        /// <summary>
        /// Draws the shader without special uniforms.
        /// </summary>
        /// <param name="color"></param>
        public void Draw(ColorAttachment color)
        {
            GL.UseProgram(this);
            GL.BindVertexArray(Plate.Object);

            Uniforms["MVP"].SetMatrix4(PostProcessEffect.Mvp);
            Uniforms["renderedTexture"].SetTexture(color, 0);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        /// <summary>
        /// Draws the shader with special uniforms.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="setUniformAction"></param>
        public void Draw(ColorAttachment color, Action<UniformCollection> setUniformAction)
        {
            GL.UseProgram(this);
            GL.BindVertexArray(Plate.Object);

            Uniforms["MVP"].SetMatrix4(PostProcessEffect.Mvp);
            Uniforms["renderedTexture"].SetTexture(color, 0);

            setUniformAction(Uniforms);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}