using System.Collections.Generic;
using OpenTK;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public interface IShader
    {
        void Draw(DrawContext context);
        void DrawInstanced(DrawContext context, ICollection<Matrix4> instanceCollection);
    }
}