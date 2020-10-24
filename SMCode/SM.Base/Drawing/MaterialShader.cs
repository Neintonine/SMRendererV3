#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.OGL.Shaders;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     A general class to work with material shaders properly.
    /// </summary>
    public abstract class MaterialShader : GenericShader
    {

        protected MaterialShader(string vertex, string fragment) : base(vertex, fragment)
        {
        }

        protected MaterialShader(ShaderFileCollection shaderFileFiles) : base(shaderFileFiles)
        {
        }

        /// <summary>
        ///     Draws the context.
        /// </summary>
        /// <param name="context">The context</param>
        public virtual void Draw(DrawContext context)
        {
            GL.UseProgram(this);

            GL.BindVertexArray(context.Mesh);

            DrawProcess(context);

            CleanUp();

            GL.UseProgram(0);
        }

        protected virtual void DrawProcess(DrawContext context)
        {

        }
    }
}