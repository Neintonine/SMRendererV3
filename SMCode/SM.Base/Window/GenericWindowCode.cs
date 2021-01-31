using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Objects.Static;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.Base.ShaderExtension;
using SM.OGL;
using SM.Utility;

namespace SM.Base
{
    public class GenericWindowCode
    {
        internal static void Load(IGenericWindow window)
        {
            SMRenderer.CurrentWindow = window;

            GLSystem.INIT_SYSTEM();
            GLSettings.ShaderPreProcessing = true;

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
        }

        internal static void Resize(IGenericWindow window) 
        {
            window.Aspect = (float) window.Width / window.Height;
            window.WorldScale = new Vector2(window.Width, window.Height);
            window.SetWorldScale();
            GL.Viewport(window.ClientRectangle);
        }

        internal static void Resize<TScene, TCamera>(IGenericWindow<TScene, TCamera> window)
            where TScene : GenericScene, new()
            where TCamera : GenericCamera, new()
        {
            window.ViewportCamera.RecalculateWorld(window.WorldScale, window.Aspect);
            window.RenderPipeline?.Resize();

            PostProcessEffect.Model = Matrix4.CreateScale(window.WorldScale.X, -window.WorldScale.Y, 1);
            PostProcessEffect.Mvp = PostProcessEffect.Model *
                                    Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                                    GenericCamera.OrthographicWorld;
        }

        internal static void Render<TScene, TCamera>(IGenericWindow<TScene, TCamera> window, float deltatime)
            where TScene : GenericScene, new()
            where TCamera : GenericCamera, new()
        {
            if (window.CurrentScene == null) return;

            SMRenderer.CurrentFrame++;

            Deltatime.RenderDelta = deltatime;
            var drawContext = new DrawContext
            {
                ForceViewport = window.ForceViewportCamera,
                ActiveScene = window.CurrentScene,
                Window = window,

                Instances = new[]
                {
                    new Instance
                        {ModelMatrix = Matrix4.Identity, TexturePosition = Vector2.Zero, TextureScale = Vector2.One}
                },
                Mesh = Plate.Object,

                WorldScale = window.WorldScale,
                LastPassthough = window,

                ShaderArguments = new Dictionary<string, object>(),

                World = window.ViewportCamera.World,
                View = window.ViewportCamera.CalculateViewMatrix(),
                ModelMaster = Matrix4.Identity
            };


            window.RenderPipeline?.Render(ref drawContext);
        }
    }
}