using System;
using System.Drawing;
using OpenTK;
using SM.Base.Controls;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

namespace SM.Base.Windows
{
    public interface IGenericWindow : IFramebufferWindow
    {
        bool Loading { get; }
        float AspectRatio { get; set; }

        GenericCamera ViewportCamera { get; set; }
        bool ForceViewportCamera { get; set; }

        bool DrawWhileUnfocused { get; set; }
        bool UpdateWhileUnfocused { get; set; }

        int Width { get; }
        int Height { get; }
        Vector2 WindowSize { get; set; }

        Rectangle ClientRectangle { get; }

        ISetup AppliedSetup { get; }

        event Action<IGenericWindow> Resize;
        event Action<IGenericWindow> Load;

        GenericScene CurrentScene { get; }
        RenderPipeline CurrentRenderPipeline { get; }

        void Update(UpdateContext context);

        void ApplySetup(ISetup setup);

        void SetScene(GenericScene scene);
        void SetRenderPipeline(RenderPipeline renderPipeline);

        void TriggerLoad();
        void TriggerResize();

        void Close();
    }
}