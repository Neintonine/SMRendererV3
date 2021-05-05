using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SM.Base;
using SM.Base.Animation;
using SM.Base.Controls;
using SM.Base.Drawing;
using SM.Base.Time;
using SM.Base.Window;
using SM2D;
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
        private static DrawParticles particles;

        private static InterpolationProcess interpolation;
        static void Main(string[] args)
        {
            Font font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 30,
            };
            font.RegenerateTexture();

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

            DrawObject2D test = new DrawObject2D()
            {
                Texture = new Bitmap("test.png")
            };
            test.Material.Blending = true;
            test.Transform.Size.Set(100);
            test.TextureTransform.SetRectangleRelative(test.Texture, new Vector2(234, 0), new Vector2(220, 201));
            test.Transform.AdjustSizeToTextureTransform(test.TextureTransform);
            
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