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
using SM2D.Object;
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
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.RenderFrame += WindowOnUpdateFrame;
            window.Run();
        }

        private static DrawParticles particles;
        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (Keyboard.GetState()[Key.R])
                particles.Trigger();
            //particles.Paused = Keyboard.GetState()[Key.P];
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            //scene.ShowAxisHelper = true;

            DrawObject2D kasten = new DrawObject2D();
            kasten.Texture = new Texture(new Bitmap("herosword.png"));
            kasten.Transform.ApplyTextureSize(kasten.Texture, 500);
            scene.Objects.Add(kasten);

            DrawText text = new DrawText(font, "Text");
            text.Transform.Position.Set(0, 500);
            scene.Objects.Add(text);

            //particles.Trigger();
        }
    }
}