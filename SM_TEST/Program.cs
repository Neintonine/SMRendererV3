using System;
using OpenTK;
using OpenTK.Input;
using SM.Base;
using SM.Game.Controls;
using SM2D;
using SM2D.Drawing;
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

            window = new GLWindow2D {Scaling = new Vector2(0, 1000), VSync = VSyncMode.Off};
            //window.GrabCursor();
            window.SetRenderPipeline(new TestRenderPipeline());
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

            GameControllerState s1 = new GameController(0).GetState();
            GameControllerState s2 = new GameController(1).GetState();
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            scene.ShowAxisHelper = true;


            //scene.Background.Color = Color4.White;

            DrawText text = new DrawText(font, "Text");
            text.Transform.Position.Set(0, 0);
            text.Transform.Size.Set(2);
            scene.Objects.Add(text);

            //particles.Trigger();
        }
    }
}