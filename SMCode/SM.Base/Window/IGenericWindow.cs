#region usings

using System;
using System.Drawing;
using OpenTK;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

#endregion

namespace SM.Base.Window
{
    /// <summary>
    /// This interface sets ground functions for windows.
    /// </summary>
    public interface IGenericWindow : IFramebufferWindow
    {
        /// <summary>
        /// If true, the window is currently loading.
        /// </summary>
        bool Loading { get; }
        /// <summary>
        /// Holds the aspect ratio of the window.
        /// </summary>
        float AspectRatio { get; set; }

        /// <summary>
        /// The viewport camera is used, when no camera is found in the scene.
        /// </summary>
        GenericCamera ViewportCamera { get; set; }
        /// <summary>
        /// Turning this to true, will force the window to render in the viewport camera.
        /// </summary>
        bool ForceViewportCamera { get; set; }

        /// <summary>
        /// Turning this to false will not allow drawing while the window is not in focus.
        /// </summary>
        bool DrawWhileUnfocused { get; set; }
        /// <summary>
        /// Turning this to false will not allow updating while the window is not in focus.
        /// </summary>
        bool UpdateWhileUnfocused { get; set; }

        /// <summary>
        /// Contains the window size.
        /// </summary>
        Vector2 WindowSize { get; set; }

        /// <summary>
        /// The rectangle the window is using.
        /// </summary>
        Rectangle ClientRectangle { get; }

        /// <summary>
        /// The setup that was applied to the window.
        /// </summary>
        ISetup AppliedSetup { get; }

        /// <summary>
        /// The scene that is currently used.
        /// </summary>
        GenericScene CurrentScene { get; }
        /// <summary>
        /// The render pipeline that is currently used.
        /// </summary>
        RenderPipeline CurrentRenderPipeline { get; }

        /// <summary>
        /// An event, when the window resizes.
        /// </summary>
        event Action<IGenericWindow> Resize;
        /// <summary>
        /// An event, when the window is loading.
        /// </summary>
        event Action<IGenericWindow> Load;

        /// <summary>
        /// This gets executed, when the window should update something.
        /// </summary>
        /// <param name="context">The context of the update.</param>
        void Update(UpdateContext context);

        /// <summary>
        /// This applies a setup to the window.
        /// </summary>
        /// <param name="setup"></param>
        void ApplySetup(ISetup setup);

        /// <summary>
        /// This sets a scene for the window to use.
        /// </summary>
        /// <param name="scene"></param>
        void SetScene(GenericScene scene);
        /// <summary>
        /// This sets a render pipeline, from where the scene gets rendered.
        /// </summary>
        /// <param name="renderPipeline"></param>
        void SetRenderPipeline(RenderPipeline renderPipeline);

        /// <summary>
        /// This triggeres the <see cref="Load"/> event.
        /// </summary>
        void TriggerLoad();
        /// <summary>
        /// This triggeres the <see cref="Resize"/> event.
        /// </summary>
        void TriggerResize();

        /// <summary>
        /// This closes the window.
        /// </summary>
        void Close();
    }
}