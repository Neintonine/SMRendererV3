using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Window;
using SM2D.Scene;
using SM2D.Shader;

namespace SM2D
{
    /// <summary>
    /// Sets up a 2D window.
    /// </summary>
    public struct Window2DSetup : ISetup
    {
        /// <inheritdoc />
        public void Applied(IGenericWindow window)
        {
            window.ViewportCamera = new Camera();

            SMRenderer.DefaultMaterialShader = ShaderCollection.Instanced;
        }

        /// <inheritdoc />
        public void Load(IGenericWindow window)
        {
            GL.Enable(EnableCap.DepthTest);
        }

        /// <inheritdoc />
        public void Loaded(IGenericWindow window)
        {
        }

        /// <inheritdoc />
        public void Resize(IGenericWindow window)
        {
            Camera.ResizeCounter++;
        }
    }
}