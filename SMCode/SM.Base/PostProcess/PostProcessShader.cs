using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects.Static;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM.Base.PostProcess
{
    public class PostProcessShader : GenericShader
    {
        private static ShaderFile _fragExtensions = new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.extensions.frag"));
        private static ShaderFile _normalVertex = new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexFile.vert"));
        private static string _normalVertexWithExt =
            AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexWithExt.vert");

        public PostProcessShader(string fragment) : this(_normalVertex,
            new ShaderFile(fragment)) { }

        public PostProcessShader(string vertexExt, string fragment) : this(new ShaderFile(_normalVertexWithExt)
        {
            GLSLExtensions = new List<ShaderFile>() { new ShaderFile(vertexExt) }
        }, new ShaderFile(fragment)) { }

        private PostProcessShader(ShaderFile vertex, ShaderFile fragment) : base(
            new ShaderFileCollection(vertex, fragment))
        {
            fragment.GLSLExtensions.Add(_fragExtensions);
        }

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