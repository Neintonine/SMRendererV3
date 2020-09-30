using System.Collections.Generic;
using OpenTK;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// A general interface to work with material shaders properly.
    /// </summary>
    public interface IShader
    {
        /// <summary>
        /// Draws the context.
        /// </summary>
        /// <param name="context">The context</param>
        void Draw(DrawContext context);
    }
}