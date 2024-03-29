﻿#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects.Static;
using SM.Base.Utility;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;

#endregion

namespace SM.Base.PostProcess
{
    /// <summary>
    ///     Specific shader for post processing.
    /// </summary>
    public class PostProcessShader : GenericShader
    {
        private static readonly ShaderFile _fragExtensions =
            new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.extensions.frag"));

        private static readonly ShaderFile _normalVertex =
            new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexFile.vert"));

        private static readonly string _normalVertexWithExt =
            AssemblyUtility.ReadAssemblyFile("SM.Base.PostProcess.DefaultFiles.vertexWithExt.vert");

        /// <summary>
        /// Generates an action for the texture handling.
        /// </summary>
        /// <param name="renderedTexture"></param>
        /// <returns></returns>
        public static Action<UniformCollection> DefaultTextureAction(ColorAttachment renderedTexture)
        {
            return (col) =>
            {
                col["renderedTexture"].SetTexture(renderedTexture);
                col["renderedTextureTexelSize"].SetVector2(renderedTexture.TexelSize);
            };
        }

        /// <summary>
        ///     Creates the shader with the default vertex shader and custom fragment.
        /// </summary>
        public PostProcessShader(string fragment) : this(_normalVertex, new ShaderFile(fragment))
        {
        }
        /// <summary>
        ///     Creates the shader with the default vertex shader and custom fragment shader.
        /// </summary>
        public PostProcessShader(ShaderFile fragment) : this(_normalVertex, fragment)
        {
        }

        /// <summary>
        ///     Creates the shader with an vertex extension and custom fragment.
        /// </summary>
        /// <param name="vertexExt"></param>
        /// <param name="fragment"></param>
        public PostProcessShader(string vertexExt, string fragment) : this(new ShaderFile(_normalVertexWithExt)
        {
            GLSLExtensions = new List<ShaderFile> {new ShaderFile(vertexExt)}
        }, new ShaderFile(fragment))
        {
        }
        /// <summary>
        ///     Creates the shader with an vertex shader and custom fragment.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        public PostProcessShader(ShaderFile vertex, string fragment) : this(vertex, new ShaderFile(fragment))
        {
        }

        /// <summary>
        ///     Creates the shader with an vertex shader and custom fragment.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        public PostProcessShader(ShaderFile vertex, ShaderFile fragment) : base(
            new ShaderFileCollection(vertex, fragment))
        {
            fragment.GLSLExtensions.Add(_fragExtensions);
        }

        /// <summary>
        /// Draws the shader with the color attachment as texture.
        /// </summary>
        /// <param name="renderedTexture"></param>
        public void Draw(ColorAttachment renderedTexture)
        {
            Draw(DefaultTextureAction(renderedTexture));
        }

        /// <summary>
        /// Draws the shader with the color attachment as texture and provides access to the uniforms.
        /// </summary>
        /// <param name="renderedTexture"></param>
        /// <param name="setUniformAction"></param>
        public void Draw(ColorAttachment renderedTexture, Action<UniformCollection> setUniformAction)
        {
            var texAction = DefaultTextureAction(renderedTexture);
            Draw((a) => {
                texAction(a);
                setUniformAction(a);
            });
        }

        /// <summary>
        ///     Draws the shader with special uniforms.
        /// </summary>
        /// <param name="setUniformAction"></param>        
        public void Draw(Action<UniformCollection> setUniformAction)
        {
            if (ErrorInShader) return;
            
            Activate();
            Plate.Object.Activate();

            Uniforms["MVP"].SetMatrix4(PostProcessEffect.Mvp);

            setUniformAction(Uniforms);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);

            CleanUp();
        }
    }
}