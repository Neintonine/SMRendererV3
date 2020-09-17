using OpenTK;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Shader;
using SM.Base.StaticObjects;
using SM.OGL.Mesh;

namespace SM2D.Drawing
{
    public class DrawEmpty : IShowItem
    {
        public void Update(UpdateContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(DrawContext context)
        {
            context.ModelMatrix = Matrix4.CreateScale(100, 100, 1);

            Shaders.Default.Draw(context);
        }
    }
}