using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ShaderToolParser;
using SharpDX.XInput;
using SM.Base;
using SM.Base.Animation;
using SM.Base.Controls;
using SM.Base.Drawing;
using SM.Base.Drawing.Text;
using SM.Base.Shaders;
using SM.Base.Time;
using SM.Base.Utility;
using SM.Base.Window;
using SM.Intergrations.ShaderTool;
using SM.Utils.Controls;
using SM2D;
using SM2D.Controls;
using SM2D.Drawing;
using SM2D.Object;
using SM2D.Scene;
using Font = SM.Base.Drawing.Text.Font;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static GLWindow window;
        private static GameController controller;

        private static GameKeybindActor actor;

        public static STPProject portal;
        static void Main(string[] args){
            Font font = new Font(@".\GapSansBold.ttf")
            {
                FontSize = 51,
            };
            
            controller = new GameController(0);
            GameKeybindHost host = new GameKeybindHost(new GameKeybindList()
            {
                {"g_test", context => Keyboard.IsAnyKeyPressed, context => context.ControllerState.Buttons[GamepadButtonFlags.A, true]}
            });
            actor = GameKeybindActor.CreateControllerActor(controller);
            actor.ConnectHost(host);


            portal = STPProject.CreateFromZIP("portal.zip");

            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());

            window.SetScene(scene = new Scene()
            {
                ShowAxisHelper = true
            });
            //scene.Background.Color = Color4.Red;
            scene.Camera = new Camera()
            {
                
            };

            SimpleShader shader = new SimpleShader("basic", AssemblyUtility.ReadAssemblyFile("SM_TEST.Default Fragment Shader1.frag"), (a, b) => {
                a["Color"].SetColor(b.Material.Tint);
                a["Scale"].SetFloat(b.Material.ShaderArguments.Get("Scale", 1f));

            });
            DrawObject2D obj = new DrawObject2D()
            {
                Material =
                {
                    CustomShader = shader,
                    Tint = new Color4(1f, 0.151217f, 0.050313f, 1),
                    ShaderArguments =
                    {
                        ["Scale"] = 50f
                    }
                }
            };/*
            DrawObject2D obj2 = new DrawObject2D()
            {
                Material =
                {
                    Tint = Color4.Aqua,
                    CustomShader = shader,
                    ShaderArguments =
                    {
                        ["Scale"] = 1000f
                    }
                }
            };
            obj2.Transform.Position.Set(300);*/

            scene.Objects.Add(obj);

            window.RenderFrame += Window_RenderFrame;
            window.Run();

            Debug.WriteLine("Window Closed");
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            window.Title = Math.Floor(e.Time * 1000) + "ms";
        }
    }
}