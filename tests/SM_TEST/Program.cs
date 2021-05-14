using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ShaderToolParser;
using SM.Base;
using SM.Base.Animation;
using SM.Base.Controls;
using SM.Base.Drawing;
using SM.Base.Time;
using SM.Base.Window;
using SM.Intergrations.ShaderTool;
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
        private static PolyLine line;

        private static ItemCollection test;
        private static DrawParticles particles;

        private static InterpolationProcess interpolation;

        public static STPProject portal;
        static void Main(string[] args)
        {
            Font font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 30,
            };
            font.RegenerateTexture();

            portal = STPProject.CreateFromZIP("portal.zip");

            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());

            window.SetScene(scene = new Scene()
            {
                ShowAxisHelper = true
            });
            scene.Background.Color = Color4.Red;
            scene.Camera = new Camera()
            {
                
            };
            DrawObject2D obj = new DrawObject2D()
            {
                Material = new STMaterial(portal.DrawNodes.First(a => a.Variables.ContainsKey("_MATColor")))
                {
                    ShaderArguments = {
                        { "RingLoc", .33f },
                         
                    }
                },
                Mesh = Polygon.GenerateCircle()
            };
            obj.Transform.Size.Set(200);
            scene.Objects.Add(obj);

            window.UpdateFrame += WindowOnUpdateFrame;
            window.RenderFrame += Window_RenderFrame;
            window.Run();

            Debug.WriteLine("Window Closed");
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            window.Title = Math.Floor(e.Time * 1000) + "ms";
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            /*
            if (Mouse.LeftClick)
                particles.Trigger();
            if (Mouse.RightClick)
                particles.ContinuousInterval = .05f;*/

        }
    }
}