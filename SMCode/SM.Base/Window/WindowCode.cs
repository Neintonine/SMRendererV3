#region usings

using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base.Drawing;
using SM.Base.Objects.Static;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.Base.Shaders.Extensions;
using SM.Base.Time;
using SM.Base.Utility;
using SM.OGL;
using SM.OGL.Framebuffer;
using Keyboard = SM.Base.Controls.Keyboard;
using Mouse = SM.Base.Controls.Mouse;

#endregion

namespace SM.Base.Window
{
    internal class WindowCode
    {
        internal static void Load(IGenericWindow window)
        {
            GLSystem.INIT_SYSTEM();
            GLSettings.ShaderPreProcessing = true;
            Framebuffer.ScreenWindow = window;

            var args = Environment.GetCommandLineArgs();
            if (args.Contains("--advDebugging"))
            {
                SMRenderer.AdvancedDebugging = true;
                GLSettings.InfoEveryUniform = true;
            }

            Log.Init();

            Log.Write("#", ConsoleColor.Cyan, "----------------------",
                "--- OpenGL Loading ---",
                "----------------------------------",
                $"--- {"DeviceVersion",14}: {GLSystem.DeviceVersion,-10} ---",
                $"--- {"ForcedVersion",14}: {GLSettings.ForcedVersion,-10} ---",
                $"--- {"ShadingVersion",14}: {GLSystem.ShadingVersion,-10} ---",
                $"--- {"Debugging",14}: {GLSystem.Debugging,-10} ---",
                $"--- {"AdvDebugging",14}: {SMRenderer.AdvancedDebugging,-10} ---",
                "----------------------------------");

            if (SMRenderer.AdvancedDebugging) Log.Write("Extension", ConsoleColor.DarkCyan, GLSystem.Extensions);

            ExtensionManager.InitExtensions();

            window.TriggerLoad();
            window.AppliedSetup?.Load(window);
        }

        internal static void Resize(IGenericWindow window)
        {
            window.WindowSize = new Vector2(window.Width, window.Height);
            window.AspectRatio = (float) window.Width / window.Height;
            GL.Viewport(window.ClientRectangle);

            window.CurrentRenderPipeline?.Resize();

            PostProcessEffect.Mvp = Matrix4.CreateScale(window.Width, -window.Height, 1) *
                                    Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY) *
                                    Matrix4.CreateOrthographic(window.Width, window.Height, .1f, 100f);

            window.AppliedSetup?.Resize(window);
        }

        internal static void Update(IGenericWindow window, float deltatime)
        {
            Deltatime.UpdateDelta = deltatime;
            Mouse.SetState();
            Keyboard.SetStage();
            var context = new UpdateContext
            {
                Window = window,

                Scene = window.CurrentScene
            };

            if (Keyboard.IsDown(Key.AltLeft) && Keyboard.IsDown(Key.F4)) window.Close();

            Stopwatch.PerformTicks(context);
            window.CurrentScene?.Update(context);
            window.Update(context);
        }

        internal static void Render(IGenericWindow window, float deltatime)
        {
            if (window.CurrentScene == null) return;
            if (window.CurrentRenderPipeline == null) window.SetRenderPipeline(SMRenderer.DefaultRenderPipeline);

            SMRenderer.CurrentFrame++;

            GLObject.DisposeMarkedObjects();

            Deltatime.RenderDelta = deltatime;
            var drawContext = new DrawContext
            {
                Window = window,
                Scene = window.CurrentScene,
                RenderPipeline = window.CurrentRenderPipeline,

                Mesh = Plate.Object,
                Material = window.CurrentRenderPipeline.DefaultMaterial,

                ModelMatrix = Matrix4.Identity,
                TextureMatrix = Matrix3.Identity,
                Instances = new Instance[1]
                {
                    new Instance {ModelMatrix = Matrix4.Identity, TextureMatrix = Matrix3.Identity}
                }
            };
            drawContext.SetCamera(window.ViewportCamera);

            GL.DepthFunc(DepthFunction.Lequal);
            window.CurrentRenderPipeline?.Render(ref drawContext);
        }

        internal static void PrepareScene(IGenericWindow window, GenericScene scene)
        {
            window.CurrentScene?.Deactivate();

            Util.Activate(scene);
        }

        internal static void PreparePipeline(IGenericWindow window, RenderPipeline pipeline)
        {
            pipeline.ConnectedWindow = window;
            Util.Activate(pipeline);
        }
    }
}