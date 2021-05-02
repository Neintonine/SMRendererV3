using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SM.Base;
using SM.Base.Animation;
using SM.Base.Controls;
using SM.Base.Drawing;
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
                FontSize = 30,
            };
            font.RegenerateTexture();

            SMRenderer.DefaultMesh = new Polygon(new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) });

            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            
            window.SetScene(scene = new Scene()
            {
                ShowAxisHelper = true
            });
            scene.Background.Color = Color4.Blue;
            scene.Camera = new Camera()
            {
                
            };

            Material uvMaterial = new Material()
            {
                Tint = new Color4(1f, 0, 0, .5f),
                Blending = true,
                Texture = font
            };

            DrawText test = new DrawText(font, "Level Completed")
            {
                Material = uvMaterial,
                Origin = TextOrigin.Right
            };
            test.Transform.Position.Set(0, 2);
            test.Transform.Size.Set(.5f);
            
            scene.Objects.Add(test);

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