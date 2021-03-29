using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using SM.Base.Animation;
using SM.Base.Controls;
using SM.Base.Drawing.Text;
using SM.Base.Time;
using SM.Base.Window;
using SM2D;
using SM2D.Drawing;
using SM2D.Object;
using SM2D.Scene;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static GLWindow window;
        private static PolyLine line;
        private static DrawParticles particles;

        private static InterpolationProcess interpolation;
        static void Main(string[] args)
        {
            Font font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 20,
                CharSet = new List<char>(){'H', 'i', 'I', ','}
            };

            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());

            window.SetScene(scene = new Scene());

            DrawText text = new DrawText(font, "Hi, HI");
            scene.Objects.Add(text);

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
            if (Mouse.LeftClick)
                interpolation.Stop();
            if (Mouse.RightClick)
                interpolation.Stop(false);
        }
    }
}