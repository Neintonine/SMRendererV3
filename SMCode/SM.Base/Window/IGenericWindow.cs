#region usings

using System;
using System.Drawing;
using OpenTK;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

#endregion

namespace SM.Base.Window
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

        GenericScene CurrentScene { get; }
        RenderPipeline CurrentRenderPipeline { get; }

        event Action<IGenericWindow> Resize;
        event Action<IGenericWindow> Load;

        void Update(UpdateContext context);

        void ApplySetup(ISetup setup);

        void SetScene(GenericScene scene);
        void SetRenderPipeline(RenderPipeline renderPipeline);

        void TriggerLoad();
        void TriggerResize();

        void Close();
    }
}