using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base;
using SM.Base.Window;
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
        private static GLWindow window;
        static void Main(string[] args)
        {
            font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 16
            };

            //Log.SetLogFile(compressionFolder:"logs");

            window = new GLWindow {VSync = VSyncMode.Off};
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());
            window.SetScene(scene = new Scene());
            window.RunFixedUpdate(60);
            window.Load += WindowOnLoad;
            window.RenderFrame += WindowOnUpdateFrame;
            window.Run();
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
        }

        private static void WindowOnLoad(IGenericWindow window)
        {
            scene.ShowAxisHelper = true;
            //scene.Background.Color = Color4.White;

            DrawObject2D box = new DrawObject2D();
            scene.Objects.Add(box);

            DrawText text = new DrawText(font, "Test Text");
            text.Transform.Position.Set(50, 0);
            text.Transform.Size.Set(2);
            scene.HUD.Add(text);

            //particles.Trigger();
        }
    }
}