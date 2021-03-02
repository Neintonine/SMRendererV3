using System.Drawing;
using System.Windows;
using OpenTK;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

namespace SM.Base
{
    public interface IGenericWindow : IFramebufferWindow
    {
        bool Loading { get; }
        float Aspect { get; set; }

        GenericCamera ViewportCamera { get; }
        bool ForceViewportCamera { get; set; }

        int Width { get; }
        int Height { get; }

        Rectangle ClientRectangle { get; }
        Vector2 WorldScale { get; set; }

        void SetWorldScale();
    }

    public interface IGenericWindow<TScene, TCamera> : IGenericWindow
        where TScene : GenericScene, new()
        where TCamera : GenericCamera, new()
    {
        TScene CurrentScene { get; }

        RenderPipeline<TScene> RenderPipeline { get; }

        void SetScene(TScene scene);
        void SetRenderPipeline(RenderPipeline<TScene> renderPipeline);
    }
}