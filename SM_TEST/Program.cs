using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
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

            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            
            window.SetScene(scene = new Scene());
            scene.Background.Color = Color4.Blue;
            scene.Camera = new Camera()
            {
                RequestedWorldScale = new Vector2(0, 10)
            };

            ItemCollection col = new ItemCollection();

            DrawObject2D textTex = new DrawObject2D()
            {
                Texture = font,
                Material = {Blending =  true}
            };
            float aspect = font.Height / (float) font.Width;
            textTex.Transform.Size.Set(font.Width * aspect, font.Height * aspect);
            textTex.Transform.Position.Set(textTex.Transform.Size.X / 2, 0);

            Vector2 fontSize = new Vector2(font.Width * aspect, font.Height * aspect);

            Material uvMaterial = new Material()
            {
                Tint = new Color4(1f, 0, 0, .5f),
                Blending = true
            };

            col.Transform.Size.Set(1);


            DrawText test = new DrawText(font, "Level Completed")
            {
                Material = uvMaterial,
                Font = font
            };
            test.Transform.Size.Set(aspect);
            test.Transform.Position.Set(0, 2);


            col.Add(test, textTex);

            scene.Objects.Add(col);

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