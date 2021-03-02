using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.PostProcess;
using SM.Base.Windows;
using SM2D.Scene;
using SM2D.Shader;

namespace SM2D
{
    public struct Window2DSetup : I2DSetup
    {
        public Vector2? WorldScale { get; set; }

        public void Applied(IGenericWindow window)
        {
            window.ViewportCamera = new Camera();

            SMRenderer.DefaultMaterialShader = ShaderCollection.Instanced;
        }

        public void Load(IGenericWindow window)
        {
            (window.ViewportCamera as Camera).RequestedWorldScale = WorldScale;
            GL.Enable(EnableCap.DepthTest);
        }

        public void Loaded(IGenericWindow window)
        {
        }

        public void Resize(IGenericWindow window)
        {
            Camera.ResizeCounter++;
        }
    }
}