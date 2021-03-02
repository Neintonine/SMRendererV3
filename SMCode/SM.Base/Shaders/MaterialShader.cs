#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Windows;
using SM.OGL.Mesh;
using SM.OGL.Shaders;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     A general class to work with material shaders properly.
    /// </summary>
    public abstract class MaterialShader : GenericShader
    {
        /// <inheritdoc />
        protected MaterialShader(string combinedData) : base(combinedData)
        {}

        /// <inheritdoc />
        protected MaterialShader(string vertex, string fragment) : base(vertex, fragment)
        {
        }

        /// <inheritdoc />
        protected MaterialShader(ShaderFileCollection shaderFileFiles) : base(shaderFileFiles)
        {
        }

        /// <summary>
        ///     Prepares the context for the drawing.
        /// </summary>
        /// <param name="context">The context</param>
        public virtual void Draw(DrawContext context)
        {
            context.Shader.Activate();

            context.Mesh.Activate();

            if (context.Mesh is ILineMesh lineMesh) 
                GL.LineWidth(context.Material.ShaderArguments.Get("LineWidth", lineMesh.LineWidth)); 

            if (context.Material.Blending)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            } else GL.Disable(EnableCap.Blend);

            DrawProcess(context);

            CleanUp();
        }

        /// <summary>
        ///     Draws the context.
        /// </summary>
        /// <param name="context"></param>
        protected abstract void DrawProcess(DrawContext context);
    }
}