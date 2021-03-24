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
        private static Font font;
        private static GLWindow window;
        private static PolyLine line;
        static void Main(string[] args)
        {
            window = new GLWindow();
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
            window.Run();

            Debug.WriteLine("Window Closed");
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            
            line.Vertex.RemoveRange(3, 3);
            line.Vertex.Add(Mouse2D.InWorld(window.ViewportCamera as Camera), 0);
            line.Update();

        }

        private static void WindowOnLoad(IGenericWindow window)
        { }
    }
}