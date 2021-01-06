using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection.Configuration;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base;
using SM.Base.Scene;
using SM.Base.Textures;
using SM.Base.Time;
using SM.Utility;
using SM2D;
using SM2D.Drawing;
using SM2D.Light;
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
        private static GLWindow2D window;
        static void Main(string[] args)
        {
            font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 32
            };

            Log.SetLogFile(compressionFolder:"logs");

            window = new GLWindow2D {Scaling = new Vector2(0, 1000)};
            //window.GrabCursor();
            window.SetRenderPipeline(new TestRenderPipeline());
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.RenderFrame += WindowOnUpdateFrame;
            window.Run();
        }

        private static PointLight light;
        private static DrawParticles particles;
        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (Keyboard.GetState()[Key.R])
                particles.Trigger();
            //particles.Paused = Keyboard.GetState()[Key.P];
            light.Position.Set( window.Mouse.InWorld());
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        { 
            scene.ShowAxisHelper = true;


            //scene.Background.Color = Color4.White;

            DrawText text = new DrawText(font, "Text");
            text.Transform.Position.Set(0, 0);
            text.Transform.Size.Set(2);
            scene.Objects.Add(text);

            light = new PointLight
            {
                Color = new Color4(0, 1, 1, 1), 
                Power = 100
            };
            scene.LightInformations.Lights.Add(light);

            scene.LightInformations.Ambient = Color4.White;

            //particles.Trigger();
        }
    }
}