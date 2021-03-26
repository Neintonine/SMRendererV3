using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base;
using SM.Base.Time;
using SM.Base.Window;
using SM2D;
using SM2D.Controls;
using SM2D.Drawing;
using SM2D.Object;
using SM2D.Pipelines;
using SM2D.Scene;
using Font = SM.Base.Drawing.Text.Font;
using Vector2 = OpenTK.Vector2;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static GLWindow window;
        private static PolyLine line;
        static void Main(string[] args)
        {
            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());

            window.SetScene(scene = new Scene());

            line = new PolyLine(new Vector2[] { Vector2.Zero, Vector2.One }, PolyLineType.Connected);
            var display = new DrawObject2D()
            {
                Mesh = line
            };
            display.Transform.Size.Set(1);
            scene.Objects.Add(display);
            
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
            if (SM.Base.Controls.Mouse.LeftClick)
                line.Vertex.Add(Vector3.Zero);

            line.Vertex[1] = new Vector3(Mouse2D.InWorld(window.ViewportCamera as Camera));
            line.Update();
            

        }
    }
}